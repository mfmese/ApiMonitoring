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

        AppMonitoringSingletonContext _dbSingletonContext;


        public NotificationService(AppMonitoringContext dbContext, AppMonitoringSingletonContext dbSingletonContext, ILogService logService)
        {
            _dbContext = dbContext;
            _dbSingletonContext = dbSingletonContext;
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

                return _dbSingletonContext.Notification.FirstOrDefault(x => x.UserId == user.UserId);
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
