using ExpForge.Application.Behaviors;
using ExpForge.Application.Commands;
using ExpForge.Application.Interfaces;
using ExpForge.CLI.Actions;
using ExpForge.CLI.Services;
using ExpForge.Infrastructure.Providers;
using ExpForgeCli.Services;
using FluentValidation;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

class Program
{
    static int Main(string[] args)
    {

        var services = new ServiceCollection();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();

        var app = new CommandLineApplication<MainAction>();
        app.Conventions
           .UseDefaultConventions()
           .UseConstructorInjection(serviceProvider);

        try
        {
            return app.Execute(args);
        }
        catch (UnrecognizedCommandParsingException ex)
        {
            TerminalMessageService.WriteLine($"Error: Unrecognized command or argument:", MessageStatus.Error);
            return 1;
        }
    }

    static void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(cfg =>
        {
            cfg.AddConsole();
            cfg.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);
        });

        var applicationAssembly = typeof(ExpForge.Application.Commands.CreateWidgetCommand).Assembly;
        services.AddValidatorsFromAssembly(typeof(CreateWidgetCommand).Assembly);

        // pipeline behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<ITemplatePathProvider, TemplatePathProvider>();


        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(applicationAssembly);
        });

        var templatesPath = Path.Combine(AppContext.BaseDirectory, "templates");

        services.AddSingleton<CreateWidgetService>();
    }
}
