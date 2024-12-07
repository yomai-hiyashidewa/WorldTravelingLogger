using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class TransportationTypeModel : BaseTypeModel
    {

        public double TotalDistance { get; private set; }
        public int TotalTime { get; private set; }

        public TransportationTypeModel(Transportationtype type) : 
            base(type.ToString())
        {
            TotalDistance = 0.0;
            TotalTime = 0;
        }

        public void SetParameter(double distance, int time)
        {
            TotalDistance += distance;
            TotalTime += time;
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
