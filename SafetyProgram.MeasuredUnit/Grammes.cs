namespace SafetyProgram.MeasuredUnit
{
    public class Grammes : IUnit
    {
        public Grammes()
        {
            UnitType = new Mass();
            Indicie = 1;
            SiScale = 0.001M;
        }

        public IUnitType UnitType
        {
            get;
            private set;
        }

        public int Indicie
        {
            get;
            private set;
        }

        public decimal SiScale
        {
            get;
            private set;
        }
    }
}
