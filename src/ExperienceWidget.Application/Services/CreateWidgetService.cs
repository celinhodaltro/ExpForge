
using ExperienceWidget.CLI.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExperienceWidgetCli.Services
{
    public class CreateWidgetService : IWidgetGeneratorService
    {
        private readonly string _templatesPath;

        public CreateWidgetService(string templatesPath)
        {
            _templatesPath = templatesPath;
        }

        public bool Generate(string widgetName, string templateName, string? outputRoot = null)
        {
            if (string.IsNullOrWhiteSpace(templateName))
            {
                templateName = "empty";
            }

            if (!Directory.Exists(_templatesPath))
            {
                TerminalMessageService.WriteLine($"Error: The templates path '{_templatesPath}' does not exist.", MessageStatus.Error);
                return false;
            }

            var templatePath = Path.Combine(_templatesPath, templateName);
            if (!Directory.Exists(templatePath))
            {
                TerminalMessageService.WriteLine($"Error: The template '{templateName}' does not exist in {_templatesPath}.", MessageStatus.Error);
                return false;
            }

            outputRoot ??= Directory.GetCurrentDirectory();
            var widgetPath = Path.Combine(outputRoot, widgetName);

            if (Directory.Exists(widgetPath))
            {
                TerminalMessageService.WriteLine($"Error: Widget '{widgetName}' already exists at '{widgetPath}'.", MessageStatus.Error);
                return false;
            }

            var tags = new Dictionary<string, string>
            {
                { "WIDGET_NAME", widgetName },
                { "AUTHOR", "Your Organization/Name" },
                { "DATE", DateTime.UtcNow.ToString("yyyy-MM-dd") }
            };

            var templateCopier = new TemplateCopierService(tags);
            templateCopier.Copy(templatePath, widgetPath);

            return true;
        }

    }


}
