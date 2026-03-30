using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using BOSS_Application.MVVM;

namespace BOSS_Application.Model
{
    internal class CountableBehavior : Behavior
    {

        private int count;
        public int Count { get { return count; } set { this.count = value; this.OnPropertyChanged(); } }

        public RelayCommand TickUp => new RelayCommand(execute => this.Count += 1);

        public RelayCommand TickDown => new RelayCommand(execute => this.Count -= 1);


        public CountableBehavior(string description) : base(description)
        {
            this.Count = 0;
            this.IsCountable = true;
        }

    }
}
