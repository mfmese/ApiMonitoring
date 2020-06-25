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
        AppMonitoringSingletonContext _dbSingletonContext;

        public LogService(AppMonitoringContext dbContext, AppMonitoringSingletonContext dbSingletonContext)
        {
            _dbContext = dbContext;
            _dbSingletonContext = dbSingletonContext;
        }

        public void InsertLog(Log log)
        {
            try
            {
                _dbContext.Log.Add(log);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _dbSingletonContext.Log.Add(log);             
                var result = _dbSingletonContext.SaveChangesAsync();
            }
            
        }
    }
}
