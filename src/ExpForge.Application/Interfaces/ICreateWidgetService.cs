namespace ExperienceWidgetCli.Services
{
    /// <summary>
    /// Interface para gerar widgets do ArcGIS Experience Builder.
    /// </summary>
    public interface ICreateWidgetService
    {
        /// <summary>
        /// Gera um widget vazio a partir do nome informado.
        /// </summary>
        /// <param name="widgetName">Nome do widget a ser criado.</param>
        public bool Generate(string widgetName, string templatePath, string templateName, string? outputRoot = null);
    }
}
