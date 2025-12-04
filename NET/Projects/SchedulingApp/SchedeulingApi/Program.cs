using SchedulingApi.Services; // or SchedulingApi.Services depending on your namespace

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Register your AppointmentService
builder.Services.AddSingleton<AppointmentService>();

// CORS: allow the React app origin
var corsPolicyName = "AllowReactApp";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy
            .WithOrigins("http://localhost:5173") // <- change if Vite uses a different port
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors(corsPolicyName);

app.UseHttpsRedirection(); // still fine even if you're only using http

app.UseAuthorization();

app.MapControllers();

app.Run();
