namespace BucketList.Commands
{
    using Core;
    using System;
    using System.Runtime.Remoting.Messaging;

    public class GetCommand : IConsoleCommand
    {
        public string Name => "get";

        public ParameterCollection CheckParameters(string[] parameters)
        {
            ParameterCollection parameterCollection = new ParameterCollection();
            switch (parameters.Length)
            {
                case 0:
                    parameterCollection.Add("all");
                    return parameterCollection;

                case 1:
                    if (parameters[0] == "all" ||
                        parameters[0] == "random")
                    {
                        parameterCollection.Add(parameters[0]);
                        return parameterCollection;
                    }

                    int id;
                    if (int.TryParse(parameters[0], out id))
                    {
                        parameterCollection.Add("findbyid");
                        parameterCollection.Add(parameters[0]);
                        return parameterCollection;
                    }

                    if (parameters[0].StartsWith("\"") && parameters[0].EndsWith("\""))
                    {
                        parameterCollection.Add("findbydescription");
                        parameterCollection.Add(parameters[0].Substring(1, parameters[0].Length - 2));
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
                case "all":
                    Console.WriteLine("getting all entries");
                    return ConsoleCommandResult.Success;

                case "random":
                    Console.WriteLine("getting 5 random entries");
                    return ConsoleCommandResult.Success;

                case "findbyid":
                    Console.WriteLine("getting entry with id= {0}", Convert.ToInt32(parameterCollection[1]));
                    return ConsoleCommandResult.Success;

                case "findbydescription":
                    Console.WriteLine("searching for entries containing= {0}", parameterCollection[1]);
                    return ConsoleCommandResult.Success;
            }
            return ConsoleCommandResult.BadInvoke;
        }
    }
}