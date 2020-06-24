using Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class AppCallerResponse
    {
        public StatusEnum status { get; set; }
        public string ResponseMessage { get; set; }
        public bool HasNotification { get; set; }
    }
}
