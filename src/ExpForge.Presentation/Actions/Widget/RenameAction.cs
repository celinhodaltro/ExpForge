using ExpForge.Application.Commands;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExpForge.CLI.Actions.Widget;

[Command(Name = "Rename", Description = "Rename Widget")]
public class RenameAction
{
    private readonly IMediator _mediator;

    public RenameAction(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Argument(0, Description = "New widget name (optional, will prompt if not provided)")]
    public string NewWidgetName { get; set; }

    [Argument(1, Description = "Widget folder path (optional, will prompt if not provided)")]
    public string WidgetPath { get; set; }

    public async Task OnExecuteAsync()
    {
        NewWidgetName = GetWidgetName();
        if (string.IsNullOrWhiteSpace(NewWidgetName)) return;

        WidgetPath = GetWidgetPath();
        if (string.IsNullOrWhiteSpace(WidgetPath)) return;

        var renameWidgetCommand = new RenameWidgetCommand(WidgetPath, NewWidgetName);
        await _mediator.Send(renameWidgetCommand);
    }

    private string GetWidgetName()
    {
        if (!string.IsNullOrWhiteSpace(NewWidgetName))
            return NewWidgetName;

        var input = Prompt.GetString("Enter the new widget name:");
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.Error.WriteLine("❌ Widget name cannot be empty.");
            return null;
        }

        return input;
    }

    private string GetWidgetPath()
    {
        if (!string.IsNullOrWhiteSpace(WidgetPath))
            return WidgetPath;

        var input = Prompt.GetString("Enter the widget folder path (default: current folder):");
        return string.IsNullOrWhiteSpace(input) ? Directory.GetCurrentDirectory() : input;
    }
}
