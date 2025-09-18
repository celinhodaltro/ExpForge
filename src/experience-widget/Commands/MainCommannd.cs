using McMaster.Extensions.CommandLineUtils;
using System;

namespace CLI
{
    [Command(Name = "experience-builder", Description = "CLI Experience Builder")]
    [Subcommand(typeof(CreateCommand))]
    class MainCommannd
    {
        private void OnExecute()
        {
            Console.WriteLine("Use um comando: create");
        }
    }
}