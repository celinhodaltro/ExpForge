using ExperienceWidget.Application.Commands;
using ExperienceWidget.CLI.Services;
using ExperienceWidgetCli.Services;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System.IO;
using System.Threading.Tasks;

namespace ExperienceWidget.CLI.Actions;

[Command(Name = "Rename", Description = "Rename Widget")]
public class RenameAction
{
    private readonly IMediator _mediator;
    private readonly WidgetGeneratorService _generator;

    public RenameAction(IMediator mediator, WidgetGeneratorService generator)
    {
        _mediator = mediator;
        _generator = generator;
    }

    [Argument(0, Description = "Widget name")]
    public string Name { get; set; }

    [Argument(1, Description = "New widget folder path (optional, default: same folder)")]
    public string? NewWidgetPath { get; set; }

    private async Task OnExecuteAsync()
    {
        var currentWidgetPath = Path.Combine(Directory.GetCurrentDirectory(), Name);

        var targetWidgetPath = string.IsNullOrWhiteSpace(NewWidgetPath)
            ? Path.Combine(Directory.GetParent(currentWidgetPath)!.FullName, NewWidgetPath ?? Name)
            : Path.GetFullPath(NewWidgetPath);

        if(!Directory.Exists(currentWidgetPath))
        {
            TerminalMessageService.WriteLine($"Erro: O widget '{currentWidgetPath}' não foi encontrado.", MessageStatus.Error);
            return;
        }

        var renameWidgetCommand = new RenameWidgetCommand(currentWidgetPath, targetWidgetPath);
        await _mediator.Send(renameWidgetCommand);

    }
}
