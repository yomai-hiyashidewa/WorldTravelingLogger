using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;
using WorldTravelLogger.Models.Context;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.ViewModels.Base;

namespace WorldTravelLogger.ViewModels
{
    public class RouteRegionMiniViewModel : ViewModelBase
    {
        MainModel model_;
        ControlModel control_;

        private int selectIndex_;

        private RegionModel[] regionsModels_;

        public RouteRegionMiniViewModel(MainModel model)
        {
            model_ = model;
            control_ = model.GetControlModel();
            regionsModels_ = []; 
        }

     

        private RegionModel? CurrentModel
        {
            get
            {

                if(regionsModels_.Length > selectIndex_)
                {
                    return regionsModels_[selectIndex_];
                }
                else
                {
                    return null;
                }
            }
        }

        private TransportationModel? Arrival
        {
            get
            {
                if(CurrentModel != null)
                {
                    return CurrentModel.Arrival;
                }
                return null;

            }
        }

        private TransportationModel? Departure
        {
            get
            {
                if (CurrentModel != null)
                {
                    return CurrentModel.Departure;
                }
                return null;

            }
        }


        private void UpdateAll()
        {
            this.RaisePropertyChanged("Regions");
            this.RaisePropertyChanged("CurrentRegion");
            this.RaisePropertyChanged("Dates");
            this.RaisePropertyChanged("SelectedIndex");
            this.RaisePropertyChanged("Events");
            this.RaisePropertyChanged("FromRegion");
            this.RaisePropertyChanged("FromMoving");
            this.RaisePropertyChanged("EnableFromRegion");
            this.RaisePropertyChanged("ToRegion");
            this.RaisePropertyChanged("ToMoving");
            this.RaisePropertyChanged("EnableToRegion");
        }

        public string[] Regions
        {
            get
            {
                return model_.GetCurrentRegions();
            }
        }


        public string CurrentRegion
        {
            get
            {
                return control_.CurrentRegion;
            }
            set
            {
                if(control_.CurrentRegion != value)
                {
                    control_.CurrentRegion = value;
                    UpdateAll();
                }
            }
        }

        string[] Dates
        {
            get
            {
                var list = new List<string>();
                foreach(var model in regionsModels_)
                {
                    list.Add(model.StartEndDate);
                }
                return list.ToArray();
            }
        }

        public int SelectedIndex
        {
            get { return selectIndex_; }
            set
            {
                selectIndex_ = value;
                // 
            }
        }

        public string[] Events
        {
            get
            {
                var list = new List<string>();
                list.Add("Event test1");
                list.Add("Event test2");
                list.Add("Event test3");
                return list.ToArray();
            }
        }

        // from

        public string FromRegion
        {
            get
            {
                if(Arrival != null)
                {
                    return Arrival.StartRegion;
                }
                else
                {
                    return "";
                }
            }
        }

        public MovingModel[] FromMoving
        {
            get
            {
                if(Arrival != null)
                {
                    
                    MovingModel[] models = [Arrival.GetMovingMoidel()];
                    return models;
                }
                return [];
            }
        }

        public bool EnableFromRegion
        {
            get
            {
                return Arrival != null;
            }
        }

        // to

        public string ToRegion
        {
            get
            {
                if (Departure != null)
                {
                    return Departure.StartRegion;
                }
                else
                {
                    return "";
                }
            }
        }

        public MovingModel[] ToMoving
        {
            get
            {
                if (Departure != null)
                {

                    MovingModel[] models = [Departure.GetMovingMoidel()];
                    return models;
                }
                return [];
            }
        }

        public bool EnableToRegion
        {
            get
            {
                return Departure != null;
            }
        }




    }
}
