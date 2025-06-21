using OrderApplication.Api.Extensions;
using OrderApplication.Application.Extensions;
using OrderApplication.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(builder.Configuration["FrontendUrl"])
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddMigrations(builder.Configuration);

builder.Services.AddLogging();

builder.Services.AddServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

app.ApplyMigrations();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowFrontend");

app.Run();
