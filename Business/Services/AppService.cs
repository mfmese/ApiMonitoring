using Business.Abstractions;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Enums;
using Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AppService : IAppService
    {
        private AppMonitoringContext _dbContext;

        private readonly ILogService _logService;

        AppMonitoringSingletonContext _dbSingletonContext;


        public AppService(AppMonitoringContext dbContext, AppMonitoringSingletonContext dbSingletonContext, ILogService logService)
        {
            _dbContext = dbContext;
            _dbSingletonContext = dbSingletonContext;
            _logService = logService;
        }

        public void Create(App app)
        {
            _dbContext.App.Add(app);
            _dbContext.SaveChanges();
        }

        public  List<App> GetAll()
        {          

            try
            {
                User user = UserSevice.CreateUserSingleton();

                return _dbSingletonContext.App.Where(x => x.UserId == user.UserId).ToList();
            }
            catch (Exception ex)
            {
                _logService.InsertLog(new Data.Entities.Log() { Exception = ex.Message, Request = "" });
            }

            return null;
        }

        public List<Applications> GetApplications()
        {
            try
            {
                var apps = GetAll();

                var applicaitons = new List<Applications>();

                foreach (var app in apps)
                {
                    var application = new Applications()
                    {
                        ApplicationName = app.Name,
                        Status = (StatusEnum)app.StatusId,
                        Url = app.Url
                    };
                    applicaitons.Add(application);
                }

                return applicaitons;
            }
            catch (Exception ex)
            {
                _logService.InsertLog(new Data.Entities.Log() { Exception = ex.Message, Request = "" });
            }

            return null;
        }

        public void Update(App app)
        {
            try
            {
                _dbSingletonContext.Entry(app).State = EntityState.Modified;

                _dbSingletonContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logService.InsertLog(new Data.Entities.Log() { Exception = ex.Message, Request = JsonConvert.SerializeObject(app) });
            }
        }

    }
}

