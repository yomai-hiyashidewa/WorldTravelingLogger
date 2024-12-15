using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;

namespace WorldTravelLogger.ViewModels
{
    public class AccomodationViewModel : ViewModelBase
    {
        MainModel model_;
        public AccomodationViewModel()
        {
            // dummy
        }

        public AccomodationViewModel(MainModel model)
        {
            model_ = model;
            model_.FileLoaded_ += Model__FileLoaded_;
            model_.ControlChanged_ += Model__ControlChanged_;
        }


        private void UpdateAll()
        {
            this.RaisePropertyChanged("TypeAccomodations");
            this.RaisePropertyChanged("CurrentAccomodationTypes");
            this.RaisePropertyChanged("CurrentAccomodationType");
            this.RaisePropertyChanged("EnableCurrentAccomodationType");
            this.RaisePropertyChanged("TypeAccomodations");
            this.RaisePropertyChanged("Accomodations");
            this.RaisePropertyChanged("TotalCost");

        }

        private void Model__FileLoaded_(object? sender, FileLoadedEventArgs e)
        {
            if (e.Type == ListType.AccomodationList || e.Type == ListType.ExchangeRateList)
            {
                if (model_.ReadyAccomodations)
                {
                    UpdateAll();
                }
            }
        }

        private void Model__ControlChanged_(object? sender, EventArgs e)
        {
            this.UpdateAll();
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
                    return model_.CalcAccomodationTotalCost().ToString("C", CultureInfo.CreateSpecificCulture(cultureStr));
                }
            }
        }


        public AccomodationTypeModel[] TypeAccomodations
        {
            get
            {
                if (model_ == null)
                {
                    return null;
                }
                else
                {
                    return model_.GetTypeAccomodations();
                }
            }
        }



        public AccomodationType CurrentAccomodationType
        {
            get
            {
                if (model_ == null)
                {
                    return AccomodationType.Domitory;
                }
                else
                {
                    return model_.CurrentAccomodationtype;
                }

            }
            set
            {
                if (model_.CurrentAccomodationtype != value)
                {

                    model_.CurrentAccomodationtype = value;
                    this.RaisePropertyChanged("Accomodations");
                }
            }
        }

        public bool EnableCurrentAccomodationType
        {
            get
            {
                if (model_ == null)
                {
                    return false;
                }
                else
                {
                    return CurrentAccomodationTypes.Count() > 0;
                }
            }
        }

        public AccomodationType[] CurrentAccomodationTypes
        {
            get
            {
                if (model_ == null)
                {
                    return [];
                }
                else
                {
                    var list = new List<AccomodationType>();
                    list.AddRange(model_.GetCurrentAccomodationTypes());
                    return list.ToArray();
                }
            }
        }

        public AccomodationModel[] Accomodations
        {
            get
            {
                if (model_ == null)
                {
                    return null;
                }
                else
                {
                    var value = model_.GetAccomodations().Where(j => j.Accomodation == CurrentAccomodationType);
                    return value.ToArray();
                }
            }
        }


    }



}
