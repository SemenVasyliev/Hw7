namespace Hw7.Exercise2
{
    public sealed class RecurrentJob : IDisposable
    {
        private RecurrentJob(TimeSpan dueTime, TimeSpan interval, int times, Action<int, object?> job, object? context, Timer timer)
        {
            DueTime = dueTime;
            Interval = interval;
            Times = times;
            Job = job;
            Context = context;
            Timer = timer;
        }

        public bool IsRunning { get; private set; }
        public TimeSpan DueTime { get; }
        public TimeSpan Interval { get; }
        public int Times { get; }
        public Action<int, object?> Job { get; }
        public object? Context { get; }
        public Timer Timer { get; }

        public void Dispose()
        {
            IsRunning = false;
        }

        public static RecurrentJob Run(TimeSpan dueTime, TimeSpan interval, int times, Action<int, object?> job, object? context)
        {
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }

            if (dueTime.TotalMilliseconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dueTime));
            }
            if (interval.TotalMilliseconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(interval));
            }
            if (times < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(times));
            }

            var tc = new TimerCallback(Callback);
            var t = new Timer(tc, job, dueTime, interval);

            job(times, context); // for test

            return new RecurrentJob(dueTime, interval, times, job, context, t);
        }
        public static void Callback(object? state)
        {
            // cant add follow on job
        }
    }
}
