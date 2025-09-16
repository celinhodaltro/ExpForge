using System;
using System.IO;

namespace ExperienceWidgetCli.Services
{
    public class WidgetGenerator : IWidgetGenerator
    {
        private readonly string _templatesPath;

        public WidgetGenerator(string templatesPath)
        {
            _templatesPath = templatesPath;
        }

        public void Generate(string widgetName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), widgetName);

            if (Directory.Exists(path))
            {
                Console.WriteLine($"Erro: A pasta '{widgetName}' já existe.");
                return;
            }

            Directory.CreateDirectory(path);

            foreach (var file in Directory.GetFiles(_templatesPath))
            {
                var content = File.ReadAllText(file);
                content = content.Replace("{{WIDGET_NAME}}", widgetName);
                var fileName = Path.GetFileName(file).Replace(".template", "");
                File.WriteAllText(Path.Combine(path, fileName), content);
            }

            Console.WriteLine($"Widget '{widgetName}' criado em {path}");
        }
    }
}
