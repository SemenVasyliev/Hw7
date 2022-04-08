using Hw7.Exercise2;
using System;
using System.Collections.Generic;
using Xunit;

#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Hw7.Tests.Exercise2
{
    public class RecurrentJobInvalidArgsTests
    {
        public static IEnumerable<object[]> GetInvalidRunArgs()
        {
            var second = TimeSpan.FromSeconds(1);
            var negSecond = TimeSpan.FromSeconds(-1);
            var mockJob = (int i, object? o) => { };

            var invalidArgs = new List<object[]>
            {
                new object[] { negSecond, second, 1, mockJob, typeof(ArgumentOutOfRangeException), "dueTime" },
                new object[] { second, negSecond, 1, mockJob, typeof(ArgumentOutOfRangeException), "interval" },
                new object[] { second, second, -1, mockJob, typeof(ArgumentOutOfRangeException), "times" },
                new object[] { second, second, 1, null!, typeof(ArgumentNullException), "job" },
            };
            return invalidArgs;
        }

        [Theory]
        [MemberData(nameof(GetInvalidRunArgs))]
        public void Run_WithInvalidArgs_ThrowsArgException(
            TimeSpan dueTime,
            TimeSpan interval,
            int times,
            Action<int, object?> job,
            Type exceptionType,
            string paramName)
        {
            var exception = Assert.Throws(exceptionType, () =>
            {
                using var recurrentJob = RecurrentJob.Run(dueTime, interval, times, job, null);
            });

            var argException = Assert.IsAssignableFrom<ArgumentException>(exception);

            Assert.Equal(paramName, argException.ParamName);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
