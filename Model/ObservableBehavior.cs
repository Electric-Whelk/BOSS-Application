using System;
using System.Collections.Generic;
using System.Printing;
using System.Text;
using System.Windows;
using BOSS_Application.MVVM;

namespace BOSS_Application.Model
{
    internal class ObservableBehavior : Behavior
    {


        private bool noticed;
        public bool Noticed { get { return noticed; } set { this.noticed = value; this.OnPropertyChanged(); } }

        private bool cantick;
        public bool CanTick { get { return cantick; } set { this.cantick = value; this.OnPropertyChanged(); } }

        public TestTimer Timer { get; set; }

        public RelayCommand TickIt => new RelayCommand(execute => this.Noticed = !this.Noticed);
        public ObservableBehavior(string description, TestTimer timer) : base(description)
        {

            this.noticed = false;
            this.IsCountable = false;
            this.Timer = timer;
            this.cantick = true;
            this.Timer.Timer.Tick += new EventHandler(this.UpdateCanTick);
        }


        public void UpdateCanTick(object sender, EventArgs e)
        {
            if (this.Noticed || this.Timer.LiveStatus())
            {
                this.CanTick = true;
            }
            else
            {
                this.CanTick = false;
            }

        }
    }

}
