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
            this.RaisePropertyChanged("TotalDays");
            this.RaisePropertyChanged("CurrentCountry");
            this.RaisePropertyChanged("IsWithCrossBorder");
            this.RaisePropertyChanged("StartDate");
            this.RaisePropertyChanged("EndDate");
            this.RaisePropertyChanged("TotalCost");
            this.RaisePropertyChanged("TotalAccomodationCost");
            this.RaisePropertyChanged("TotalTransportationCost");
            this.RaisePropertyChanged("TotalSightseeingCost");
            this.RaisePropertyChanged("TotalOtherCost");
            this.RaisePropertyChanged("TotalMovingDistance");
            this.RaisePropertyChanged("TotalMovingTime");
            this.RaisePropertyChanged("Countries");
            this.RaisePropertyChanged("TotalCalcCountries");
            this.RaisePropertyChanged("TotalCountries");
            this.RaisePropertyChanged("StartCalctDate");
            this.RaisePropertyChanged("EndCalcDate");
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

        public DateTime StartDate
        {
            get
            {
                if (model_ == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return model_.StartDate;
                }
            }
            set
            {
                model_.StartDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                if (model_ == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return model_.EndDate;
                }
            }
            set
            {
                model_.EndDate = value;
            }
        }

        public string StartCalctDate
        {
            get
            {
                var date = DateTime.Now;
                if(model_ != null && model_.StartCalcDate != null)
                {
                    date = (DateTime)model_.StartCalcDate;
                }
                return date.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
            }
        }

        public string EndCalcDate
        {
            get
            {
                var date = DateTime.Now;
                if (model_ != null && model_.EndCalcDate != null)
                {
                    date = (DateTime)model_.EndCalcDate;
                }
                return date.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
            }
        }

        public int TotalDays
        {
            get
            {
                if(model_ == null)
                {
                    return 0;
                }
                else
                {
                    return model_.TotalCalcDays;
                }
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
                this.RaisePropertyChanged("TotalCost");
               
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
                this.RaisePropertyChanged("TotalCost");
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
                this.RaisePropertyChanged("TotalCost");
            }
        }


   





        public string TotalCost
        {
            get
            {

                if (model_ == null)
                {
                    
                    return "0";
                }
                else
                {
                    var cultureStr = base.GetCurrencyStr(model_.CurrentMajorCurrencyType);
                    return model_.CalcTotalCost().ToString("C", CultureInfo.CreateSpecificCulture(cultureStr));                   
                }
            }
        }

        public string TotalAccomodationCost
        {
            get
            {

                if (model_ == null)
                {

                    return "0";
                }
                else
                {
                    var cultureStr = base.GetCurrencyStr(model_.CurrentMajorCurrencyType);
                    return model_.CalcAccomodationTotalCost().ToString("C", CultureInfo.CreateSpecificCulture(cultureStr));
                }
            }
        }

        public string TotalTransportationCost
        {
            get
            {

                if (model_ == null)
                {

                    return "0";
                }
                else
                {
                    var cultureStr = base.GetCurrencyStr(model_.CurrentMajorCurrencyType);
                    return model_.CalcTransportationCost().ToString("C", CultureInfo.CreateSpecificCulture(cultureStr));
                }
            }
        }

        public string TotalSightseeingCost
        {
            get
            {

                if (model_ == null)
                {

                    return "0";
                }
                else
                {
                    var cultureStr = base.GetCurrencyStr(model_.CurrentMajorCurrencyType);
                    return model_.CalcSightseeingCost().ToString("C", CultureInfo.CreateSpecificCulture(cultureStr));
                }
            }
        }

        public string TotalOtherCost
        {
            get
            {

                if (model_ == null)
                {

                    return "0";
                }
                else
                {
                    var cultureStr = base.GetCurrencyStr(model_.CurrentMajorCurrencyType);
                    return model_.CalcOtherCost().ToString("C", CultureInfo.CreateSpecificCulture(cultureStr));
                }
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
