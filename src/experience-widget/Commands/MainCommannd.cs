using McMaster.Extensions.CommandLineUtils;
using System;

namespace CLI
{
    [Command(Name = "experience-widget", Description = "CLI Experience Widget Builder")]
    [Subcommand(typeof(CreateCommand))]
    class MainCommannd
    {
        private void OnExecute()
        {
            Console.WriteLine("Use --help command");
        }
    }
}