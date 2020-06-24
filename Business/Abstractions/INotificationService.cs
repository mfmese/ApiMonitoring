using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstractions
{
    public interface INotificationService
    {
        void AddNotification(Notification notification);

        void UpdateNotification(Notification notification);

        Notification GetNotification();

    }
}
