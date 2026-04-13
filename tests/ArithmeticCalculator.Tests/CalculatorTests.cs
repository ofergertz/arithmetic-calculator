using ArithmeticCalculator.Core;
using Xunit;

namespace ArithmeticCalculator.Tests;

public class CalculatorTests
{
    private readonly Calculator _calculator = new();

    // ── Addition ──────────────────────────────────────────────────────────────
    [Theory]
    [InlineData(3, 2, 5)]
    [InlineData(-3, 2, -1)]
    [InlineData(0, 0, 0)]
    [InlineData(1.5, 2.5, 4.0)]
    public void Add_ReturnsCorrectResult(double a, double b, double expected)
        => Assert.Equal(expected, _calculator.Add(a, b));

    // ── Subtraction ───────────────────────────────────────────────────────────
    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(3, 5, -2)]
    [InlineData(0, 0, 0)]
    [InlineData(-4, -4, 0)]
    public void Subtract_ReturnsCorrectResult(double a, double b, double expected)
        => Assert.Equal(expected, _calculator.Subtract(a, b));

    // ── Multiplication ────────────────────────────────────────────────────────
    [Theory]
    [InlineData(3, 4, 12)]
    [InlineData(-3, 4, -12)]
    [InlineData(0, 100, 0)]
    [InlineData(2.5, 4, 10)]
    public void Multiply_ReturnsCorrectResult(double a, double b, double expected)
        => Assert.Equal(expected, _calculator.Multiply(a, b));

    // ── Division ──────────────────────────────────────────────────────────────
    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(7, 2, 3.5)]
    [InlineData(-9, 3, -3)]
    public void Divide_ReturnsCorrectResult(double a, double b, double expected)
        => Assert.Equal(expected, _calculator.Divide(a, b));

    [Fact]
    public void Calculate_DivideByZero_ReturnsNullDivide()
    {
        var result = _calculator.Calculate(10, 0);
        Assert.Null(result.Divide);
        Assert.Null(result.Modulo);
    }

    // ── Modulo ────────────────────────────────────────────────────────────────
    [Theory]
    [InlineData(10, 3, 1)]
    [InlineData(10, 5, 0)]
    [InlineData(7, 4, 3)]
    public void Modulo_ReturnsCorrectResult(double a, double b, double expected)
        => Assert.Equal(expected, _calculator.Modulo(a, b));

    // ── Power ─────────────────────────────────────────────────────────────────
    [Theory]
    [InlineData(2, 10, 1024)]
    [InlineData(3, 3, 27)]
    [InlineData(5, 0, 1)]
    [InlineData(9, 0.5, 3)]
    public void Power_ReturnsCorrectResult(double a, double b, double expected)
        => Assert.Equal(expected, _calculator.Power(a, b), precision: 10);

    // ── Calculate (full result object) ────────────────────────────────────────
    [Fact]
    public void Calculate_ReturnsAllOperations()
    {
        var result = _calculator.Calculate(6, 3);

        Assert.Equal(9, result.Add);
        Assert.Equal(3, result.Subtract);
        Assert.Equal(18, result.Multiply);
        Assert.Equal(2, result.Divide);
        Assert.Equal(0, result.Modulo);
        Assert.Equal(216, result.Power);
    }

    // ── Overflow / Edge Cases ─────────────────────────────────────────────────

    /// <summary>
    /// Regression test: 91^1561 overflows double and produces Infinity.
    /// The Core layer should return Infinity (raw math result).
    /// The API layer (Program.cs Sanitize) is responsible for converting to null.
    /// This test verifies the Core correctly computes an overflowed value
    /// and that callers can detect it via double.IsInfinity().
    /// </summary>
    [Fact]
    public void Calculate_PowerOverflow_ReturnsInfinity()
    {
        var result = _calculator.Calculate(91, 1561);
        Assert.True(result.Power.HasValue);
        Assert.True(double.IsInfinity(result.Power!.Value),
            "91^1561 should overflow double and return Infinity");
    }

    [Theory]
    [InlineData(double.MaxValue, 2)]   // MaxValue^2 overflows
    [InlineData(1e200, 1e200)]         // extremely large base and exponent
    public void Calculate_ExtremeValues_PowerIsInfinityOrNaN(double a, double b)
    {
        var result = _calculator.Calculate(a, b);
        Assert.True(result.Power.HasValue);
        Assert.True(
            double.IsInfinity(result.Power!.Value) || double.IsNaN(result.Power.Value),
            $"Power({a}, {b}) should be Infinity or NaN, got {result.Power}");
    }

    [Fact]
    public void Calculate_NegativeBase_FractionalExponent_ReturnsNaN()
    {
        // (-8)^0.5 = sqrt(-8) = NaN in real numbers
        var result = _calculator.Calculate(-8, 0.5);
        Assert.True(result.Power.HasValue);
        Assert.True(double.IsNaN(result.Power!.Value),
            "(-8)^0.5 should return NaN");
    }
}
