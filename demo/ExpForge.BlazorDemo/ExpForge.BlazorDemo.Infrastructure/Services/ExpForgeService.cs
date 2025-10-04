using System.Diagnostics;
using System.Text.Json;
using ExpForge.BlazorDemo.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ExpForge.BlazorDemo.Infrastructure.Services;

/// <summary>
/// Implementação do serviço para integração com ExpForge CLI
/// </summary>
public class ExpForgeService : IExpForgeService
{
    private readonly ILogger<ExpForgeService> _logger;
    private const string ExpForgeCommand = "expforge";

    public ExpForgeService(ILogger<ExpForgeService> logger)
    {
        _logger = logger;
    }

    public async Task<string> GenerateWidgetAsync(string templateName, string widgetName, Dictionary<string, object> parameters)
    {
        try
        {
            var arguments = new List<string>
            {
                "widget",
                "create",
                "--template", templateName,
                "--name", widgetName
            };

            // Adicionar parâmetros como argumentos
            foreach (var param in parameters)
            {
                arguments.Add($"--{param.Key}");
                arguments.Add(param.Value.ToString() ?? string.Empty);
            }

            var result = await ExecuteExpForgeCommandAsync(arguments.ToArray());
            _logger.LogInformation("Widget {WidgetName} gerado com sucesso usando template {TemplateName}", widgetName, templateName);
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao gerar widget {WidgetName} com template {TemplateName}", widgetName, templateName);
            throw;
        }
    }

    public async Task<IEnumerable<string>> GetAvailableTemplatesAsync()
    {
        try
        {
            var result = await ExecuteExpForgeCommandAsync(new[] { "template", "list" });
            
            // Parse do resultado para extrair lista de templates
            var templates = result.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                                 .Where(line => !string.IsNullOrWhiteSpace(line))
                                 .Select(line => line.Trim())
                                 .ToList();

            _logger.LogInformation("Encontrados {Count} templates disponíveis", templates.Count);
            return templates;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter lista de templates disponíveis");
            return Enumerable.Empty<string>();
        }
    }

    public async Task<bool> ValidateTemplateAsync(string templateName)
    {
        try
        {
            var availableTemplates = await GetAvailableTemplatesAsync();
            var isValid = availableTemplates.Contains(templateName, StringComparer.OrdinalIgnoreCase);
            
            _logger.LogInformation("Template {TemplateName} é válido: {IsValid}", templateName, isValid);
            return isValid;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao validar template {TemplateName}", templateName);
            return false;
        }
    }

    public async Task<string> ExecuteCommandAsync(string command, string[] arguments)
    {
        try
        {
            var allArgs = new[] { command }.Concat(arguments).ToArray();
            var result = await ExecuteExpForgeCommandAsync(allArgs);
            
            _logger.LogInformation("Comando {Command} executado com sucesso", command);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao executar comando {Command}", command);
            throw;
        }
    }

    public async Task<string> GetVersionAsync()
    {
        try
        {
            var result = await ExecuteExpForgeCommandAsync(new[] { "--version" });
            _logger.LogInformation("Versão do ExpForge obtida: {Version}", result.Trim());
            
            return result.Trim();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter versão do ExpForge");
            return "Versão não disponível";
        }
    }

    private async Task<string> ExecuteExpForgeCommandAsync(string[] arguments)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = ExpForgeCommand,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        foreach (var arg in arguments)
        {
            processStartInfo.ArgumentList.Add(arg);
        }

        using var process = new Process { StartInfo = processStartInfo };
        
        process.Start();
        
        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();
        
        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException($"ExpForge command failed with exit code {process.ExitCode}. Error: {error}");
        }

        return output;
    }
}
