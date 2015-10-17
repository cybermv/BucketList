namespace BucketList.DAL
{
    using SQLite;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Repository used to query, insert and update bucket list entries
    /// from the underlying SQLite database
    /// </summary>
    public class BucketListRepository : IDisposable
    {
        public const string DatabaseName = "BucketList.db";
        private readonly SQLiteConnection _db;

        public BucketListRepository()
        {
            this._db = new SQLiteConnection(DatabaseName, storeDateTimeAsTicks: true);
            this._db.CreateTable<BucketListEntry>();
            this._db.TimeExecution = true;
        }

        public IEnumerable<BucketListEntry> Query => this._db.Table<BucketListEntry>();

        public BucketListEntry Create(string descripion, EntryDifficulty difficulty)
        {
            BucketListEntry entry = new BucketListEntry
            {
                CreatedDate = DateTime.Now,
                Description = descripion,
                Difficulty = difficulty
            };

            int result = this._db.Insert(entry);

            return result > 0 ? entry : null;
        }

        public bool Check(int entryId)
        {
            BucketListEntry foundEntry = this._db.Find<BucketListEntry>(entryId);

            if (foundEntry == null || foundEntry.CheckedDate.HasValue)
            {
                return false;
            }

            foundEntry.CheckedDate = DateTime.Now;
            int updated = this._db.Update(foundEntry);
            return updated > 0;
        }

        public void Dispose()
        {
            this._db.Dispose();
        }
    }
}