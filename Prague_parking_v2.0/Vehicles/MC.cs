﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_parking_v2._0.Vehicles
{
    class MC:Vehicle, iPriceCalculator
    {
        private readonly int vehicleSize;
        private readonly string vehicleType;
        private readonly string regNum;
        private readonly DateTime parkedDateTime;

        public MC(string regNum)
        {
            this.vehicleType = "mc";
            this.vehicleSize = 50;
            this.regNum = regNum;
            this.parkedDateTime = DateTime.Now;
        }
        public MC(DateTime dt)
        {
            this.parkedDateTime = dt;
        }

        public MC(string regNum, string vehicleType, int vehicleSize, DateTime parkedDateTime)
        {
            this.vehicleType = vehicleType;
            this.vehicleSize = 50;
            this.regNum = regNum;
            this.parkedDateTime = parkedDateTime;
        }

        public override int VehicleSize
        {
            get
            {
                return this.vehicleSize;
            }
        }

        public override string RegNum
        {
            get
            {
                return this.regNum;
            }
        }

        public override string VehicleType
        {
            get
            {
                return this.vehicleType;
            }
        }

        public override DateTime ParkedDateTime
        {
            get
            {
                return this.parkedDateTime;
            }
        }

        double iPriceCalculator.CalculateCost()
        {
            DateTime removedAt = DateTime.Now;
            TimeSpan result = removedAt - this.parkedDateTime;
            bool ok = Double.TryParse(result.TotalMinutes.ToString(), out double totalMinutesD);
            int totalMinutes = Convert.ToInt32(totalMinutesD);
            Menu.PrintGreen("Total time: " + totalMinutes + " minutes");
            if (ok)
            {
                double cost = 10;
                totalMinutes = totalMinutes - 5;
                if (totalMinutes < 1)
                {
                    return 0;
                }
                else if (totalMinutes <= 120)
                {
                    return cost * 2;
                }
                else if (totalMinutes > 120)
                {
                    int hours = (int)totalMinutes / 60;
                    return cost * (hours + 1);
                }
            }
            return -1;
        }
    }
}
