namespace BucketList.Commands
{
    using Core;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class BackupCommand : IConsoleCommand
    {
        private const string BackupDirectoryName = "backup";
        private const string BackupFileExtension = ".db.backup";
        private readonly DirectoryInfo _backupDirectory;
        private readonly DirectoryInfo _currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        public BackupCommand()
        {
            if (!Directory.Exists(BackupDirectoryName))
            {
                Directory.CreateDirectory(BackupDirectoryName);
            }

            this._backupDirectory = new DirectoryInfo(BackupDirectoryName);
        }

        public string Name => "backup";

        public ParameterCollection CheckParameters(string[] parameters)
        {
            ParameterCollection parameterCollection = new ParameterCollection();
            switch (parameters.Length)
            {
                case 0:
                    parameterCollection.Add("create");
                    return parameterCollection;

                case 1:
                    if (parameters[0] == "show")
                    {
                        parameterCollection.Add("show");
                        return parameterCollection;
                    }
                    break;

                case 2:
                    if (parameters[0] == "restore" &&
                        parameters[1].Length == 15)
                    {
                        parameterCollection.Add("restore");
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
                case "create":
                    CreateBackup();
                    break;

                case "show":
                    PrintAvailableBackups();
                    break;

                case "restore":
                    RestoreBackup(parameterCollection[1]);
                    break;

                default:
                    return ConsoleCommandResult.BadInvoke;
            }
            return ConsoleCommandResult.Success;
        }

        private void CreateBackup()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string backupFilename = $@"{BackupDirectoryName}\{timestamp}{BackupFileExtension}";

            try
            {
                FileInfo createdBackup = new FileInfo($@"{this._currentDirectory}\BucketList.db").CopyTo(backupFilename, true);
                if (createdBackup.Exists)
                {
                    ConsoleWriter.WriteLine("Created backup with name {0}", backupFilename);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            catch
            {
                ConsoleWriter.WriteLine("Could not create backup", ConsoleColor.Red);
            }
        }

        private void PrintAvailableBackups()
        {
            List<FileInfo> backupDatabases = this._backupDirectory
                .GetFiles($"*{BackupFileExtension}")
                .OrderByDescending(f => f.CreationTime)
                .ToList();

            if (backupDatabases.Any())
            {
                ConsoleWriter.WriteLine("Available backups:");
                foreach (FileInfo backupDatabase in backupDatabases)
                {
                    ConsoleWriter.WriteLine(" {0}", backupDatabase.Name);
                }
            }
            else
            {
                ConsoleWriter.WriteLine("No backups available", ConsoleColor.Yellow);
            }
        }

        private void RestoreBackup(string backupName)
        {
            string fileToRestore = $@"{BackupDirectoryName}\{backupName}{BackupFileExtension}";
            FileInfo foundBackup = new FileInfo(fileToRestore);

            if (!foundBackup.Exists)
            {
                ConsoleWriter.WriteLine($"Cannot find backup with name {backupName}", ConsoleColor.Yellow);
                return;
            }

            if (File.Exists($@"{this._currentDirectory}\BucketList.db"))
            {
                ConsoleWriter.WriteLine("Restoring this backup will erase the current database. Continue? (y/n)",
                    ConsoleColor.Red);
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Y)
                {
                    return;
                }
            }

            try
            {
                FileInfo copiedFile = foundBackup.CopyTo($@"{this._currentDirectory}\BucketList.db", true);
                if (!copiedFile.Exists)
                {
                    throw new FileNotFoundException();
                }
                ConsoleWriter.WriteLine("Backup successfully restored");
            }
            catch
            {
                ConsoleWriter.WriteLine($"Could not restore backup {backupName}", ConsoleColor.Red);
            }
        }
    }
}