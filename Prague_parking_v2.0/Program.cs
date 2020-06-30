using Prague_parking_v2._0.Parkering;
using Prague_parking_v2._0.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//path till txt kan man ställa in i ReadWrite.cs
namespace Prague_parking_v2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Singelton S = Singelton.Instance;
            Parkering.Parkering parkering = new Parkering.Parkering();

            Car car1 = new Car("car1");
            Car car2 = new Car("car2");
            Car car3 = new Car("жэф");
            MC mc1 = new MC("mc1");
            MC mc2 = new MC("mc2");
            MC mc3 = new MC("mc3");
            CustomVehicle cv1 = new CustomVehicle("cv1","cykel",10);
            CustomVehicle cv2 = new CustomVehicle("cv2", "cykel", 10);
            CustomVehicle cv3 = new CustomVehicle("cv3", "cykel", 10);
            CustomVehicle cv4 = new CustomVehicle("cv4", "scooter", 35);
            CustomVehicle cv5 = new CustomVehicle("фый", "skateboard", 5);

            parkering.Park(car1,0);
            parkering.Park(car2,98);
            parkering.Park(car3,99);
            parkering.Park(mc1,5);
            parkering.Park(mc2,15);
            parkering.Park(mc3,16);
            parkering.Park(cv1,17);
            parkering.Park(cv2,18);
            parkering.Park(cv3,19);
            parkering.Park(cv4,20);
            parkering.Park(cv5, 21);

            while (true) {
                int x=Menu.MenuPrint();
                if (x >= 0)
                {
                    switch (x)
                    {
                        case 1:
                            if (Menu.MenuPark())
                            {
                                int userChoise = Menu.ParkAt();
                                if (userChoise == 1)
                                {
                                    parkering.Park(S.Return_Vehicle);
                                }
                                if (userChoise == 2)
                                {
                                    int spot = Menu.ChooseASpot();
                                    parkering.Park(S.Return_Vehicle, spot);
                                }
                            }
                            break;
                        case 2:          
                            if(Menu.MenuRemove(out string regNum))
                            {
                                parkering.Remove(regNum);
                            }
                            break;
                        case 3:
                            if (Menu.MenuMove(out regNum, out int index))
                            {
                               string moved = parkering.Move(regNum, index, out string errorMove);
                                if (errorMove.Equals(""))
                                {
                                    Menu.PrintGreen(moved);
                                }
                                else
                                {
                                    Menu.PrintError(errorMove);
                                }
                            }
                            break;
                        case 4:
                            if (Menu.RegNumCheck(out string userInput, out string error))
                            {
                                int found = parkering.Search(userInput);
                                Menu.PrintGreen($"{userInput} was found at {found}");
                            }
                            else
                            {
                                Menu.PrintError(error);
                            }
                            break;
                        case 5:
                            if(Menu.MenuIndex(out int output))
                            {
                                if (output >= 0&&output<100)
                                {
                                    Menu.PrintGreen(parkering[output].ToString());
                                }
                                else
                                {
                                    Menu.PrintError($"Spot {output}, doesnt exsist");
                                }
                            }
                            break;
                        case 6:
                            if(parkering.Overview().Length>0)
                            {
                                parkering.PrintOverview();
                            }
                            parkering.Overview();
                            break;
                        case 7:
                            parkering.Read();
                            break;
                        case 8:
                            parkering.Write();
                            break;
                        case 9:
                            parkering.SortMC();
                            break;
                        case 10:
                            parkering.Sort();
                            break;
                        default:
                            Console.WriteLine("Default case");
                        break;
                    }
                }
                else
                {
                    Menu.PrintError("Provide a digit between 1-10");
                }
            }
        }
    }
}
