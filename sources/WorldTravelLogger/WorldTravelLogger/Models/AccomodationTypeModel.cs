using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class AccomodationTypeModel
    {
        public AccomodationTypeModel(AccomodationType type)
        {
            Type = type;
            Count = 0;
            TotalCost = 0;
        }

        public void Set(double cost)
        {
            Count++;
            TotalCost += cost;
        }
        // 日本円で計算
        public AccomodationType Type { get; private set; }
        public int Count { get; private set; }
        public double TotalCost { get; private set; }

        public double AveCoast 
        { 
            get
            {
                if (Count > 0)
                {
                    return TotalCost / Count;
                }
                else
                {
                    return 0;
                }
            } 
        }

      

    }
}
