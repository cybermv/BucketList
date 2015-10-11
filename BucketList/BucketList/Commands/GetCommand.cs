namespace BucketList.Commands
{
    using Core;
    using DAL;
    using System;
    using System.Collections.Generic;
    using Util;

    public class GetCommand : IConsoleCommand
    {
        private static readonly string TableTopBorder = "+" + new string('-', 98) + "+";

        private static readonly string TableCaption = "|Id  |Description" + new string(' ', 47) + "|Difficulty|Created    |Checked    |";
        private static readonly string TableEntryTemplate = "|{0,-4}|{1,-58}|{2,-10}|{3,-11}|{4,-11}|";

        public string Name => "get";

        public ParameterCollection CheckParameters(string[] parameters)
        {
            ParameterCollection parameterCollection = new ParameterCollection();
            switch (parameters.Length)
            {
                case 0:
                    parameterCollection.Add("unchecked");
                    return parameterCollection;

                case 1:
                    if (parameters[0] == "all" ||
                        parameters[0] == "unchecked" ||
                        parameters[0] == "checked" ||
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
            BucketListRepository repo = new BucketListRepository();

            switch (parameterCollection[0])
            {
                case "all":
                    Console.WriteLine("getting all entries");
                    return PrintEntryList(repo.Query);

                case "unchecked":
                    Console.WriteLine("getting all unchecked entries");
                    return ConsoleCommandResult.Success;

                case "checked":
                    Console.WriteLine("getting all checked");
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

        private ConsoleCommandResult PrintEntryList(IEnumerable<BucketListEntry> entries)
        {
            ConsoleWriter.WriteLine(TableTopBorder);
            ConsoleWriter.WriteLine(TableCaption);

            foreach (BucketListEntry entry in entries)
            {
                ConsoleWriter.WriteLine(TableEntryTemplate,
                    entry.Id,
                    entry.Description,
                    entry.Difficulty.ToDisplayString(),
                    entry.CreatedDate.ToString("d"),
                    entry.CheckedDate?.ToString("d"));
            }

            ConsoleWriter.WriteLine(TableTopBorder);

            return ConsoleCommandResult.Success;
        }
    }
}