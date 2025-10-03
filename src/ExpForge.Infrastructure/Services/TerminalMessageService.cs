using ExpForge.Application.Interfaces.Services;
using ExpForge.Domain.Enums;

namespace ExpForge.Infrastructure.Services;

public class TerminalMessageService : ITerminalMessageService
{
    public void Write(string message, MessageStatus status = MessageStatus.Default)
    {
        var originalColor = Console.ForegroundColor;

        Console.ForegroundColor = status switch
        {
            MessageStatus.Success => ConsoleColor.Green,
            MessageStatus.Warning => ConsoleColor.Yellow,
            MessageStatus.Error => ConsoleColor.Red,
            _ => originalColor,
        };
        Console.Write(message);
        Console.ForegroundColor = originalColor;
    }

    public  void WriteLine(string message, MessageStatus status = MessageStatus.Default)
    {
        Write(message + Environment.NewLine, status);
    }

    public  void WriteLines(IEnumerable<string> messages, MessageStatus status = MessageStatus.Default)
    {
        foreach (var message in messages)
        {
            WriteLine(message, status);
        }
    }
}
