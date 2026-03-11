# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore BlazorApp.csproj
RUN dotnet publish BlazorApp.csproj -c Release -o /app/publish

# Runtime stage
FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80
