using ExpForge.BlazorDemo.Domain.Enums;

namespace ExpForge.BlazorDemo.Application.DTOs;

/// <summary>
/// DTO para transferência de dados de Widget
/// </summary>
public class WidgetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string TemplatePath { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public string Configuration { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
}

/// <summary>
/// DTO para criação de Widget
/// </summary>
public class CreateWidgetDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid TemplateId { get; set; }
    public string Configuration { get; set; } = "{}";
    public List<string> Tags { get; set; } = new();
}

/// <summary>
/// DTO para atualização de Widget
/// </summary>
public class UpdateWidgetDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Configuration { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public bool IsActive { get; set; } = true;
}
