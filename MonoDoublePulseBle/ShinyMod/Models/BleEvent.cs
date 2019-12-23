using System;
using SQLite;


namespace ShinyMod.Models
{
    public class BleEvent
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
