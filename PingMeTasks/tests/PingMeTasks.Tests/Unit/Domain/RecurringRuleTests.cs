using Moq;
using PingMeTasks.Core.Domain;
using PingMeTasks.Core.Interfaces.Common;
using System;

namespace PingMeTasks.Tests.Unit.Domain
{
    public class RecurringRuleTests
    {
        [Fact]
        public void GetUpcomingOccurrences_DailyRule_Returns_CorrectDates()
        {
            var rule = new RecurringRule
            {
                Type = RepeatType.Daily,
                Interval = 1,
                StartDateUtc = new DateTime(2025, 4, 1),
                MaxOccurrences = 3
            };

            var mockClock = new Mock<IClock>();
            var fakeUtc = new DateTime(2025, 4, 2, 0, 0, 0, DateTimeKind.Utc);
            mockClock.Setup(c => c.Now).Returns(fakeUtc);            

            var dates = rule.GetUpcomingOccurrences(mockClock.Object, 2);

            Assert.Equal(new[] {
                new DateTime(2025, 4, 2),
                new DateTime(2025, 4, 3)
                }, dates);
        }

        [Fact]
        public void Rule_2DaysOn_2DaysOff_ShouldGenerate_CorrectDates()
        {
            // Arrange
            var rule = new RecurringRule
            {
                Id = 1,
                TaskItemId = 1,
                Type = RepeatType.CustomDaysPattern,
                StartDateUtc = new DateTime(2025, 4, 1),
                ActiveDays = 2,
                RestDays = 2
            };

            var mockClock = new Mock<IClock>();
            var fakeUtc = new DateTime(2025, 4, 1, 0, 0, 0, DateTimeKind.Utc);
            mockClock.Setup(c => c.Now).Returns(fakeUtc);

            // Act
            var upcomingDates = rule.GetUpcomingOccurrences(mockClock.Object,  maxCount: 6);

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


        [Fact]
        public void Rule_2DaysOn_2DaysOff_ShouldGenerate_CorrectDates2()
        {
            // Arrange
            var rule = new RecurringRule
            {
                Id = 1,
                TaskItemId = 1,
                Type = RepeatType.Weekly,
                Interval = 2,
                DayOfWeek = DayOfWeek.Monday,
                StartDateUtc = new DateTime(2025, 4, 1),
                MaxOccurrences = 5
            };

            var mockClock = new Mock<IClock>();
            var fakeUtc = new DateTime(2025, 4, 1, 0, 0, 0, DateTimeKind.Local);
            mockClock.Setup(c => c.Now).Returns(fakeUtc);

            //Act

            var nextDate = rule.GetUpcomingOccurrences(mockClock.Object, maxCount: 1);
            // Вернет ближайший понедельник через 2 недели от стартовой даты


            var upcomingDates = rule.GetUpcomingOccurrences(mockClock.Object, maxCount: 3);
            // Получишь список из 3 ближайших дат

            // Assert
        }

        [Fact]
        public void GetNextOccurrence_WhenUserInNewYork_ShouldUseLocalTime()
        {
            // Arrange
            var fixedUtcNow = new DateTime(2025, 4, 5, 12, 0, 0, DateTimeKind.Utc);
            var timeZoneId = "Eastern Standard Time"; // UTC-4 летом

            var mockClock = new Mock<IClock>();
            mockClock.Setup(c => c.GetTimeInTimeZone(timeZoneId))
                     .Returns(TimeZoneInfo.ConvertTimeFromUtc(fixedUtcNow,
                        TZConvert.GetTimeZoneInfo(timeZoneId)));

            var rule = new RecurringRule
            {
                StartDateUtc = new DateTime(2025, 4, 1),
                Type = RepeatType.Daily,
                Interval = 1
            };

            var mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(u => u.TimeZoneId).Returns(timeZoneId);

            var service = new RecurringRuleService(mockClock.Object, mockUserService.Object);

            // Act
            var nextDate = service.GetNextOccurrence(rule);

            // Assert
            // Время пользователя: 2025-04-05 08:00:00 (UTC-4)
            // Последняя задача была 4 апреля → следующая 5 апреля
            Assert.Equal(new DateTime(2025, 4, 5), nextDate?.Date);
        }

    }
}
