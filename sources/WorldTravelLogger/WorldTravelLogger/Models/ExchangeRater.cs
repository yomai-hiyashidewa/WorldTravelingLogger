using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WorldTravelLogger.Models
{
    public class ExchangeRater
    {

        private Dictionary<CurrencyType, Dictionary<string, double>> rateList_;
        public List<int[]> ErrorList
        {
            get;
            private set;
        }

        private string[] GetDateList(string[] stringList)
        {
            var dateList = new List<string>();
            for (int i = 1; i < stringList.Length; i++)
            {
                dateList.Add(stringList[i]);
            }
            return dateList.ToArray();
        }

        private void SetRate(string[] dateList, string[] stringList)
        {
            var type = (CurrencyType)Enum.Parse(typeof(CurrencyType), stringList[0]);
            if (type == CurrencyType.JPY)
            {
                // none
            }
            else
            {
                var dic = new Dictionary<string, double>();
                for (int i = 1; i < dateList.Length; i++)
                {
                   
                    double value = 0;
                    if (double.TryParse(stringList[i + 1], out value))
                    {
                        dic.Add(dateList[i], value);
                    }
                }
                rateList_.Add(type, dic);

            }
        }

        //　為替表を設定する
        private void Set(object[] arrays)
        {
            var length = arrays.Length;
            string[] dateList = null;
            for (var d = 0; d < length; d++)
            {
                var row = (string[])arrays[d];
                var list = new List<string>();
                if (d == 0)
                {
                    dateList = GetDateList(row);
                }
                else
                {
                    SetRate(dateList, row);
                }
            }
        }

        private void SetErrorList(int i, int j)
        {
            int[] error = { i, j };
            ErrorList.Add(error);
        }

        private bool CheckFormat(object[] arrays)
        {
            var length = arrays.Length;
            var list = new List<int[]>();
            for (var i = 0; i < length; i++)
            {
                var row = (string[])arrays[i];
                for (var j = 0; j < row.Length; j++)
                    // 通貨コード
                    if (j == 0)
                    {
                        CurrencyType type;
                        if (!Enum.TryParse(row[j], out type))
                        {
                            SetErrorList(i, j);
                        }
                    }
                    else
                    {
                        // 時期
                        if (i == 0)
                        {
                            DateTime date;
                            if (!DateTime.TryParse(row[j], out date))
                            {
                                SetErrorList(i, j);
                            }
                        }
                        // レート
                        else
                        {
                            double val;
                            if (!double.TryParse(row[j], out val))
                            {
                                SetErrorList(i, j);
                            }
                        }
                    }

            }
            return ErrorList.Count != 0;
        }


        public ExchangeRater()
        {
            rateList_ = new Dictionary<CurrencyType, Dictionary<string, double>>();
            ErrorList = new List<int[]>();
        }

        public void Init()
        {
            rateList_.Clear();
            ErrorList.Clear();
        }

        // 為替を取得する

        public double GetRate(CurrencyType type, DateTime date)
        {
            if (rateList_.ContainsKey(type))
            {
                var value = rateList_.GetValueOrDefault(type);
                // 1日でデータ格納しているので変換。
                var searchDate = new DateTime(date.Year, date.Month, 1);
                string valueStr = searchDate.ToString("yyyy/MM/dd");
                if (value.ContainsKey(valueStr))
                {
                    return value.GetValueOrDefault(valueStr);
                }
            }

            return 0.0;

        }





        public ErrorTypes Load(string filePath)
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
            if (!string.Equals(filename, FileNames.ExchangeRateFile))
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

            this.Set(result);
            return ErrorTypes.None;
        }

    }
}
