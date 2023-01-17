using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private float m_RemainingEnergy;
        private float m_MaxEnergy;

        protected Engine(float i_MaxAmountAllowed)
        { 
            this.m_MaxEnergy = i_MaxAmountAllowed;
        }

        public void SetRemainingEnergy(float i_RemeiningEnergy)
        {
            if (i_RemeiningEnergy < 0 || i_RemeiningEnergy > this.m_MaxEnergy)
            {
                throw new ValueOutOfRangeException("WARNING: You overloaded the tank, load did not accepted, please try again:", 0, this.m_MaxEnergy);
            }
            else
            {
                this.m_RemainingEnergy = i_RemeiningEnergy;
            }
        }

        protected void EnergyCharge(float i_EnergyToCharge)
        {
            if (i_EnergyToCharge + m_RemainingEnergy > m_MaxEnergy)
            {
                throw new ValueOutOfRangeException(string.Format("You overloaded the tank, current tank: {0} max is: {1} max to add: {2}", m_RemainingEnergy,
                        m_MaxEnergy, m_MaxEnergy - m_RemainingEnergy),0, m_MaxEnergy-m_RemainingEnergy);
            }
            else
            {
                this.m_RemainingEnergy += i_EnergyToCharge;
            }
        }

        public float MaxEnergy
        {
            get { return m_MaxEnergy; }
        }

        public float RemainingEnergy
        {
            get { return m_RemainingEnergy; }
        }
    }
}
