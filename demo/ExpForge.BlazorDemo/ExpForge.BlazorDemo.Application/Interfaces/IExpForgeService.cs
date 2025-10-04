using ExpForge.BlazorDemo.Application.DTOs;

namespace ExpForge.BlazorDemo.Application.Interfaces;

/// <summary>
/// Interface para integração com o ExpForge CLI
/// </summary>
public interface IExpForgeService
{
    /// <summary>
    /// Gera um widget usando o ExpForge CLI
    /// </summary>
    Task<string> GenerateWidgetAsync(string templateName, string widgetName, Dictionary<string, object> parameters);
    
    /// <summary>
    /// Lista templates disponíveis no ExpForge
    /// </summary>
    Task<IEnumerable<string>> GetAvailableTemplatesAsync();
    
    /// <summary>
    /// Valida se um template existe
    /// </summary>
    Task<bool> ValidateTemplateAsync(string templateName);
    
    /// <summary>
    /// Executa comando personalizado do ExpForge
    /// </summary>
    Task<string> ExecuteCommandAsync(string command, string[] arguments);
    
    /// <summary>
    /// Obtém informações sobre a versão do ExpForge
    /// </summary>
    Task<string> GetVersionAsync();
}
