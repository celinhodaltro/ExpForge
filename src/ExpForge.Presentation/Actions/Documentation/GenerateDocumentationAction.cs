using ExpForge.Application.Interfaces.Services;
using ExpForge.Infrastructure.Services;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ExpForge.Presentation.Actions;

[Command(
    Name = "Generate-Documentation",
    Description = "Automatically scans all CLI actions and generates corresponding Blazor documentation. " +
                  "Extracts command metadata, parameters, and descriptions via reflection, " +
                  "then builds an interactive documentation interface under the 'Commands' module."
)]
public class GenerateDocumentationAction
{
    private readonly IMediator _mediator;
    private readonly ITerminalMessageService _terminalMessageService;

    public GenerateDocumentationAction(IMediator mediator, ITerminalMessageService terminalMessageService)
    {
        _mediator = mediator;
        _terminalMessageService = terminalMessageService;
    }

    public async Task OnExecuteAsync(CommandLineApplication app)
    {
        _terminalMessageService.WriteLine("Buscando comandos CLI via reflexão...");

        var assembly = Assembly.GetExecutingAssembly();

        var commands = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<CommandAttribute>() != null
                        && t.Name != "MainAction") 
            .Select(t =>
            {
                var attr = t.GetCustomAttribute<CommandAttribute>();
                var title = attr?.Name ?? t.Name;
                var description = attr?.Description ?? string.Empty;

                var parameters = t.GetProperties()
                    .Where(p => p.GetCustomAttribute<ArgumentAttribute>() != null)
                    .Select(p =>
                    {
                        var argAttr = p.GetCustomAttribute<ArgumentAttribute>();
                        var descAttr = p.GetCustomAttribute<DescriptionAttribute>();
                        return new GenerateDocumentationCommand.ParameterInfo(
                            p.Name,
                            p.PropertyType.Name,
                            descAttr?.Description ?? argAttr?.Description ?? ""
                        );
                    })
                    .ToList();

                return new GenerateDocumentationCommand.CommandInfo(title, description, parameters);
            })
            .ToList();



        _terminalMessageService.WriteLine($"{commands.Count} comandos encontrados.");
        await _mediator.Send(new GenerateDocumentationCommand(commands));
    }
}
