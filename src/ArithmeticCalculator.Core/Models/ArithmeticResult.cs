namespace ArithmeticCalculator.Core.Models;

/// <summary>
/// Holds the results of all arithmetic operations performed on two numbers.
/// </summary>
public record ArithmeticResult(
    double A,
    double B,
    double Add,
    double Subtract,
    double Multiply,
    double? Divide,
    double? Modulo,
    double Power
)
{
    public override string ToString() =>
        $"""
         Arithmetic Results for ({A}, {B}):
           Addition       : {A} + {B} = {Add}
           Subtraction    : {A} - {B} = {Subtract}
           Multiplication : {A} × {B} = {Multiply}
           Division       : {A} ÷ {B} = {(Divide.HasValue ? Divide.ToString() : "undefined (division by zero)")}
           Modulo         : {A} % {B} = {(Modulo.HasValue ? Modulo.ToString() : "undefined (modulo by zero)")}
           Power          : {A} ^ {B} = {Power}
         """;
}
