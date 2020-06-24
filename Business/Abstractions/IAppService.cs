using Data.Entities;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstractions
{
    public interface IAppService
    {
        List<App> GetAll();
        List<Applications> GetApplications();
        void Update(App app);
        void Create(App app);        
    }
}
