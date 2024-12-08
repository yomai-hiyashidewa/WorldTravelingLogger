using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class TransportationModel : BaseContext
    {
        private Transportationtype transportationtype_; // 交通機関
        private PlaceType startPlace_;                  // 開始場所
        private DateTime endDate_;                      // 終了日時
        private CountryType endCountry_;                // 終了国
        private string? endRegion_;                     // 終了地域
        private PlaceType endPlace_;                    // 終了場所
        private double distance_;                       // 移動距離(km)
        private int time_;                              // 移動時間(min)

        public TransportationModel() :
            base()
        {
            transportationtype_ = Transportationtype.Train;
            startPlace_ = PlaceType.Station;                  // 開始場所
            endDate_ = DateTime.Now;                         // 終了日時
            endCountry_ = CountryType.JPN;                  // 終了国
            endRegion_ = null;                              // 終了地域
            endPlace_ = PlaceType.Station;                  // 終了場所
            distance_ = 0.0;                                // 移動距離(km)
            time_ = 0;                                      // 移動時間(min)
        }

        public TransportationModel(Transportationtype transportationtype, 
            DateTime startDate, 
            CountryType startCountry, 
            string? startRegion, 
            PlaceType startPlace, 
            DateTime endDate, 
            CountryType endCountry, 
            string? endRegion, 
            PlaceType endPlace, 
            double distance, 
            int time, 
            double price, 
            CurrencyType currency, 
            string? memo) :
           base(startDate, startCountry, startRegion, price, currency, memo)
        {
            transportationtype_ = transportationtype;
            startPlace_ = startPlace;                  // 開始場所
            endDate_ = endDate;                         // 終了日時
            endCountry_ = endCountry;                  // 終了国
            endRegion_ = endRegion;                              // 終了地域
            endPlace_ = endPlace;                  // 終了場所
            distance_ = distance;                                // 移動距離(km)
            time_ = time;                                      // 移動時間(min)
        }



        public Transportationtype Transportationtype { get { return transportationtype_; } } // 交通機関

        public DateTime StartDate
        {
            get
            {
                return base.Date;
            }
        }

        public CountryType StartCountry
        {
            get
            {
                return base.Country;
            }
        }

        public string? StartRegion
        {
            get
            {
                return base.Region;
            }
        }

        public PlaceType StartPlace
        {
            get
            {
                return startPlace_;
            }
        }                  // 開始場所

        public DateTime EndDate
        {
            get
            {
                return endDate_;
            }
        }                      // 終了日時

        public string EndDateString
        {
            get
            {
                return endDate_.ToString("yyyy/MM/dd");
            }
        }

        public CountryType EndCountry
        {
            get
            {
                return endCountry_;
            }
        }                // 終了国

        public string? EndRegion
        {
            get
            {
                return endRegion_;
            }
        }                     // 終了地域

        public PlaceType EndPlace
        {
            get
            {
                return endPlace_;
            }
        }                    // 終了場所

        public double Distance
        {
            get { return distance_; }
        }// 移動距離(km)

        public int Time
        {
            get { return time_; }
        }                           // 移動時間(min)

    }
}
