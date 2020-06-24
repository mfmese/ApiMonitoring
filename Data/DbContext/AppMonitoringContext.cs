using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Entities
{
    public partial class AppMonitoringContext : DbContext
    {
        //public AppMonitoringContext()
        //{
        //}

        public AppMonitoringContext(DbContextOptions<AppMonitoringContext> options)
            : base(options)
        {
        }

        public virtual DbSet<App> App { get; set; }
        public virtual DbSet<Interval> Interval { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<NotificationType> NotificationType { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Table> Table { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Projects\\Homework\\AppMonitoring\\Data\\LocalDatabase\\AppMonitoring.mdf;Integrated Security=True");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<App>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(150);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.App)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_App_Status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.App)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_App_User");
            });

            modelBuilder.Entity<Interval>(entity =>
            {
                entity.Property(e => e.Time).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Interval)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Interval_User");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationValue).HasMaxLength(150);

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_NotificationType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_User");
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.Property(e => e.NotificationTypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__Table__C8EE20630C502FEA");

                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).HasMaxLength(100);
            });
        }
    }
}
