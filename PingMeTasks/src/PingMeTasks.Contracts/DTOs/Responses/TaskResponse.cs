

namespace PingMeTasks.Contracts.DTOs.Responses
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }        
        public RecurrenceDto? Recurrence { get; set; }
    }

    public class RecurrenceDto
    {
        public RecurrenceType Type { get; set; }
        public int Interval { get; set; }
        public List<string>? DaysOfWeek { get; set; }
    }
    
}
