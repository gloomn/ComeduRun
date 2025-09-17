//Copyright (C) 2025 Lee Ki Joon
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMEDURUN_.Timers
{
    public class GameTimer : Timer
    {
        public GameTimer(int interval, EventHandler tickHandler)
        {
            this.Interval = interval;
            this.Tick += tickHandler;
        }
    }
}
