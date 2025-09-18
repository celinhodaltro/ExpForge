using ExperienceWidget.Application.Commands;
using ExperienceWidgetCli.Services;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System;
using System.IO;
using System.Threading.Tasks;

[Command(Name = "create", Description = "Create Widget")]
class CreateAction
{
    private readonly IMediator _mediator;
    private readonly WidgetGeneratorService _generator;

    public CreateAction(IMediator mediator, WidgetGeneratorService generator)
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
