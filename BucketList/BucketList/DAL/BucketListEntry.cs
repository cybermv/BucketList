namespace BucketList.DAL
{
    using SQLite;
    using System;

    /// <summary>
    /// The entity representing a row from the SQLite database,
    /// containing all the informations about an entry
    /// </summary>
    public class BucketListEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(256)]
        public string Description { get; set; }

        public EntryDifficulty Difficulty { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? CheckedDate { get; set; }
    }

    public enum EntryDifficulty
    {
        VeryEasy = 1,
        Easy = 2,
        Moderate = 3,
        Hard = 4,
        VeryHard = 5
    }
}