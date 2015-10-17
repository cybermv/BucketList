namespace BucketList.Commands
{
    using Core;
    using System;
    using System.IO;

    public class PurgeCommand : IConsoleCommand
    {
        private readonly DirectoryInfo _currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        public string Name => "purge";

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
            ConsoleWriter.WriteLine("Purge will remove all entries from the database.", ConsoleColor.Red);
            ConsoleWriter.WriteLine("This operation cannot be undone. Continue? (y/n)", ConsoleColor.Red);
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Y)
            {
                FileInfo databaseFile = new FileInfo($@"{_currentDirectory}\BucketList.db");
                databaseFile.Delete();
                ConsoleWriter.WriteLine("Database deleted.");
            }

            return ConsoleCommandResult.Success;
        }
    }
}