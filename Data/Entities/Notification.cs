using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int NotificationTypeId { get; set; }
        public string NotificationValue { get; set; }
        public int UserId { get; set; }

        public virtual NotificationType NotificationType { get; set; }
        public virtual User User { get; set; }
    }
}
