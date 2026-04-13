# ── Stage 1: Build ────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /repo

# Copy solution + project files first (layer-cache friendly)
COPY ArithmeticCalculator.sln ./
COPY src/ArithmeticCalculator.Core/ArithmeticCalculator.Core.csproj       src/ArithmeticCalculator.Core/
COPY src/ArithmeticCalculator.Web/ArithmeticCalculator.Web.csproj         src/ArithmeticCalculator.Web/
COPY tests/ArithmeticCalculator.Tests/ArithmeticCalculator.Tests.csproj   tests/ArithmeticCalculator.Tests/

RUN dotnet restore

# Copy all source and publish the web app
COPY . .
RUN dotnet publish src/ArithmeticCalculator.Web/ArithmeticCalculator.Web.csproj \
    -c Release -o /app/publish --no-restore

# ── Stage 2: Runtime ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ArithmeticCalculator.Web.dll"]
