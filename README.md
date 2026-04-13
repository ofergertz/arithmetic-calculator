# Arithmetic Calculator 🧮

<div dir="rtl">

## עברית

### מה זה?
אפליקציית קונסול ב-C# (.NET 8) שמקבלת שני מספרים ומחזירה את תוצאות כל פעולות החשבון:

| פעולה | סמל |
|-------|-----|
| חיבור | `+` |
| חיסור | `-` |
| כפל | `×` |
| חילוק | `÷` |
| מודולו | `%` |
| חזקה | `^` |

> חילוק ומודולו באפס מוחזרים כ-`undefined` (אין שגיאת קריסה).

---

### הרצה מקומית (dotnet run)

**דרישות מקדימות:** .NET 8 SDK

```bash
# שכפול הפרויקט
git clone https://github.com/ofergertz/arithmetic-calculator.git
cd arithmetic-calculator

# הרצה
dotnet run --project src/ArithmeticCalculator.App
```

---

### הרצה עם Docker

**דרישות מקדימות:** Docker מותקן ופועל

```bash
# בנייה והרצה ידנית
docker build -t arithmetic-calculator .
docker run -it arithmetic-calculator

# או עם docker-compose (מומלץ)
docker-compose up --build
```

---

### דוגמת קלט/פלט

```
=== Arithmetic Calculator ===
Computes: Add, Subtract, Multiply, Divide, Modulo, Power

Enter first number (A):  10
Enter second number (B):  3

Arithmetic Results for (10, 3):
  Addition       : 10 + 3 = 13
  Subtraction    : 10 - 3 = 7
  Multiplication : 10 × 3 = 30
  Division       : 10 ÷ 3 = 3.3333333333333335
  Modulo         : 10 % 3 = 1
  Power          : 10 ^ 3 = 1000
```

---

### הרצת טסטים

```bash
dotnet test
```

</div>

---

## English

### What is this?
A C# (.NET 8) console application that accepts two numbers and returns the results of all arithmetic operations:

| Operation | Symbol |
|-----------|--------|
| Addition | `+` |
| Subtraction | `-` |
| Multiplication | `×` |
| Division | `÷` |
| Modulo | `%` |
| Power | `^` |

> Division and Modulo by zero return `undefined` — no crash, just a graceful null.

---

### Run Locally (dotnet run)

**Prerequisites:** .NET 8 SDK ([download](https://dotnet.microsoft.com/download/dotnet/8.0))

```bash
# Clone
git clone https://github.com/ofergertz/arithmetic-calculator.git
cd arithmetic-calculator

# Run
dotnet run --project src/ArithmeticCalculator.App
```

---

### Run with Docker

**Prerequisites:** Docker installed and running

```bash
# Manual build + run
docker build -t arithmetic-calculator .
docker run -it arithmetic-calculator

# Or with docker-compose (recommended)
docker-compose up --build
```

The `-it` flag is required because the app reads from stdin interactively.

---

### Example Input / Output

```
=== Arithmetic Calculator ===
Computes: Add, Subtract, Multiply, Divide, Modulo, Power

Enter first number (A):  10
Enter second number (B):  3

Arithmetic Results for (10, 3):
  Addition       : 10 + 3 = 13
  Subtraction    : 10 - 3 = 7
  Multiplication : 10 × 3 = 30
  Division       : 10 ÷ 3 = 3.3333333333333335
  Modulo         : 10 % 3 = 1
  Power          : 10 ^ 3 = 1000
```

Division by zero example:
```
Enter first number (A):  7
Enter second number (B):  0

Arithmetic Results for (7, 0):
  Addition       : 7 + 0 = 7
  Subtraction    : 7 - 0 = 7
  Multiplication : 7 × 0 = 0
  Division       : 7 ÷ 0 = undefined (division by zero)
  Modulo         : 7 % 0 = undefined (modulo by zero)
  Power          : 7 ^ 0 = 1
```

---

### Run Unit Tests

```bash
dotnet test
# Expected: 24 passed, 0 failed
```

---

### Project Structure

```
arithmetic-calculator/
├── src/
│   ├── ArithmeticCalculator.Core/       # Business logic (Calculator + ArithmeticResult)
│   └── ArithmeticCalculator.App/        # Console entry point
├── tests/
│   └── ArithmeticCalculator.Tests/      # xUnit tests (24 test cases)
├── Dockerfile                           # Multi-stage Docker build
├── docker-compose.yml                   # Easy spin-up
└── ArithmeticCalculator.sln
```

---

### Tech Stack

- **Language:** C# 12
- **Runtime:** .NET 8
- **Tests:** xUnit
- **Container:** Docker (multi-stage build, `mcr.microsoft.com/dotnet/runtime:8.0`)
