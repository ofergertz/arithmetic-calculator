using ArithmeticCalculator.Core;

Console.WriteLine("=== Arithmetic Calculator ===");
Console.WriteLine("Computes: Add, Subtract, Multiply, Divide, Modulo, Power");
Console.WriteLine();

double a = ReadNumber("Enter first number (A): ");
double b = ReadNumber("Enter second number (B): ");

var calculator = new Calculator();
var result = calculator.Calculate(a, b);

Console.WriteLine();
Console.WriteLine(result);

static double ReadNumber(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        if (double.TryParse(input, out var value))
            return value;
        Console.WriteLine("  Invalid input — please enter a numeric value.");
    }
}
