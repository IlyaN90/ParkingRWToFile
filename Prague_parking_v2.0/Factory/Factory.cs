using Prague_parking_v2._0.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_parking_v2._0
{
    class Factory 
    {
        public void getCost(DateTime parkedDateTime, string type)
        {
            if (type.ToLower().Equals("car")) 
            { 
                Singelton singleton = Singelton.Instance;
                iPriceCalculator iCar = new Car(parkedDateTime);
                singleton.Return_Cost = iCar;
            }
            else if (type.ToLower().Equals("mc"))
            {
                Singelton singleton = Singelton.Instance;
                iPriceCalculator iMC = new MC(parkedDateTime);
                singleton.Return_Cost = iMC;
            }
            else
            {
                Singelton singleton = Singelton.Instance;
                iPriceCalculator iCustom = new CustomVehicle(parkedDateTime);
                singleton.Return_Cost = iCustom;
            }
        }

        public void onCreate(string regNum, DateTime parkedDateTime, string type="", int size=0)
        {
            Singelton singleton = Singelton.Instance;

            if (type.ToLower().Equals("car"))
            {
                Vehicle car = new Car(regNum, type, size, parkedDateTime);
                singleton.Return_Vehicle = car;
            }
            else if (type.ToLower().Equals("mc"))
            {
                Vehicle mc = new MC(regNum, type, size, parkedDateTime);
                singleton.Return_Vehicle = mc;
            }
            else if (!type.ToLower().Equals("mc")&& !type.ToLower().Equals("car"))
            {
                Vehicle custom = new CustomVehicle(regNum, type, size, parkedDateTime);
                singleton.Return_Vehicle = custom;
            }
        }
    }
}
