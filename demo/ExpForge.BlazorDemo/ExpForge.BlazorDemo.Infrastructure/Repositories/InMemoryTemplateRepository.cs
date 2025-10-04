using ExpForge.BlazorDemo.Domain.Entities;
using ExpForge.BlazorDemo.Domain.Enums;
using ExpForge.BlazorDemo.Domain.Interfaces;
using System.Collections.Concurrent;

namespace ExpForge.BlazorDemo.Infrastructure.Repositories;

/// <summary>
/// Implementação em memória do repositório de templates para demonstração
/// </summary>
public class InMemoryTemplateRepository : ITemplateRepository
{
    private readonly ConcurrentDictionary<Guid, Template> _templates = new();

    public InMemoryTemplateRepository()
    {
        // Adicionar alguns templates de exemplo
        SeedTemplates();
    }

    public Task<Template?> GetByIdAsync(Guid id)
    {
        _templates.TryGetValue(id, out var template);
        return Task.FromResult(template);
    }

    public Task<IEnumerable<Template>> GetAllAsync()
    {
        return Task.FromResult(_templates.Values.AsEnumerable());
    }

    public Task<IEnumerable<Template>> GetByTypeAsync(TemplateType type)
    {
        var filteredTemplates = _templates.Values
            .Where(t => t.Type == type)
            .AsEnumerable();
            
        return Task.FromResult(filteredTemplates);
    }

    public Task<Template> CreateAsync(Template template)
    {
        template.Id = Guid.NewGuid();
        template.CreatedAt = DateTime.UtcNow;
        template.UpdatedAt = DateTime.UtcNow;
        
        _templates.TryAdd(template.Id, template);
        return Task.FromResult(template);
    }

    public Task<Template> UpdateAsync(Template template)
    {
        template.UpdatedAt = DateTime.UtcNow;
        _templates.AddOrUpdate(template.Id, template, (key, oldValue) => template);
        return Task.FromResult(template);
    }

    public Task DeleteAsync(Guid id)
    {
        _templates.TryRemove(id, out _);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return Task.FromResult(_templates.ContainsKey(id));
    }

    private void SeedTemplates()
    {
        var blazorTemplate = new Template
        {
            Id = Guid.NewGuid(),
            Name = "Blazor Component",
            Description = "Template básico para componente Blazor",
            Type = TemplateType.BlazorComponent,
            Content = @"@page ""/{{ComponentName}}""

<h3>{{ComponentTitle}}</h3>

<div class=""{{ComponentName}}-container"">
    <p>{{ComponentDescription}}</p>
    
    @if (IsVisible)
    {
        <div class=""content"">
            {{ComponentContent}}
        </div>
    }
</div>

@code {
    [Parameter] public string ComponentTitle { get; set; } = ""{{ComponentTitle}}"";
    [Parameter] public string ComponentDescription { get; set; } = ""{{ComponentDescription}}"";
    [Parameter] public bool IsVisible { get; set; } = true;
}",
            Parameters = new Dictionary<string, object>
            {
                { "ComponentName", "MyComponent" },
                { "ComponentTitle", "Meu Componente" },
                { "ComponentDescription", "Descrição do componente" },
                { "ComponentContent", "Conteúdo do componente" }
            }
        };

        var jsTemplate = new Template
        {
            Id = Guid.NewGuid(),
            Name = "JavaScript Widget",
            Description = "Template para widget JavaScript",
            Type = TemplateType.JavaScriptWidget,
            Content = @"class {{WidgetName}} {
    constructor(container, options = {}) {
        this.container = container;
        this.options = {
            title: '{{WidgetTitle}}',
            theme: 'default',
            ...options
        };
        this.init();
    }

    init() {
        this.render();
        this.bindEvents();
    }

    render() {
        this.container.innerHTML = `
            <div class=""{{WidgetName}}-widget"">
                <h3>${this.options.title}</h3>
                <div class=""widget-content"">
                    {{WidgetContent}}
                </div>
            </div>
        `;
    }

    bindEvents() {
        // Adicionar event listeners aqui
    }
}

// Exportar para uso global
window.{{WidgetName}} = {{WidgetName}};",
            Parameters = new Dictionary<string, object>
            {
                { "WidgetName", "MyWidget" },
                { "WidgetTitle", "Meu Widget" },
                { "WidgetContent", "Conteúdo do widget" }
            }
        };

        _templates.TryAdd(blazorTemplate.Id, blazorTemplate);
        _templates.TryAdd(jsTemplate.Id, jsTemplate);
    }
}
