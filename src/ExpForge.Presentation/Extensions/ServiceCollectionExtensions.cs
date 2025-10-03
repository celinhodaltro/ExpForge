using ExpForge.Application.Behaviors;
using ExpForge.Application.Commands.Widget;
using ExpForge.Application.Interfaces.Providers;
using ExpForge.Application.Interfaces.Services;
using ExpForge.Application.Services;
using ExpForge.Infrastructure.Providers;
using ExpForge.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace ExpForge.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var applicationAssembly = typeof(NewWidgetCommand).Assembly;

        services.AddValidatorsFromAssembly(applicationAssembly);

        // Pipeline behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // Providers e services
        services.AddScoped<ITemplatePathProvider, TemplatePathProvider>();
        services.AddScoped<ITemplateGeneratorService, TemplateGeneratorService>();
        services.AddScoped<ITerminalMessageService, TerminalMessageService>();

        // MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(applicationAssembly);
        });


        var templatesPath = Path.Combine(AppContext.BaseDirectory, "templates");
        services.AddSingleton(new TemplateOptions { TemplatesPath = templatesPath });

        return services;
    }

    public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
    {
        services.AddLogging(cfg =>
        {
            cfg.AddConsole();
            cfg.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);
        });

        return services;
    }
}

public class TemplateOptions
{
    public string TemplatesPath { get; set; } = string.Empty;
}
