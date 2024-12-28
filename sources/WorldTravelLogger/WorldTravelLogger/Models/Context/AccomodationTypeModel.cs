using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models.Base;
using WorldTravelLogger.Models.Enumeration;

namespace WorldTravelLogger.Models.Context
{
    public class AccomodationTypeModel : BaseTypeModel
    {
        public AccomodationTypeModel(AccomodationType type) :
            base(type.ToString())
        {
            
        }
    }
}
