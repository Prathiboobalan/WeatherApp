using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace BlazorApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public WeatherService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        // ✅ Current Weather
        public async Task<WeatherResponse?> GetWeatherAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return null;

            var apiKey = _config["OpenWeather:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("API key not found. Check appsettings.json");

            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            try
            {
                var result = await _http.GetFromJsonAsync<WeatherResponse>(url);
                return result ?? new WeatherResponse { name = city, main = new Main(), weather = new List<Weather>() };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Weather API error: {ex.Message}");
                return null;
            }
        }

        // ✅ 7-Day Forecast (approximated from 5-day API)
        public async Task<List<DailyForecast>?> Get7DayForecastAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return null;

            var apiKey = _config["OpenWeather:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("API key not found. Check appsettings.json");

            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units=metric";

            try
            {
                var response = await _http.GetFromJsonAsync<ForecastResponse>(url);

                if (response?.list == null || response.list.Count == 0)
                    return null;

                var dailyForecasts = response.list
                    .GroupBy(x => DateTime.Parse(x.dt_txt).Date)
                    .Select(g => new DailyForecast
                    {
                        Date = g.Key,
                        Temp = Math.Round(g.Average(x => x.main.temp), 1),
                        Min = g.Min(x => x.main.temp_min),
                        Max = g.Max(x => x.main.temp_max),
                        Condition = g.First().weather.FirstOrDefault()?.description ?? "Unknown"
                    })
                    .Take(7)
                    .ToList();

                return dailyForecasts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Forecast API error: {ex.Message}");
                return null;
            }
        }
    }

    // ✅ Data Models
    public class WeatherResponse
    {
        public string name { get; set; } = string.Empty;
        public Main main { get; set; } = new();
        public List<Weather> weather { get; set; } = new();
    }

    public class ForecastResponse
    {
        public List<ForecastItem> list { get; set; } = new();
    }

    public class ForecastItem
    {
        public Main main { get; set; } = new();
        public List<Weather> weather { get; set; } = new();
        public string dt_txt { get; set; } = string.Empty;
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Weather
    {
        public string description { get; set; } = string.Empty;
    }

    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public double Temp { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public string Condition { get; set; } = string.Empty;
    }
}