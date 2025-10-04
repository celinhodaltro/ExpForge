using ExpForge.BlazorDemo.Domain.Enums;

namespace ExpForge.BlazorDemo.Application.DTOs;

/// <summary>
/// DTO para transferência de dados de Template
/// </summary>
public class TemplateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public TemplateType Type { get; set; }
    public string Version { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
}

/// <summary>
/// DTO para criação de Template
/// </summary>
public class CreateTemplateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public TemplateType Type { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
}

/// <summary>
/// DTO para atualização de Template
/// </summary>
public class UpdateTemplateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Dictionary<string, object> Parameters { get; set; } = new();
    public bool IsActive { get; set; } = true;
}
