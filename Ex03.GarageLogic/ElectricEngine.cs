using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxAmountAllowed) : base(i_MaxAmountAllowed) {}

        internal void LoadBattery(float i_AmountToAdd)
        {
            EnergyCharge(i_AmountToAdd);
        }

        public override string ToString()
        {
            StringBuilder details = new StringBuilder();

            details.AppendLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
            details.AppendLine("Engine type:                    Electric");
            details.AppendLine("Current battery left:           " + this.RemainingEnergy);
            details.AppendLine("Max amount of battery is:       " + this.MaxEnergy);

            return details.ToString();
        }
    }
}