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
    public class ExchangeRater : BaseList
    {

        private Dictionary<CurrencyType, Dictionary<string, double>> rateList_;

        public override bool IsLoaded
        {
            get { return rateList_.Count > 0; }
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
            var type = base.ConvertCurrency(stringList[0]);
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
                rateList_.Add((CurrencyType)type, dic);

            }
        }

        //　為替表を設定する
        protected override void Set(object[] arrays)
        {
            var length = arrays.Length;
            string[] dateList = null;
            for (var d = 0; d < length; d++)
            {
                var row = (string[])arrays[d];
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


        protected override bool CheckFormat(object[] arrays)
        {
            var length = arrays.Length;
            for (var i = 0; i < length; i++)
            {
                var row = (string[])arrays[i];
                for (var j = 0; j < row.Length; j++)
                    // 通貨コード
                    if (j == 0)
                    {
                        var type = base.ConvertCurrency(row[j]);
                        if (type == null)
                        {
                            base.SetErrorList(i, j, row[j]);
                        }
                    }
                    else
                    {
                        // 時期
                        if (i == 0)
                        {
                            var date = base.ConvertDate(row[j]);
                            if (date == null)
                            {
                                base.SetErrorList(i, j, row[j]);
                            }
                        }
                        // レート
                        else
                        {
                            var val = base.ConvertDouble(row[j]);
                            if (val == null)
                            {
                                base.SetErrorList(i, j, row[j]);
                            }
                        }
                    }

            }
            return base.IsError;
        }


        public ExchangeRater() 
        {
            rateList_ = new Dictionary<CurrencyType, Dictionary<string, double>>();
           
        }

        public override void Init()
        {
            base.Init();
            rateList_.Clear();
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

        public override void ConvertAnotherCurrency(ExchangeRater rater)
        {
            // not need(そもそもこの処理いらない。別の継承クラスに別けるべきかも)
        }
    }
}
