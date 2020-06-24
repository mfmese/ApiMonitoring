using Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class Applications
    {
        public string ApplicationName { get; set; }
        public string Url { get; set; }
        public StatusEnum Status { get; set; }
    }
}
