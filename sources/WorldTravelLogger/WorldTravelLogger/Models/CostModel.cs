using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class CostModel
    {
        private CurrencyType currencytype_;
        private Dictionary<ListType, double> costs_;

        public CostModel(CurrencyType currencytype)
        {
            currencytype_ = currencytype;
            costs_ = new Dictionary<ListType, double>();
        }

        public void Set(ListType type, double cost)
        {
            costs_[type] = cost;
        }

      

        public string Total
        {
            get
            {
                return CurrencyConverter.GetCurrencyStr(currencytype_, 
                    costs_.Values.Sum());
                
            }
        }

        public string Accomodation
        {
            get
            {
                return CurrencyConverter.GetCurrencyStr(currencytype_,
                    costs_[ListType.AccomodationList]);
            }
        }

        public string Transportation
        {
            get
            {
                return CurrencyConverter.GetCurrencyStr(currencytype_,
                  costs_[ListType.TransportationList]);
            }
        }

        public string Sightseeing
        {
            get
            {
                return CurrencyConverter.GetCurrencyStr(currencytype_,
                  costs_[ListType.SightSeeingList]);

            }
        }

        public string Other
        {
            get
            {
                return CurrencyConverter.GetCurrencyStr(currencytype_,
                  costs_[ListType.Other]);
            }
        }


    }
}
