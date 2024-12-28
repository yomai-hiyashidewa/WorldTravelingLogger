using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Xsl;
using WorldTravelLogger.Models.Base;
using WorldTravelLogger.Models.Context;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.Models.Interface;

namespace WorldTravelLogger.Models.List
{
    public class AccomodationList : BaseContextList
    {
        public AccomodationType CurrentAccomodationtype { get; set; }

        public AccomodationList()
        {
        }

        private void CheckFormats(int index, string[] row)
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
                    base.SetErrorList(index, j, str);
                }

            }
        }

        private IContext CreateModel(string[] row)
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
            if (date != null && country != null && region != null && accomodation != null &&
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

        private AccomodationType? ConvertType(string typeStr)
        {
            AccomodationType type;
            if (Enum.TryParse(typeStr, out type))
            {
                return type;
            }
            else
            {
                return null;
            }
        }

        private void SetCurrentAccomodationType()
        {
            if (!base.ContainType(CurrentAccomodationtype.ToString()))
            {
                var tModel = hSet_.FirstOrDefault();
                if (tModel != null) {
                    var type = ConvertType(tModel.Type);
                    if (type != null)
                    {
                        CurrentAccomodationtype = (AccomodationType)type;
                    }
                }
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
                if (model != null)
                {
                    base.SetContext(model);
                }
            }
        }

        protected override void CalcTypeModels(IEnumerable<IContext> list)
        {
            var dic = new Dictionary<AccomodationType, AccomodationTypeModel>();
            foreach (var model in list)
            {
                var aModel = (AccomodationModel)model;
                AccomodationTypeModel tModel;
                if (dic.TryGetValue(aModel.Accomodation, out tModel))
                {
                    tModel.Set(model.JPYPrice);
                }
                else
                {
                    tModel = new AccomodationTypeModel(aModel.Accomodation);
                    tModel.Set(model.JPYPrice);
                    dic.Add(aModel.Accomodation, tModel);
                }
            }
            base.Clear();
            foreach (var pair in dic)
            {
                base.SetModel(pair.Value);
            }
            SetCurrentAccomodationType();
        }

        public AccomodationTypeModel[] TypeAccomodations
        {
            get
            {
                return hSet_.OfType<AccomodationTypeModel>().ToArray();
            }
        }

        public AccomodationType[] CurrentAccomodationTypes
        {
            get
            {
                var list = new List<AccomodationType>();
                foreach (var model in hSet_)
                {
                    var type = ConvertType(model.Type);
                    if (type != null)
                    {
                        list.Add((AccomodationType)type);
                    }
                }
                return list.ToArray();
            }
        }










    }
}
