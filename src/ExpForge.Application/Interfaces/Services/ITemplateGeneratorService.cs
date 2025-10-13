using ExpForge.Domain.Enums;
namespace ExpForge.Application.Interfaces.Services
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
                    Dictionary<TemplateTag, string>? tags,
                    string? outputRoot = null,
                    bool useUnifiedFolder = false);
    }
}
