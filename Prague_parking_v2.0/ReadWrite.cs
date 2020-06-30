using Prague_parking_v2._0.Parkering;
using Prague_parking_v2._0.Vehicles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_parking_v2._0
{
    class ReadWrite
    {
        //path till txt filen
        static string path = @"C:\Users\nagib\Desktop\Parking.txt";
        public static bool Write(ParkeringsPlats[] parkering)
        {
            bool ok = false;
            try 
            { 
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
                {
                    foreach (ParkeringsPlats pp in parkering)
                    {
                        if (pp.Count > 0)
                        {
                            file.WriteLine(pp);
                        }
                    }
                    ok = true;
                }
                return ok;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                FileAttributes attr = (new FileInfo(path)).Attributes;
                Console.Write("UnAuthorizedAccessException: Unable to access file. ");
                if ((attr & FileAttributes.ReadOnly) > 0)
                    Console.Write("The file is read-only.");
                return false;
            }
        }

        public static bool Read(out ParkeringsPlats[] rutor)
        {
            try 
            {
                string[] vehicles = System.IO.File.ReadAllLines(path);
                string regNum = string.Empty;
                string type = string.Empty;
                DateTime dateTime = DateTime.Now;
                int size = 0;
                int parkingCellNumber = 0;
                Parkering.Parkering parkering = new Parkering.Parkering();
                foreach (string vehicle in vehicles)
                {
                    string[] tempVehicle = vehicle.Split(',');
                    if (vehicle.Length > 0)
                    {
                        try
                        {
                            parkingCellNumber = Int32.Parse(tempVehicle[0]);
                            regNum = tempVehicle[1];
                            type = tempVehicle[2];
                            size = Int32.Parse(tempVehicle[3]);
                            dateTime = DateTime.Parse(tempVehicle[4]);
                        }
                        catch (FormatException)
                        {
                            Menu.PrintError($"something went wrong while reading from {path} ");
                            rutor = null;
                            return false;
                        }
                        Factory factory = new Factory();
                        Singelton S = Singelton.Instance;
                        factory.onCreate(regNum, dateTime, type, size);
                        parkering.Park(S.Return_Vehicle, parkingCellNumber);
                    }
                }
                rutor = parkering.Overview();
                if (rutor.Length <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
                rutor = null;
                return false;
            }
            catch(DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex);
                rutor = null;
                return false;
            }
        }
    }
}
