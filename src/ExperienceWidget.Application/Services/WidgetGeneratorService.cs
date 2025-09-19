
using ExperienceWidget.CLI.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExperienceWidgetCli.Services
{
    public class WidgetGeneratorService : IWidgetGeneratorService
    {
        private readonly string _templatesPath;

        public WidgetGeneratorService(string templatesPath)
        {
            _templatesPath = templatesPath;
        }

        public void Generate(string widgetName, string templateName, string? outputRoot = null)
        {
            if (string.IsNullOrWhiteSpace(widgetName))
            {
                TerminalMessageService.WriteLine("Erro: É necessário informar um nome para o widget.", MessageStatus.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(templateName))
            {
                templateName = "empty";
            }

            if (!Directory.Exists(_templatesPath))
            {
                TerminalMessageService.WriteLine($"Erro: O caminho dos templates '{_templatesPath}' não existe.", MessageStatus.Error);
                return;
            }

            var templatePath = Path.Combine(_templatesPath, templateName);
            if (!Directory.Exists(templatePath))
            {
                TerminalMessageService.WriteLine($"Erro: O template '{templateName}' não existe em {_templatesPath}.", MessageStatus.Error);
                return;
            }

            outputRoot ??= Directory.GetCurrentDirectory();
            var widgetPath = Path.Combine(outputRoot, widgetName);

            if (Directory.Exists(widgetPath))
                Directory.Delete(widgetPath, true);


            var tags = new Dictionary<string, string>
            {
                { "WIDGET_NAME", widgetName },
                { "AUTHOR", "Your Organization/Name" },
                { "DATE", DateTime.UtcNow.ToString("yyyy-MM-dd") }
            };

            var templateCopier = new TemplateCopierService(tags);
            templateCopier.Copy(templatePath, widgetPath);
        }

    }


}
