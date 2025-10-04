using ExpForge.BlazorDemo.Application.DTOs;

namespace ExpForge.BlazorDemo.Application.Interfaces;

/// <summary>
/// Interface para serviços de Widget
/// </summary>
public interface IWidgetService
{
    Task<WidgetDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<WidgetDto>> GetAllAsync();
    Task<IEnumerable<WidgetDto>> GetByTagsAsync(IEnumerable<string> tags);
    Task<WidgetDto> CreateAsync(CreateWidgetDto createWidgetDto);
    Task<WidgetDto> UpdateAsync(Guid id, UpdateWidgetDto updateWidgetDto);
    Task DeleteAsync(Guid id);
    Task<string> GenerateWidgetCodeAsync(Guid widgetId);
}
