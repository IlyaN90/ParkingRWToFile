using Prague_parking_v2._0.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_parking_v2._0
{
    public sealed class Singelton
    {
        private iPriceCalculator PriceCalculator;
        private Vehicle v;
        private static Singelton instance = null;
        private Singelton()
        {
        }
        public iPriceCalculator Return_Cost
        {
            get
            {
                return PriceCalculator;
            }
            set
            {
                PriceCalculator = value;
            }
        }
        public Vehicle Return_Vehicle
        {
            get
            {
                return v;
            }
            set
            {
                v = value;
            }
        }
        public static Singelton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singelton();
                }
                return instance;
            }
        }
    }
}
