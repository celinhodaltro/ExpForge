using ExpForge.BlazorDemo.Domain.Entities;

namespace ExpForge.BlazorDemo.Domain.Interfaces;

/// <summary>
/// Interface para repositório de widgets
/// </summary>
public interface IWidgetRepository
{
    Task<Widget?> GetByIdAsync(Guid id);
    Task<IEnumerable<Widget>> GetAllAsync();
    Task<IEnumerable<Widget>> GetByTagsAsync(IEnumerable<string> tags);
    Task<Widget> CreateAsync(Widget widget);
    Task<Widget> UpdateAsync(Widget widget);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
