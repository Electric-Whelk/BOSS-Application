using System;
using System.Collections.Generic;
using System.Text;
using BOSS_Application.MVVM;
using BOSS_Application.Model;
using System.Security.Policy;
using System.Windows;

namespace BOSS_Application.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        public string Strobe { get { return this.Timer.LiveStatus() ? "Yellow" : "Blue"; } set; }

        public TestTimer Timer { get; set; }

        public RelayCommand TimerClick => new RelayCommand(execute => this.Timer.Switch());

        public MainWindowViewModel()
        {
            this.Timer = new TestTimer(delay:TimeSpan.FromSeconds(1));
            this.Timer.PropertyChanged += (sender, e) => { if (e.PropertyName == nameof(Timer.Elapsed))
            { this.OnPropertyChanged(nameof(Strobe)); } };
     
        }

    }
}
