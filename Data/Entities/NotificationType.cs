using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class NotificationType
    {
        public NotificationType()
        {
            Notification = new HashSet<Notification>();
        }

        public int NotificationTypeId { get; set; }
        public string NotificationTypeName { get; set; }

        public virtual ICollection<Notification> Notification { get; set; }
    }
}
