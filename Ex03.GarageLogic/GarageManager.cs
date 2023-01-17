using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private Dictionary<string, ClientDetailsAndStatus> m_VehiclesInGarage;

        public GarageManager()
        {
            this.m_VehiclesInGarage = new Dictionary<string, ClientDetailsAndStatus>();
        }

        public bool CheckIfVehicleInGarrage(string i_LicenceNumber)
        {
            bool isExist = false;

            if (m_VehiclesInGarage.ContainsKey(i_LicenceNumber))
            {
                isExist = true;
            }

            return isExist;
        }

        public void AddNewVehicle(Vehicle i_NewVehicle, string i_ClientPhoneNumber, string i_ClientName)
        {
            if (m_VehiclesInGarage.ContainsKey(i_NewVehicle.LicenseNumber))
            {
                m_VehiclesInGarage[i_NewVehicle.LicenseNumber].VehicleStatus = ClientDetailsAndStatus.eCarStatus.InRepair;
            }
            else
            {
                ClientDetailsAndStatus newClient = new ClientDetailsAndStatus(i_NewVehicle, i_ClientPhoneNumber, i_ClientName);
                m_VehiclesInGarage.Add(i_NewVehicle.LicenseNumber, newClient);
            }
        }

        public List<string> GetAllLicenseNumber()
        {
            return new List<string>(this.m_VehiclesInGarage.Keys);
        }

        public List<string> GetAllLicenseNumber(ClientDetailsAndStatus.eCarStatus i_CarStatus)
        {
            List<string> mappedByStatusVehicles = new List<string>();

            foreach (string key in this.m_VehiclesInGarage.Keys)
            {
                if (m_VehiclesInGarage[key].VehicleStatus == i_CarStatus)
                {
                    mappedByStatusVehicles.Add(key);
                }
            }

            return mappedByStatusVehicles;
        }

        private ClientDetailsAndStatus findClientDetails(string i_LicenceNumber)
        {
            ClientDetailsAndStatus foundClientDetails;
            if (!m_VehiclesInGarage.TryGetValue(i_LicenceNumber, out foundClientDetails))
            {
                throw new ArgumentException(string.Format("License number -{0}-- does not exist in the system", i_LicenceNumber));
            }

            return foundClientDetails;
        }

        public void ChangeVehicleStatus(string i_LicenceNumber, ClientDetailsAndStatus.eCarStatus i_newStatus)
        {
            findClientDetails(i_LicenceNumber).VehicleStatus = i_newStatus;
        }

        public void FillMaxPressureInWheels(string i_LicenceNumber)
        {
            foreach (Wheel currentWheel in m_VehiclesInGarage[i_LicenceNumber].Vehicle.Wheels)
            {
                currentWheel.InflatingAirToMax();
            }
        }

        public void FillGas(string i_LicenceNumber, GasEngine.eGasType i_GasType, float i_AmonutToFill)
        {
            if (findClientDetails(i_LicenceNumber).Vehicle.Engine is GasEngine)
            {
                ((GasEngine)findClientDetails(i_LicenceNumber).Vehicle.Engine).AddGas(i_GasType, i_AmonutToFill);
            }
            else
            {
                throw new ArgumentException("Gas fill can execute only for car which have a gas engine");
            }
        }

        public void LoadBattery(string i_LicenceNumber, float i_AmountToFill)
        {
            if (findClientDetails(i_LicenceNumber).Vehicle.Engine is ElectricEngine)
            {
                ((ElectricEngine)findClientDetails(i_LicenceNumber).Vehicle.Engine).LoadBattery(i_AmountToFill);
            }
            else
            {
                throw new ArgumentException("Battery load can execute only for car which have an electric engine");
            }
        }

        public string ShowVehicleDetails(string i_LicenseNumber)
        {
            ClientDetailsAndStatus currentClientDetails = findClientDetails(i_LicenseNumber);

            return currentClientDetails.ToString();
        }
    }
}
