using Prague_parking_v2._0.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_parking_v2._0.Parkering
{
    class Parkering: IEnumerable
    {
        private ParkeringsPlats[] rutor; 
        private int indexer;
        public Parkering()
        {
            rutor = new ParkeringsPlats[100];
            for (int i = 0; i <rutor.Length; i++)
            {
                ParkeringsPlats p = new ParkeringsPlats(i);
                rutor[i]=p;
            }
            this.indexer = 0;
        }
        public IEnumerator GetEnumerator()
        {
            return rutor.GetEnumerator();
        }
        public ParkeringsPlats this[int i]
        {
            get 
            {
                if (i < 0 || i>this.rutor.Length-1)
                {
                    return null;
                }
                Console.WriteLine(i);
                return rutor[i];
            }
        }
        public int Park(Vehicle v)
        {
            if (Search(v) < 0) 
            { 
                foreach(ParkeringsPlats pp in rutor)
                {
                   if (v != null && v.VehicleSize <= pp.SpaceLeft)
                    {
                        pp.ParkVehicle(v, this.indexer);
                        Menu.PrintGreen($"{v.ToString()} är nu parkerad på {pp.ParkingCellNumber}");
                        return pp.ParkingCellNumber;
                    }
                }
            }
            else
            {
                Menu.PrintError($"Vehicle with regestration number {v.RegNum} is already parked!");
            }
            return -1;
        }
        public int Park(Vehicle v, int parkingCellNumber)
        {
            if (Search(v) < 0)
            {
                foreach (ParkeringsPlats pp in rutor)
                {
                    if (parkingCellNumber >= 0 && parkingCellNumber < rutor.Length && v != null && v.VehicleSize <= this.rutor[parkingCellNumber].SpaceLeft)
                    {
                        rutor[parkingCellNumber].ParkVehicle(v, parkingCellNumber);
                        Menu.PrintGreen($"{v.ToString()} är nu parkerad på {parkingCellNumber}");
                        return rutor[parkingCellNumber].ParkingCellNumber;
                    }
                }
                Menu.PrintError("Can't do that");
            }
            return -1;
        }
        public int Search(Vehicle v)
        {
            foreach(ParkeringsPlats pp in rutor)
            {
                if (pp.Search(v))
                {
                    return pp.ParkingCellNumber;
                }
            }
            return -1;
        }
        public int Search(string regNum)
        {
            foreach (ParkeringsPlats pp in rutor)
            {
                if (pp.Search(regNum))
                {
                    return pp.ParkingCellNumber;
                }
            }
            return -1;
        }
        public ParkeringsPlats[] Overview()
        {
            return this.rutor;
        }
        public bool Write()
        {
            if (ReadWrite.Write(rutor))
            {
                Menu.PrintGreen("Parking was saved to a file");
                return true;
            }
            return false;
            
        }
        public void PrintOverview()
        {
            if (this.rutor != null)
            {
                int i = 0;
                foreach (ParkeringsPlats pp in rutor)
                {
                    if (pp.SpaceLeft == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(pp);
                    }
                    else if (pp.SpaceLeft == 100)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("{0} is empty ",i);
                    }
                    else if (pp.SpaceLeft < 100)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(pp);
                    }
                    i++;
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        public ParkeringsPlats[] Read()
        {
            if(ReadWrite.Read(out ParkeringsPlats[] output))
            {
                this.rutor = output;
                if (rutor != null)
                {
                    PrintOverview();
                }
                else 
                {
                    Menu.PrintError("Parkeringen finns inte!"); 
                }   
                return rutor;
            }
            return null;
        }
        public Vehicle Remove(string regNum)
        {
            int theSpot = Search(regNum);
            if (theSpot >= 0)
            {
                Vehicle v = rutor[theSpot].RemoveVehicle(regNum);
                DateTime dt= v.ParkedDateTime;
                string type = v.VehicleType;
                Factory factory = new Factory();
                Singelton S = Singelton.Instance;
                factory.getCost(dt, type);
                Menu.PrintGreen($"Cost for {v.VehicleType} {v.RegNum}: {S.Return_Cost.CalculateCost()}");
                return v;
            }
            else
            {
                Menu.PrintError($"{regNum} is not parked");
            }
            return null;
        }
        public bool Sort()
        {
            bool ok = false;
            while (!ok) 
            { 
                int freeSpot = FindFreeSpot();
                if (findLastVehicle() != null)
                {
                    int lastParkedVehicle=Search(findLastVehicle());
                    if (freeSpot < lastParkedVehicle)
                    {
                           Menu.PrintGreen(Move(findLastVehicle(), freeSpot,out string error));
                    }
                    else { ok = true; }
                } else 
                {
                    Console.WriteLine("Something is very wrong with Sort()");
                    ok = true; 
                }
            }
            SortMC();
            return ok;
        }
        public bool SortMC()
        {
            bool ok = false;
            while (!ok) 
            {
                Vehicle v = FindLonleyMC();
                int sweetSpot=FindSweetSpot(v.VehicleSize);
                if (sweetSpot < 0)
                {
                    ok = true;
                }
                else
                {
                    if (FindLonleyMC() != null) 
                    {   
                        if (sweetSpot != Search(v)&&(v.VehicleSize<=rutor[sweetSpot].SpaceLeft))
                        {
                            Menu.PrintGreen(Move(v.RegNum, sweetSpot, out string moveError));
                        }
                        else
                        {
                            ok = true;
                        }
                    }
                    else
                    {
                        ok = true;
                    }
                }
            }
            return ok;
        }
        private string findLastVehicle()
        {
            foreach (ParkeringsPlats pp in rutor.Reverse())
            {
                foreach (Vehicle v in pp)
                {
                    return v.RegNum;
                }
            }
            return null;
        }
        private Vehicle FindLonleyMC()
        {
            foreach (ParkeringsPlats pp in rutor.Reverse())
            {
                if (pp.SpaceLeft > 0 && pp.SpaceLeft < 100)
                {
                    foreach(Vehicle v in pp)
                    {
                        return v;
                    }
                }
            }
            return null;
        }
        private int FindSweetSpot()
        {
            foreach (ParkeringsPlats pp in rutor)
            {
                if (pp.SpaceLeft > 0 && pp.SpaceLeft < 100)
                {
                    return pp.ParkingCellNumber;
                }
            }
            return -1;
        }
        private int FindSweetSpot(int size)
        {
            foreach (ParkeringsPlats pp in rutor)
            {
                if (pp.SpaceLeft > size && pp.SpaceLeft < 100)
                {
                    return pp.ParkingCellNumber;
                }
            }
            return -1;
        }
        private int FindFreeSpot()
        {
            foreach (ParkeringsPlats pp in rutor)
            {
                if (pp.SpaceLeft==100)
                {
                    return pp.ParkingCellNumber;
                }
            }
            return -1;
        }
        public string Move(string regNum, int newSpot,out string error)
        {
            if(newSpot>=0 && newSpot < 100) 
            { 
                int oldSpot = Search(regNum);
                error = "";
                if (oldSpot >= 0 && newSpot != oldSpot)
                {
                    foreach (Vehicle v in rutor[oldSpot])
                    {
                        if (v.RegNum == regNum && rutor[newSpot].SpaceLeft >= v.VehicleSize)
                        {
                            rutor[oldSpot].RemoveVehicle(regNum);
                            rutor[newSpot].ParkVehicle(v, newSpot);
                            return $"Flytta {v.VehicleType}, {v.RegNum} från {oldSpot} till {newSpot}";
                        }
                    }
                }
                error = "Can't move!";
                return "Can't move!";
            }else
            { 
                error= "This parking spot doesnt exsist";
                return "This parking spot doesnt exsist"; }
        }
    }
}
