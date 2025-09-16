using System;
using ExperienceWidgetCli.Services;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2 || args[0].ToLower() != "create")
        {
            Console.WriteLine("Uso: experience-widget create <nome-do-widget>");
            return;
        }

        var widgetName = args[1];
        var templatesPath = Path.Combine(AppContext.BaseDirectory, "templates");

        var generator = new WidgetGenerator(templatesPath);
        generator.Generate(widgetName);
    }
}
