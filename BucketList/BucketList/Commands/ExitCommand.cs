namespace BucketList.Commands
{
    using Core;

    public class ExitCommand : IConsoleCommand
    {
        public string Name => "exit";

        public ParameterCollection CheckParameters(string[] parameters)
        {
            return new ParameterCollection();
        }

        public ConsoleCommandResult Execute(ParameterCollection parameterCollection)
        {
            return ConsoleCommandResult.Terminate;
        }
    }
}