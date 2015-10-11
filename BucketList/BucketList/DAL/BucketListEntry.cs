namespace BucketList.DAL
{
    using SQLite;
    using System;
    using System.ComponentModel;

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
        [Description("Very easy")]
        VeryEasy = 1,

        [Description("Easy")]
        Easy = 2,

        [Description("Moderate")]
        Moderate = 3,

        [Description("Hard")]
        Hard = 4,

        [Description("Very hard")]
        VeryHard = 5
    }
}