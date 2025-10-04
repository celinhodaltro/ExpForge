using System.ComponentModel.DataAnnotations;

namespace ExpForge.BlazorDemo.Domain.Entities;

/// <summary>
/// Representa um widget criado pelo ExpForge
/// </summary>
public class Widget
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public string TemplatePath { get; set; } = string.Empty;
    
    [Required]
    public string Version { get; set; } = "1.0.0";
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Configurações específicas do widget em formato JSON
    /// </summary>
    public string Configuration { get; set; } = "{}";
    
    /// <summary>
    /// Tags para categorização do widget
    /// </summary>
    public List<string> Tags { get; set; } = new();
}
