using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GasEngine : Engine
    {
        public enum eGasType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        private eGasType m_GasType;

        public GasEngine(eGasType i_selectedType, float i_MaxAmountAllowed) : base(i_MaxAmountAllowed)
        {
            this.m_GasType = i_selectedType;
        }

        internal void AddGas(eGasType i_GasType, float i_AmonutToAdd)
        {
            if (this.m_GasType != i_GasType)
            {
                throw new ArgumentException(string.Format(
                    "WARNING:You are trying to fill gas of type {0} \nYou can only fill gas of type {1}", i_GasType,
                    m_GasType));
            }
            else
            {
                EnergyCharge(i_AmonutToAdd);
            }
        }

        public override string ToString()
        {
            StringBuilder details = new StringBuilder();

            details.AppendLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
            details.AppendLine("Engine type:                    Gas");
            details.AppendLine("Gas type:                       " + m_GasType);
            details.AppendLine("Remained gas:                   " + this.RemainingEnergy);
            details.AppendLine("Max amount of Gas is:           " + this.MaxEnergy);

            return details.ToString();
        }
    }
}
