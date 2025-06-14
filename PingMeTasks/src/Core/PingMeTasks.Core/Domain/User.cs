using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingMeTasks.Core.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Навигационные свойства
        public List<Category> Categories { get; set; } = new();
        public List<TaskItem> TaskItems { get; set; } = new();

        //public TelegramSettings? TelegramSettings { get; set; }
        //public PushUpSettings? PushUpSettings { get; set; }
    }
}
