using PingMeTasks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var upcomingDates = rule.GetUpcomingOccurrences(maxCount: 6);

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

            Assert.Equal(expectedDates.Count, upcomingDates.Count);
            for (int i = 0; i < expectedDates.Count; i++)
            {
                Assert.Equal(expectedDates[i].Date, upcomingDates[i].Date);
            }
        }
    }
}
