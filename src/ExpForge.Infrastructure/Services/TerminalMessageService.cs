using ExpForge.Application.Interfaces.Services;
using ExpForge.Domain.Enums;

namespace ExpForge.Infrastructure.Services;

public class TerminalMessageService : ITerminalMessageService
{
    public void Write(string message, MessageStatus status = MessageStatus.Default)
    {
        var originalColor = Console.ForegroundColor;

        switch (status)
        {
            case MessageStatus.Success:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case MessageStatus.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case MessageStatus.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            default:
                Console.ForegroundColor = originalColor;
                break;
        }

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
