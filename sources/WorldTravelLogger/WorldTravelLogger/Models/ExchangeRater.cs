using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class ExchangeRater
    {
        Dictionary<CurrencyType, Dictionary<string, double>> rateList_;

        public ExchangeRater()
        {
            rateList_ = new Dictionary<CurrencyType, Dictionary<string, double>>();
        }

        // 為替を取得する

        public double GetRate(CurrencyType type, DateTime date)
        {
            if (rateList_.ContainsKey(type))
            {
                var value = rateList_.GetValueOrDefault(type);
                string valueStr = date.ToString("yyyy/MM/dd");
                if (value.ContainsKey(valueStr))
                {
                    return value.GetValueOrDefault(valueStr);
                }
            }

            return 0.0;

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
        public void Set(string[,] arrays, int dimensions)
        {
            string[] dateList = null;
            for (var d = 0; d < dimensions; d++)
            {
                var list = new List<string>();
                for (int i = 0; i < arrays.GetLength(d); i++)
                {
                    list.Add(arrays[d, i]);
                }
                if (d == 0)
                {
                    dateList = GetDateList(list.ToArray());
                }
                else
                {
                    SetRate(dateList, list.ToArray());
                }
            }
        }
    }
}
