using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class TranspotationList : BaseList
    {
        private List<TransportationModel> list_;
        private List<TransportationModel> calcList_;
        private Dictionary<Transportationtype, TransportationTypeModel> calcDic_;

        public override bool IsLoaded
        {
            get { return list_.Count > 0; }
        }

        

        public bool IsWithAirplane { get; set; }

        public bool IsWithCrossBorder { get; set; }

        public Transportationtype CurrentTransportationType { get; set; }





        public TranspotationList()
        {
            list_ = new List<TransportationModel>();
            calcList_ = new List<TransportationModel>();
            calcDic_ = new Dictionary<Transportationtype, TransportationTypeModel>();
            IsWithAirplane = true;
            IsWithCrossBorder = true;
        }

        public TransportationModel[] GetArray()
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
                    // type
                    case 0:
                        var sttype = base.ConvertTransportationType(str);
                        flag = sttype == null;
                        break;
                        // start
                        // date
                    case 1:
                        var sdate = base.ConvertDate(str);
                        flag = sdate == null;
                        break;
                        // Country
                    case 2:
                        var countury = base.ConvertCountry(str);
                        flag = countury == null;
                        break;
                        // place
                    case 3:
                        flag = string.IsNullOrWhiteSpace(str);
                        break;
                        // place type
                    case 4:
                        var sptype = base.ConvertPlaceType(str);
                        flag = sptype == null;
                        break;
                    // start
                    // date
                    case 5:
                        var edate = base.ConvertDate(str);
                        flag = edate == null;
                        break;
                    // Country
                    case 6:
                        var ecountry = base.ConvertCountry(str);
                        flag = ecountry == null;
                        break;
                    // place
                    case 7:
                        string.IsNullOrWhiteSpace(str);
                        break;
                    // place type
                    case 8:
                        var eptype = base.ConvertPlaceType(str);
                        flag = eptype == null;
                        break;
                    // distance
                    case 9:
                        var val = base.ConvertDouble(str);
                        flag = val == null;
                        break;
                    // time
                    case 10:
                        var time = base.ConvertInt(str);
                        flag = time == null;
                        break;
                    // price
                    case 11:
                        var place = base.ConvertDouble(str);
                        flag = place == null;
                        break;
                    // currency
                    case 12:
                        var currency = base.ConvertCurrency(str);
                        flag = currency == null;
                        break;
                    // memo
                    case 13:
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
            Transportationtype? ttype = null;
            DateTime? startDate = null; ;
            CountryType? startCountry = null;
            string? startPlace = null;
            PlaceType? startPlaceType = null;
            DateTime? endDate = null;
            CountryType? endCountry = null;
            string? endPlace = null;
            PlaceType? endPlaceType = null;
            double? distance = null;
            int? time = null;
            double? price = null;
            CurrencyType? currencyType = null;
            string? memo = null;
            for (var j = 0; j < row.Length; j++)
            {
                var str = row[j];

               
                switch (j)
                {
                    // type
                    case 0:
                        ttype = base.ConvertTransportationType(str);
                        break;
                    // start
                    // date
                    case 1:
                        startDate = base.ConvertDate(str);
                        break;
                    // Country
                    case 2:
                        startCountry = base.ConvertCountry(str);
                        break;
                    // place
                    case 3:
                        startPlace = str;
                        break;
                    // place type
                    case 4:
                        startPlaceType = base.ConvertPlaceType(str);
                        break;
                    // start
                    // date
                    case 5:
                        endDate = base.ConvertDate(str);
                        break;
                    // Country
                    case 6:
                        endCountry = base.ConvertCountry(str);
                        break;
                    // place
                    case 7:
                        endPlace = str;
                        break;
                    // place type
                    case 8:
                        endPlaceType = base.ConvertPlaceType(str);
                        break;
                    // distance
                    case 9:
                        distance = base.ConvertDouble(str);
                        break;
                    // time
                    case 10:
                        time = base.ConvertInt(str);
                        break;
                    // price
                    case 11:
                        price = base.ConvertDouble(str);
                        break;
                    // currency
                    case 12:
                        currencyType = base.ConvertCurrency(str);
                        break;
                    // memo
                    case 13:
                        memo = str;
                        break;
                }
            }
            if (ttype != null &&
                   startDate != null &&
                   startCountry != null &&
                   startPlace != null &&
                   startPlaceType != null &&
                   endDate != null &&
                   endCountry != null &&
                   endPlace != null &&
                   endPlaceType != null &&
                   distance != null &&
                   time != null &&
                   price != null &&
                   currencyType != null
                   )
            {
                var model = new TransportationModel((Transportationtype)ttype, (DateTime)startDate, (CountryType)startCountry, startPlace, (PlaceType)startPlaceType,
                    (DateTime)endDate, (CountryType)endCountry, endPlace, (PlaceType)endPlaceType,
                    (double)distance, (int)time, (double)price, (CurrencyType)currencyType, memo);
                list_.Add(model);
                base.SetCountry(model.StartCountry,model.StartRegion);
                base.SetCountry(model.EndCountry,model.EndRegion);
                base.SetDate(model.StartDate);
                base.SetDate(model.EndDate);
            }
        }

        protected override bool CheckFormat(object[] arrays)
        {
            var length = arrays.Length;
            for (var i = 2; i < length; i++)
            {
                CheckFormats(i, (string[])arrays[i]);

            }
            return base.IsError;
        }



        protected override void Set(object[] arrays)
        {
            var length = arrays.Length;
            for (var i = 2; i < length; i++)
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

       

        public TransportationTypeModel[] GetTypeArray()
        {
            var list = new List<TransportationTypeModel>();
            foreach (var pair in calcDic_)
            {
                list.Add(pair.Value);
            }
            return list.ToArray();
        }

        public TransportationModel[] GetCalcArray()
        {
            return calcList_.ToArray();
        }

        public IEnumerable<Transportationtype> GetCurrentTransportationTypes()
        {
            return calcDic_.Keys;
        }

        public bool GetNeedingEndDate()
        {
            var model = calcList_.FirstOrDefault(m => m.IsSameDate);
            return model != null;
        }

        private IEnumerable<TransportationModel> GetList(bool isWorld, CountryType type, DateTime start, DateTime end)
        {
            // dateの設定に関してはまた後で考える
            if (isWorld)
            {
                if (IsWithAirplane)
                {
                    return list_.Where(m => start <= m.Date && m.Date <= end);
                }
                else
                {
                    return list_.Where(m => start <= m.Date && m.Date <= end && m.Transportationtype != Transportationtype.AirPlane);
                }
            }
            
            else
            {
                if (IsWithAirplane)
                {
                    if (IsWithCrossBorder)
                    {
                        return list_.Where(m => (m.StartCountry == type || m.EndCountry == type) &&
                       start <= m.Date && m.Date <= end);
                    }
                    else
                    {
                        return list_.Where(m => (m.StartCountry == type && m.EndCountry == type) &&
                    start <= m.Date && m.Date <= end);
                       
                    }
                }
                else
                {
                    if (IsWithCrossBorder)
                    {
                        return list_.Where(m => (m.StartCountry == type || m.EndCountry == type) &&
                       start <= m.Date && m.Date <= end && m.Transportationtype != Transportationtype.AirPlane);
                    }
                    else
                    {
                        return list_.Where(m => (m.StartCountry == type && m.EndCountry == type) &&
                        start <= m.Date && m.Date <= end && m.Transportationtype != Transportationtype.AirPlane);
                       
                    }
                }
            }
        }

        private void SetCalcModels(bool isWorld, CountryType type, DateTime start, DateTime end)
        {
            calcList_.Clear();
            foreach(var model in GetList(isWorld, type, start, end))
            {
                calcList_.Add(model);
            }
        }

        private void SetCalcDic()
        {
            calcDic_.Clear();

            foreach (var model in calcList_)
            {
                TransportationTypeModel tModel;
                if (calcDic_.TryGetValue(model.Transportationtype, out tModel))
                {
                    tModel.Set(model.JPYPrice);
                    tModel.SetParameter(model.Distance, model.Time);
                }
                else
                {
                    tModel = new TransportationTypeModel(model.Transportationtype);
                    tModel.Set(model.JPYPrice);
                    calcDic_.Add(model.Transportationtype, tModel);
                    tModel.SetParameter(model.Distance, model.Time);
                }
            }
            if (calcDic_.Count >= 0 && !calcDic_.ContainsKey( CurrentTransportationType))
            {
                CurrentTransportationType = calcDic_.Keys.FirstOrDefault();
            }
        }

        public override void CalcModels(bool isWorld, CountryType type, DateTime start, DateTime end)
        {
            SetCalcModels(isWorld, type, start, end);
            SetCalcDic();
        }

        public override IEnumerable<CountryType> GetCalcCounties()
        {
            var sets = new HashSet<CountryType>();
            foreach (var model in calcList_)
            {
                if (!sets.Contains(model.StartCountry))
                {
                    sets.Add(model.StartCountry);
                }
                if (!sets.Contains(model.EndCountry))
                {
                    sets.Add(model.EndCountry);
                }
            }
            foreach (var c in sets)
            {
                yield return c;
            }
        }

        public override DateTime? GetStartCalcDate()
        {
            if (calcList_.Count > 0)
            {
                return calcList_.Min(m => m.StartDate); 
            }
            else
            {
                return null;
            }
        }

        public override DateTime? GetEndCalcDate()
        {
            if (calcList_.Count > 0)
            {
                return calcList_.Max(m => m.EndDate);
            }
            else
            {
                return null;
            }
        }

        public override int GetCalcDateCount()
        {
            var hSet = new HashSet<DateTime>();
            foreach (var model in calcList_)
            {
                if (!hSet.Contains(model.StartDate))
                {
                    hSet.Add(model.StartDate);
                }
                if (!hSet.Contains(model.EndDate))
                {
                    hSet.Add(model.EndDate);
                }

            }
            return hSet.Count;
        }

        public double TotalDistance
        {
            get
            {
                double sum = 0;
                foreach (var pair in calcDic_)
                {
                    sum += pair.Value.TotalDistance;
                }
                return sum;
            }
        }

        public int TotalTime
        {
            get
            {
                int sum = 0;
                foreach (var pair in calcDic_)
                {
                    sum += pair.Value.TotalTime;
                }
                return sum;
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
