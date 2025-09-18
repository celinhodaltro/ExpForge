using McMaster.Extensions.CommandLineUtils;
using System;

namespace ExperienceWidget.CLI.Actions;

[Command(Name = "experience-widget", Description = "CLI Experience Widget Builder")]
[Subcommand(typeof(CreateAction))]
public class MainAction
{
    private void OnExecute()
    {
        Console.WriteLine("Use --help command");
    }
}