using SchedulingApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Adds controller support
builder.Services.AddControllers();

// Registers AppointmentService so it can be injected into controllers
builder.Services.AddSingleton<AppointmentService>();

// CORS so React (on localhost:5173 / 5174) can talk to this API
var corsPolicyName = "AllowReactApp";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://localhost:5174"   // Vite switched to this
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable CORS
app.UseCors(corsPolicyName);

// ❌ Turn OFF HTTPS redirection for local dev
// app.UseHttpsRedirection();

app.UseAuthorization();

// Maps controller routes like /api/appointments
app.MapControllers();

app.Run();
