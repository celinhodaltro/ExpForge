using System.ComponentModel.DataAnnotations;
using ExpForge.BlazorDemo.Domain.Enums;

namespace ExpForge.BlazorDemo.Domain.Entities;

/// <summary>
/// Representa um template usado para gerar widgets
/// </summary>
public class Template
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public TemplateType Type { get; set; }
    
    [Required]
    public string Version { get; set; } = "1.0.0";
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Parâmetros configuráveis do template
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new();
    
    /// <summary>
    /// Widgets criados a partir deste template
    /// </summary>
    public List<Widget> Widgets { get; set; } = new();
}
