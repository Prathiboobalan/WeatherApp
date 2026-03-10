
using Supabase;

namespace BlazorApp.Services
{
    public class SupabaseClientService
    {
        private readonly Client _client;

        public SupabaseClientService()
        {
            var url = "https://irsvncfxbhcykbukfplm.supabase.co";
            var anonkey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imlyc3ZuY2Z4YmhjeWtidWtmcGxtIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjIwODc2NTIsImV4cCI6MjA3NzY2MzY1Mn0.LWA_ssv4Gn66XxWoSg-QdbE63HvuqgDBPX9grDArCh8";

            var options = new SupabaseOptions
            {
                AutoConnectRealtime = true,
                AutoRefreshToken = true
               
            };


            _client = new Client(url, anonkey, options);
        }

        public Client Client => _client;
    }
}

