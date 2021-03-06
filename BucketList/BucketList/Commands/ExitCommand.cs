﻿namespace BucketList.Commands
{
    using Core;

    public class ExitCommand : IConsoleCommand
    {
        public string Name => "exit";

        public ParameterCollection CheckParameters(string[] parameters)
        {
            if (parameters.Length == 0)
            {
                return new ParameterCollection();
            }

            return null;
        }

        public ConsoleCommandResult Execute(ParameterCollection parameterCollection)
        {
            return ConsoleCommandResult.Terminate;
        }
    }
}