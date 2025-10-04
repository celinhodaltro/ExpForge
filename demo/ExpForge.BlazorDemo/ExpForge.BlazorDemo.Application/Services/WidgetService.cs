using ExpForge.BlazorDemo.Application.DTOs;
using ExpForge.BlazorDemo.Application.Interfaces;
using ExpForge.BlazorDemo.Domain.Entities;
using ExpForge.BlazorDemo.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ExpForge.BlazorDemo.Application.Services;

/// <summary>
/// Implementação do serviço de Widget
/// </summary>
public class WidgetService : IWidgetService
{
    private readonly IWidgetRepository _widgetRepository;
    private readonly ITemplateRepository _templateRepository;
    private readonly IExpForgeService _expForgeService;
    private readonly ILogger<WidgetService> _logger;

    public WidgetService(
        IWidgetRepository widgetRepository,
        ITemplateRepository templateRepository,
        IExpForgeService expForgeService,
        ILogger<WidgetService> logger)
    {
        _widgetRepository = widgetRepository;
        _templateRepository = templateRepository;
        _expForgeService = expForgeService;
        _logger = logger;
    }

    public async Task<WidgetDto?> GetByIdAsync(Guid id)
    {
        var widget = await _widgetRepository.GetByIdAsync(id);
        return widget != null ? MapToDto(widget) : null;
    }

    public async Task<IEnumerable<WidgetDto>> GetAllAsync()
    {
        var widgets = await _widgetRepository.GetAllAsync();
        return widgets.Select(MapToDto);
    }

    public async Task<IEnumerable<WidgetDto>> GetByTagsAsync(IEnumerable<string> tags)
    {
        var widgets = await _widgetRepository.GetByTagsAsync(tags);
        return widgets.Select(MapToDto);
    }

    public async Task<WidgetDto> CreateAsync(CreateWidgetDto createWidgetDto)
    {
        try
        {
            // Verificar se o template existe
            var template = await _templateRepository.GetByIdAsync(createWidgetDto.TemplateId);
            if (template == null)
            {
                throw new ArgumentException($"Template com ID {createWidgetDto.TemplateId} não encontrado");
            }

            var widget = new Widget
            {
                Name = createWidgetDto.Name,
                Description = createWidgetDto.Description,
                TemplatePath = $"templates/{template.Name.ToLowerInvariant().Replace(" ", "-")}",
                Configuration = createWidgetDto.Configuration,
                Tags = createWidgetDto.Tags
            };

            var createdWidget = await _widgetRepository.CreateAsync(widget);
            
            _logger.LogInformation("Widget {WidgetName} criado com sucesso", createdWidget.Name);
            
            return MapToDto(createdWidget);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar widget {WidgetName}", createWidgetDto.Name);
            throw;
        }
    }

    public async Task<WidgetDto> UpdateAsync(Guid id, UpdateWidgetDto updateWidgetDto)
    {
        try
        {
            var existingWidget = await _widgetRepository.GetByIdAsync(id);
            if (existingWidget == null)
            {
                throw new ArgumentException($"Widget com ID {id} não encontrado");
            }

            existingWidget.Name = updateWidgetDto.Name;
            existingWidget.Description = updateWidgetDto.Description;
            existingWidget.Configuration = updateWidgetDto.Configuration;
            existingWidget.Tags = updateWidgetDto.Tags;
            existingWidget.IsActive = updateWidgetDto.IsActive;

            var updatedWidget = await _widgetRepository.UpdateAsync(existingWidget);
            
            _logger.LogInformation("Widget {WidgetId} atualizado com sucesso", id);
            
            return MapToDto(updatedWidget);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar widget {WidgetId}", id);
            throw;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var exists = await _widgetRepository.ExistsAsync(id);
            if (!exists)
            {
                throw new ArgumentException($"Widget com ID {id} não encontrado");
            }

            await _widgetRepository.DeleteAsync(id);
            
            _logger.LogInformation("Widget {WidgetId} excluído com sucesso", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir widget {WidgetId}", id);
            throw;
        }
    }

    public async Task<string> GenerateWidgetCodeAsync(Guid widgetId)
    {
        try
        {
            var widget = await _widgetRepository.GetByIdAsync(widgetId);
            if (widget == null)
            {
                throw new ArgumentException($"Widget com ID {widgetId} não encontrado");
            }

            // Simular geração de código usando ExpForge
            var parameters = new Dictionary<string, object>
            {
                { "name", widget.Name },
                { "description", widget.Description }
            };

            // Para demonstração, retornar código simulado
            var generatedCode = $@"// Código gerado para o widget: {widget.Name}
// Descrição: {widget.Description}
// Configuração: {widget.Configuration}

@page ""/{widget.Name.ToLowerInvariant()}""

<h3>{widget.Name}</h3>
<p>{widget.Description}</p>

@code {{
    // Código do componente aqui
}}";

            _logger.LogInformation("Código gerado para widget {WidgetId}", widgetId);
            
            return generatedCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao gerar código para widget {WidgetId}", widgetId);
            throw;
        }
    }

    private static WidgetDto MapToDto(Widget widget)
    {
        return new WidgetDto
        {
            Id = widget.Id,
            Name = widget.Name,
            Description = widget.Description,
            TemplatePath = widget.TemplatePath,
            Version = widget.Version,
            CreatedAt = widget.CreatedAt,
            UpdatedAt = widget.UpdatedAt,
            IsActive = widget.IsActive,
            Configuration = widget.Configuration,
            Tags = widget.Tags
        };
    }
}
