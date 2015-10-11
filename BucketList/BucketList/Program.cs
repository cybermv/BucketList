namespace BucketList
{
    using Commands;
    using Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private static readonly IEnumerable<IConsoleCommand> Commands = new IConsoleCommand[]
        {
            new ExitCommand(),
            new HelpCommand(),
            new GetCommand(),
            new AddCommand(),
            new StatsCommand()
        };

        public static void Main(string[] args)
        {
            ConsoleWriter.WriteLine("BucketList - your own bucket list manager");

            while (true)
            {
                ConsoleWriter.Write(">> ");
                string userInput = Console.ReadLine();

                ConsoleCommandResult result = ExecuteCommand(userInput);

                switch (result)
                {
                    case ConsoleCommandResult.Success:
                        break;

                    case ConsoleCommandResult.NotFound:
                        ConsoleWriter.WriteLine("Command not found", ConsoleColor.Yellow);
                        break;

                    case ConsoleCommandResult.BadInvoke:
                        ConsoleWriter.WriteLine("Called command with bad parameters", ConsoleColor.Yellow);
                        break;

                    case ConsoleCommandResult.Exception:
                        ConsoleWriter.WriteLine("Command failed with exception", ConsoleColor.Red);
                        break;

                    case ConsoleCommandResult.Terminate:
                        ConsoleWriter.WriteLine("Command terminating application", ConsoleColor.DarkRed);
                        return;
                }
            }
        }

        private static ConsoleCommandResult ExecuteCommand(string userInput)
        {
            userInput = userInput.Trim();

            string commandName = userInput;
            string[] parameters = new string[0];
            int firstSpace = userInput.IndexOf(' ');

            if (firstSpace > 0)
            {
                commandName = userInput.Substring(0, firstSpace).Trim();
                parameters = userInput
                    .Substring(firstSpace + 1)
                    .SafeSplit(' ')
                    .Select(p => p.Trim())
                    .ToArray();
            }

            foreach (IConsoleCommand command in Commands)
            {
                if (command.Name == commandName)
                {
                    ParameterCollection parameterCollection = command.CheckParameters(parameters);
                    if (parameterCollection != null)
                    {
                        try
                        {
                            return command.Execute(parameterCollection);
                        }
                        catch (Exception)
                        {
                            return ConsoleCommandResult.Exception;
                        }
                    }
                    else
                    {
                        return ConsoleCommandResult.BadInvoke;
                    }
                }
            }

            return ConsoleCommandResult.NotFound;
        }
    }
}