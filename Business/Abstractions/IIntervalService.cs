
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstractions
{
    public interface IIntervalService
    {
        void Update(Interval interval);

        Interval GetActiveInterval();
    }
}
