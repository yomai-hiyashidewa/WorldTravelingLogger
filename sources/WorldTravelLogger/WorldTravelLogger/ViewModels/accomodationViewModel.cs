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
            model_.CalcCompleted_ += Model__CalcCompleted_;
        }

        private void Model__CalcCompleted_(object? sender, EventArgs e)
        {
            this.UpdateAll();   
        }

        private void UpdateAll()
        {
            this.RaisePropertyChanged("TypeAccomodations");
            this.RaisePropertyChanged("CurrentAccomodationTypes");
            this.RaisePropertyChanged("CurrentAccomodationType");
            this.RaisePropertyChanged("EnableCurrentAccomodationType");
            this.RaisePropertyChanged("TypeAccomodations");
            this.RaisePropertyChanged("Accomodations");
           

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
