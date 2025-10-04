namespace ExpForge.BlazorDemo.Domain.Enums;

/// <summary>
/// Tipos de templates suportados pelo ExpForge
/// </summary>
public enum TemplateType
{
    /// <summary>
    /// Template para componente Blazor
    /// </summary>
    BlazorComponent = 1,
    
    /// <summary>
    /// Template para widget JavaScript
    /// </summary>
    JavaScriptWidget = 2,
    
    /// <summary>
    /// Template para componente React
    /// </summary>
    ReactComponent = 3,
    
    /// <summary>
    /// Template para componente Vue
    /// </summary>
    VueComponent = 4,
    
    /// <summary>
    /// Template genérico HTML/CSS/JS
    /// </summary>
    GenericHtml = 5
}
