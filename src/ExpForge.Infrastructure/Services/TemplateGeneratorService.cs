using ExpForge.Application.Interfaces.Services;
using ExpForge.Application.Services.Enums;
using ExpForge.CLI.Services;
using static ExpForge.Application.Services.Enums.Template;

namespace ExpForge.Application.Services
{
    public class TemplateGeneratorService : ITemplateGeneratorService
    {
        public bool Generate(
            string name,
            string templatePath,
            string templateName,
            TemplateType type,
            string? outputRoot = null)
        {
            if (string.IsNullOrWhiteSpace(templateName))
            {
                templateName = "empty";
            }

            if (!Directory.Exists(templatePath))
            {
                TerminalMessageService.WriteLine(
                    $"Error: The templates root path '{templatePath}' does not exist.",
                    MessageStatus.Error
                );
                return false;
            }

            // 🔥 usa o tipo para resolver a pasta correta
            var typeFolder = Template.ConvertTypeToFolderName(type);
            var typePath = Path.Combine(templatePath, typeFolder);

            if (!Directory.Exists(typePath))
            {
                TerminalMessageService.WriteLine(
                    $"Error: The template type '{typeFolder}' does not exist in {templatePath}.",
                    MessageStatus.Error
                );
                return false;
            }

            var templateSelectedPath = Path.Combine(typePath, templateName);

            if (!Directory.Exists(templateSelectedPath))
            {
                TerminalMessageService.WriteLine(
                    $"Error: The template '{templateName}' does not exist in {typePath}.",
                    MessageStatus.Error
                );
                return false;
            }

            outputRoot ??= Directory.GetCurrentDirectory();
            var outputPath = Path.Combine(outputRoot, name);

            if (Directory.Exists(outputPath))
            {
                TerminalMessageService.WriteLine(
                    $"Error: {type} '{name}' already exists at '{outputPath}'.",
                    MessageStatus.Error
                );
                return false;
            }

            var tags = new Dictionary<TemplateTag, string>
            {
                { TemplateTag.NAME, name },
                { TemplateTag.WIDGETNAME, name },
                { TemplateTag.AUTHOR, "Your Organization/Name" },
                { TemplateTag.DATE, DateTime.UtcNow.ToString("yyyy-MM-dd") }
            };

            var templateCopier = new TemplateCopierService(tags);
            templateCopier.Copy(templateSelectedPath, outputPath);

            TerminalMessageService.WriteLine(
                $"{type} '{name}' generated successfully at '{outputPath}'.",
                MessageStatus.Success
            );

            return true;
        }
    }
}
