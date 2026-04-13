using ArithmeticCalculator.Core.Models;

namespace ArithmeticCalculator.Core;

/// <summary>
/// Performs all arithmetic operations on two numbers.
/// Division and Modulo return null when the divisor is zero.
/// </summary>
public class Calculator
{
    public ArithmeticResult Calculate(double a, double b)
    {
        return new ArithmeticResult(
            A: a,
            B: b,
            Add: Add(a, b),
            Subtract: Subtract(a, b),
            Multiply: Multiply(a, b),
            Divide: b != 0 ? Divide(a, b) : null,
            Modulo: b != 0 ? Modulo(a, b) : null,
            Power: Power(a, b)
        );
    }

    public double Add(double a, double b) => a + b;
    public double Subtract(double a, double b) => a - b;
    public double Multiply(double a, double b) => a * b;
    public double Divide(double a, double b) => a / b;
    public double Modulo(double a, double b) => a % b;
    public double Power(double a, double b) => Math.Pow(a, b);
}
