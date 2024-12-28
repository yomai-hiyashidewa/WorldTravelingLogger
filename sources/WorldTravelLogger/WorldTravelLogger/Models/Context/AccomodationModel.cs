using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models.Base;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.Models.Interface;

namespace WorldTravelLogger.Models.Context
{
    public class AccomodationModel : BaseContext
    {
        private AccomodationType accomodation_;     // 宿泊先

        public AccomodationModel() :
            base()
        {
            accomodation_ = AccomodationType.Domitory;
        }

        public AccomodationModel(DateTime date,
            CountryType country,
            string? region,
            AccomodationType accomodation,
            double price,
            CurrencyType currency, 
            string? memo) :
            base(date, country, region, price, currency, memo)
        {
            this.accomodation_ = accomodation;
        }

        public AccomodationType Accomodation { get { return accomodation_; } }
    }
}
