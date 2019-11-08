



namespace Org.Joey.Common
{
    using System;
    public class LeakyBucket
    {
        public const int CAPACITY = 60;
        public const int INTERVAL = 60;

        protected double Timestamp;
        public int Left { get; protected set; }

        private int capacity;
        private int interval;
        private double finterval;
        public int Capacity
        {
            get { return this.capacity < 0 ? CAPACITY : this.capacity; }
            set { this.capacity = value; this.finterval = this.Interval * 1D / this.Capacity; }
        }
        public int Interval
        {
            get { return this.interval <= 0 ? INTERVAL : this.interval; }
            set { this.interval = value; this.finterval = this.Interval * 1D / this.Capacity; }
        }

        /// <summary>
        /// Leaky-Bucket Constructor
        /// </summary>
        /// <param name="capacity">The Leaky-Bucket capacity. (items per interval, defaults to 60)</param>
        /// <param name="interval">The rate of Leaking. (seconds, defaults to 60 seconds)</param>
        public LeakyBucket(int capacity = CAPACITY, int interval = INTERVAL)
        {
            this.Timestamp = DateTime.Now.Ticks;
            this.Capacity = capacity;
            this.Interval = interval;
        }

        /// <summary>
        /// Throttling
        /// </summary>
        /// <param name="cost">The cost of this. (defaults to 1)</param>
        /// <returns>Overflow or Not. (true: have not overflowed, false: overflowed)</returns>
        public bool Throttle(int cost = 1)
        {
            var grant = false;
            lock (this)
            {
                this.Leaking();
                var after = this.Left + cost;
                if (after <= this.Capacity)
                {
                    this.Left = after;
                    grant = true;
                }
            }
            return grant;
        }

        private void Leaking()
        {
            var now = DateTime.Now.Ticks;
            while (this.Timestamp < now)
            {
                if (this.Left > 0)
                {
                    this.Left -= 1;
                    this.Timestamp += 10000000 * finterval;
                }
                else
                {
                    break;
                }
            }
            this.Timestamp = Math.Max(this.Timestamp, (now - 10000000 * 60));
        }
    }
}

