using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eCarColor
        {
            Red,
            Blue,
            White,
            Black
        }

        public enum eDoorsNum
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        private const float k_MaxGass = 48;
        private const float k_MaxBatteryLoad = 2.6f;
        private eCarColor m_CarColor;
        private eDoorsNum m_DoorNum;

        public Car()
        {
            this.m_MaxAirPressure = 29;
            this.m_NumOfWheels = 4;
        }

        public Car(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
        {
            this.m_MaxAirPressure = 29;
            this.m_NumOfWheels = 4;
        }

        public override void SetEngineType(eEngineType i_TypeOfEngine)
        {
            switch (i_TypeOfEngine)
            {
                case eEngineType.Electric:
                    m_Engine = new ElectricEngine( k_MaxBatteryLoad);
                    break;
                case eEngineType.Gas:
                    m_Engine = new GasEngine(GasEngine.eGasType.Octan95, k_MaxGass);
                    break;
            }
        }

        public override void SetVehicleDetails(Dictionary<string, string> i_Details)
        {
            int numOfDoors;
            int color;

            if (int.TryParse(i_Details["color"], out color) && (color >= (int)(eCarColor.Red + 1) && color <= (int)(eCarColor.Black+1)))
            {
                m_CarColor = (eCarColor)(color-1);
            }
            else
            {
                throw new FormatException("Illegal color value, possible value are only: red, blue, white and black");
            }

            if (int.TryParse(i_Details["num of doors"], out numOfDoors) && (numOfDoors >= (int)(eDoorsNum.Two) && numOfDoors <= (int)(eDoorsNum.Five)))
            {
                m_DoorNum = (eDoorsNum)(numOfDoors);
            }
            else
            {
                throw new FormatException("Illegal doors value, possible value are only: 2-5");
            }
        }

        public override string ToString()
        {
            StringBuilder details = new StringBuilder(base.ToString());

            details.AppendLine(string.Format("Color:          \t{0}", m_CarColor.ToString()));
            details.AppendLine(string.Format("Number of Doors:\t{0}", m_DoorNum.ToString()));

            return details.ToString();
        }
    }
}
