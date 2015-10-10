namespace BucketList
{
    using DAL;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            BucketListRepository repo = new BucketListRepository();

            List<BucketListEntry> entries = repo.Query.ToList();

            Console.WriteLine("Hello!");
            Console.ReadKey();
        }
    }
}