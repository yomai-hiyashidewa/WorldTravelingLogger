using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WorldTravelLogger.Models.Enumeration;

namespace WorldTravelLogger.Models.Context
{
    public class CountryModel
    {
        private CountryType countryType_;

        private string imageDir_;

        private List<TransportationModel> arrivals_;

        private List<TransportationModel> departures_;

        private List<RegionModel> regions_;

        private DateTime? startDate_;

        private DateTime? endDate_;

        public CountryModel(CountryType countryType_, string imageDir_)
        {
            this.countryType_ = countryType_;
            this.imageDir_ = imageDir_;
            this.Init();
        }

        public void Init()
        {
            arrivals_ = new List<TransportationModel>();
            departures_ = new List<TransportationModel>();
            regions_ = new List<RegionModel>();
            startDate_ = null;
            endDate_ = null;
            currentRegionModel_ = null;
        }

        public CountryType Type { get { return countryType_; } }

        public string ImagePath
        {
            get
            {
                if(!string.IsNullOrWhiteSpace(imageDir_) && Path.Exists(imageDir_))
                {
                    var path = Path.Combine(imageDir_, "Flags", countryType_.ToString() + ".png");
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }
                return null;
            }
        }

        private RegionModel currentRegionModel_;

        private void SetRegions()
        {
            if (currentRegionModel_ != null)
            {
                regions_.Add(currentRegionModel_);
                currentRegionModel_ = null;
            }
        }

        private void StartCalcRegion(string region)
        {
            if(currentRegionModel_ != null)
            {
                SetRegions();
            }
            currentRegionModel_ = new RegionModel(region);
        }

        private void SetArrivalOrDepartures(TransportationModel model)
        {
            if (countryType_ == model.StartCountry)
            {
                departures_.Add(model);
                SetRegions();

            }
            else
            {
                arrivals_.Add(model);
                StartCalcRegion(model.EndRegion);
            }
        }

        private void InitCurrentRegion(string region)
        {
            if (currentRegionModel_ == null)
            {
                StartCalcRegion(region);
            }
        }
        
        private void SetRegion(TransportationModel model)
        {
            var isEnd = currentRegionModel_.SetTransportation(model);
            if (isEnd)
            {
                SetRegions();
                StartCalcRegion(model.EndRegion);
            }
        }

        private void SetSameCountryRegion(TransportationModel model)
        {
            InitCurrentRegion(model.StartRegion);
            if (model.IsSameRegion(currentRegionModel_.Region))
            {
                SetRegion(model);
            }
            else
            {
                SetRegions();
                StartCalcRegion(model.StartRegion);
                SetRegion(model);
            }
           
        }

        public void EndCalc()
        {
            SetRegions();
        }

        public void SetTransportationModel(TransportationModel model)
        {
            if (model.SameCountry)
            {
                SetSameCountryRegion(model);
            }
            else
            {
                SetArrivalOrDepartures(model);
            }
        }

      
    }
}
