namespace BucketList.Commands
{
    using Core;
    using DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Util;

    public class GetCommand : IConsoleCommand
    {
        private static readonly string TableTopBorder = "+" + new string('-', 98) + "+";
        private static readonly string TableCaption = "|Id  |Description" + new string(' ', 47) + "|Difficulty|Created    |Checked    |";
        private static readonly string TableEntryTemplate = "|{0,-4}|{1,-58}|{2,-10}|{3,-11}|{4,-11}|";

        private const int PageSize = 10;
        private const int RandomEntriesCount = 5;
        private static readonly Random Rand = new Random();

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
            using (BucketListRepository repo = new BucketListRepository())
            {
                switch (parameterCollection[0])
                {
                    case "all":
                        List<BucketListEntry> allEntries = repo.Query.ToList();
                        PrintEntryList(allEntries);
                        break;

                    case "unchecked":
                        List<BucketListEntry> uncheckedEntries = repo.Query.Where(e => !e.CheckedDate.HasValue).ToList();
                        PrintEntryList(uncheckedEntries);
                        break;

                    case "checked":
                        List<BucketListEntry> checkedEntries = repo.Query.Where(e => e.CheckedDate.HasValue).ToList();
                        PrintEntryList(checkedEntries);
                        break;

                    case "random":
                        List<BucketListEntry> entries = GetRandomEntries(repo);
                        PrintEntryList(entries);
                        break;

                    case "findbyid":
                        int entryId = Convert.ToInt32(parameterCollection[1]);
                        BucketListEntry entry = repo.Query.SingleOrDefault(e => e.Id == entryId);
                        PrintEntryList(new List<BucketListEntry> { entry });
                        break;

                    case "findbydescription":
                        List<BucketListEntry> foundEntries = repo.Query
                            .Where(e => e.Description.Contains(parameterCollection[1]))
                            .ToList();
                        PrintEntryList(foundEntries);
                        break;

                    default:
                        return ConsoleCommandResult.BadInvoke;
                }
                return ConsoleCommandResult.Success;
            }
        }

        private List<BucketListEntry> GetRandomEntries(BucketListRepository repo)
        {
            // TODO replace stupid algorithm
            List<BucketListEntry> list = new List<BucketListEntry>(RandomEntriesCount);
            List<int> ids = new List<int>();

            List<BucketListEntry> entries = repo.Query.Where(e => !e.CheckedDate.HasValue).ToList();
            entries.ForEach(e => ids.AddRange(Enumerable.Repeat(e.Id, (int)e.Difficulty)));

            for (int i = 0; i < RandomEntriesCount; i++)
            {
                int id = ids[Rand.Next(0, ids.Count)];
                list.Add(entries.Single(e => e.Id == id));
            }

            return list;
        }

        private void PrintEntryList(List<BucketListEntry> entries)
        {
            if (!entries.Any())
            {
                ConsoleWriter.WriteLine("No entries found", ConsoleColor.Yellow);
            }

            ConsoleWriter.WriteLine(TableTopBorder);
            ConsoleWriter.WriteLine(TableCaption);

            for (int idx = 0; idx < entries.Count; idx++)
            {
                BucketListEntry entry = entries[idx];

                if (idx > 0 && idx % PageSize == 0)
                {
                    ConsoleWriter.Write("{0} more entries; Press any key to continue or X to stop",
                        ConsoleColor.Green,
                        entries.Count - idx);
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    ConsoleWriter.ClearCurrentConsoleLine();
                    if (key.Key == ConsoleKey.X) { break; }
                }

                ConsoleWriter.WriteLine(TableEntryTemplate,
                    entry.Id,
                    entry.Description,
                    entry.Difficulty.ToDisplayString(),
                    entry.CreatedDate.ToString("d"),
                    entry.CheckedDate?.ToString("d"));
            }

            ConsoleWriter.WriteLine(TableTopBorder);
        }
    }
}