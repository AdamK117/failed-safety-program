namespace SafetyProgram.Models.DataModels
{
    public class CoshhChemicalModel : ChemicalModel
    {
        private float value;
        public float Value
        {
            get{ return value; }
            set
            {
                this.value = value;
                RaisePropertyChanged("Value");
            }
        }

        private string unit;
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                RaisePropertyChanged("Unit");
            }
        }
    }
}