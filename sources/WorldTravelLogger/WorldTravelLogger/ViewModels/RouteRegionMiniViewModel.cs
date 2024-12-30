using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;
using WorldTravelLogger.Models.Context;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.Models.List;
using WorldTravelLogger.ViewModels.Base;

namespace WorldTravelLogger.ViewModels
{
    public class RouteRegionMiniViewModel : ViewModelBase
    {
        MainModel model_;
        ControlModel control_;
        TransportationList transportationList_;




        public RouteRegionMiniViewModel(MainModel model)
        {
            model_ = model;
            model.CalcCompleted_ += Model_CalcCompleted_;
            control_ = model.GetControlModel();
            control_.CountryChanged_ += Control__CountryChanged_;
            transportationList_ = model.GetTransportationList();
        }

        private void Control__CountryChanged_(object? sender, EventArgs e)
        {
            UpdateAll();
        }

        private void Model_CalcCompleted_(object? sender, EventArgs e)
        {
            UpdateAll();
        }

        public TransportationModel[] Routes
        {
            get
            {
                var list = new List<TransportationModel>();
                foreach (var model in transportationList_.GetRoute(control_.CurrentRouteCountryType))
                {
                    list.Add(model);
                }
                return list.ToArray();
            }
        }
        private void UpdateAll()
        {
            this.RaisePropertyChanged("Routes");
        }

    }
      
}
