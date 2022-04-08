
namespace Hw7.Exercise1
{
    public static class Threads
    {
        public static Thread[] StartAll(ParameterizedThreadStart entryPoint, IEnumerable<object> args)
        {
            if (entryPoint == null)
            {
                throw new ArgumentNullException(nameof(entryPoint));
            }
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            var list = new List<Thread>();
            foreach (var arg in args)
            {
                var thread = new Thread(entryPoint);
                thread.Start(arg);
                list.Add(thread);
            }
            return list.ToArray();
        }

        public static void WaitAll(IEnumerable<Thread> threads)
        {
            if (threads is null)
            {
                throw new ArgumentNullException(nameof(threads));
            }

            foreach (var t in threads)
            {
                t.Join();
            }
        }
    }
}
