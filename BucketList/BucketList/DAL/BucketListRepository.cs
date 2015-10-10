namespace BucketList.DAL
{
    using SQLite;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Repository used to query, insert and update bucket list entries
    /// from the underlying SQLite database
    /// </summary>
    public class BucketListRepository
    {
        private SQLiteConnection _db;

        public BucketListRepository()
        {
            this._db = new SQLiteConnection("BucketList.db");
            this._db.CreateTable<BucketListEntry>();

            this._db.Insert(new BucketListEntry
            {
                Description = "My entry",
                Difficulty = EntryDifficulty.Moderate,
                CreatedDate = DateTime.Now,
            });
        }

        public IEnumerable<BucketListEntry> Query
        {
            get { return this._db.Table<BucketListEntry>(); }
        }
    }
}