using BOSS_Application.Model;
using BOSS_Application.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace BOSS_Application.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        public string Strobe { get { return this.Timer.LiveStatus() ? "Yellow" : "Blue"; } set; }

        public int BarLength { get; set; }

        public TestTimer Timer { get; set; }

        public RelayCommand TimerClick => new RelayCommand(execute => this.Timer.Switch());

        public RelayCommand Output => new RelayCommand(execute => this.ShowData());


        public RelayCommand SortItemsPerTick => new RelayCommand(execute => this.RearrangeByTicked());


        //public ObservableCollection<ObservableBehavior> OBs { get; set { this.OBs = value; this.OnPropertyChanged(); } }
        public ObservableCollection<ObservableBehavior> OBs { get; set; }
        public ObservableCollection<CountableBehavior> CBs { get; set; }
        public MainWindowViewModel()
        {

            //set up the timer
            this.Timer = new TestTimer(delay:TimeSpan.FromSeconds(1));
            this.Timer.PropertyChanged += (sender, e) => { if (e.PropertyName == nameof(Timer.Elapsed))
            { this.OnPropertyChanged(nameof(Strobe)); } };
            this.BarLength = (int)this.Timer.Period.TotalSeconds - (int)this.Timer.Interval.TotalSeconds;

            //set up CountableBehaviors
            this.CBs = new ObservableCollection<CountableBehavior>();
            this.CBs.Add(new CountableBehavior("Licked himself"));
            this.CBs.Add(new CountableBehavior("Stuck his whole face in my bowl"));
            this.CBs.Add(new CountableBehavior("Stole food out of my hand"));


            //set up ObservableBehaviors
            this.OBs = new ObservableCollection<ObservableBehavior>();
            this.OBs.Add(new ObservableBehavior("Dropped a live shrew on Rosie during yoga", this.Timer));
            this.OBs.Add(new ObservableBehavior("Learnt how to open the fridge", this.Timer));
            this.OBs.Add(new ObservableBehavior("Woke up Ollie by jumping on his head when he was hungover", this.Timer));

        }

        public void RearrangeByTicked()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(OBs);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(nameof(ObservableBehavior.Noticed), ListSortDirection.Ascending));
        }
        public void ShowData()
        {
            string output = "Observable Behaviors:\n";
            foreach (var ob in this.OBs)
            {
                output += $"{ob.Description}: {(ob.Noticed ? "Noticed" : "Not Noticed")}\n";
            }
            output += "\nCountable Behaviors:\n";
            foreach (var cb in this.CBs)
            {
                output += $"{cb.Description}: {cb.Count}\n";

            }
            MessageBox.Show(output);
        }

    }
}
