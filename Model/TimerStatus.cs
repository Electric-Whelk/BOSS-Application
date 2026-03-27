using System;
using System.Collections.Generic;
using System.Text;

namespace BOSS_Application.Model
{

    class TimerStatus
    {
        private enum StatusOptions
        {
            Live,
            Dormant
        }

        private StatusOptions Status { get; set; }

        public TimerStatus()
        {
            this.Status = StatusOptions.Live;
        }

    }
}
