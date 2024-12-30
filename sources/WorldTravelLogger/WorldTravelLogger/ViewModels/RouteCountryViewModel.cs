using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models.Context;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.ViewModels.Base;

namespace WorldTravelLogger.ViewModels
{
    public class RouteCountryViewModel : ViewModelBase
    {
        private bool isArrival_;
        private CountryModel? model_;

        private CountryListViewModel clVM_;

        private TransportationModel current_;

        private MovingModel moving_;


        public RouteCountryViewModel(bool isArrival)
        {
            isArrival_ = isArrival;
            moving_ = new MovingModel();
            clVM_ = new CountryListViewModel();
            clVM_.CountryChanged += ClVM__CountryChanged;
        }

        public void SetModel(CountryModel model)
        {
            model_ = model;
            clVM_.SetCountries(model.GetCountries(isArrival_), model.ImagePath);
            var type = model.GetFirstCountryType(isArrival_);
            if (type != null)
            {
                SetCurrent((CountryType)type);
            }
        }

        public CountryListViewModel GetCountryListViewModel()
        {
            return clVM_;
        }



        private void ClVM__CountryChanged(object? sender, Models.Utility.CountryChangedEventArgs e)
        {
            SetCurrent(e.Type);
        }

        private void SetCurrent(CountryType type)
        {
            current_ = model_.GetTransportationModel(isArrival_, type);
            if (current_ != null)
            {
                moving_.Set(current_.Distance, current_.Time);
            }
            UpdateAll();
        }

        private void UpdateAll()
        {
            this.RaisePropertyChanged("Type");
            this.RaisePropertyChanged("Distance");
            this.RaisePropertyChanged("Time");
        }

        public Transportationtype Type
        {
            get
            {
                if(current_ != null)
                {
                    return current_.Transportationtype;
                }
                else
                {
                    return Transportationtype.Bus;
                }
            }
        }

        public string Distance
        {
            get
            {
                return moving_.Distance;
            }
        }

        public string Time
        {
            get
            {
                return moving_.Time;
            }
        }


    }
}
