
namespace ExperienceWidget.CLI.Services
{
    public enum MessageStatus
    {
        Default,
        Success,
        Warning,
        Error
    }

    public static class TerminalMessageService
    {
        public static void Write(string message, MessageStatus status = MessageStatus.Default)
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

        public static void WriteLine(string message, MessageStatus status = MessageStatus.Default)
        {
            Write(message + Environment.NewLine, status);
        }

        public static void WriteLines(IEnumerable<string> messages, MessageStatus status = MessageStatus.Default)
        {
            foreach (var message in messages)
            {
                WriteLine(message, status);
            }
        }
    }
}
