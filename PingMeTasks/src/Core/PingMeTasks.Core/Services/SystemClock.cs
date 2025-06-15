using PingMeTasks.Core.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Core.Services
{
    public class SystemClock : IClock
    {
        public DateTime Now => DateTime.Now;
    }
}
