using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Jelly.Core
{
    /// <summary>
	/// Represents a schedule timer.
	/// </summary>
    public abstract class ScheduleTimer
    {
        private double _interval = 1000;
        private Timer _internalTimer;
        private int _timerStartCount;
        private readonly object _lockTimer = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleTimer"/> class.
        /// </summary>
        /// <remarks>The timer default interval is 1000ms.</remarks>
        protected ScheduleTimer() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleTimer"/> class.
        /// </summary>
        /// <param name="interval">The interval time.</param>
        protected ScheduleTimer(double interval)
        {
            this._interval = interval;
        }

        /// <summary>
        /// The schedule internal timer.
        /// </summary>
        protected virtual Timer InternalTimer
        {
            get
            {
                if (this._internalTimer == null)
                {
                    lock (this._lockTimer)
                    {
                        if (this._internalTimer == null)
                        {
                            this._internalTimer = new Timer
                            {
                                AutoReset = false,
                                Enabled = true
                            };
                        }
                    }
                }

                return this._internalTimer;
            }
        }

        /// <summary>
        /// The timer interval.
        /// </summary>
        protected virtual double Interval
        {
            get { return this._interval; }
            set { this._interval = value; }
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        protected virtual void StartTimer()
        {
            if (this.Interval > 0)
            {
                if (this._internalTimer == null)
                {
                    this.InternalTimer.Interval = this.Interval;
                    this.InternalTimer.Elapsed += this.OnTimedEvent;
                }
                try
                {
                    this.InternalTimer.Start();
                }
                catch (ObjectDisposedException)
                {
                    this._internalTimer = null;
                    this._timerStartCount++;
                    if (this._timerStartCount <= 3)
                    {
                        this.StartTimer();
                    }
                    else
                    {
                        throw;
                    }
                }
                this._timerStartCount = 0;
            }
        }

        /// <summary>
        /// On timed event.
        /// </summary>
        /// <param name="sender"><see cref="System.Timers.Timer"/></param>
        /// <param name="e"><see cref="ElapsedEventArgs"/></param>
        protected virtual void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            this.InternalTimer.Stop();
            this.HandleOnTime();
            this.InternalTimer.Start();
        }

        /// <summary>
        /// Handle on time.
        /// </summary>
        protected abstract void HandleOnTime();
    }
}
