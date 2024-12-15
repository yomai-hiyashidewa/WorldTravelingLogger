using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class AccomodationList : BaseList
    {
        private List<AccomodationModel> list_;
        private List<AccomodationModel> calcList_;
        private Dictionary<AccomodationType, AccomodationTypeModel> calcDic_;

        public AccomodationType CurrentAccomodationtype { get; set; }

        public override bool IsLoaded
        {
            get { return list_.Count > 0;}
        }

       

        public AccomodationList()
        {
            list_ = new List<AccomodationModel>();
            calcList_ = new List<AccomodationModel>();
            calcDic_ = new Dictionary<AccomodationType, AccomodationTypeModel>();
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
                    base.SetCountry(model.Country);
                }
            }
        }

        public override void Init()
        {
            base.Init();
            list_.Clear();
        }

        // 別通貨へ両替する
        public override void ConvertAnotherCurrency(ExchangeRater rater)
        {
            foreach(var model in list_)
            {
                model.ConvertPrice(rater);
            }
           
        }

        public override void CalcModels(bool isWorld, CountryType type, DateTime start, DateTime end)
        {
            calcList_.Clear();
            calcDic_.Clear();
            if (isWorld)
            {
                foreach(var model in list_.Where(m => start <= m.Date && m.Date <= end))
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
               
                AccomodationTypeModel tModel;
                if (calcDic_.TryGetValue(model.Accomodation, out tModel))
                {
                    tModel.Set(model.JPYPrice);
                }
                else
                {
                    tModel = new AccomodationTypeModel(model.Accomodation);
                    tModel.Set(model.JPYPrice);
                    calcDic_.Add(model.Accomodation, tModel);
                }
            }
            if (calcDic_.Count >= 0 && !calcDic_.ContainsKey(CurrentAccomodationtype))
            {
                CurrentAccomodationtype = calcDic_.Keys.FirstOrDefault();
            }

        }

        

        public AccomodationTypeModel[] GetTypeArray()
        {
            var list = new List<AccomodationTypeModel>();
            foreach(var pair in calcDic_)
            {
                list.Add(pair.Value);
            }
            return list.ToArray();
        }

        public AccomodationModel[] GetCalcArray()
        {
            return calcList_.ToArray();
        }

        public IEnumerable<AccomodationType> GetCurrentAccomodationTypes()
        {
            return calcDic_.Keys;
        }

        public override IEnumerable<CountryType> GetCalcCounties()
        {
            var sets = new HashSet<CountryType>();
            foreach(var model in calcList_)
            {
                if (!sets.Contains(model.Country))
                {
                    sets.Add(model.Country);
                }
            }
            foreach(var c in sets)
            {
                yield return c;
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

       
    }
}
