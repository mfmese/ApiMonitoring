using Business.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class EmailSenderService : INotificationSender
    {
        public void Send(string to, string subject, string body)
        {
            Console.WriteLine("to: {0} body:{1} subject:{2}", to, subject, body);
        }
    }
}
