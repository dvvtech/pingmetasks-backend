using Microsoft.EntityFrameworkCore;
using PingMeTasks.Data.SqlServer.Entities;
using PingMeTasks.Data.SqlServer.Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Data.SqlServer.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<TaskEntity> Tasks { get; set; }

        public DbSet<TaskRecurrenceEntity> TaskRecurrence { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<NotificationEntity> Notifications { get; set; }

        public DbSet<TelegramSettingsEntity> TelegramSettings { get; set; }

        public DbSet<PushUpSettingsEntity> PushUpSettings { get; set; }
    }
}
