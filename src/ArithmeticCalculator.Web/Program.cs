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

    // Sanitize: JSON cannot represent Infinity or NaN — return null for those
    static double? Sanitize(double? val) =>
        val.HasValue && (double.IsInfinity(val.Value) || double.IsNaN(val.Value))
            ? null
            : val;

    return Results.Ok(new
    {
        add      = Sanitize(result.Add),
        subtract = Sanitize(result.Subtract),
        multiply = Sanitize(result.Multiply),
        divide   = Sanitize(result.Divide),
        modulo   = Sanitize(result.Modulo),
        power    = Sanitize(result.Power)
    });
});

app.Run();

record CalculateRequest(string A, string B);
