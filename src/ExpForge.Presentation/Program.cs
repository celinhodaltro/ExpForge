using ExpForge.CLI.Actions;
using ExpForge.CLI.Extensions;
using ExpForge.CLI.Services;
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
            .BuildServiceProvider();

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
            TerminalMessageService.WriteLine($"Error: Unrecognized command or argument:", MessageStatus.Error);
            return 1;
        }
    }
}
