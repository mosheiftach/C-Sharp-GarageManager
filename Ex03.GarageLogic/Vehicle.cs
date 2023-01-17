using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eEngineType
        {
            Gas,
            Electric
        }

        protected string m_ModelName;
        protected readonly string m_LicenseNumber;
        protected List<Wheel> m_Wheels = new List<Wheel>();
        protected Engine m_Engine;
        protected int m_NumOfWheels;
        protected float m_MaxAirPressure;

        protected Vehicle() {}

        protected Vehicle(string i_ModelName, string i_LicenceNumber)
        {
            m_LicenseNumber = i_LicenceNumber;
            m_ModelName = i_ModelName;
        }

        public string ModelName
        {
            get { return m_ModelName; }
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
        }

        public Engine Engine
        {
            get { return m_Engine; }
        }

        public void SetWheels(string i_Producer, float i_AirPressure)
        {
            m_Wheels = new List<Wheel>(m_NumOfWheels);

            for (int i = 0; i < m_NumOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_Producer, i_AirPressure, m_MaxAirPressure));
            }
        }

        public override string ToString()
        {
            StringBuilder details = new StringBuilder();

            details.AppendLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
            details.AppendLine(string.Format("License number is: -{0}- \n", m_LicenseNumber));
            details.AppendLine(string.Format("Model of vehicle is: -{0}- \n", m_ModelName));
            details.AppendLine("WHEELS: ");
            details.AppendLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
            details.AppendLine(string.Format("The vehicle has: {0} wheels", m_Wheels.Count));
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                details.AppendLine(string.Format("Wheel Num {0}: ", i + 1));
                details.AppendLine(m_Wheels[i].ToString());
            }

            if (m_Engine != null)
            {
                details.AppendLine(m_Engine.ToString());
            }

            return details.ToString();
        }

        public abstract void SetEngineType(eEngineType i_EngineType);
        public abstract void SetVehicleDetails(Dictionary<string,string> i_Details);
    }
}
