using System;
using System.IO;

namespace ExperienceWidgetCli.Services
{
    public class WidgetGenerator : IWidgetGenerator
    {
        private string _templatesPath;

        public WidgetGenerator(string templatesPath)
        {
            _templatesPath = templatesPath;
        }

        public void Generate(string widgetName, string templateName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), widgetName);

            if (Directory.Exists(path))
            {
                Console.WriteLine($"Erro: A pasta '{widgetName}' já existe.");
                return;
            }

            Directory.CreateDirectory(path);


            if (String.IsNullOrEmpty(templateName))
            {
                Console.WriteLine($"Erro: É necessario um nome de template");
                return;
            }

            if(!Directory.Exists(_templatesPath + "\\" + templateName))
            {
                Console.WriteLine($"Erro: Template não existe {templateName}");
            }


            if (!Directory.Exists(_templatesPath))
            {
                Console.WriteLine($"Erro: O caminho dos templates '{_templatesPath}' não existe.");
                return;
            }

            var templateCopier = new TemplateCopier();
            var templatePath = Path.Combine(_templatesPath, templateName);

            templateCopier.Copy(templatePath, path);
            Console.WriteLine($"Widget '{widgetName}' criado em {path}");
        }
    }
}
