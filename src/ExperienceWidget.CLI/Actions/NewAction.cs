using ExperienceWidget.Application.Commands;
using ExperienceWidgetCli.Services;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System.Threading.Tasks;

namespace ExperienceWidget.CLI.Actions;

[Command(Name = "New", Description = "Create Widget")]
public class NewAction
{
    private readonly IMediator _mediator;
    private readonly CreateWidgetService _generator;

    public NewAction(IMediator mediator, CreateWidgetService generator)
    {
        _mediator = mediator;
        _generator = generator;
    }

    [Argument(0, Description = "Widget name")]
    public string Name { get; set; }

    [Argument(1, Description = "Template name")]
    public string Template { get; set; }

    private async Task OnExecuteAsync()
    {
        var createWidgetCommand = new CreateWidgetCommand(Name, Template);
        await _mediator.Send(createWidgetCommand);
    }
}
