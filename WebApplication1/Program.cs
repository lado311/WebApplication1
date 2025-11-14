var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Always enable Swagger (optional, works in Production)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Swagger at root "/"
});

// Optional: Minimal test endpoint
app.MapGet("/", () => "API is running successfully");

// Use HTTPS redirection if needed
// app.UseHttpsRedirection(); // Render handles HTTPS via reverse proxy

app.UseAuthorization();

// Bind to Render-assigned PORT
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");
Console.WriteLine($"Listening on port {port}");

// Map Controllers
app.MapControllers();

app.Run();
