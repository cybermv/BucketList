namespace BucketList.Commands
{
    using Core;
    using DAL;
    using System;

    public class CheckCommand : IConsoleCommand
    {
        public string Name => "check";

        public ParameterCollection CheckParameters(string[] parameters)
        {
            ParameterCollection parameterCollection = new ParameterCollection();
            switch (parameters.Length)
            {
                case 1:
                    int id;
                    if (int.TryParse(parameters[0], out id))
                    {
                        parameterCollection.Add("check");
                        parameterCollection.Add(parameters[0]);
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
                case "check":
                    int id = Convert.ToInt32(parameterCollection[1]);
                    BucketListRepository repo = new BucketListRepository();
                    bool isChecked = repo.Check(id);
                    if (isChecked)
                    {
                        ConsoleWriter.WriteLine($"Checked entry with Id {id}", ConsoleColor.Green);
                    }
                    else
                    {
                        ConsoleWriter.WriteLine($"Could not check entry with Id {id}", ConsoleColor.Yellow);
                    }
                    break;

                default:
                    return ConsoleCommandResult.BadInvoke;
            }
            return ConsoleCommandResult.Success;
        }
    }
}