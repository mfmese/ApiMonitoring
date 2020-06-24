using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class App
    {
        public int AppId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? StatusId { get; set; }
        public int? UserId { get; set; }

        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}
