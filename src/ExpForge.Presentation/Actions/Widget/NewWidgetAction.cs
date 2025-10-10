using ExpForge.Application.Commands.Widget;
using ExpForge.Application.Interfaces.Providers;
using ExpForge.Domain.Enums;
using ExpForge.Domain.Extensions;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExpForge.Presentation.Actions.Widget;

[Command(
    Name = "New-Widget",
    Description = "Generates a new widget from one of the available templates. " +
                  "Supports both direct and interactive selection of the widget name and template. " +
                  "Useful for building reusable UI or functional elements while keeping consistency across the system."
)]
public class NewWidgetAction
{
    private readonly IMediator _mediator;
    private readonly ITemplatePathProvider _templatePathProvider;

    public NewWidgetAction(IMediator mediator, ITemplatePathProvider templatePathProvider)
    {
        _mediator = mediator;
        _templatePathProvider = templatePathProvider;
    }

    [Argument(0, Description = "Widget name (optional, will prompt if not provided)")]
    public string WidgetName { get; set; }

    [Argument(1, Description = "Template name (optional, will prompt if not provided)")]
    public string TemplateName { get; set; }

    public string TemplatePath { get; set; }

    public async Task OnExecuteAsync(CommandLineApplication app)
    {
        TemplatePath = app.GetTemplatePath(() => _templatePathProvider.GetTemplatesPath());
        if (TemplatePath == null) return;

        WidgetName = GetWidgetName(app);
        if (string.IsNullOrEmpty(WidgetName)) return;

        TemplateName = GetTemplate(app);
        if (string.IsNullOrEmpty(TemplateName)) return;

        await _mediator.Send(new NewWidgetCommand(
            widgetName: WidgetName,
            templateName: TemplateName,
            templatePath: TemplatePath
        ));
    }

    private string GetWidgetName(CommandLineApplication app)
    {
        return app.GetValueOrPrompt(
            currentValue: WidgetName,
            promptMessage: "Enter the widget name:",
            validateOrResolve: () =>
            {
                if (string.IsNullOrEmpty(TemplatePath) || !Directory.Exists(TemplatePath))
                {
                    app.Error.WriteLine("❌ 'templates' folder not found.");
                    return null;
                }

                return !string.IsNullOrWhiteSpace(WidgetName) ? WidgetName : null;
            },
            errorMessageIfEmpty: "❌ Widget name cannot be empty."
        );
    }

    private string GetTemplate(CommandLineApplication app)
    {
        return app.GetValueOrPrompt(
            currentValue: TemplateName,
            promptMessage: "Enter the template name:",
            validateOrResolve: () =>
            {
                if (!Directory.Exists(TemplatePath))
                {
                    app.Error.WriteLine("❌ 'templates' folder not found.");
                    return null;
                }

                var widgetTemplatePath = Path.Combine(TemplatePath, TemplateType.Widget.ConvertTypeToFolderName());

                var templates = Directory.GetDirectories(widgetTemplatePath)
                                         .Select(Path.GetFileName)
                                         .ToList();

                if (templates.Count == 0)
                {
                    app.Error.WriteLine("❌ No templates available.");
                    return null;
                }

                return PromptTemplateSelection(templates, app);
            },
            errorMessageIfEmpty: "❌ Template name cannot be empty."
        );
    }
    private static string PromptTemplateSelection(List<string> templates, CommandLineApplication app)
    {
        app.Out.WriteLine("Choose a template:");
        for (int i = 0; i < templates.Count; i++)
        {
            app.Out.WriteLine($"[ {i + 1} ] - {templates[i]}");
        }

        int choice = -1;
        while (true)
        {
            var input = Prompt.GetString("Enter the number of the template:");

            if (int.TryParse(input, out choice) && choice >= 1 && choice <= templates.Count)
            {
                break;
            }

            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            app.Out.WriteLine("Invalid choice. Please enter a valid number.");
            Console.ForegroundColor = originalColor;
        }

        return templates[choice - 1];
    }

}
