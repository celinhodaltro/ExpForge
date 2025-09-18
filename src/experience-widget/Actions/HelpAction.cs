using ExperienceWidget.Application.Commands;
using ExperienceWidgetCli.Services;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExperienceWidget.CLI.Actions;

[Command(Name = "Help", Description = "Create Widget")]
public class HelpAction
{
    private readonly IMediator _mediator;
    private readonly WidgetGeneratorService _generator;

    public HelpAction(IMediator mediator, WidgetGeneratorService generator)
    {
        _mediator = mediator;
        _generator = generator;
    }

    private async Task OnExecuteAsync()
    {
        Console.WriteLine("Usage: experience-widget create <WidgetName> <TemplateName>");
    }
}
