using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Contracts.DTOs.Requests
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int? CategoryId { get; set; }
        
        public CreateRecurrenceRequest? Recurrence { get; set; }
    }
}
