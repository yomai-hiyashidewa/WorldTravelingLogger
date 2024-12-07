using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class AccomodationTypeModel : BaseTypeModel
    {
        public AccomodationTypeModel(AccomodationType type) :
            base(type.ToString())
        {
            
        }
    }
}
