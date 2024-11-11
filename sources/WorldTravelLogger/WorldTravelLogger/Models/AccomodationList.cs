using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    internal class AccomodationList : BaseList
    {
        private List<AccomodationModel> list_;

        public AccomodationList()
        {
            list_ = new List<AccomodationModel>();
        }

        public AccomodationModel[] GetArray()
        {
            return list_.ToArray();
        }


        



        private void CheckFormats(int index,string[] row)
        {
            for (var j = 0; j < row.Length; j++)
            {
                var str = row[j];
                bool flag = false;
                switch (j)
                {
                    // date
                    case 0:
                        var date = base.ConvertDate(str);
                        flag = date == null;
                        break;
                        // country
                    case 1:
                        var country = base.ConvertCountry(str);
                        flag = country == null;
                        break;
                        // region
                    case 2:
                        // none
                        break;
                        // accomodation
                    case 3:
                        var accomodation = base.ConvertAccomodationType(str);
                        flag = accomodation == null;
                        break;
                        // price
                    case 4:
                        var price = base.ConvertDouble(str);
                        flag = price == null;
                        break;
                        /// currency
                    case 5:
                        var currency = base.ConvertCurrency(str);
                        flag = currency == null;
                        break;
                        // memo
                    case 6:
                        break;
                }
                if (flag)
                {
                    base.SetErrorList(index, j,str);
                }

            }
        }

        private AccomodationModel CreateModel(string[] row)
        {
            DateTime? date = null; 
            CountryType? country = null;
            string region = null;
            AccomodationType? accomodation = null;
            double? price = null;
            CurrencyType? currency = null;
            string memo = null;


            for (var i = 0; i < row.Length; i++)
            {
                var str = row[i];
                switch (i)
                {
                    // date
                    case 0:
                        date = base.ConvertDate(str);
                        break;
                    // country
                    case 1:
                        country = base.ConvertCountry(str);
                        break;
                    // region
                    case 2:
                        region = str;
                        break;
                    // accomodation
                    case 3:
                        accomodation = base.ConvertAccomodationType(str);
                        break;
                    // price
                    case 4:
                        price = base.ConvertDouble(str);
                        break;
                    /// currency
                    case 5:
                        currency = base.ConvertCurrency(str);
                        break;
                    // memo
                    case 6:
                        memo = str;
                        break;
                }
            }
            if(date != null && country != null && region != null && accomodation != null &&
                price != null && memo != null)
            {
                return new AccomodationModel((DateTime)date, (CountryType)country, region, 
                    (AccomodationType)accomodation, (double)price, (CurrencyType)currency, memo);
            }
            else
            {
                return null;
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
            var list = new List<int[]>();
            for (var i = 1; i < length; i++)
            {
                var model = CreateModel((string[])arrays[i]);
                if(model != null)
                {
                    list_.Add(model);
                }
            }
        }

        public override void Init()
        {
            base.Init();
            list_.Clear();
        }
    }
}
