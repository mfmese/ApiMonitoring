using Business.Abstractions;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services
{
    public class IntervalService : IIntervalService
    {
        AppMonitoringContext _dbContext;

        private readonly ILogService _logService;

        public IntervalService(AppMonitoringContext dbContext, ILogService logService)
        {
            _dbContext = dbContext;
            _logService = logService;
        }

        public Model.Models.Interval GetActiveInterval()
        {
            try
            {
                User user = UserSevice.CreateUserSingleton();

                var interval = _dbContext.Interval.FirstOrDefault(x => x.UserId == user.UserId);

                if (interval == null)
                    return null;

                return new Model.Models.Interval()
                {
                    Time = interval.Time,
                    IntervalId = interval.IntervalId,
                    UserId = interval.UserId
                };
            }
            catch (Exception ex)
            {
                _logService.InsertLog(new Data.Entities.Log() { Exception = ex.Message, Request = "" });
            }

            return null;
        }

        public void Update(Model.Models.Interval interval)
        {

            try
            {
                var intervalEntity = new Interval()
                {
                    Time = interval.Time,
                    IntervalId = interval.IntervalId,
                    UserId = interval.UserId
                };

                _dbContext.Entry(intervalEntity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logService.InsertLog(new Data.Entities.Log() { Exception = ex.Message, Request = JsonConvert.SerializeObject(interval) });
            }
            
        }
    }
}
