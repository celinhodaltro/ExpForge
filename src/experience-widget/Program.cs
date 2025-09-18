using CLI;
using McMaster.Extensions.CommandLineUtils;

class Program
{
    static int Main(string[] args)
    {
        return CommandLineApplication.Execute<MainAction>(args);
    }
}