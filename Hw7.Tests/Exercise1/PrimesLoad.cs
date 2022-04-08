using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Hw7.Tests.Exercise1
{
    internal class PrimesLoad
    {
        public ConcurrentBag<int> safeCollection = new();

        public IEnumerable<int> GetPrimesCache()
        {
            return safeCollection;
        }

        public void FindPrimes(object? range)
        {
            if (range is not Range rng)
                throw new ArgumentException("Invalid range", nameof(range));

            for (var i = rng.Start.Value; i < rng.End.Value; i++)
            {
                if (IsPrime(i))
                {
                    AppendPrimesCache(i);
                }
            }
        }

        private void AppendPrimesCache(int i)
        {
            safeCollection.Add(i);
        }

        public static bool IsPrime(int n)
        {
            if (n <= 1)
                return false;

            for (var i = 2; i < n; i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }
    }
}
