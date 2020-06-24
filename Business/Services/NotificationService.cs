using Business.Abstractions;
using Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services
{
    public class NotificationService : INotificationService
    {
        AppMonitoringContext _dbContext;

        private readonly ILogService _logService;

        public NotificationService(AppMonitoringContext dbContext, ILogService logService)
        {
            _dbContext = dbContext;
            _logService = logService;
        }

        public void AddNotification(Notification notification)
        {
            try
            {
                _dbContext.Notification.Add(notification);

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logService.InsertLog(new Data.Entities.Log() { Exception = ex.Message, Request = JsonConvert.SerializeObject(notification) });
            }
           
        }

        public Notification GetNotification()
        {
            try
            {
                User user = UserSevice.CreateUserSingleton();

                return _dbContext.Notification.FirstOrDefault(x => x.UserId == user.UserId);
            }
            catch (Exception ex)
            {
                _logService.InsertLog(new Data.Entities.Log() { Exception = ex.Message, Request = "" });
            }

            return null;
            
        }

        public void UpdateNotification(Notification notification)
        {
            try
            {
                _dbContext.Entry(notification).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logService.InsertLog(new Data.Entities.Log() { Exception = ex.Message, Request = JsonConvert.SerializeObject(notification) });
            }
           
        }
    }
}
