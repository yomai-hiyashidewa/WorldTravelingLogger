using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.ViewModels;

namespace WorldTravelLogger.Models.Context
{
    public class CountryModel : CountryViewModel
    {
        private CountryType countryType_;

        private string imageDir_;

        private List<TransportationModel> arrivals_;

        private List<TransportationModel> departures_;

        private List<RegionModel> regions_;

        private DateTime? startDate_;

        private DateTime? endDate_;

        public CountryModel(CountryType countryType, string imageDir):
            base(countryType,imageDir)
        {
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

        public TransportationModel GetTransportationModel(bool isArrival,CountryType type)
        {
            if (isArrival)
            {
                return arrivals_.FirstOrDefault(m => m.StartCountry == type);
            }
            else
            {
                return departures_.FirstOrDefault(m => m.EndCountry == type);
            }
        }

        public IEnumerable<CountryType> GetCountries(bool isArrival)
        {
            if (isArrival)
            {
                foreach(var model in arrivals_)
                {
                    yield return model.StartCountry;
                }
            }
            else
            {
                foreach (var model in departures_)
                {
                    yield return model.EndCountry;
                }
            }
        }

        public CountryType? GetFirstCountryType(bool isArrival)
        {
            CountryType? type = null;
            if (isArrival)
            {
                var model = arrivals_.FirstOrDefault();
                if(model != null)
                {
                    type = model.StartCountry;
                }
            }
            else
            {
                var model = departures_.FirstOrDefault();
                if (model != null)
                {
                    type = model.EndCountry;
                }
            }
            return type;
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
