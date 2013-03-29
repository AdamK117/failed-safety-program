using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Window.Commands;

namespace SafetyProgram.Window.Ribbon
{
    public class CoshhRibbon
    {
        CoshhRibbonView ribbon;
        CoshhWindow window;

        public CoshhRibbon(CoshhWindow window)
        {
            this.window = window;
            ribbon = new CoshhRibbonView(this);
        }

        public CoshhRibbonView View
        {
            get { return ribbon; }
        }

        public WindowCommands Commands
        {
            get { return window.Commands; }
        }
    }
}
