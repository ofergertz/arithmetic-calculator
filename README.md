# Arithmetic Calculator 🧮

A full-stack arithmetic calculator — ASP.NET Core 8 Web API backend + a slick single-page UI.  
Enter two numbers, hit **Calculate**, get all 6 results instantly without a page reload.

---

<div dir="rtl">

## עברית

### מה זה?
אפליקציית Web ב-C# (.NET 8) עם ממשק משתמש מודרני.  
מקבלת שני מספרים ומחזירה את תוצאות כל פעולות החשבון ללא רענון דף:

| פעולה | סמל |
|-------|-----|
| חיבור | `+` |
| חיסור | `-` |
| כפל | `×` |
| חילוק | `÷` |
| מודולו | `%` |
| חזקה | `^` |

> חילוק ומודולו באפס מוחזרים כ-`undefined` — אין קריסה.

---

### הרצה מקומית (dotnet run)

**דרישות מקדימות:** .NET 8 SDK

```bash
git clone https://github.com/ofergertz/arithmetic-calculator.git
cd arithmetic-calculator
git checkout feature/arithmetic-operations

dotnet run --project src\ArithmeticCalculator.Web
```

פתח דפדפן על: **http://localhost:8080**

---

### הרצה עם Docker

**דרישות מקדימות:** Docker

```bash
# בנייה והרצה ידנית
docker build -t arithmetic-calculator .
docker run -p 8080:8080 arithmetic-calculator

# או עם docker-compose (מומלץ)
docker-compose up --build
```

פתח דפדפן על: **http://localhost:8080**

---

### דוגמת קלט/פלט

```
קלט:  A = 10,  B = 3

פלט:
  חיבור        10 + 3  = 13
  חיסור        10 - 3  = 7
  כפל          10 × 3  = 30
  חילוק        10 ÷ 3  = 3.333333333
  מודולו       10 % 3  = 1
  חזקה         10 ^ 3  = 1000
```

---

### הרצת טסטים

```bash
dotnet test
# Expected: 24 passed, 0 failed
```

</div>

---

## English

### What is this?

A modern web calculator built with:
- **Backend:** ASP.NET Core 8 Minimal API
- **Frontend:** Single-page HTML/CSS/JS (Bootstrap 5 + glassmorphism UI)
- **Engine:** `ArithmeticCalculator.Core` — pure C# class library

No page reloads. Results appear instantly below the inputs via `fetch` API.

---

### Operations

| Operation | Symbol | Notes |
|-----------|--------|-------|
| Addition | `+` | |
| Subtraction | `-` | |
| Multiplication | `×` | |
| Division | `÷` | Returns `undefined` on divide-by-zero |
| Modulo | `%` | Returns `undefined` on divide-by-zero |
| Power | `^` | Uses `Math.Pow` |

---

### Run Locally

**Prerequisites:** .NET 8 SDK ([download](https://dotnet.microsoft.com/download/dotnet/8.0))

```bash
git clone https://github.com/ofergertz/arithmetic-calculator.git
cd arithmetic-calculator
git checkout feature/arithmetic-operations

# Windows
dotnet run --project src\ArithmeticCalculator.Web

# macOS / Linux
dotnet run --project src/ArithmeticCalculator.Web
```

Open your browser at: **http://localhost:8080**

---

### Run with Docker

**Prerequisites:** Docker installed and running

```bash
# Build and run manually
docker build -t arithmetic-calculator .
docker run -p 8080:8080 arithmetic-calculator

# Or with docker-compose (recommended)
docker-compose up --build
```

Open your browser at: **http://localhost:8080**

---

### Example Input / Output

```
Input:  A = 10,  B = 3

Output:
  Addition       10 + 3  = 13
  Subtraction    10 - 3  = 7
  Multiplication 10 × 3  = 30
  Division       10 ÷ 3  = 3.333333333
  Modulo         10 % 3  = 1
  Power          10 ^ 3  = 1000
```

Division by zero:
```
Input:  A = 7,  B = 0

Output:
  Division       7 ÷ 0  = undefined (÷ 0)
  Modulo         7 % 0  = undefined (÷ 0)
  (all others calculated normally)
```

---

### API

The UI calls a single REST endpoint:

```
POST /api/calculate
Content-Type: application/json

{ "a": "10", "b": "3" }
```

Response:
```json
{
  "add": 13,
  "subtract": 7,
  "multiply": 30,
  "divide": 3.3333333333333335,
  "modulo": 1,
  "power": 1000
}
```

---

### Run Tests

```bash
dotnet test
# Total: 24  |  Passed: 24  |  Failed: 0
```

---

### Project Structure

```
arithmetic-calculator/
├── src/
│   ├── ArithmeticCalculator.Core/       # Business logic (Calculator + ArithmeticResult)
│   └── ArithmeticCalculator.Web/        # ASP.NET Core Minimal API + static UI
│       ├── Program.cs                   # API endpoint wiring
│       ├── appsettings.json
│       └── wwwroot/
│           └── index.html               # Single-page UI (Bootstrap 5 glassmorphism)
├── tests/
│   └── ArithmeticCalculator.Tests/      # xUnit — 24 test cases
├── Dockerfile                           # Multi-stage build → aspnet:8.0 runtime
├── docker-compose.yml                   # Exposes port 8080
└── ArithmeticCalculator.sln
```

---

### Tech Stack

| Layer | Tech |
|-------|------|
| Language | C# 12 |
| Runtime | .NET 8 |
| Web framework | ASP.NET Core Minimal API |
| Frontend | HTML5 + Bootstrap 5 (CDN) + Vanilla JS (`fetch`) |
| Tests | xUnit (24 cases) |
| Container | Docker multi-stage (`sdk:8.0` → `aspnet:8.0`) |
