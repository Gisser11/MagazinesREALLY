using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Tech.Api;
using Tech.Application.DependencyInjection;
using Tech.DAL;
using Tech.DAL.DependencyInjection;
using Tech.Domain.Entity;
using Tech.Domain.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));

builder.Services.AddControllers();

builder.Services.AddAuthenticationAndAuthorization(builder);

builder.Services.AddSwagger();

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration((context.Configuration)));

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tech Swagger v 1.0");
        c.RoutePrefix = "swagger";
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();