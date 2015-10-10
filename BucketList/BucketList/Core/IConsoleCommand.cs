namespace BucketList.Core
{
    public interface IConsoleCommand
    {
        string Name { get; }

        ParameterCollection CheckParameters(string[] parameters);

        ConsoleCommandResult Execute(ParameterCollection parameterCollection);
    }
}