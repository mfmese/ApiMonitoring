using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class AppMonitoringViewModel
    {
        public Interval Internal { get; set; }
        public List<Applications> Applications { get; set; }
    }
}
