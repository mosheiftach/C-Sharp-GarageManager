using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const float k_MaxAmountOfGas = 130;
        private bool m_IsCarryingFrozeStuff;
        private float m_ContainerCapacity;

        public Truck()
        {
            this.m_MaxAirPressure = 25;
            this.m_NumOfWheels = 16;
        }

        public Truck(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
        {
            this.m_MaxAirPressure = 25;
            this.m_NumOfWheels = 16;
        }

        public override void SetEngineType(eEngineType i_EnergyType)
        {
            switch (i_EnergyType)
            {
                case eEngineType.Electric:
                    throw new ArgumentException("NOTE: Meanwhile truck does not have the option to be electric please try again");
                case eEngineType.Gas:
                    m_Engine = new GasEngine(GasEngine.eGasType.Octan96, k_MaxAmountOfGas);
                    break;
            }
        }

        public override void SetVehicleDetails(Dictionary<string, string> i_Details)
        {
            if (int.Parse(i_Details["is Carring frozen"]) < 1 || int.Parse(i_Details["is Carring frozen"]) > 2)
            {
                throw new KeyNotFoundException("The information if the truck is carring frozen stuff was not found");
            }

            if (int.Parse(i_Details["is Carring frozen"]) == 1)
            {
                m_IsCarryingFrozeStuff = true;
            }
            else
            {
                m_IsCarryingFrozeStuff = false;

            }

            m_ContainerCapacity = float.Parse(i_Details["Capacity"]);
        }

        public override string ToString()
        {
            StringBuilder details = new StringBuilder(base.ToString());

            if (m_IsCarryingFrozeStuff)
            {
                details.AppendLine("The truck is carrying frozen stuff");
            }
            else
            {
                details.AppendLine("The truck is not carrying frozen stuff");
            }

            details.AppendLine("Truck container capacity:" + m_ContainerCapacity);

            return details.ToString();
        }
    }
}

