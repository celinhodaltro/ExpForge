using ExpForge.BlazorDemo.Application.Interfaces;
using ExpForge.BlazorDemo.Application.Services;
using ExpForge.BlazorDemo.Domain.Interfaces;
using ExpForge.BlazorDemo.Infrastructure.Repositories;
using ExpForge.BlazorDemo.Infrastructure.Services;

namespace ExpForge.BlazorDemo.Web.Extensions;

/// <summary>
/// Extensões para configuração de serviços
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configura os serviços da aplicação
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Repositórios
        services.AddSingleton<IWidgetRepository, InMemoryWidgetRepository>();
        services.AddSingleton<ITemplateRepository, InMemoryTemplateRepository>();
        
        // Serviços
        services.AddScoped<IWidgetService, WidgetService>();
        services.AddScoped<IExpForgeService, ExpForgeService>();
        
        return services;
    }
}
