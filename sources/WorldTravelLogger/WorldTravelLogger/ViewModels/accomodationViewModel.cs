using System;
using System.Collections.Generic;
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
            currentAccomodationtype_ = AccomodationType.Domitory;
        }


        public AccomodationTypeModel[] TypeAccomodations
        {
            get
            {
                if(model_ == null)
                {
                    return null;
                }
                else
                {
                    return model_.GetAccomodationTypeArray();
                }
            }
        }

        private AccomodationType currentAccomodationtype_;

        public AccomodationType CurrentAccomodationType
        {
            get { return currentAccomodationtype_; }
            set
            {
                if(currentAccomodationtype_ != value)
                {
                    currentAccomodationtype_ = value;
                    this.RaisePropertyChanged("TypeAccomodations");
                    this.RaisePropertyChanged("Accomodations");
                }
            }
        }

        public AccomodationModel[] Accomodations
        {
            get
            {
                if(model_ == null)
                {
                    return null;
                }
                else
                {
                    return model_.GetAccomodations(CurrentAccomodationType);
                }
            }
        }


    }
       


}
