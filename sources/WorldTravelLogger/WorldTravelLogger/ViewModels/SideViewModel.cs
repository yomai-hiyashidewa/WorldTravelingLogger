using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            model_.ControlChanged_ += Model__ControlChanged_;
        }

        private void Model__ControlChanged_(object? sender, EventArgs e)
        {
            this.RaisePropertyChanged("IsWorld");
            this.RaisePropertyChanged("IsCountryMode");
            this.RaisePropertyChanged("CurrentCountry");
            this.RaisePropertyChanged("StartDate");
            this.RaisePropertyChanged("EndDate");
            this.RaisePropertyChanged("TotalDays");
            this.RaisePropertyChanged("Countries");
            this.RaisePropertyChanged("IsCurrencyJPY");
            this.RaisePropertyChanged("IsCurrencyUSD");
            this.RaisePropertyChanged("IsCurrencyEUR");
            this.RaisePropertyChanged("TotalCost");
            this.RaisePropertyChanged("CurrentMajorCurrencyType");
            


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
                    var date = EndDate - StartDate;
                    return (int)date.TotalDays;
                }
            }
        }


        public int Countries
        {
            get
            {
                if(model_ == null)
                {
                    return 0;
                }
                else
                {
                    return 0;   // not yet
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


        private string CurrencyStr
        {
            get
            {
                string culture = "ja-JP";
                if(model_.CurrentMajorCurrencyType == MajorCurrencytype.USD)
                {
                    culture = "en-US";
                }
                else if(model_.CurrentMajorCurrencyType == MajorCurrencytype.EUR)
                {
                    culture = "fr-FR";
                }
                return culture;
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

                    return model_.CalcTotalCost().ToString("C", CultureInfo.CreateSpecificCulture(CurrencyStr));                   
                }
            }
        }

       


    }
}
