using ArithmeticCalculator.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Calculator>();

var app = builder.Build();

// Serve static files from wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

// ── API endpoint ──────────────────────────────────────────────────────────────
app.MapPost("/api/calculate", (CalculateRequest req, Calculator calc) =>
{
    if (!double.TryParse(req.A, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var a) ||
        !double.TryParse(req.B, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var b))
    {
        return Results.BadRequest(new { error = "Invalid numbers" });
    }

    var result = calc.Calculate(a, b);

    return Results.Ok(new
    {
        add      = result.Add,
        subtract = result.Subtract,
        multiply = result.Multiply,
        divide   = result.Divide,
        modulo   = result.Modulo,
        power    = result.Power
    });
});

app.Run();

record CalculateRequest(string A, string B);
