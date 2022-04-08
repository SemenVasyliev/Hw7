using Hw7.Exercise1;
using System.Linq;
using System.Threading;
using Xunit;

#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Hw7.Tests.Exercise1
{
    public class ThreadsTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void StartAll_WithValidArgs_StartsThreads(int threadsCount)
        {
            var args = Enumerable.Range(0, threadsCount).Cast<object>();
            var threads = Threads.StartAll((object? obj) => { }, args);

            Assert.All(threads, t => Assert.NotEqual(ThreadState.Unstarted, t.ThreadState));
            Assert.Equal(threadsCount, threads.Length);
            Assert.Equal(threadsCount, threads.Distinct().Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void WaitAll_WithValidArgs_WaitsThreads(int threadsCount)
        {
            var threads = Enumerable
                .Range(0, threadsCount)
                .Select(t => new Thread((object? obj) => { }))
                .ToList();

            threads.ForEach(t => t.Start(string.Empty));

            Threads.WaitAll(threads);
            Assert.All(threads, t => Assert.False(t.IsAlive));
        }

        [Fact]
        public void StartAndWaitAll_CalcPrimes_Properly()
        {
            var primesLoad = new PrimesLoad();
            var ranges = new[]
            {
                1..1000,
                1000..2000,
                2000..3000
            };

            var threads = Threads.StartAll(primesLoad.FindPrimes, ranges.Cast<object>());
            Assert.Equal(3, threads.Length);

            Threads.WaitAll(threads);

            var primes = primesLoad
                .GetPrimesCache()
                .Distinct()
                .OrderBy(p => p)
                .ToArray();

            Assert.All(primes, p => Assert.True(PrimesLoad.IsPrime(p)));

            // [430] = {2, 3, 5, ..., 2969, 2971, 2999};
            Assert.Equal(430, primes.Length);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
