using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public abstract  class BaseList
    {
        public List<FileErrorContext> ErrorList
        {
            get;
            private set;
        }


        public HashSet<CountryType> Countries
        {
            get; 
            protected set;
        }

        private DateTime? startDate_;
        private DateTime? endDate_;

       


        protected BaseList()
        {
            ErrorList = new List<FileErrorContext>();
            Countries = new HashSet<CountryType>();
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
            Countries.Clear();
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

        protected void SetCountry(CountryType type)
        {
            if (!Countries.Contains(type))
            {
                Countries.Add(type);
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

        public abstract int GetCalcDateCount();

        
        public abstract bool IsLoaded { get; }

        // memo interfaceで実装すべきかも
        public abstract void CalcModels(bool isWorld,CountryType type, DateTime start, DateTime end);

        public abstract double TotalCost { get; }

        

        
    }

    


   
}
