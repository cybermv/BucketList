namespace BucketList.Commands
{
    using Core;

    public class HelpCommand : IConsoleCommand
    {
        public string Name => "help";

        public ParameterCollection CheckParameters(string[] parameters)
        {
            return new ParameterCollection();
        }

        public ConsoleCommandResult Execute(ParameterCollection parameterCollection)
        {
            ConsoleWriter.Write(
                @"Mateo Velenik
Ovo je primjer ispisa verbose stringa
 -- ee brate
 -- ma sve radi");
            return ConsoleCommandResult.Success;
        }
    }
}