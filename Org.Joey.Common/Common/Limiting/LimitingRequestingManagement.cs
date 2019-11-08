


namespace Org.Joey.Common
{
    using System;    
    using System.Threading;

    public class LimitingRequestingManagement
    {
        protected AutoResetEvent Sinal;
        public int Left { get; protected set; }
        public int Capacity { get; set; }

        public LimitingRequestingManagement(
            int capacity)
        {
            this.Capacity = capacity;
            this.Sinal = new AutoResetEvent(false);
            this.Sinal.Set();
        }

        public T Request<T>(Func<T> func, int cost = 1)
        {
            if (func != null)
            {
                this.Sinal.WaitOne();
                while (this.Left >= this.Capacity) { Thread.Sleep(100); }
                lock (this) { this.Left = this.Left + 1; }
                this.Sinal.Set();
                try
                {
                    return func();
                }
                finally
                {
                    lock (this) { this.Left = this.Left - 1; }
                }
            }
            return default(T);
        }
    }
}
