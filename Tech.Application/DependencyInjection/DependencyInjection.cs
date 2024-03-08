using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.DependencyInjection;
using Tech.Application.Mapping;
using Tech.Application.Services;
using Tech.Application.Validations;
using Tech.Application.Validations.FluentValidations;
using Tech.Application.Validations.FluentValidations.Report;
using Tech.Domain.DTO.ReportDTO;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Interfaces.Validations;

namespace Tech.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ReportMapping));
        InitServices(services);
    }

    public static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IReportValidator, ReportValidator>();
        services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
        services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();
        services.AddScoped<IReportService, ArticleService>();
    }
}