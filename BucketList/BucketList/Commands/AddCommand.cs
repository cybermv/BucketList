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
                        int.TryParse(parameters[1], out difficultyInt) &&
                        Enum.IsDefined(typeof(EntryDifficulty), difficultyInt))
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
                    return this.PromptInput();

                case "immediate":
                    return this.CreateEntry(
                        parameterCollection[1],
                        (EntryDifficulty)Convert.ToInt32(parameterCollection[2]));
            }

            return ConsoleCommandResult.Exception;
        }

        private ConsoleCommandResult PromptInput()
        {
            ConsoleWriter.Write("Entry description: ");
            string description = Console.ReadLine();

            ConsoleWriter.Write("Difficulty (1-5): ");
            string difficulty = Console.ReadLine();

            int difficultyInt;
            if (!string.IsNullOrWhiteSpace(description) &&
                int.TryParse(difficulty, out difficultyInt) &&
                Enum.IsDefined(typeof(EntryDifficulty), difficultyInt))
            {
                return this.CreateEntry(description, (EntryDifficulty)difficultyInt);
            }
            else
            {
                ConsoleWriter.WriteLine("Cannot add entry; see help for the command", ConsoleColor.Yellow);
                return ConsoleCommandResult.BadInvoke;
            }
        }

        private ConsoleCommandResult CreateEntry(string description, EntryDifficulty difficulty)
        {
            using (BucketListRepository repo = new BucketListRepository())
            {
                BucketListEntry entry = repo.Create(description, difficulty);

                if (entry != null)
                {
                    ConsoleWriter.WriteLine("Added entry with Id {0}", ConsoleColor.Green, entry.Id);
                    return ConsoleCommandResult.Success;
                }
                else
                {
                    ConsoleWriter.WriteLine("Error occurred while saving entry", ConsoleColor.Red);
                    return ConsoleCommandResult.Exception;
                }
            }
        }
    }
}