using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public abstract  class BaseList : IList
    {
        public event EventHandler ListChanged;
        public List<FileErrorContext> ErrorList
        {
            get;
            private set;
        }


        public Dictionary<CountryType,HashSet<string>> CountriesAndRegions
        {
            get; 
            protected set;
        }

        public Dictionary<CountryType,HashSet<CurrencyType>> CountriesAndCurrencies
        {
            get; protected set;
        }

        private DateTime? startDate_;
        private DateTime? endDate_;

       


        protected BaseList()
        {
            ErrorList = new List<FileErrorContext>();
            CountriesAndRegions = new Dictionary<CountryType, HashSet<string>>();
            CountriesAndCurrencies = new Dictionary<CountryType, HashSet<CurrencyType>>();
            startDate_ = null;
            endDate_ = null;
            
        }

        public FileErrorContext[] GetErrorArray()
        {
            return ErrorList.ToArray();
        }


        protected void SetErrorList(int i, int j,string context)
        {
            
            ErrorList.Add(new FileErrorContext(i,j,context));
        }

        public bool IsError
        {
            get { return ErrorList.Count != 0; }
        }

        protected DateTime? ConvertDate(string str)
        {
            DateTime date;
            if (DateTime.TryParse(str, out date))
            {
                return date; ;
            }
            else
            {
                return null;
            }
        }

        protected CurrencyType? ConvertCurrency(string str)
        {
            CurrencyType type;
            if (Enum.TryParse(str, out type))
            {
                return type;
            }
            else
            {
                return null;
            }
        }

        protected CountryType? ConvertCountry(string str)
        {
            CountryType type;
            if (Enum.TryParse(str, out type))
            {
                return type;
            }
            else
            {
                return null;
            }
        }

        protected AccomodationType? ConvertAccomodationType(string str)
        {
            AccomodationType type;
            if (Enum.TryParse(str, out type))
            {
                return type;
            }
            else
            {
                return null;
            }
        }

        protected Transportationtype? ConvertTransportationType(string str)
        {
            Transportationtype type;
            if(Enum.TryParse(str, out type))
            {
                return type;
            }
            else
            {
                return null;
            }
        }

        protected SightseeigType? ConvertSightSeeingType(string str)
        {
            SightseeigType type;
            if(Enum.TryParse(str,out type))
            {
                return type;
            }
            else
            {
                return null;
            }
        }

        protected PlaceType? ConvertPlaceType(string str)
        {
            PlaceType type;
            if(Enum.TryParse(str, out type))
            {
                return type;
            }
            else
            {
                return null;
            }
        }

        protected double? ConvertDouble(string str)
        {
            double val;
            if (double.TryParse(str, out val))
            {
                return val;
            }
            else
            {
                return null;
            }
        }

        protected int? ConvertInt(string str)
        {
            int val;
            if(int.TryParse(str, out val))
            {
                return val;
            }
            else
            {
                return null;
            }
        }

        protected abstract bool CheckFormat(object[] arrays);

        protected abstract void Set(object[] arrays);

        public abstract void ConvertAnotherCurrency(ExchangeRater rater);

        public virtual void Init()
        {
            ErrorList.Clear();
        }

        public virtual ErrorTypes Load(string filePath,string checkFilename)
        {
            CountriesAndRegions.Clear();
            CountriesAndCurrencies.Clear();
            startDate_ = null;
            endDate_ = null;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return ErrorTypes.None;
            }
            if (!File.Exists(filePath))
            {
                return ErrorTypes.FileNotFound;
            }

            var filename = Path.GetFileNameWithoutExtension(filePath);
            if (!string.Equals(filename, checkFilename))
            {
                return ErrorTypes.FileWrong;
            }
            var result = CSVReader.ReadCSV(filePath);
            if (result == null)
            {
                return ErrorTypes.FileNotOpen;
            }
            if (CheckFormat(result))
            {
                return ErrorTypes.FormatError;
            }

            if (!this.IsError)
            {
                this.Set(result);
            }
            return ErrorTypes.None;
        }

        protected void FireListChanged()
        {
            if(ListChanged != null)
            {
                ListChanged(this, EventArgs.Empty);
            }
        }

        protected void SetCountry(CountryType type, string region)
        {
            
            if (!CountriesAndRegions.ContainsKey(type))
            {
                var context = new HashSet<string>();
                CountriesAndRegions.Add(type, context);
            }
            if (!string.IsNullOrWhiteSpace(region) && 
                !CountriesAndRegions[type].Contains(region))
            {
                CountriesAndRegions[type].Add(region);
            }
        }

        protected void SetCountryAndCurrency(CountryType type, CurrencyType currencyType)
        {
            if (!CountriesAndCurrencies.ContainsKey(type))
            {
                var context = new HashSet<CurrencyType>();
                CountriesAndCurrencies.Add(type, context);
            }
            if (!CountriesAndCurrencies[type].Contains(currencyType))
            {
                CountriesAndCurrencies[type].Add(currencyType);
            }
        }

        protected void SetDate(DateTime date)
        {
            if(startDate_ == null || endDate_ == null)
            {
                startDate_ = date;
                endDate_ = date;
            }
            else if(startDate_ > date)
            {
                startDate_ = date;
            }
            else if(endDate_ < date)
            {
                endDate_ = date;
            }
        }

        public void SetCoutriesAndRegions(Dictionary<CountryType, HashSet<string>> dic)
        {
            foreach (var context in CountriesAndRegions)
            {
                if (!dic.ContainsKey(context.Key))
                {
                    dic.Add(context.Key, context.Value);
                }
                else
                {
                    foreach (var region in context.Value)
                    {
                        if (!dic[context.Key].Contains(region))
                        {
                            dic[context.Key].Add(region);
                        }
                    }

                }
            }
        }

        public void SetCoutriesAndCurrencies(Dictionary<CountryType, HashSet<CurrencyType>> dic)
        {
            foreach (var context in CountriesAndCurrencies)
            {
                if (!dic.ContainsKey(context.Key))
                {
                    dic.Add(context.Key, context.Value);
                }
                else
                {
                    foreach (var currency in context.Value)
                    {
                        if (!dic[context.Key].Contains(currency))
                        {
                            dic[context.Key].Add(currency);
                        }
                    }

                }
            }
        }


        public DateTime StartDate
        {
            get
            {
                if(startDate_ == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return (DateTime)startDate_;
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                if (endDate_ == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return (DateTime)endDate_;
                }
            }
        }



        public abstract IEnumerable<CountryType> GetCalcCounties();

        public abstract DateTime? GetStartCalcDate();

        public abstract DateTime? GetEndCalcDate();

        public abstract HashSet<DateTime> GetCalcDates(HashSet<DateTime> dates);

        

        
        public abstract bool IsLoaded { get; }

        public abstract bool IsReady { get; }

        // memo interfaceで実装すべきかも
        public abstract void CalcModels(ControlModel control);

        public abstract void CalcRegion(ControlModel control);

       
        public abstract double TotalCost { get; }

        

        
    }

    


   
}
