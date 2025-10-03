using ExpForge.Application.Interfaces.Services;
using ExpForge.Domain.Enums;
using ExpForge.Infrastructure.Services;
using ExpForge.Presentation.Actions;
using ExpForge.Presentation.Extensions;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;

class Program
{
    static int Main(string[] args)
    {
        var services = new ServiceCollection()
            .AddLoggingConfiguration()
            .AddApplicationServices()
            .AddSingleton<ITerminalMessageService, TerminalMessageService>() // registrar o serviço
            .BuildServiceProvider();

        var terminalService = services.GetRequiredService<ITerminalMessageService>();

        var app = new CommandLineApplication<MainAction>();
        app.Conventions
           .UseDefaultConventions()
           .UseConstructorInjection(services);

        try
        {
            return app.Execute(args);
        }
        catch (UnrecognizedCommandParsingException)
        {
            terminalService.WriteLine(
                $"Error: Unrecognized command or argument:",
                MessageStatus.Error
            );
            return 1;
        }
    }
}
