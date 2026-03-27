using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Policy;
using System.Text;
using System.Windows.Threading;
using BOSS_Application.MVVM;
using System.Data;
using System.Windows;
using System.ComponentModel.DataAnnotations;

namespace BOSS_Application.Model
{
    class TestTimer : PropertyChangeObject
    {
        private DispatcherTimer Timer { get; set; }

        public TimeSpan Interval {  get; set; }
        public TimeSpan Period { get; set; }
        public TimeSpan RunTime { get; set; }
        public TimeSpan Delay { get; set; }
        public TimeSpan elapsed;
        public TimeSpan Elapsed { get => elapsed; set { this.elapsed = value; this.OnPropertyChanged(); } }
        public TimeSpan countdown;
        public TimeSpan CountDown { get => countdown; set { this.countdown = value; this.OnPropertyChanged(); } }


        public TestTimer(TimeSpan? delay = null, TimeSpan? period = null, TimeSpan? interval = null, TimeSpan? runtime = null)
        {
            this.Delay = delay==null ? TimeSpan.FromSeconds(0) : (TimeSpan)delay;
            this.Period = period==null ? TimeSpan.FromSeconds(5) : (TimeSpan)period;
            this.Interval = interval == null ? TimeSpan.FromSeconds(2) : (TimeSpan)interval;
            this.RunTime = runtime == null ? TimeSpan.FromMinutes(1) : (TimeSpan)runtime;
            this.Elapsed = TimeSpan.FromSeconds(0);
            this.CountDown = this.CountDownUpdate();

            this.Timer = new DispatcherTimer();
            this.Timer.Interval = TimeSpan.FromSeconds(1);
            this.Timer.Tick += new EventHandler(this.timer_Tick);

        }

        public bool LiveStatus(){ return this.TimeMod() < this.Interval; }

        public TimeSpan TimeMod() { return TimeSpan.FromTicks(this.TrueElapsed().Ticks % this.Period.Ticks); }

         public TimeSpan TrueElapsed() { return this.Elapsed + this.Period - this.Delay; }

        public void Start(){ this.Timer.Start(); }

        public void Stop() { this.Timer.Stop(); }

        public void Switch() { 
            if (this.Timer.IsEnabled) { this.Stop(); } else { this.Start(); }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            this.Elapsed += TimeSpan.FromSeconds(1);
            this.CountDown = this.CountDownUpdate();

        }

        public TimeSpan CountDownUpdate() { return (this.LiveStatus() ? this.Interval : this.Period) - this.TimeMod(); }

    }
}
