using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public abstract class BaseTypeModel
    {
        public BaseTypeModel(string type)
        {
            Type = type;
            Count = 0;
            TotalCost = 0;
        }

        public virtual void Set(double cost)
        {
            Count++;
            TotalCost += cost;
        }

        public string Type { get; protected set; }

        public int Count { get; protected set; }

        public double TotalCost { get; protected set; }

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
