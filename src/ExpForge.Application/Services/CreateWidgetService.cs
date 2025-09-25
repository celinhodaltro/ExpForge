
using ExpForge.CLI.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExpForgeCli.Services
{
    public class CreateWidgetService : ICreateWidgetService
    {
        public bool Generate(string widgetName, string templatePath, string templateName, string? outputRoot = null)
        {
            if (string.IsNullOrWhiteSpace(templateName))
            {
                templateName = "empty";
            }

            if (!Directory.Exists(templatePath))
            {
                TerminalMessageService.WriteLine($"Error: The templates path '{templatePath}' does not exist.", MessageStatus.Error);
                return false;
            }

            var templateSelectedPath = Path.Combine(templatePath, templateName);

            if (!Directory.Exists(templateSelectedPath))
            {
                TerminalMessageService.WriteLine($"Error: The template '{templateName}' does not exist in {templatePath}.", MessageStatus.Error);
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
            templateCopier.Copy(templateSelectedPath, widgetPath);

            return true;
        }

    }


}
