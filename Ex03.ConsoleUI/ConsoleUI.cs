using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using System.Threading;


namespace Ex03.ConsoleUI
{
    class ConsoleUI
    {
        private enum ePossibleActions
        {
            NewVehicle,
            GetAllLicenseNumbers,
            GetAllLicenseNumbersByStatus,
            ChangeVehicleStatus,
            InflateWheelsToMax,
            FillGas,
            LoadBattery,
            VehicleDetails,
            Exit
        }

        private readonly GarageManager r_GarageManager;
        private readonly string[] r_PossibleActions;

        public ConsoleUI()
        {
            int indexOfNameToAdd = 0;

            r_GarageManager = new GarageManager();
            r_PossibleActions = new string[Enum.GetNames(typeof(ePossibleActions)).Length];
            foreach (string option in Enum.GetNames(typeof(ePossibleActions)))
            {
                r_PossibleActions[indexOfNameToAdd] = option;
                indexOfNameToAdd++;
            }
        }

        private void printEnumTypeOptions(Type i_EnumType, string i_EnumName)
        {
            Console.WriteLine(i_EnumName);
            string[] options = Enum.GetNames(i_EnumType);

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine("{0} - {1}", i + 1, options[i]);
            }
        }

        private void printStartOfProgram()
        {
            Console.WriteLine(@"
   ______                                                                          
  / ________ __________ _____ ____     ____ ___  ____ _____  ____ _____ ____  _____
 / / __/ __ `/ ___/ __ `/ __ `/ _ \   / __ `__ \/ __ `/ __ \/ __ `/ __ `/ _ \/ ___/
/ /_/ / /_/ / /  / /_/ / /_/ /  __/  / / / / / / /_/ / / / / /_/ / /_/ /  __/ /    
\____/\__,_/_/   \__,_/\__, /\___/  /_/ /_/ /_/\__,_/_/ /_/\__,_/\__, /\___/_/     
                      /____/                                    /____/            
           
                     
                                     Options List 
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^                           
                   1 - Add a new vehicle                                    r==    
                   2 - See all license numbers                          _  //
                   3 - See all license numbers by a specific status    |_)//(''''':
                   4 - Change vehicle status                             //  \_____:_____.-----.P
                   5 - Inflate vehicle wheels to max                    //   | ===  |   /        \
                   6 - Fill gas to a vehicle                        .:'//.   \ \=|   \ /  .:'':.
                   7 - Load battery of a vehicle                   :' // ':   \ \ ''..'--:'-.. ':
                   8 - Vehicle details                             '. '' .'    \:.....:--'.-'' .'
                   9 - EXIT                                         ':..:'                ':..:'    

");
        }

        private Enum translateIntToUserChoiceEnum(Type i_SelectedEnum)
        {
            bool whileIncorrectInput = true;
            int optionNumber = 0;
            Enum userOption = null;

            while (whileIncorrectInput)
            {
                Console.WriteLine("Please choose an option by index: ");
                optionNumber = getAndValidateIntFromUser() - 1;
                string[] possibleNames = Enum.GetNames(i_SelectedEnum);

                if (optionNumber >= 0 && optionNumber < possibleNames.Length)
                {
                    userOption = (Enum)Enum.Parse(i_SelectedEnum, possibleNames[optionNumber]);
                    whileIncorrectInput = false;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again");
                }
            }

            return userOption;
        }

        public void Setup()
        {
            bool isInProgress = true;

            while (isInProgress)
            {
                Console.Clear();
                printStartOfProgram();
                ePossibleActions action = (ePossibleActions)translateIntToUserChoiceEnum(typeof(ePossibleActions));

                switch (action)
                {
                    case ePossibleActions.NewVehicle:
                        insertNewVehicle();
                        break;
                    case ePossibleActions.GetAllLicenseNumbers:
                        showAllLicenseNumbers();
                        break;
                    case ePossibleActions.GetAllLicenseNumbersByStatus:
                        getAllLisenceNumbersByStatus();
                        break;
                    case ePossibleActions.ChangeVehicleStatus:
                        changeStatusOfVehicle();
                        break;
                    case ePossibleActions.InflateWheelsToMax:
                        inflateVehicleWheels();
                        break;
                    case ePossibleActions.FillGas:
                        fillGas();
                        break;
                    case ePossibleActions.LoadBattery:
                        loadBattery();
                        break;
                    case ePossibleActions.VehicleDetails:
                        showDetailsOfVehicle();
                        break;
                    case ePossibleActions.Exit:
                        isInProgress = false;
                        break;
                }

                if (isInProgress)
                {
                    Console.WriteLine(@"Process success..
Press ENTER to continue:");
                    Console.ReadLine();
                }
            }
        }

        private string getAndValidateStringInput()
        {
            string strData = Console.ReadLine();

            while (strData == null || strData.Length == 0)
            {
                Console.WriteLine("Invalid input, try again.");
                strData = Console.ReadLine();
            }

            return strData;
        }

        private int getAndValidateIntFromUser()
        {
            int userInput;

            while (!int.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("Invalid input, try again.");
            }

            return userInput;
        }

        private float getAndValidateFloatFromUser()
        {
            float userInput;

            while (!float.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("Invalid input, try again.");
            }

            return userInput;
        }

        private void insertNewVehicle()
        {
            string licenseNumber;
            string clientName;
            string clientPhoneNumber;
            string modelName;
            bool isInGarage;

            Console.WriteLine(@"You choose to add a new vehicle:
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Please enter the license number of the vehicle");
            licenseNumber = getAndValidateStringInput();
            Console.WriteLine("Please enter the client name");
            clientName = getAndValidateStringInput();
            Console.WriteLine("Please enter the client's phone number: ");
            clientPhoneNumber = getAndValidateStringInput();
            Console.WriteLine("Please enter the vehicle model: ");
            modelName = getAndValidateStringInput();
            isInGarage = r_GarageManager.CheckIfVehicleInGarrage(licenseNumber);
            if (isInGarage)
            {
                Console.WriteLine("The vehicle is already exists in the GarageManager. Changed status to -InRepair-");
                r_GarageManager.ChangeVehicleStatus(licenseNumber, ClientDetailsAndStatus.eCarStatus.InRepair);
            }
            else
            {
                printEnumTypeOptions(typeof(CreateVehicle.eVehicleType), "select a Vehicle type:");
                CreateVehicle.eVehicleType selectedVehicleType = (CreateVehicle.eVehicleType)translateIntToUserChoiceEnum(typeof(CreateVehicle.eVehicleType));
                Vehicle newVehicle = CreateVehicle.CreateNewVehicle(selectedVehicleType, modelName, licenseNumber);

                setWheels(newVehicle);
                setEngine(newVehicle);
                setVehicleProperties(newVehicle);
                r_GarageManager.AddNewVehicle(newVehicle, clientPhoneNumber, clientName);
            }
        }

        private void setEngine(Vehicle i_Vehicle)
        {
            bool isEngineSet = false;
            bool isTruck = false;
            float currentAmountLeft;

            while (!isTruck)
            {
                try
                {
                    printEnumTypeOptions(typeof(Vehicle.eEngineType), "Select engine type");
                    Vehicle.eEngineType engineType = (Vehicle.eEngineType)translateIntToUserChoiceEnum(typeof(Vehicle.eEngineType));

                    i_Vehicle.SetEngineType(engineType);
                    isTruck = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            while (!isEngineSet)
            {
                try
                {
                    Console.WriteLine("Enter current amount of Gas/Battery the current tank is {0} max to add {1}", i_Vehicle.Engine.RemainingEnergy,
                        i_Vehicle.Engine.MaxEnergy-i_Vehicle.Engine.RemainingEnergy);
                    currentAmountLeft = getAndValidateFloatFromUser();
                    i_Vehicle.Engine.SetRemainingEnergy(currentAmountLeft);
                    isEngineSet = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void setWheels(Vehicle i_Vehicle)
        {
            bool validInput = false;
            string wheelsManufactorer;
            float wheelsCurrentAirPressure;

            Console.WriteLine("Please enter wheels manufacturer name: ");
            wheelsManufactorer = getAndValidateStringInput();
            while (!validInput)
            {
                try
                {
                    Console.WriteLine(@"Please enter current air pressure of your wheel, NOTE: max pressure possible : {0}",i_Vehicle.MaxAirPressure);
                    wheelsCurrentAirPressure = getAndValidateFloatFromUser();
                    i_Vehicle.SetWheels(wheelsManufactorer, wheelsCurrentAirPressure);
                    validInput = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message + i_Vehicle.MaxAirPressure);
                }
            }
        }

        private void setVehicleProperties(Vehicle i_Vehicle)
        {
            bool validInput = false;

            while (!validInput)
            {
                try
                {
                    insertExtraDetails(i_Vehicle);
                    validInput = true;
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void insertExtraDetails(Vehicle i_Vehicle)
        {
            Dictionary<string, string> details = new Dictionary<string, string>();

            if (i_Vehicle is Car)
            {
                Console.WriteLine(@"Insert color by index (1,2,3,4) you can choose either:
1.Red
2.Blue
3.White
4.Black");
                details.Add("color", getAndValidateStringInput());
                Console.WriteLine("Insert num of doors from 2 to 5:");
                details.Add("num of doors", getAndValidateStringInput());
            }
            else if (i_Vehicle is Motorcycle)
            {
                Console.WriteLine(@"Insert licence type by index (1,2,3,4) you can choose either:
1.A
2.A2
3.AA
4.B");
                details.Add("license type", getAndValidateStringInput());
                Console.WriteLine("Insert engine capacity:");
                details.Add("engine Capacity", getAndValidateStringInput());
            }
            else if (i_Vehicle is Truck)
            {
                Console.WriteLine(@"Does the truck carring frozen stuff (select by index):
1.True
2.False");
                details.Add("is Carring frozen", getAndValidateStringInput());
                Console.WriteLine("Insert carry capacity of the truck:");
                details.Add("Capacity", getAndValidateStringInput());
            }

            i_Vehicle.SetVehicleDetails(details);
        }

        private void showAllLicenseNumbers()
        {
            List<string> allLicenses = r_GarageManager.GetAllLicenseNumber();

            if (allLicenses.Count < 1)
            {
                Console.WriteLine("The garage is empty right now");
            }
            else
            {
                int countOfLicences = 0;

                Console.WriteLine("The licenses numbers that are in the garage:");
                foreach (string licenseNumber in allLicenses)
                {
                    Console.WriteLine("{0}.{1}", countOfLicences + 1, licenseNumber);
                    countOfLicences++;
                }
            }
        }

        private void getAllLisenceNumbersByStatus()
        {
            printEnumTypeOptions(typeof(ClientDetailsAndStatus.eCarStatus), "Select a status");
            ClientDetailsAndStatus.eCarStatus status = (ClientDetailsAndStatus.eCarStatus)translateIntToUserChoiceEnum(typeof(ClientDetailsAndStatus.eCarStatus));
            List<string> allLicenses = r_GarageManager.GetAllLicenseNumber(status);

            if (allLicenses.Count < 1)
            {
                Console.WriteLine("No vehicles with this status.");
            }
            else
            {
                int countOfLicences = 0;

                Console.WriteLine("All the license numbers in status {0} are:", status);
                foreach (string licenseNumber in allLicenses)
                {
                    Console.WriteLine("{0}. {1}", countOfLicences + 1, licenseNumber);
                    countOfLicences++;
                }
            }
        }

        private bool isLicenseNumberExists(string i_LicenseNumber)
        {
            bool isExist = true;

            if (r_GarageManager.GetAllLicenseNumber().Count == 0)
            {
                isExist = false;
            }
            else
            {
                isExist = r_GarageManager.CheckIfVehicleInGarrage(i_LicenseNumber);
            }

            return isExist;
        }

        private void changeStatusOfVehicle()
        {
            bool validInput = false;
            string licenseNumber;

            while (!validInput)
            {
                try
                {
                    Console.WriteLine("Enter a license number");
                    licenseNumber = getAndValidateStringInput();
                    validInput = isLicenseNumberExists(licenseNumber);
                    printEnumTypeOptions(typeof(ClientDetailsAndStatus.eCarStatus), "select a status");
                    ClientDetailsAndStatus.eCarStatus status = (ClientDetailsAndStatus.eCarStatus)translateIntToUserChoiceEnum(typeof(ClientDetailsAndStatus.eCarStatus));
                    r_GarageManager.ChangeVehicleStatus(licenseNumber, status);
                    validInput = true;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void inflateVehicleWheels()
        {
            bool validInput = false;
            string licenseNumber;

            while (!validInput)
            {
                try
                {
                    Console.WriteLine("Enter a license number");
                    licenseNumber = getAndValidateStringInput();
                    validInput = isLicenseNumberExists(licenseNumber);
                    r_GarageManager.FillMaxPressureInWheels(licenseNumber);
                    validInput = true;
                    Console.WriteLine("Inflated wheel succesfully !");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void fillGas()
        {
            bool validInput = false;
            string licenseNumber;
            float fuelAmount;
            GasEngine.eGasType gasType;

            while (!validInput)
            {
                try
                {
                    Console.WriteLine("Enter a license number");
                    licenseNumber = getAndValidateStringInput();
                    validInput = isLicenseNumberExists(licenseNumber);
                    printEnumTypeOptions(typeof(GasEngine.eGasType), "Please select a gas type");
                    gasType = (GasEngine.eGasType)translateIntToUserChoiceEnum(typeof(GasEngine.eGasType));
                    Console.WriteLine("Please enter amount liters to fill: ");
                    fuelAmount = getAndValidateFloatFromUser();
                    r_GarageManager.FillGas(licenseNumber, gasType, fuelAmount);
                    validInput = true;
                    Console.WriteLine("Gas was filled succesfully !");
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("{0}", e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void loadBattery()
        {
            bool validInput = false;
            string licenseNumber;
            float energyAmount;

            try
            {
                while (!validInput)
                {
                    Console.WriteLine("Enter a license number");
                    licenseNumber = getAndValidateStringInput();
                    validInput = isLicenseNumberExists(licenseNumber);
                    Console.WriteLine("Please enter amount enery to load: ");
                    energyAmount = getAndValidateFloatFromUser();
                    r_GarageManager.LoadBattery(licenseNumber, energyAmount);
                    validInput = true;
                    Console.WriteLine("Battery was loaded succesfully !");
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("{0}", e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ValueOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void showDetailsOfVehicle()
        {
            bool validInput = false;
            string licenseNumber;

            try
            {
                while (!validInput)
                {
                    Console.WriteLine("Enter a license number");
                    licenseNumber = getAndValidateStringInput();
                    validInput = isLicenseNumberExists(licenseNumber);
                    Console.WriteLine(r_GarageManager.ShowVehicleDetails(licenseNumber));
                    validInput = true;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
