using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Interval
    {
        public int IntervalId { get; set; }
        public string Time { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
