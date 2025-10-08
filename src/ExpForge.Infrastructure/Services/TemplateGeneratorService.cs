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

        /// <summary>
        /// Gera arquivos a partir de templates Blazor ou outros tipos definidos.
        /// </summary>
        /// <param name="name">Nome do template de saída</param>
        /// <param name="templatePath">Caminho raiz dos templates</param>
        /// <param name="templateName">Nome do template a ser usado</param>
        /// <param name="type">Tipo do template (enum TemplateType)</param>
        /// <param name="outputRoot">Diretório raiz de saída (opcional)</param>
        /// <returns>Retorna true se o template foi gerado com sucesso</returns>
        public bool Generate(
            string name,
            string templatePath,
            string templateName,
            TemplateType type,
            Dictionary<TemplateTag, string>? tags = null,
            string? outputRoot = null)
        {
            templateName ??= "empty";

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
