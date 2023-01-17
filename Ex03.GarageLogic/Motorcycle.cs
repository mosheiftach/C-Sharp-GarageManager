using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eMotorcycleLicenseType
        {
            A,
            A2,
            AA,
            B
        }

        private const float k_MaxFuel = 5.8f;
        private const float k_MaxBatteryTime = 2.3f;
        private eMotorcycleLicenseType m_eLicenseType;
        private int m_EngineCapacity;

        public Motorcycle()
        {
            this.m_NumOfWheels = 2;
            this.m_MaxAirPressure = 30f;
        }

        public Motorcycle(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
        {
            this.m_NumOfWheels = 2;
            this.m_MaxAirPressure = 30f;
        }

        public override void SetEngineType(eEngineType i_EngineType)
        {
            switch (i_EngineType)
            {
                case eEngineType.Electric:
                    m_Engine = new ElectricEngine(k_MaxBatteryTime);
                    break;
                case eEngineType.Gas:
                    m_Engine = new GasEngine(GasEngine.eGasType.Octan98, k_MaxFuel);
                    break;
            }
        }

        public override void SetVehicleDetails(Dictionary<string,string> i_Details)
        {
            int licenseType;

            if (int.TryParse(i_Details["license type"], out licenseType) && (licenseType >= (int)(eMotorcycleLicenseType.A + 1) && licenseType <= (int)(eMotorcycleLicenseType.B + 1)))
            {
                m_eLicenseType = (eMotorcycleLicenseType)(licenseType - 1);
            }
            else
            {
                throw new FormatException("Illegal license Type, possible value are only: A, A2, AA and B");
            }

            m_EngineCapacity = int.Parse(i_Details["engine Capacity"]);
        }

        public override string ToString()
        {
            StringBuilder details = new StringBuilder(base.ToString());

            details.AppendLine("Motorcycle License:\t" + m_eLicenseType.ToString());
            details.AppendLine(string.Format("Engine Capacity:   \t{0}", m_EngineCapacity));

            return details.ToString();
        }
    }
}
