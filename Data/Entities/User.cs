using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class User
    {
        public User()
        {
            App = new HashSet<App>();
            Interval = new HashSet<Interval>();
            Notification = new HashSet<Notification>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<App> App { get; set; }
        public virtual ICollection<Interval> Interval { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
    }
}
