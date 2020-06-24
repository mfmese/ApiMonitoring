using Business.Abstractions;
using Cronos;
using Data.Entities;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CronJobService : ICronJobService
    {
        private readonly IAppCallerService _appCallerService;
        private readonly IAppService _appService;
        private readonly INotificationService _notificationService;
        private readonly INotificationSender _notificationSender;
        private readonly IIntervalService _intervalService;
        private readonly ILogService _logService;

        private CronExpression _expression;
        private System.Timers.Timer _timer;


        public CronJobService(IAppCallerService appCallerService, IAppService appService, INotificationService notificationService, INotificationSender notificationSender, IIntervalService intervalService, ILogService logService)
        {
            _appCallerService = appCallerService;
            _appService = appService;
            _notificationService = notificationService;
            _notificationSender = notificationSender;
            _intervalService = intervalService;
            _logService = logService;
        }

        public virtual  void StartAsync(CancellationToken cancellationToken)
        {
            string cronExpression = _intervalService.GetActiveInterval().Time;

            try
            {
                _expression = CronExpression.Parse(cronExpression);
            }
            catch (Exception ex)
            {
                _logService.InsertLog(new Data.Entities.Log() { Exception = ex.Message });
            }

             ScheduleJob(cancellationToken);
        }

        protected virtual  void ScheduleJob(CancellationToken cancellationToken)
        {

            TimeZoneInfo timeZoneInfo = TimeZoneInfo.Local;

            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed +=  (sender, args) =>
                {
                    _timer.Dispose();  // reset and dispose timer
                        _timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        DoWork(cancellationToken);
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                         ScheduleJob(cancellationToken);    // reschedule next
                        }
                };
                _timer.Start();
            }

        }

        public virtual void DoWork(CancellationToken cancellationToken)
        {
            try
            {
                object lockObject = new object();

                lock (lockObject)
                {
                    List<App> apps = _appService.GetAll();

                    foreach (var app in apps)
                    {
                        AppCallerResponse response = _appCallerService.CallApp(app.Url);

                        if (response.HasNotification)
                        {
                            Notification notification = _notificationService.GetNotification();

                            if (notification == null)
                                continue;

                            app.StatusId = (int)response.status;

                            _appService.Update(app);

                            _notificationSender.Send(notification.NotificationValue, "notificaiton subject", "status is " + response.status.ToString() + " responseCode is " + response.ResponseMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logService.InsertLog(new Data.Entities.Log() { Exception = ex.Message });
            }
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
