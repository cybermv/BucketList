namespace BucketList.Commands
{
    using Core;

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
            ConsoleWriter.WriteLine("printing all stats for bucket list entries");
            return ConsoleCommandResult.Success;
        }
    }
}