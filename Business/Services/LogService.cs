using Business.Abstractions;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class LogService : ILogService
    {
        AppMonitoringContext _dbContext;

        public LogService(AppMonitoringContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InsertLog(Log log)
        {
            _dbContext.Log.Add(log);
            _dbContext.SaveChanges();
        }
    }
}
