using System;
using System.Collections.Generic;
using System.Text;

namespace BOSS_Application.MVVM
{

    internal class Behavior : PropertyChangeObject
    {
        public string Description { get; set; }

        private bool iscountable;
        public bool IsCountable { get; set; }

        public Behavior(string description) 
        {
        
            this.Description = description;
        }

    }
}
