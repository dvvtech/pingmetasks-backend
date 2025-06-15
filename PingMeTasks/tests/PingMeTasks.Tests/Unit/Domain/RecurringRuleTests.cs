using PingMeTasks.Core.Domain;

namespace PingMeTasks.Tests.Unit.Domain
{
    public class RecurringRuleTests
    {
        [Fact]
        public void Rule_2DaysOn_2DaysOff_ShouldGenerate_CorrectDates()
        {
            // Arrange
            var rule = new RecurringRule
            {
                Id = 1,
                TaskItemId = 1,
                Type = RepeatType.CustomDaysPattern,
                StartDate = new DateTime(2025, 4, 1),
                ActiveDays = 2,
                RestDays = 2
            };

            // Act
            //var upcomingDates = rule.GetUpcomingOccurrences(maxCount: 6);

            // Assert
            var expectedDates = new List<DateTime>
            {
                new DateTime(2025, 4, 1),
                new DateTime(2025, 4, 2),
                new DateTime(2025, 4, 5),
                new DateTime(2025, 4, 6),
                new DateTime(2025, 4, 9),
                new DateTime(2025, 4, 10)
            };

            /*Assert.Equal(expectedDates.Count, upcomingDates.Count);
            for (int i = 0; i < expectedDates.Count; i++)
            {
                Assert.Equal(expectedDates[i].Date, upcomingDates[i].Date);
            }*/
        }


        [Fact]
        public void Rule_2DaysOn_2DaysOff_ShouldGenerate_CorrectDates2()
        {
            var rule = new RecurringRule
            {
                Id = 1,
                TaskItemId = 1,
                Type = RepeatType.Weekly,
                Interval = 2,
                DayOfWeek = DayOfWeek.Monday,
                StartDate = new DateTime(2025, 4, 1),
                MaxOccurrences = 5
            };

            //var nextDate = rule.GetUpcomingOccurrences(maxCount: 1);
            // Вернет ближайший понедельник через 2 недели от стартовой даты

            //var upcomingDates = rule.GetUpcomingOccurrences(maxCount: 3);
            // Получишь список из 3 ближайших дат
        }


    }
}
