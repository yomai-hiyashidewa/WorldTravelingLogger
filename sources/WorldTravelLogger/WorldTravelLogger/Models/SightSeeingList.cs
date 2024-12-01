using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorldTravelLogger.Models
{
    internal class SightSeeingList : BaseList
    {
        private List<SightseeingModel> list_;

        public override bool IsLoaded
        {
            get { return list_.Count > 0; }
        }

        public SightSeeingList()
        {
            list_ = new List<SightseeingModel>();
        }

        public SightseeingModel[] GetArray()
        {
            return list_.ToArray();
        }

        public override void Init()
        {
            base.Init();
            list_.Clear();
        }

        private void CheckFormats(int index, string[] row)
        {
            for (var j = 0; j < row.Length; j++)
            {
                var str = row[j];
                bool flag = false;
                switch (j)
                {
                    // context
                    case 0:
                        flag = string.IsNullOrWhiteSpace(str);
                        break;
                    // type
                    case 1:
                        var sType = base.ConvertSightSeeingType(str);
                        flag = sType == null;
                        break;
                    // date
                    case 2:
                        var date = base.ConvertDate(str);
                        flag = date == null;
                        break;
                    // country
                    case 3:
                        var c = base.ConvertCountry(str);
                        flag = c == null;
                        break;
                    // region
                    case 4:
                        flag = string.IsNullOrWhiteSpace(str);
                        break;
                    // price
                    case 5:
                        var price = base.ConvertDouble(str);
                        flag = price == null;
                        break;
                    // currency
                    case 6:
                        var cType = base.ConvertCurrency(str);
                        flag = cType == null;
                        break;
                    // memo
                    case 7:
                       // none
                        break;


                }
                if (flag)
                {
                    base.SetErrorList(index, j, str);
                }
            }
        }

        private void SetContext(string[] row)
        {
            string? context = null;
            SightseeigType? sType = null;
            DateTime? date = null; ;
            CountryType? country = null;
            string? region = null;
            double? price = null;
            CurrencyType? currencyType = null;
            string? memo = null;
            for (var j = 0; j < row.Length; j++)
            {
                var str = row[j];
                
                switch (j)
                {
                    // context
                    case 0:
                        context = str;
                        break;
                    // type
                    case 1:
                        sType = base.ConvertSightSeeingType(str);
                        break;
                    // date
                    case 2:
                        date = base.ConvertDate(str);
                        break;
                    // country
                    case 3:
                        country = base.ConvertCountry(str);
                        break;
                    // region
                    case 4:
                        region = str;
                        break;
                        // price
                    case 5:
                        price = base.ConvertDouble(str);
                        break;
                    // currency
                    case 6:
                        currencyType = base.ConvertCurrency(str);
                        break;
                    // memo
                    case 7:
                        memo = str;
                        break;
                  
                 
                }
            }
            if (context != null &&
                   sType != null &&
                   date != null &&
                   country != null &&
                   region != null &&
                   price != null &&
                   currencyType != null
                   )
            {
                var model = new SightseeingModel(context, (SightseeigType)sType, (DateTime)date, (CountryType)country,
                    region, (double)price, (CurrencyType)currencyType, memo);
                list_.Add(model);
            }
        }



        protected override bool CheckFormat(object[] arrays)
        {
            var length = arrays.Length;
            for (var i = 1; i < length; i++)
            {
                CheckFormats(i, (string[])arrays[i]);

            }
            return base.IsError;
        }

        protected override void Set(object[] arrays)
        {
            var length = arrays.Length;
            for (var i = 1; i < length; i++)
            {
                SetContext((string[])arrays[i]);

            }
        }

        public override void ConvertAnotherCurrency(ExchangeRater rater)
        {
            foreach(var model in list_)
            {
                model.ConvertPrice(rater);
            }
        }
    }
}
