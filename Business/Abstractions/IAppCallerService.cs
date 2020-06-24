using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstractions
{
    public interface IAppCallerService
    {
        AppCallerResponse CallApp(string url);
    }
}
