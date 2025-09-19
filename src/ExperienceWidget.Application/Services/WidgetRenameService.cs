using ExperienceWidget.Application.Interfaces;
using ExperienceWidget.CLI.Services;
using System.Text.Json;

namespace ExperienceWidgetCli.Services
{
    public class WidgetRenameService : IWidgetRenameService
    {
        public void Rename(string currentWidgetPath, string newWidgetName)
        {
            if (!Directory.Exists(currentWidgetPath))
            {
                TerminalMessageService.WriteLine($"Erro: O widget '{currentWidgetPath}' não existe.", MessageStatus.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(newWidgetName))
            {
                TerminalMessageService.WriteLine($"Erro: É necessário informar um novo nome para o widget.", MessageStatus.Error);
                return;
            }

            // Caminho do manifest.json
            var manifestPath = Path.Combine(currentWidgetPath, "manifest.json");
            if (!File.Exists(manifestPath))
            {
                TerminalMessageService.WriteLine($"Aviso: Não foi encontrado 'manifest.json' em '{currentWidgetPath}'.", MessageStatus.Warning);
            }
            else
            {
                try
                {
                    string json = File.ReadAllText(manifestPath);
                    using var doc = JsonDocument.Parse(json);
                    var root = doc.RootElement.Clone();

                    var jsonObj = JsonSerializer.Deserialize<JsonElement>(json);
                    var jsonDict = JsonSerializer.Deserialize<Dictionary<string, object>>(json)!;

                    // Alterar o nome do widget no manifest
                    jsonDict["name"] = newWidgetName;

                    var updatedJson = JsonSerializer.Serialize(jsonDict, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(manifestPath, updatedJson);
                }
                catch (Exception ex)
                {
                    TerminalMessageService.WriteLine($"Erro ao atualizar manifest.json: {ex.Message}", MessageStatus.Error);
                    return;
                }
            }

            // Renomear a pasta
            try
            {
                var parentDir = Path.GetDirectoryName(currentWidgetPath)!;
                var newPath = Path.Combine(parentDir, newWidgetName);

                if (Directory.Exists(newPath))
                {
                    TerminalMessageService.WriteLine($"Erro: Já existe uma pasta com o nome '{newWidgetName}'.", MessageStatus.Error);
                    return;
                }

                Directory.Move(currentWidgetPath, newPath);
                TerminalMessageService.WriteLine($"Widget renomeado para '{newWidgetName}' com sucesso!", MessageStatus.Success);
            }
            catch (Exception ex)
            {
                TerminalMessageService.WriteLine($"Erro ao renomear a pasta do widget: {ex.Message}", MessageStatus.Error);
            }
        }
    }
}
