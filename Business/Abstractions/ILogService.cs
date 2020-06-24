using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstractions
{
    public interface ILogService
    {
        void InsertLog(Log log);
    }
}
