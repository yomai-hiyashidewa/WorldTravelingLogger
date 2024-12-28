using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models.Base;
using WorldTravelLogger.Models.Enumeration;

namespace WorldTravelLogger.Models.Context
{
    public class TransportationTypeModel : BaseTypeModel
    {

        public double TotalDistance { get; private set; }
        public int TotalTime { get; private set; }

        public double MaxDistance { get; private set; }

        public double MinDistance { get; private set; }

        public int MaxTime { get; private set; }

        public int MinTime { get; private set; }

        public TransportationTypeModel(Transportationtype type) : 
            base(type.ToString())
        {
            TotalDistance = 0.0;
            TotalTime = 0;
            MaxDistance = 0.0;
            MinDistance = 0.0;
            MaxTime = 0;
            MinTime = 0;
        }

        public void SetParameter(double distance, int time)
        {
            TotalDistance += distance;
            TotalTime += time;
            if (MaxDistance == 0 || MaxDistance < distance)
            {
                MaxDistance = distance;
            }
            if (MinDistance == 0 || MinDistance < MinCost)
            {
                MinDistance = distance;
            }
            if (MaxTime == 0 || MaxTime < time)
            {
                MaxTime = time;
            }
            if (MinTime == 0 || MinTime < time)
            {
                MinTime = time;
            }
        }


        public double AveDistance
        {
            get
            {
                if(Count > 0)
                {
                    return TotalDistance / Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        public double AveTime
        {
            get
            {
                if (Count > 0)
                {
                    return TotalTime / Count;
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}
