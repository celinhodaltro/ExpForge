using ExpForge.Application.Interfaces.Services;
using ExpForge.Domain.Enums;
using ExpForge.Domain.Extensions;

namespace ExpForge.Infrastructure.Services
{
    public class TemplateGeneratorService : ITemplateGeneratorService
    {
        private readonly ITerminalMessageService _terminalMessageService;

        // Injeção via construtor
        public TemplateGeneratorService(ITerminalMessageService terminalMessageService)
        {
            _terminalMessageService = terminalMessageService;
        }

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
                _terminalMessageService.WriteLine(
                    $"Error: The templates root path '{templatePath}' does not exist.",
                    MessageStatus.Error
                );
                return false;
            }

            var typeFolder = type.ConvertTypeToFolderName();
            var typePath = Path.Combine(templatePath, typeFolder);

            if (!Directory.Exists(typePath))
            {
                _terminalMessageService.WriteLine(
                    $"Error: The template type '{typeFolder}' does not exist in {templatePath}.",
                    MessageStatus.Error
                );
                return false;
            }

            var templateSelectedPath = Path.Combine(typePath, templateName);

            if (!Directory.Exists(templateSelectedPath))
            {
                _terminalMessageService.WriteLine(
                    $"Error: The template '{templateName}' does not exist in {typePath}.",
                    MessageStatus.Error
                );
                return false;
            }

            outputRoot ??= Directory.GetCurrentDirectory();
            var outputPath = Path.Combine(outputRoot, name);

            if (Directory.Exists(outputPath))
            {
                _terminalMessageService.WriteLine(
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

            _terminalMessageService.WriteLine(
                $"{type} '{name}' generated successfully at '{outputPath}'.",
                MessageStatus.Success
            );

            return true;
        }
    }
}
