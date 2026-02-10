using TextRpg.Api.Data;
using TextRpg.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddSingleton<ICharacterRepository, InMemoryCharacterRepository>();
builder.Services.AddSingleton<CombatService>(); // 👈 needed for CombatsController DI

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// keep this off for now
// app.UseHttpsRedirection();

app.UseCors();          // ✅ move ABOVE auth
app.UseAuthorization();

app.MapControllers();
app.Run();
