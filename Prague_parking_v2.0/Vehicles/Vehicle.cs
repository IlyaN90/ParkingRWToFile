using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_parking_v2._0.Vehicles
{
    public abstract class Vehicle : IComparable
    {
        public abstract string RegNum { get; }
        public abstract int VehicleSize { get; }
        public abstract string VehicleType { get; }
        public abstract DateTime ParkedDateTime { get; }
        public override string ToString()
        {
            return $"{this.RegNum},{this.VehicleType},{this.VehicleSize},{this.ParkedDateTime}";
        }
        public int CompareTo(object obj)
        {
            Vehicle v = (Vehicle)obj;
            return RegNum.CompareTo(v.RegNum);
        }
    }
}
