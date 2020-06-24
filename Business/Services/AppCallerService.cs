using Business.Abstractions;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Business.Services
{
    public class AppCallerService : IAppCallerService
    {
        private readonly ILogService _logService;

        public AppCallerService(ILogService logService)
        {
            _logService = logService;
        }

        public AppCallerResponse CallApp(string url)
        {
            AppCallerResponse response = new AppCallerResponse();

            try
            {   
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    var responseService = client.GetAsync(client.BaseAddress);

                    responseService.Wait();

                    var result = responseService.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        result.Content.ReadAsStringAsync();

                        int responseCode = (int)result.StatusCode;

                        if (responseCode > 199 && responseCode < 299)
                        {
                            response.HasNotification = true;
                            response.status = Model.Enums.StatusEnum.UP;
                            response.ResponseMessage = responseCode.ToString();
                        }
                    }
                    else
                    {
                        response.HasNotification = true;
                        response.status = Model.Enums.StatusEnum.DOWN;
                    }
                }

            }
            catch (Exception ex)
            {
                _logService.InsertLog(new Data.Entities.Log() { Exception = ex.Message, Request = url});
            }

            return response;
        }  
    }
}
