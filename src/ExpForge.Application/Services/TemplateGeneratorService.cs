using ExpForge.Application.Services.Enums;
using ExpForge.Application.Services.IServices;
using ExpForge.CLI.Services;

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
                    $"Error: The templates path '{templatePath}' does not exist.",
                    MessageStatus.Error
                );
                return false;
            }

            var templateSelectedPath = Path.Combine(templatePath, templateName);

            if (!Directory.Exists(templateSelectedPath))
            {
                TerminalMessageService.WriteLine(
                    $"Error: The template '{templateName}' does not exist in {templatePath}.",
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
