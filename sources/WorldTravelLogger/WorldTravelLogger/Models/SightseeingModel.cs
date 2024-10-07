using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class SightseeingModel: BaseContext
    {
        private string? context_;                   // 内容
        private SightseeigType sightseeigTypecs_;   // 観光種別

        public SightseeingModel() :
            base()
        {
            context_ = null;
            sightseeigTypecs_ = SightseeigType.Visiting;

        }

        public SightseeingModel(string? context, 
            SightseeigType sightseeigType, 
            DateTime date, 
            CountryType country, 
            string? region, 
            double price, 
            CurrencyType currency, 
            string? memo):
            base(date,country, region, price, currency, memo)
        {
            context_ = context;
            sightseeigTypecs_ = sightseeigType;

        }

        public string? Context
        {
            get { return context_; }    
        }

        public SightseeigType SightseeigType
        {
            get { return sightseeigTypecs_; }
        }


    }
}
