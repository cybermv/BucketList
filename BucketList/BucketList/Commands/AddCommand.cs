namespace BucketList.Commands
{
    using Core;
    using DAL;
    using System;

    public class AddCommand : IConsoleCommand
    {
        public string Name => "add";

        public ParameterCollection CheckParameters(string[] parameters)
        {
            ParameterCollection parameterCollection = new ParameterCollection();

            switch (parameters.Length)
            {
                case 0:
                    parameterCollection.Add("prompt");
                    return parameterCollection;

                case 2:
                    int difficultyInt;
                    if (parameters[0].StartsWith("\"") && parameters[0].EndsWith("\"") &&
                        int.TryParse(parameters[1], out difficultyInt))
                    {
                        parameterCollection.Add("immediate");
                        parameterCollection.Add(parameters[0].Substring(1, parameters[0].Length - 2));
                        parameterCollection.Add(parameters[1]);
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
                case "prompt":
                    Console.WriteLine("prompting user to insert data for add");
                    return ConsoleCommandResult.Success;

                case "immediate":
                    Console.WriteLine("immediately adding item {0} - diff {1}",
                        parameterCollection[1], (EntryDifficulty)Convert.ToInt32(parameterCollection[2]));
                    return ConsoleCommandResult.Success;
            }

            return ConsoleCommandResult.Exception;
        }
    }
}