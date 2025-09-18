using System;
using ExperienceWidgetCli.Services;

class Program
{
    static void Main(string[] args)
    {
        var isTest = false;
        
        if (isTest)
        {
            args = new string[] { "create", "MyWidget" };
        }

        if (args.Length < 2 || args[0].ToLower() != "create")
        {
            Console.WriteLine("experience-widget create <name-of-widget>");
            return;
        }

        var widgetName = args[1];
        var templatesPath = Path.Combine(AppContext.BaseDirectory, "templates");

        var generator = new WidgetGenerator(templatesPath);

        var templateName = "empty";

        if (args.Length == 3)
            templateName = args[2];
        

        generator.Generate(widgetName, templateName);
    }
}
