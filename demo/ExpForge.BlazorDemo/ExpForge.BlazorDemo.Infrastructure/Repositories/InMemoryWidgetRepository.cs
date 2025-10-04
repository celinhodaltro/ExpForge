using ExpForge.BlazorDemo.Domain.Entities;
using ExpForge.BlazorDemo.Domain.Interfaces;
using System.Collections.Concurrent;

namespace ExpForge.BlazorDemo.Infrastructure.Repositories;

/// <summary>
/// Implementação em memória do repositório de widgets para demonstração
/// </summary>
public class InMemoryWidgetRepository : IWidgetRepository
{
    private readonly ConcurrentDictionary<Guid, Widget> _widgets = new();

    public Task<Widget?> GetByIdAsync(Guid id)
    {
        _widgets.TryGetValue(id, out var widget);
        return Task.FromResult(widget);
    }

    public Task<IEnumerable<Widget>> GetAllAsync()
    {
        return Task.FromResult(_widgets.Values.AsEnumerable());
    }

    public Task<IEnumerable<Widget>> GetByTagsAsync(IEnumerable<string> tags)
    {
        var tagList = tags.ToList();
        var filteredWidgets = _widgets.Values
            .Where(w => w.Tags.Any(tag => tagList.Contains(tag, StringComparer.OrdinalIgnoreCase)))
            .AsEnumerable();
            
        return Task.FromResult(filteredWidgets);
    }

    public Task<Widget> CreateAsync(Widget widget)
    {
        widget.Id = Guid.NewGuid();
        widget.CreatedAt = DateTime.UtcNow;
        widget.UpdatedAt = DateTime.UtcNow;
        
        _widgets.TryAdd(widget.Id, widget);
        return Task.FromResult(widget);
    }

    public Task<Widget> UpdateAsync(Widget widget)
    {
        widget.UpdatedAt = DateTime.UtcNow;
        _widgets.AddOrUpdate(widget.Id, widget, (key, oldValue) => widget);
        return Task.FromResult(widget);
    }

    public Task DeleteAsync(Guid id)
    {
        _widgets.TryRemove(id, out _);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return Task.FromResult(_widgets.ContainsKey(id));
    }
}
