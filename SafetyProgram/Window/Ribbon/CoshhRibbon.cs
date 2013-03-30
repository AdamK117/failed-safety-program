using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Window.Commands;
using System.ComponentModel;

namespace SafetyProgram.Window.Ribbon
{
    public class CoshhRibbon : BaseINPC
    {
        CoshhRibbonView ribbon;
        CoshhWindow window;

        public CoshhRibbon(CoshhWindow window)
        {
            this.window = window;
            ribbon = new CoshhRibbonView(this);
            window.PropertyChanged += new PropertyChangedEventHandler(window_PropertyChanged);
        }

        public CoshhRibbonView View
        {
            get { return ribbon; }
        }

        public WindowCommands Commands
        {
            get { return window.Commands; }
        }

        public void HideBackstage()
        {
            backstageVisibility = "False";
            RaisePropertyChanged("BackstageVisibility");
        }
        private string backstageVisibility;
        public string BackstageVisibility
        {
            get { return backstageVisibility; }
            set { backstageVisibility = value; }
        }

        public bool RibbonVisibility 
        { 
            get { return window.Document == null ? false : true; } 
        }

        void window_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Document")
            {
                HideBackstage();
                RaisePropertyChanged("RibbonVisibility");
            }
        }    
    }
}
