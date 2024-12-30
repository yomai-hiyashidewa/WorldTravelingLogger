using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;
using WorldTravelLogger.Models.Context;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.ViewModels.Base;

namespace WorldTravelLogger.ViewModels
{
    public class RouteRegionViewModel : ViewModelBase
    {
        ControlModel control_;
        MainModel model_;

        CountryViewModel cVM_;

        public RouteRegionMiniViewModel MiniVM { get; private set; }

        public RouteRegionViewModel(MainModel model)
        {
            model_ = model;
            model.CalcCompleted_ += Model_CalcCompleted_;
            control_ = model.GetControlModel();
            control_.CountryChanged_ += Control__CountryChanged_;
            MiniVM = new RouteRegionMiniViewModel(model);
            SetCVM();
        }

        private void SetCVM()
        {
            cVM_ = new CountryViewModel(control_.CurrentRouteCountryType, model_.ImageDir);
        }

        private void Model_CalcCompleted_(object? sender, EventArgs e)
        {
            SetCVM();
            UpdateAll();
        }

        private void Control__CountryChanged_(object? sender, EventArgs e)
        {
            SetCVM();
            UpdateAll();
        }

        private void UpdateAll()
        {
            this.RaisePropertyChanged("Type");
            this.RaisePropertyChanged("EnableImage");
            this.RaisePropertyChanged("CountryFlagPath");
        }

        public CountryType Type
        {
            get
            {
                return cVM_.Type;
            }
        }

        public bool EnableImage
        {
            get
            {
                return cVM_.EnableImage;
            }
        }

        public string CountryFlagPath
        {
            get
            {
                return cVM_.ImagePath;
            }
        }
    }
}
