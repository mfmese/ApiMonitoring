using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Log
    {
        public int LogId { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Exception { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
