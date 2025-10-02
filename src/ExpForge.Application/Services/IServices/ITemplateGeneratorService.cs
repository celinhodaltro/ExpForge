using ExpForge.Application.Services.Enums;
using static ExpForge.Application.Services.Enums.Template;

namespace ExpForge.Application.Services.IServices
{
    /// <summary>
    /// Interface para gerar widgets do ArcGIS Experience Builder.
    /// </summary>
    public interface ITemplateGeneratorService
    {
        /// <summary>
        /// Gera um widget vazio a partir do nome informado.
        /// </summary>
        /// <param name="widgetName">Nome do widget a ser criado.</param>
        public bool Generate(
                    string name,
                    string templatePath,
                    string templateName,
                    TemplateType type,
                    string? outputRoot = null);
    }
}
