using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    public class AppMonitoringSingletonContext: BaseDbContext<AppMonitoringSingletonContext>
    {

        public AppMonitoringSingletonContext(DbContextOptions<AppMonitoringSingletonContext> options) : base(options)
        {

        }

        //private static AppMonitoringSingletonContext _context;

        ////private AppMonitoringSingletonContext(): base(null)
        ////{

        ////}

        //public AppMonitoringSingletonContext(DbContextOptions<AppMonitoringSingletonContext> options) : base(options)
        //{

        //}       

        //public static AppMonitoringSingletonContext CreateSingleton()
        //{
        //    object lockObject = new object();

        //    lock (lockObject)
        //    {
        //        if (_context == null)
        //        {
        //            _context = new AppMonitoringSingletonContext(null);
        //        }

        //        return _context;
        //    }
        //}

        //public static AppMonitoringSingletonContext CreateSingleton(DbContextOptions<AppMonitoringSingletonContext> options)
        //{
        //    object lockObject = new object();

        //    lock (lockObject)
        //    {
        //        if (_context == null)
        //        {
        //            _context = new AppMonitoringSingletonContext(options);
        //        }

        //        return _context;
        //    }
        //}
    }
}
