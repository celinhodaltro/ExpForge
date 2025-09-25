using ExpForge.Application.Services.IServices;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System.IO;
using System.Threading.Tasks;

namespace ExpForge.Presentation.Actions.Components;

[Command(Name = "New-Component", Description = "Create Component")]
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

    }

    private string GetComponentName(CommandLineApplication app)
    {
        return app.GetValueOrPrompt(
            currentValue: TemplatePath,
            promptMessage: "Enter the component name:",
            validateOrResolve: () =>
            {
                TemplatePath = _templatePathProvider.GetTemplatesPath();
                if (string.IsNullOrEmpty(TemplatePath) || !Directory.Exists(TemplatePath))
                {
                    app.Error.WriteLine("❌ 'templates' folder not found.");
                    return null;
                }
                return TemplatePath;
            });
    }


}
