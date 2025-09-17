//Copyright (C) 2025 Lee Ki Joon
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMEDURUN_.Utils
{
    public static class InputHandler
    {
        public static bool isJumpKeys(Keys key)
        {
            return key == Keys.Space;
        }
    }
}
