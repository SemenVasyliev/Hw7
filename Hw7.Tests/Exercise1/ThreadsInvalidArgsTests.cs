using Hw7.Exercise1;
using System;
using Xunit;

#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Hw7.Tests.Exercise1
{
    public class ThreadsInvalidArgsTests
    {
        [Fact]
        public void StartAll_WithInvalidEntryPoint_ThrowsArgNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = Threads.StartAll(null!, Array.Empty<object>());
            });

            Assert.Equal("entryPoint", exception.ParamName);
        }

        [Fact]
        public void StartAll_WithInvalidArgs_ThrowsArgNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = Threads.StartAll((object? _) => { }, null!);
            });

            Assert.Equal("args", exception.ParamName);
        }

        [Fact]
        public void WaitAll_WithInvalidThreads_ThrowsArgNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                Threads.WaitAll(null!);
            });

            Assert.Equal("threads", exception.ParamName);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
