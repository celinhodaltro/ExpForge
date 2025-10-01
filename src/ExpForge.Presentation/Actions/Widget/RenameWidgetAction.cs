using ExpForge.Application.Commands;
using ExpForge.Application.Commands.Widget;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExpForge.Presentation.Actions.Widget;

[Command(Name = "Rename-Widget", Description = "Rename Widget")]
public class RenameWidgetAction
{
    private readonly IMediator _mediator;

    public RenameWidgetAction(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Argument(0, Description = "New widget name (optional, will prompt if not provided)")]
    public string NewWidgetName { get; set; }

    [Argument(1, Description = "Widget folder path (optional, will prompt if not provided)")]
    public string WidgetPath { get; set; }

    public async Task OnExecuteAsync(CommandLineApplication app)
    {
        NewWidgetName = GetWidgetName(app);
        if (string.IsNullOrWhiteSpace(NewWidgetName)) return;

        WidgetPath = GetWidgetPath();
        if (string.IsNullOrWhiteSpace(WidgetPath)) return;

        var renameWidgetCommand = new RenameWidgetCommand(WidgetPath, NewWidgetName);
        await _mediator.Send(renameWidgetCommand);
    }

    private string GetWidgetName(CommandLineApplication app)
    {
        return app.GetValueOrPrompt(
            currentValue: NewWidgetName,
            promptMessage: "Enter the widget name:",
            validateOrResolve: () =>
            {
                if (string.IsNullOrEmpty(WidgetPath) || !Directory.Exists(WidgetPath))
                {
                    app.Error.WriteLine("❌ 'templates' folder not found.");
                    return null;
                }

                return !string.IsNullOrWhiteSpace(NewWidgetName) ? NewWidgetName : null;
            },
            errorMessageIfEmpty: "❌ Widget name cannot be empty."
        );
    }

    private string GetWidgetPath()
    {
        if (!string.IsNullOrWhiteSpace(WidgetPath))
            return WidgetPath;

        var input = Prompt.GetString("Enter the widget folder path (default: current folder):");
        return string.IsNullOrWhiteSpace(input) ? Directory.GetCurrentDirectory() : input;
    }
}
