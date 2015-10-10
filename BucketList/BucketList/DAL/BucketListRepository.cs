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

            this._db.Insert(new BucketListEntry
            {
                Description = "My entry",
                Difficulty = EntryDifficulty.Moderate,
                CreatedDate = DateTime.Now,
            });
        }

        public IEnumerable<BucketListEntry> Query => this._db.Table<BucketListEntry>();

        public void Dispose()
        {
            this._db.Dispose();
        }
    }
}