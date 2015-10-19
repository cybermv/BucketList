namespace BucketList.Commands
{
    using Core;
    using System;

    public class StatsCommand : IConsoleCommand
    {
        public string Name => "stats";

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
            ConsoleWriter.WriteLine("TODO: add stats", ConsoleColor.Blue);
            return ConsoleCommandResult.Success;
        }
    }
}