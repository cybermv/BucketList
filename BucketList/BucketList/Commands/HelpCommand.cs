namespace BucketList.Commands
{
    using Core;
    using System.Collections.Generic;
    using System.Linq;

    public class HelpCommand : IConsoleCommand
    {
        private static readonly Dictionary<string, string> CommandsHelp = new Dictionary<string, string>
        {
            {"add", @"Usages for 'add' command:
    >> add
        - prompts user for input
    >> add ""my bucket list entry"" 3
        - adds an entry immediately
"},
            {"get", @"Usages for 'get' command:
    >> get / get unchecked
        - gets all unchecked entries
    >> get checked
        - gets all the checked entries
    >> get all
        - gets all entries
    >> get ""keyword""
        - gets all entries whose Description contains given keyword
    >> get 42
        - gets the entry with the given Id
"},
            {"check", @"Usages for 'check' command:
    >> check 42
        -- checks the entry with the given Id"},
            {"stats", @"Usages for the 'stats' command:
    >> stats
        -- display various statistics for checked and unchecked entries"},
            {"backup", @"Usages for the 'backup' command:
    >> backup
        -- creates a backup of the current database
    >> backup show
        - lists all available backups, sorted from newest to oldest
    >> backup restore 20151017-153522
        - restores the given backup by replacing the current database"},
            {"purge", @"Usages for 'purge' command:
    >> purge
        -- deletes the current database"},
            {"exit", @"Usages for 'exit' command:
    >> exit
        -- closes the program"}
        };

        public string Name => "help";

        public ParameterCollection CheckParameters(string[] parameters)
        {
            ParameterCollection parameterCollection = new ParameterCollection();

            switch (parameters.Length)
            {
                case 0:
                    parameterCollection.Add("summary");
                    return parameterCollection;

                case 1:
                    if (CommandsHelp.Keys.Contains(parameters[0].ToLowerInvariant()))
                    {
                        parameterCollection.Add("command");
                        parameterCollection.Add(parameters[0].ToLowerInvariant());
                        return parameterCollection;
                    }
                    break;
            }

            return null;
        }

        public ConsoleCommandResult Execute(ParameterCollection parameterCollection)
        {
            switch (parameterCollection[0])
            {
                case "summary":
                    ConsoleWriter.WriteLine("BucketList is a console-based bucket list tracker.");
                    ConsoleWriter.WriteLine("Manage your bucket list using these commands:");
                    foreach (string commandName in CommandsHelp.Keys)
                    {
                        ConsoleWriter.WriteLine($"    >> {commandName}");
                    }
                    ConsoleWriter.WriteLine("Use 'help <commandName>' to learn more about the command.");
                    return ConsoleCommandResult.Success;

                case "command":
                    string helpMessage = CommandsHelp[parameterCollection[1]];
                    ConsoleWriter.WriteLine(helpMessage);
                    return ConsoleCommandResult.Success;
            }
            return ConsoleCommandResult.Exception;
        }
    }
}