using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ClientDetailsAndStatus
    {
        public enum eCarStatus
        {
            InRepair,
            RepairDone,
            PayAccepted
        }

        private Vehicle m_Vehicle;
        private string m_ClientPhoneNumber;
        private string m_ClientName;
        private eCarStatus m_VehicleStatus;

        internal ClientDetailsAndStatus(Vehicle i_Vehicel, string i_ClientPhoneNumber, string i_ClientName)
        {
            this.m_Vehicle = i_Vehicel;
            this.m_VehicleStatus = eCarStatus.InRepair;
            this.m_ClientName = i_ClientName;
            this.m_ClientPhoneNumber = i_ClientPhoneNumber;
        }

        public eCarStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
        }

        public override string ToString()
        {
            StringBuilder details = new StringBuilder();

            details.AppendLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
            details.AppendLine(m_Vehicle.ToString());
            details.AppendLine("Client name:\t" + m_ClientName);
            details.AppendLine("Car Status: \t" + m_VehicleStatus);

            return details.ToString();
        }
    }
}
