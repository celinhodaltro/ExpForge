using ExpForge.BlazorDemo.Domain.Entities;
using ExpForge.BlazorDemo.Domain.Enums;

namespace ExpForge.BlazorDemo.Domain.Interfaces;

/// <summary>
/// Interface para repositório de templates
/// </summary>
public interface ITemplateRepository
{
    Task<Template?> GetByIdAsync(Guid id);
    Task<IEnumerable<Template>> GetAllAsync();
    Task<IEnumerable<Template>> GetByTypeAsync(TemplateType type);
    Task<Template> CreateAsync(Template template);
    Task<Template> UpdateAsync(Template template);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
