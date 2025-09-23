using ExperienceWidget.Application.Commands;
using ExperienceWidget.CLI.Services;
using ExperienceWidgetCli.Services;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System.IO;
using System.Threading.Tasks;

namespace ExperienceWidget.CLI.Actions.Widget;

[Command(Name = "Rename", Description = "Rename Widget")]
public class RenameAction
{
    private readonly IMediator _mediator;
    private readonly CreateWidgetService _generator;

    public RenameAction(IMediator mediator, CreateWidgetService generator)
    {
        _mediator = mediator;
        _generator = generator;
    }

    [Argument(0, Description = "Widget name", ShowInHelpText = true)]
    public string NewWidgetName { get; set; }

    [Argument(1, Description = "New widget folder path (optional, default: same folder)",ShowInHelpText = true)]
    public string WidgetPath { get; set; }

    private async Task OnExecuteAsync()
    {
        var renameWidgetCommand = new RenameWidgetCommand(WidgetPath, NewWidgetName);
        await _mediator.Send(renameWidgetCommand);

    }
}
