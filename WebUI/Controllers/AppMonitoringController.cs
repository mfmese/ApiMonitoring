using Business.Abstractions;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Threading;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AppMonitoringController : Controller
    {
        private readonly IIntervalService intervalService;
        private readonly IAppService applicationService;
        private readonly ICronJobService cronJobService;

        public AppMonitoringController(IIntervalService intervalService, ICronJobService cronJobService, IAppService applicationService)
        {
            this.intervalService = intervalService;
            this.cronJobService = cronJobService;
            this.applicationService = applicationService;
        }

        // GET: AppMonitoring
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            UserSevice.CreateUserSingleton(userId: 1, userName: "Mehmet");

            var model = new AppMonitoringViewModel();

            var interval = intervalService.GetActiveInterval();

            model.Internal = interval;
            model.Applications = applicationService.GetApplications();

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(Interval interval)
        {
            try
            {
                intervalService.Update(interval);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult StartCronJob()
        {
            try
            {
                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken token = source.Token;

                this.cronJobService.StartAsync(token);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult StopCronJob()
        {
            try
            {
                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken token = source.Token;

                this.cronJobService.StopAsync(token);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppMonitoring/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppMonitoring/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}