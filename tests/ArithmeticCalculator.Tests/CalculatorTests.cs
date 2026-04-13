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
}
