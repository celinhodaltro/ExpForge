using ExperienceWidget.Application.Behaviors;
using ExperienceWidget.Application.Commands;
using ExperienceWidget.CLI.Actions;
using ExperienceWidgetCli.Services;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluentValidation;
using System;
using System.IO;
using ExperienceWidget.CLI.Services;

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

        var applicationAssembly = typeof(ExperienceWidget.Application.Commands.CreateWidgetCommand).Assembly;
        services.AddValidatorsFromAssembly(typeof(CreateWidgetCommand).Assembly);

        // pipeline behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(applicationAssembly);
        });

        var templatesPath = Path.Combine(AppContext.BaseDirectory, "templates");

        services.AddSingleton<WidgetGeneratorService>(sp => new WidgetGeneratorService(templatesPath));
    }
}
