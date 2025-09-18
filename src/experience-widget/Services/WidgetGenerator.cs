
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

        public void Generate(string widgetName, string templateName, string? outputRoot = null)
        {
            if (string.IsNullOrWhiteSpace(widgetName))
            {
                Console.WriteLine("Erro: É necessário informar um nome para o widget.");
                return;
            }

            if (string.IsNullOrWhiteSpace(templateName))
            {
                Console.WriteLine("Erro: É necessário informar um nome de template.");
                return;
            }

            if (!Directory.Exists(_templatesPath))
            {
                Console.WriteLine($"Erro: O caminho dos templates '{_templatesPath}' não existe.");
                return;
            }

            var templatePath = Path.Combine(_templatesPath, templateName);
            if (!Directory.Exists(templatePath))
            {
                Console.WriteLine($"Erro: O template '{templateName}' não existe em {_templatesPath}.");
                return;
            }

            outputRoot ??= Directory.GetCurrentDirectory();
            var widgetPath = Path.Combine(outputRoot, widgetName);

            if (Directory.Exists(widgetPath))
            {
                Directory.Delete(widgetPath, true);
            }

            // Copia os arquivos do template
            var templateCopier = new TemplateCopier();
            templateCopier.Copy(templatePath, widgetPath);

            Console.WriteLine($"Widget '{widgetName}' criado em {widgetPath}");
        }

    }
}
