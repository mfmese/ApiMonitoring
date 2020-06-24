using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Status
    {
        public Status()
        {
            App = new HashSet<App>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<App> App { get; set; }
    }
}
