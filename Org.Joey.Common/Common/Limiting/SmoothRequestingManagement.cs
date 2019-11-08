

namespace Org.Joey.Common
{
    using System;
    using System.Threading;

    public class SmoothRequestingManagement
    {
        protected AutoResetEvent Sinal;
        protected LeakyBucket LeakyBucket;

        public SmoothRequestingManagement(
            int capacity = LeakyBucket.CAPACITY,
            int interval = LeakyBucket.INTERVAL)
        {
            this.Sinal = new AutoResetEvent(false);
            this.LeakyBucket = new LeakyBucket(capacity, interval);
            this.Sinal.Set();
        }

        public T Request<T>(Func<T> func, int cost = 1)
        {
            if (func != null)
            {
                this.Sinal.WaitOne();
                while (this.LeakyBucket.Throttle(cost) == false) { Thread.Sleep(100); }
                this.Sinal.Set();
                return func();
            }
            return default(T);
        }
    }
}
