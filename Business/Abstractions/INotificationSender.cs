using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstractions
{
    public interface INotificationSender
    {
        void Send(string to, string subject, string body);
    }
}
