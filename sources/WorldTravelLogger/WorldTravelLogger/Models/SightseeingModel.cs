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
            ResetType();
        }

        private void ResetType()
        {
            if (string.IsNullOrWhiteSpace(context_))
            {
                return;
            }
            if (sightseeigTypecs_ == SightseeigType.Visiting || sightseeigTypecs_ == SightseeigType.Walking)
            {
                var upperC = context_.ToUpper();
                if (upperC.Contains("BEACH"))
                {
                    sightseeigTypecs_ = SightseeigType.Beach;
                }
                else if (upperC.Contains("BAY") || upperC.Contains("CAVE") || upperC.Contains("VALLY") ||
                    upperC.Contains("LAKE"))
                {
                    sightseeigTypecs_ = SightseeigType.Nature;
                }
                else if (upperC.Contains("MUSEUM"))
                {
                    sightseeigTypecs_ = SightseeigType.Museum;
                }
                else if (upperC.Contains("CHURCH") || upperC.Contains("CATHEDRAL") || 
                    upperC.Contains("MOSK") || upperC.Contains("SHRINE"))
                {
                    sightseeigTypecs_ = SightseeigType.Church;
                }
                else if (upperC.Contains("ZOO"))
                {
                    sightseeigTypecs_ = SightseeigType.Zoo;
                }
                else if (upperC.Contains("HERITAGE"))
                {
                    sightseeigTypecs_ = SightseeigType.Heritage;
                }
                else if (upperC.Contains("OVERVIEWING"))
                {
                    sightseeigTypecs_ = SightseeigType.Overviewing;
                }
                else if (upperC.Contains("WATERFALL"))
                {
                    sightseeigTypecs_ = SightseeigType.Waterfall;
                }
                else if (upperC.Contains("CASTLE") || upperC.Contains("FORTLESS") ||
                    upperC.Contains("PALACE"))
                {
                    sightseeigTypecs_ = SightseeigType.Castle;
                }
                else if (upperC.Contains("PARK") || upperC.Contains("GARDEN"))
                {
                    sightseeigTypecs_ = SightseeigType.Park;
                }
            }
            


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
