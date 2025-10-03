using ExpForge.Domain.Enums;

namespace ExpForge.Infrastructure.Services
{
    public interface ITerminalMessageService
    {
        void Write(string message, MessageStatus status = MessageStatus.Default);
        void WriteLine(string message, MessageStatus status = MessageStatus.Default);
        void WriteLines(IEnumerable<string> messages, MessageStatus status = MessageStatus.Default);
    }
}
