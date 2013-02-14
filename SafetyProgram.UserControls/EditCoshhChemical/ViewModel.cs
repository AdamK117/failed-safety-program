using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;
using Microsoft.Practices.ServiceLocation;

namespace SafetyProgram.UserControls
{
    public class ViewModel : BaseINPC
    {
        private CoshhChemicalModel model;
        protected ActiveCoshhData currentlyOpen;

        private void baseCtor()
        {
            currentlyOpen = ServiceLocator.Current.GetInstance<ActiveCoshhData>();
            RaisePropertyChanged("Model");
        }

        public ViewModel()
        {            
            this.model = new CoshhChemicalModel();
            baseCtor();
        }

        public ViewModel(CoshhChemicalModel model)
        {
            this.model = model;
            baseCtor();
        }

        public CoshhChemicalModel Model
        {
            get { return model; }
            set { model = value; }
        }
    }
}
