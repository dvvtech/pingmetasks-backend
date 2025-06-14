using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Contracts.DTOs.Requests
{
    public class CreateRecurrenceRequest
    {
        public RecurrenceType Type { get; set; }
        public int Interval { get; set; } = 1;
        public List<string>? DaysOfWeek { get; set; }
        public int? DayOfMonth { get; set; }
        public DateTime? EndDate { get; set; }
    }    
}
