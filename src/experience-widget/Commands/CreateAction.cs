using ExperienceWidgetCli.Services;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;

[Command(Name = "create", Description = "Cria um widget")]
class CreateAction
{
    [Argument(0, Description = "Widget name")]
    public string Name { get; set; }

    [Argument(1, Description = "Template name")]
    public string Template { get; set; }

    private void OnExecute()
    {
        var templatesPath = Path.Combine(AppContext.BaseDirectory, "templates");
        var generator = new WidgetGeneratorService(templatesPath);
        generator.Generate(Name, Template);
    }
}