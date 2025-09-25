using ExpForge.Application.Commands;
using ExpForge.Application.Interfaces;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExpForge.CLI.Actions.Widget;

[Command(Name = "New-Widget", Description = "Create Widget")]
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

        await _mediator.Send(new CreateWidgetCommand(widgetName: WidgetName, templateName: TemplateName, templatePath: TemplatePath));
    }



    private string GetWidgetName(CommandLineApplication app)
    {
        if (!string.IsNullOrWhiteSpace(WidgetName))
            return WidgetName;

        var input = Prompt.GetString("Enter the widget name:");
        if (string.IsNullOrWhiteSpace(input))
        {
            app.Error.WriteLine("❌ Widget name cannot be empty.");
            return null;
        }

        return input;
    }


    private string GetTemplate(CommandLineApplication app)
    {
        if (!string.IsNullOrWhiteSpace(TemplateName))
            return TemplateName;

        if (!Directory.Exists(TemplatePath))
        {
            app.Error.WriteLine("❌ 'templates' folder not found.");
            return null;
        }

        var templates = Directory.GetDirectories(TemplatePath)
                                 .Select(Path.GetFileName)
                                 .ToList();

        if (templates.Count == 0)
        {
            app.Error.WriteLine("❌ No templates available.");
            return null;
        }

        return PromptTemplateSelection(templates, app);
    }

    private string PromptTemplateSelection(IList<string> templates, CommandLineApplication app)
    {
        app.Out.WriteLine("Choose a template:");
        for (int i = 0; i < templates.Count; i++)
        {
            app.Out.WriteLine($"[ {i + 1} ] - {templates[i]}");
        }

        int choice = -1;
        while (choice < 1 || choice > templates.Count)
        {
            var input = Prompt.GetString("Enter the number of the template:");
            int.TryParse(input, out choice);
        }

        return templates[choice - 1];
    }
}
