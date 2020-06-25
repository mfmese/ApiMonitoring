using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Entities
{
    public partial class AppMonitoringContext : BaseDbContext<AppMonitoringContext>
    {
        public AppMonitoringContext(DbContextOptions<AppMonitoringContext> options) : base(options)
        {
        }

    }
}
