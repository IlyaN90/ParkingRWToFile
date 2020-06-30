using Prague_parking_v2._0.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Prague_parking_v2._0
{
    public static class Menu
    {
        enum FType
        {
            Car=1,
            MC=2,
            Custom=3
        }

        public static int MenuPrint()
        {
            int userInput = 0;
            Console.WriteLine("1:Park a Vehicle");
            Console.WriteLine("2:Remove a Vehicle");
            Console.WriteLine("3:Move a Vehicle");
            Console.WriteLine("4:Search for a Vehicle");
            Console.WriteLine("5:Peek at a Parking Spot");
            Console.WriteLine("6:Get overview over Parking");
            Console.WriteLine("7:Read from File");
            Console.WriteLine("8:Write to File");
            Console.WriteLine("9:Sort all MC");
            Console.WriteLine("10:Sort everything");
            Console.Write("What do you want to do: ");
            if(Int32.TryParse(Console.ReadLine(),out userInput))
            {
                if (userInput > 0 && userInput < 11)
                {
                    return userInput;
                }
            }
            return -1;
        }

        public static bool MenuPark()
        {
            bool ok=RegNumCheck(out string regNum, out string error);
            if (!ok)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ForegroundColor = ConsoleColor.White;
                return ok;
            }
            Console.WriteLine("Provide your vehicle type:");
            Console.WriteLine("1:Car");
            Console.WriteLine("2:MC");
            Console.WriteLine("3:Custom");
            ok = Int32.TryParse(Console.ReadLine(),out int vehicleInt);
            if (ok && vehicleInt > 0 && vehicleInt < 4)
            {
                int size = 0;
                error = "";
                var enumDisplayStatus = (FType)vehicleInt;
                string stringValue = enumDisplayStatus.ToString();
                if(stringValue== "Custom")
                {
                    Console.WriteLine("Provide your custom vehicle type:");
                    string customType = Console.ReadLine();
                    for (int i = 0; i < customType.Length; i++)
                    {
                        if (!Char.IsLetterOrDigit(customType[i]))
                        {
                            Menu.PrintError("Can only include letters and digits.");
                            return false;
                        }
                        stringValue= customType;
                    }
                    Console.WriteLine("Provide your custom vehicle size:");
                    ok = Int32.TryParse(Console.ReadLine(), out int vehicleSize);
                    if (ok && vehicleSize>0 && vehicleSize<101)
                    {
                        size = vehicleSize;
                    }
                    else
                    {
                        Menu.PrintError("Must be > 0 and <101");
                        return false;
                    }
                }
                DateTime dateTime = DateTime.Now;
                Factory factory = new Factory();
                Singelton S = Singelton.Instance;
                factory.onCreate(regNum, dateTime, stringValue, size);
                return ok;
            }
            else
            {
                PrintError("Try again!");
                ok = false;
            }
            
            return ok;
        }

        public static bool RegNumCheck(out string userInput, out string error)
        {
            Console.WriteLine("Provide registration number:");
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            userInput = Console.ReadLine();

            if (userInput.Length < 3)
            {
                error = "Regestration number has to be at least 3 characters long";
                return false;
            }
            if (userInput.Length > 10)
            {
                error = "Regestration number is too long (max 10).";
                return false;
            }

            for (int i = 0; i < userInput.Length; i++)
            {
                if(!Char.IsLetterOrDigit(userInput[i]))
                {
                    error = "Can only include letters and digits.";
                    return false;
                }
            }
            error = "";
            return true;
        }

        public static bool MenuRemove(out string regNum)
        {
            if (RegNumCheck(out regNum, out string error))
            {
                return true;
            }
            else
            {
                PrintError(error);
                return false;
            }
        }

        public static bool PrintError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
            return true;
        }

        public static bool PrintGreen(string ok)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(ok);
            Console.ForegroundColor = ConsoleColor.White;
            return true;
        }

        public static bool MenuMove(out string regNum, out int index)
        {
            regNum = null;
            index = -1;
            if(RegNumCheck(out string userInput, out string error))
            {
                regNum = userInput;
                Console.WriteLine("Please provide where you want to move {0} :", regNum);
                if(Int32.TryParse(Console.ReadLine(),out int output))
                {
                    index = output;
                    return true;
                }
                return false;
            }
            PrintError(error);
            return false;
        }

        public static bool MenuIndex(out int index)
        {
            Console.WriteLine("Provide parkingspot between 0 and 99");
            int temp = -1;
            bool ok = Int32.TryParse(Console.ReadLine(),out temp);
            index = temp;
            return ok;
        }

        public static bool CustomCost(out int cost)
        {
            cost = 0;
            Console.WriteLine("Provide custom cost/hour for custom vehicle");
            string str = Console.ReadLine();
            bool ok = Int32.TryParse(str, out int customCost);
            if (ok)
            {
                cost = customCost;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int ParkAt()
        {
            Console.WriteLine("1.Chose parking spot for me");
            Console.WriteLine("2.I want to chose a parking spot");
            bool ok = Int32.TryParse(Console.ReadLine(), out int userChoice);
            if (ok && (userChoice == 1 || userChoice == 2))
            {
                return userChoice;
            }
            else
            {
                return -1;
            }
        }

        public static int ChooseASpot()
        {
            Console.WriteLine("Where do you want to park it?");
            bool ok = Int32.TryParse(Console.ReadLine(), out int userChoice);
            if (ok && (userChoice >=0 || userChoice <100))
            {
                return userChoice;
            }
            else
            {
                return -1;
            }
        }
    }
}
