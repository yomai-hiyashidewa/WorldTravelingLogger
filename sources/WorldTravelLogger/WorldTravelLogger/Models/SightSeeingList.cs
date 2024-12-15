using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.CompilerServices;

namespace WorldTravelLogger.Models
{
    public class SightSeeingList : BaseList
    {
        private List<SightseeingModel> list_;
        private List<SightseeingModel> calcList_;
        private Dictionary<SightseeigType, SightseeingTypeModel> calcDic_;

        public override bool IsLoaded
        {
            get { return list_.Count > 0; }
        }

        public SightseeigType CurrentSightSeeingType { get; set; }


        public SightSeeingList()
        {
            list_ = new List<SightseeingModel>();
            calcList_ = new List<SightseeingModel>();
            calcDic_ = new Dictionary<SightseeigType, SightseeingTypeModel>();
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
                base.SetCountry(model.Country);
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

     

        public SightseeingTypeModel[] GetTypeArray()
        {
            var list = new List<SightseeingTypeModel>();
            foreach (var pair in calcDic_)
            {
                list.Add(pair.Value);
            }
            return list.ToArray();
        }

        public SightseeingModel[] GetCalcArray()
        {
            return calcList_.ToArray();
        }

        public override void CalcModels(bool isWorld, CountryType type, DateTime start, DateTime end)
        {
            calcList_.Clear();
            calcDic_.Clear();
            if (isWorld)
            {
                foreach (var model in list_.Where(m => start <= m.Date && m.Date <= end))
                {
                    calcList_.Add(model);
                }
            }
            else
            {
                foreach (var model in list_.Where(m => m.Country == type &&
                start <= m.Date && m.Date <= end))
                {
                    calcList_.Add(model);
                }
            }
            foreach (var model in calcList_)
            {

                SightseeingTypeModel tModel;
                if (calcDic_.TryGetValue(model.SightseeigType, out tModel))
                {
                    tModel.Set(model.JPYPrice);
                }
                else
                {
                    tModel = new SightseeingTypeModel(model.SightseeigType);
                    tModel.Set(model.JPYPrice);
                    calcDic_.Add(model.SightseeigType, tModel);
                }
            }
            if (calcDic_.Count >= 0 && !calcDic_.ContainsKey(CurrentSightSeeingType))
            {
                CurrentSightSeeingType = calcDic_.Keys.FirstOrDefault();
            }
        }

        public override double TotalCost
        {
            get
            {
                double sum = 0;
                foreach (var pair in calcDic_)
                {
                    sum += pair.Value.TotalCost;
                }
                return sum;
            }
        }

       

        // others
        private bool CheckOthers(SightseeigType type)
        {
            if (type == SightseeigType.Insurance ||
                type == SightseeigType.Ticket ||
                type == SightseeigType.Accident ||
                type == SightseeigType.Other ||
                type == SightseeigType.Shopping ||
                type == SightseeigType.Medical ||
                type == SightseeigType.Washing ||
                type == SightseeigType.Tax ||
                type == SightseeigType.Exchange ||
                type == SightseeigType.Cashing ||
                type == SightseeigType.Haircut ||
                type == SightseeigType.Tips ||
                type == SightseeigType.PartTimeJob ||
                type == SightseeigType.Toilet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<SightseeingModel> ExportOthers()
        {
            var list = new List<SightseeingModel>();
            foreach(var model in list_.Where(m => CheckOthers(m.SightseeigType)).ToArray())
            {
                list.Add(model);
                list_.Remove(model);
            }
            return list;
        }

        public void ImportOthers(List<SightseeingModel> list)
        {
            list_ = list;
        }

        public IEnumerable<SightseeigType> GetCurrentSightSeeingTypes()
        {
            return calcDic_.Keys;
        }

        public override IEnumerable<CountryType> GetCalcCounties()
        {
            var sets = new HashSet<CountryType>();
            foreach (var model in calcList_)
            {
                if (!sets.Contains(model.Country))
                {
                    sets.Add(model.Country);
                }
            }
            foreach (var c in sets)
            {
                yield return c;
            }
        }
    }
}
