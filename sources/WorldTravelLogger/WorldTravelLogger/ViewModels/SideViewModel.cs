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
        public SideViewModel()
        {
            // dummy   
        }

        public SideViewModel(MainModel model)
        {
            model_ = model;
            model_.FileLoaded_ += Model__FileLoaded_;
            model_.ControlChanged_ += Model__ControlChanged_;
            model.TransportationChanged_ += Model_TransportationChanged_;
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
            this.RaisePropertyChanged("IsWithAirplane");
            this.RaisePropertyChanged("IsCountryMode");
            
  
            
            //this.RaisePropertyChanged("IsCurrencyJPY");
            //this.RaisePropertyChanged("IsCurrencyUSD");
            //this.RaisePropertyChanged("IsCurrencyEUR");
            //this.RaisePropertyChanged("CurrentMajorCurrencyType");
            UpdateView();

        }

        private void UpdateView()
        {
            
            this.RaisePropertyChanged("CurrentCountry");
            this.RaisePropertyChanged("IsWithCrossBorder");
            this.RaisePropertyChanged("StartDate");
            this.RaisePropertyChanged("EndDate");
            this.RaisePropertyChanged("TotalMovingDistance");
            this.RaisePropertyChanged("TotalMovingTime");
            this.RaisePropertyChanged("Countries");
            this.RaisePropertyChanged("TotalCalcCountries");
            this.RaisePropertyChanged("TotalCountries");
 
            this.RaisePropertyChanged("RegionsCount");
            this.RaisePropertyChanged("Regions");
        }

        public bool IsWorld
        {
            get
            {
                if (model_ == null)
                {
                    return false;
                }
                else
                {
                    return model_.IsWorldMode;
                }
            }
            set
            {
                model_.IsWorldMode = value;
            }
        }

        public bool IsWithAirplane
        {
            get
            {
                if (model_ == null)
                {
                    return false;
                }
                else
                {
                    return model_.IsWithAirplane;
                }
            }
            set
            {
                model_.IsWithAirplane = value;
            }
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
                if (model_ == null)
                {
                    return CountryType.JPN;
                }
                else
                {
                    return model_.CurrentCountryType;
                }
            }
            set
            {
                model_.CurrentCountryType = value;
            }
        }

        public bool IsWithCrossBorder
        {
            get
            {
                if (model_ == null)
                {
                    return false;
                }
                else
                {
                    return model_.IsWithCrossBorder;
                }
            }
            set
            {
                model_.IsWithCrossBorder = value;
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

        public int RegionsCount
        {
            get
            {
                if (model_ == null)
                {
                    return 0;
                }
                else
                {
                    return model_.GetCurrentRegionCount(); 
                }
            }
        }


        public int TotalCalcCountries
        {
            get
            {
                if(model_ == null)
                {
                    return 0;
                }
                else
                {
                    return model_.TotalCalcCountries;
                }
            }
        }

        public int TotalCountries
        {
            get
            {
                if (model_ == null)
                {
                    return 0;
                }
                else
                {
                    return model_.GetCountries().Count();
                }
            }
        }

       

        public bool IsCurrencyJPY
        {
            get
            {
                if(model_ == null)
                {
                    return false;
                }
                else
                {
                    return model_.CurrentMajorCurrencyType == MajorCurrencytype.JPN;
                }

            }
            set
            {
                model_.CurrentMajorCurrencyType = MajorCurrencytype.JPN;
                
               
            }
        }

        public bool IsCurrencyUSD
        {
            get
            {
                if (model_ == null)
                {
                    return false;
                }
                else
                {
                    return model_.CurrentMajorCurrencyType == MajorCurrencytype.USD;
                }

            }
            set
            {
                model_.CurrentMajorCurrencyType = MajorCurrencytype.USD;
                
            }
        }

        public bool IsCurrencyEUR
        {
            get
            {
                if (model_ == null)
                {
                    return false;
                }
                else
                {
                    return model_.CurrentMajorCurrencyType == MajorCurrencytype.EUR;
                }

            }
            set
            {
                model_.CurrentMajorCurrencyType = MajorCurrencytype.EUR;

            }
        }

        public string TotalMovingDistance
        {
            get
            {
                if (model_ == null)
                {
                    return "0";
                }
                else
                {
                    return string.Format("{0:#,0}", model_.GetTotalDistance()) + "km";
                }
            }
        }

        public string TotalMovingTime
        {
            get
            {
                if (model_ == null)
                {
                    return "0";
                }
                else
                {
                    var ts = new TimeSpan(0, model_.GetTotalTime(), 0);
                    return string.Format("{0}days {1}hours {2}mins", ts.TotalDays, ts.TotalHours, ts.TotalMinutes);
                }
            }
        }





    }
}
