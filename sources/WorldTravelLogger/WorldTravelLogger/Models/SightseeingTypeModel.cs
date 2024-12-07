using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class SightseeingTypeModel : BaseTypeModel
    {
        public SightseeingTypeModel(SightseeigType type) : 
            base(type.ToString())
        {
        }
    }
}
