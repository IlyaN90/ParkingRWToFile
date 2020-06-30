using Prague_parking_v2._0.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_parking_v2._0.Parkering
{
    class ParkeringsPlats : IEnumerable
    {
        private readonly int maxSize;
        private int spaceLeft;
        private int indexer;
        private int parkingCellNumber;

        List<Vehicle> vehicles = new List<Vehicle>();

        public ParkeringsPlats(int parkingCellNumber)
        {
            this.maxSize = 100;
            this.indexer = 0;
            this.spaceLeft = this.maxSize;
            this.parkingCellNumber = parkingCellNumber;
        }

        public IEnumerator GetEnumerator()
        {
            return vehicles.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return this.vehicles.Count;
            }
        }

        public int MaxSize
        {
            get
            {
                return this.maxSize;
            }
        }

        public int ParkingCellNumber
        {
            get
            {
                return this.parkingCellNumber;
            }
        }

        public int SpaceLeft
        {
            get
            {
                return this.spaceLeft;
            }
        }

        public int Indexer
        {
            get 
            { 
                return this.indexer;
            }
        }

        public bool ParkVehicle(Vehicle v, int indexer)
        {
            if (this.spaceLeft >= v.VehicleSize)
            {
                vehicles.Add(v);
                this.spaceLeft -= v.VehicleSize;
                return true;
            }
            return false;
        }
        
        public Vehicle RemoveVehicle(string regNum)
        {
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.RegNum == regNum)
                {
                    vehicles.Remove(vehicle);
                    spaceLeft += vehicle.VehicleSize;
                    return vehicle;
                }
            }
            return null;          
        }

        public bool Search(string regNum)
        {
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.RegNum == regNum)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Search(Vehicle v)
        {
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.RegNum == v.RegNum)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            string output = string.Empty;
            int i = 0;
            foreach(Vehicle v in vehicles)
            {
                    if (i != 0)
                    {
                        output += "\n";
                    }
                    output += this.parkingCellNumber + "," + v.ToString();
                    i++;
            }
            return output;
        }
    }
}
