# ── Stage 1: Build ────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /repo

# Copy solution + project files first (layer-cache friendly)
COPY ArithmeticCalculator.sln ./
COPY src/ArithmeticCalculator.Core/ArithmeticCalculator.Core.csproj   src/ArithmeticCalculator.Core/
COPY src/ArithmeticCalculator.App/ArithmeticCalculator.App.csproj     src/ArithmeticCalculator.App/
COPY tests/ArithmeticCalculator.Tests/ArithmeticCalculator.Tests.csproj tests/ArithmeticCalculator.Tests/

RUN dotnet restore

# Copy remaining source and publish
COPY . .
RUN dotnet publish src/ArithmeticCalculator.App/ArithmeticCalculator.App.csproj \
    -c Release -o /app/publish --no-restore

# ── Stage 2: Runtime ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# The app is interactive (reads stdin), so keep stdin open
ENTRYPOINT ["dotnet", "ArithmeticCalculator.App.dll"]
