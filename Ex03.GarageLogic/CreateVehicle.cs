using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class CreateVehicle
    {
        public enum eVehicleType
        {
            Car,
            Motorcycle,
            Truck
        }

        public static Vehicle CreateNewVehicle(eVehicleType i_VehicleType, string i_ModelName, string i_LicenseNumber)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    newVehicle = new Car(i_ModelName, i_LicenseNumber);
                    break;
                case eVehicleType.Motorcycle:
                    newVehicle = new Motorcycle(i_ModelName, i_LicenseNumber);
                    break;
                case eVehicleType.Truck:
                    newVehicle = new Truck(i_ModelName, i_LicenseNumber);
                    break;
            }

            return newVehicle;
        }
    }
}
