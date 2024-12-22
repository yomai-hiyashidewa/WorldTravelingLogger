using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WorldTravelLogger.Converters;
using WorldTravelLogger.Models;

namespace WorldTravelLogger.ViewModels
{
    public class SideViewModel : ViewModelBase
    {
        private MainModel? model_;
        private ControlModel control_;
        public SideViewModel()
        {
            // dummy   
        }

        public SideViewModel(MainModel model)
        {
            model_ = model;
            control_ = model.GetControlModel();


            model_.FileLoaded_ += Model__FileLoaded_;
            model_.ImageListReady_ += Model__ImageListReady_;
        }

        private void Model__ImageListReady_(object? sender, EventArgs e)
        {
            this.RaisePropertyChanged("CountryImagePath");
        }

        private void Model__FileLoaded_(object? sender, FileLoadedEventArgs e)
        {
            if (model_.ReadyApplication)
            {
                UpdateView();
            }
            
        }

        private void Model_TransportationChanged_(object? sender, EventArgs e)
        {
            UpdateView();
        }

        private void Model__ControlChanged_(object? sender, EventArgs e)
        {
            this.RaisePropertyChanged("IsWorld");
            this.RaisePropertyChanged("IsWithJapan");
            this.RaisePropertyChanged("IsCountryMode");
            
  
            
            //this.RaisePropertyChanged("CurrentMajorCurrencyType");
            UpdateView();

        }

        private void UpdateView()
        {
            
            this.RaisePropertyChanged("CurrentCountry");
            this.RaisePropertyChanged("IsWithCrossBorder");
            this.RaisePropertyChanged("StartDate");
            this.RaisePropertyChanged("EndDate");

            this.RaisePropertyChanged("Movings");
            this.RaisePropertyChanged("Countries");
            this.RaisePropertyChanged("TotalCalcCountries");
            //this.RaisePropertyChanged("TotalCountries");
 
            this.RaisePropertyChanged("RegionsCount");
            this.RaisePropertyChanged("Regions");
            this.RaisePropertyChanged("ExchangeRates");
            this.RaisePropertyChanged("CountryFlagPath");
            this.RaisePropertyChanged("CountryImagePath");
        }

        public bool IsWorld
        {
            get
            {
                if (control_ == null)
                {
                    return false;
                }
                else
                {
                    return control_.IsWorldMode;
                }
            }
            set
            {
                control_.IsWorldMode = value;
            }
        }

        public bool IsWithJapan
        {
            get; set;
        }



        public bool IsCountryMode
        {
            get
            {
                return !IsWorld;
            }
        }

        

        public CountryType CurrentCountry
        {
            get
            {
                if (control_ == null)
                {
                    return CountryType.JPN;
                }
                else
                {
                    return control_.CurrentCountryType;
                }
            }
            set
            {
                control_.CurrentCountryType = value;
            }
        }

        public bool IsWithCrossBorder
        {
            get
            {
                if (control_ == null)
                {
                    return false;
                }
                else
                {
                    return control_.IsWithCrossBorder;
                }
            }
            set
            {
                control_.IsWithCrossBorder = value;
            }

        }

       

      


        public CountryType[] Countries
        {
            get
            {
                
                if (model_ != null)
                {
                    var list = new List<CountryType>();
                    list.AddRange(model_.GetCountries());
                    if (list.Count > 0)
                    {
                        return list.ToArray();
                    }
                    
                }
                return (CountryType[])Enum.GetValues(typeof(CountryType));

            }
        }

        public string[] Regions
        {
            get
            {
                if(model_ == null)
                {
                    return [];
                }
                else
                {
                    return model_.GetCurrentRegions(); 
                }
            }
        }

        public string RegionsCount
        {
            get
            {
                var count = 0;
                if (model_ == null)
                {
                    count = 0;
                }
                else
                {
                    if (IsWorld)
                    {
                        count = model_.GetTotalRegionCount();
                    }
                    else
                    {
                        count = model_.GetCurrentRegionCount();
                    }
                }
                return count + " regions";
            }
        }

        public ExchangeRateModel[] ExchangeRates
        {
            get
            {
                if(model_ == null)
                {
                    return [];
                }
                else
                {
                    return model_.GetCurrentExchangeRates();
                }
            }
        }



        public string TotalCalcCountries
        {
            get
            {
                if(model_ == null)
                {
                    return "0";
                }
                else
                {
                    return model_.TotalCalcCountries + " countries"; ;
                }
            }
        }

        public string TotalCountries
        {
            get
            {
                if (model_ == null)
                {
                    return "0";
                }
                else
                {
                    return model_.GetCountries().Count() + " countries";
                }
            }
        }

       

        public MovingModel[] Movings
        {
            get
            {
                if(model_ == null)
                {
                    return [];
                }
                else
                {
                    var list = new List<MovingModel>();
                    list.Add(model_.GetMovingModel());
                    return list.ToArray();
                }
            }
        }

        public string CountryFlagPath
        {
            get
            {
                if(model_ == null)
                {
                    return null;
                }
                else
                {
                    return model_.GetCountryFlagPath();
                }
            }
        }

        public string CountryImagePath
        {
            get
            {
                if(model_ == null)
                {
                    return null;
                }
                else
                {
                    return model_.GetCountryImagePath();
                }
                
            }
        }
    }
}
