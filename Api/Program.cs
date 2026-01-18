using Api.Configurations;
using Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
#region Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerConfiguration();
#endregion

#region Context
builder.Services.AddContextConfiguration(builder.Configuration);
#endregion

#region DependencyInjection
builder.Services.AddApplicationServicesConfiguration();
#endregion

#region Authentication/Cors
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddCorsConfiguration();
#endregion

#region Logging
builder.AddLoggingConfiguration();
#endregion

var app = builder.Build();
app.UseCorsConfiguration();
app.UseSwaggerConfiguration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
