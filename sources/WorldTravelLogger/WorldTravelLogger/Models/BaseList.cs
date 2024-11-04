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
        public List<int[]> ErrorList
        {
            get;
            private set;
        }


        protected BaseList()
        {
            ErrorList = new List<int[]>();
        }

        protected void SetErrorList(int i, int j)
        {
            int[] error = { i, j };
            ErrorList.Add(error);
        }

        protected bool IsError
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
            if (!Enum.TryParse(str, out type))
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
            if (!Enum.TryParse(str, out type))
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
            if (!Enum.TryParse(str, out type))
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
            if (!double.TryParse(str, out val))
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

        public virtual void Init()
        {
            ErrorList.Clear();
        }

        public virtual ErrorTypes Load(string filePath,string checkFilename)
        {
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
    }


   
}
