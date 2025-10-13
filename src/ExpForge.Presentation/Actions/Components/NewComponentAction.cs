using ExpForge.Application.Commands.Component;
using ExpForge.Application.Interfaces.Providers;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System.IO;
using System.Threading.Tasks;

namespace ExpForge.Presentation.Actions.Components;

[Command(
    Name = "New-Component",
    Description = "Creates a new component based on an existing template. " +
                  "Allows you to specify the component name and automatically uses the configured template path. " +
                  "Ideal for quickly scaffolding standardized components that follow the framework’s structure and conventions."
)]

public class NewComponentAction
{
    private readonly IMediator _mediator;
    private readonly ITemplatePathProvider _templatePathProvider;

    public NewComponentAction(IMediator mediator, ITemplatePathProvider templatePathProvider)
    {
        _mediator = mediator;
        _templatePathProvider = templatePathProvider;
    }

    [Argument(0, Description = "Widget name (optional, will prompt if not provided)")]
    public string ComponentName { get; set; }
    public string TemplatePath { get; set; }

    public async Task OnExecuteAsync(CommandLineApplication app)
    {
        TemplatePath = app.GetTemplatePath(() => _templatePathProvider.GetTemplatesPath());
        if (TemplatePath == null) return;

        ComponentName = GetComponentName(app);
        if (string.IsNullOrEmpty(ComponentName)) return;

        await _mediator.Send(new NewComponentCommand(componentName: ComponentName, templatePath: TemplatePath));

    }

    private string GetComponentName(CommandLineApplication app)
    {
        return app.GetValueOrPrompt(
            currentValue: ComponentName,
            promptMessage: "Enter the component name:",
            validateOrResolve: () =>
            {
                if (string.IsNullOrEmpty(TemplatePath))
                {
                    app.Error.WriteLine("❌ 'templates' folder not found.");
                    return null;
                }

                if (!string.IsNullOrWhiteSpace(ComponentName))
                    return ComponentName;

                return null;
            });
    }



}
