using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    // 内容抽象クラス
    public abstract class BaseContext
    {
        private DateTime date_;         // 日付
        private CountryType country_;   // 国
        private string? region_;        // 地域
        private double price_;             // 値段
        private CurrencyType currency_; // 通貨
        private string? memo_;          // メモ

        public BaseContext()
        {
            date_       = DateTime.Now;
            country_    = CountryType.JPN;
            region_     = null;
            price_      = 0.0;
            currency_ = CurrencyType.JPY;
            memo_ = null;
        }

        public BaseContext(DateTime date, CountryType country, string? region, double price, CurrencyType currency, string? memo)
        {
            date_ = date;
            country_ = country;
            region_ = region;
            price_ = price;
            currency_ = currency;
            memo_ = memo;
        }


        public  DateTime Date { get { return Date; } }         // 日付
        public CountryType Country { get { return country_; } }   // 国
        public string? Region { get { return region_; } }        // 地域
        public double Price { get { return price_; } }             // 値段
        public CurrencyType Currency { get { return currency_; } } // 通貨
        public string? Memo { get { return memo_; } }          // メモ

        public int JPYPrice
        {
            get;
            private set;

        }

        public int EURPrice
        {
            get;
            private set;

        }

        public int USDPrice
        {
            get;
            private set;

        }


    }
}
