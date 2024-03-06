using Microsoft.AspNetCore.Identity;
using Serilog;
using Tech.Api;
using Tech.Application.DependencyInjection;
using Tech.DAL.DependencyInjection;
using Tech.Domain.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));
builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.AddAuthenticationAndAuthorization(builder);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration((context.Configuration)));
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tech Swagger v 1.0");
        c.RoutePrefix = "swagger";
    });
}

app.MapIdentityApi<IdentityUser>();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();