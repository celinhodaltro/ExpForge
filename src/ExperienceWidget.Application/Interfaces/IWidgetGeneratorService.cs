namespace ExperienceWidgetCli.Services
{
    /// <summary>
    /// Interface para gerar widgets do ArcGIS Experience Builder.
    /// </summary>
    public interface IWidgetGeneratorService
    {
        /// <summary>
        /// Gera um widget vazio a partir do nome informado.
        /// </summary>
        /// <param name="widgetName">Nome do widget a ser criado.</param>
        void Generate(string widgetName, string templateName, string? outputRoot = null);
    }
}
