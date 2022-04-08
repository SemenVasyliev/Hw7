using Hw7.Exercise2;
using System;
using System.Collections.Concurrent;
using System.Threading;
using Xunit;

#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Hw7.Tests.Exercise2
{
    public class RecurrentJobTests
    {
        [Theory]
        [InlineData(1000, 1500, 1)]
        [InlineData(1000, 2000, 3)]
        [InlineData(750, 1200, 4)]
        public void Run_WithValidArgs_RunsProperly(double dueTimeMs, double intervalMs, int times)
        {
            var journal = new ConcurrentDictionary<int, DateTime>();
            var startTime = DateTime.UtcNow;

            using var job = RecurrentJob.Run(
                TimeSpan.FromMilliseconds(dueTimeMs),
                TimeSpan.FromMilliseconds(intervalMs),
                times,
                (int cnt, object? ctx) =>
                {
                    journal[cnt] = DateTime.UtcNow;
                },
                journal);

            Assert.Empty(journal);

            _ = SpinWait.SpinUntil(
                () => !job.IsRunning,
                (int)((intervalMs * times * 2) + dueTimeMs));

            Assert.False(job.IsRunning);

            Assert.NotEmpty(journal);

            for (var i = 0; i < journal.Count; i++)
            {
                // journal should contains valid records
                // with valid timestamps
                Assert.True(journal.ContainsKey(i));

                if (i == 0)
                {
                    Assert.InRange(
                        (journal[i] - startTime).TotalMilliseconds,
                        dueTimeMs * 0.5, dueTimeMs * 1.5);
                }
                else
                {
                    Assert.InRange(
                        (journal[i] - journal[i - 1]).TotalMilliseconds,
                        intervalMs * 0.5, intervalMs * 1.5);
                };
            }
        }

        [Fact]
        public void Run_EarlyDispose_StopsProperly()
        {
            var journal = new ConcurrentDictionary<int, DateTime>();

            using var job = RecurrentJob.Run(
               TimeSpan.FromMilliseconds(100),
               TimeSpan.FromMilliseconds(100),
               5,
               (int cnt, object? ctx) =>
               {
                   journal[cnt] = DateTime.UtcNow;
               },
               journal);

            Assert.True(job.IsRunning);

            job.Dispose();

            // job should be stopped after disposing
            Assert.False(job.IsRunning);

            Thread.Sleep(500);
            Assert.Empty(journal);
        }

        [Fact]
        public void Run_WithContext_PassValidContext()
        {
            object source = new();
            object? context = null;

            using var job = RecurrentJob.Run(
               TimeSpan.FromMilliseconds(1),
               TimeSpan.FromMilliseconds(1),
               1,
               (int cnt, object? ctx) =>
               {
                   context = ctx;
               },
               source);

            _ = SpinWait.SpinUntil(() => !job.IsRunning, 1000);

            Assert.Same(source, context);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
