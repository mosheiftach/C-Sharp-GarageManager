using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        internal Wheel(string i_ManfacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            this.r_ManufacturerName = i_ManfacturerName;
            if (i_CurrentAirPressure > i_MaxAirPressure || i_CurrentAirPressure < 0 || i_MaxAirPressure < 0)
            {
                throw new ValueOutOfRangeException("WARNING: You overloaded the wheel pressure, min to add: 0, max to add:",0, m_MaxAirPressure);
            }
            else
            {
                this.m_CurrentAirPressure = i_CurrentAirPressure;
                this.m_MaxAirPressure = i_MaxAirPressure;
            }
        }

        public void InflatingAir(float i_AmountOfAirToAdd)
        {
            if (i_AmountOfAirToAdd < 0 || i_AmountOfAirToAdd + this.m_CurrentAirPressure > this.m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(string.Format("Air pressure can not be over {0}, you also can not reduce air pressure", m_MaxAirPressure),0, m_MaxAirPressure);
            }
            else
            {
                m_CurrentAirPressure += i_AmountOfAirToAdd;
            }
        }

        public void InflatingAirToMax()
        {
            this.m_CurrentAirPressure = this.m_MaxAirPressure;
        }

        public string ManufacturerName
        {
            get { return r_ManufacturerName; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public override string ToString()
        {
            StringBuilder details = new StringBuilder();

            details.AppendLine("Wheel's manufacturer name:   \t" + r_ManufacturerName);
            details.AppendLine("Wheel's current air Pressure:\t" + m_CurrentAirPressure);
            details.AppendLine("Wheel's max air pressure:    \t" + m_MaxAirPressure);

            return details.ToString();
        }
    }
}
