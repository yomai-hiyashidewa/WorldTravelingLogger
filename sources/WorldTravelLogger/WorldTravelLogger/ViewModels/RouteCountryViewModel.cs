using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;
using WorldTravelLogger.Models.Context;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.Models.List;
using WorldTravelLogger.ViewModels.Base;

namespace WorldTravelLogger.ViewModels
{
    public class RouteCountryViewModel : ViewModelBase
    {
        private bool isArrival_;

        private MainModel model_;

        private ControlModel control_;

        private TransportationList transportationList_;

        private List<TransportationModel> transportations_;

        private CountryListViewModel clVM_;

        private TransportationModel? current_;



        public RouteCountryViewModel(bool isArrival,MainModel model)
        {
            isArrival_ = isArrival;
            model_ = model;
            control_ = model.GetControlModel();
            transportationList_ = model.GetTransportationList();
            model.CalcCompleted_ += Model_CalcCompleted_;
            control_.CountryChanged_ += Control__CountryChanged_;
            transportations_ = new List<TransportationModel>();
            clVM_ = new CountryListViewModel();
            clVM_.CountryChanged += ClVM__CountryChanged;
        }

        private void Set()
        {
            var cList = new List<CountryType>();
            transportations_.Clear();
            foreach (var model in isArrival_ ? transportationList_.GetArrivals(control_.CurrentRouteCountryType) :
                transportationList_.GetDepartures(control_.CurrentRouteCountryType))
            {
                transportations_.Add(model);
                if (isArrival_)
                {
                    cList.Add(model.StartCountry);
                }
                else
                {
                    cList.Add(model.EndCountry);
                }
            }
            clVM_.SetCountries(cList, model_.ImageDir);
            current_ = transportations_.FirstOrDefault();
            this.UpdateAll();
        }

        private void Control__CountryChanged_(object? sender, EventArgs e)
        {
            Set();

        }

        private void Model_CalcCompleted_(object? sender, EventArgs e)
        {
            Set();
        }

        //public void SetModel(CountryModel model)
        //{
        //    model_ = model;
       
        //    var type = model.GetFirstCountryType(isArrival_);
        //    if (type != null)
        //    {
        //        SetCurrent((CountryType)type);
        //    }
        //}

        public CountryListViewModel GetCountryListViewModel()
        {
            return clVM_;
        }



        private void ClVM__CountryChanged(object? sender, Models.Utility.CountryChangedEventArgs e)
        {
            if (transportations_.Count > e.Index)
            {
                current_ = transportations_[e.Index];
                UpdateAll();
            }
        }

    
        public TransportationModel[] Transportations
        {
            get
            {
                return transportations_.ToArray();
            }
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
               if(current_ != null)
                {
                    return current_.GetMovingMoidel().Distance;
                }
                else
                {
                    return "km";
                }
            }
        }

        public string Time
        {
            get
            {

                if (current_ != null)
                {
                    return current_.GetMovingMoidel().Time;
                }
                else
                {
                    return "min";
                }
            }
        }


    }
}
