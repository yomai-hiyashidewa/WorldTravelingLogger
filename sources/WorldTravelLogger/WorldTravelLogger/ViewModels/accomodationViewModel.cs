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
        AccomodationList list_;
        public AccomodationViewModel()
        {
            // dummy
        }

        public AccomodationViewModel(AccomodationList list)
        {
            list_ = list;
            currentAccomodationtype_ = AccomodationType.Domitory;
        }


        public AccomodationTypeModel[] TypeAccomodations
        {
            get
            {
                if(list_ == null)
                {
                    return null;
                }
                else
                {
                    return list_.GetTypeArray();
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
                if(list_ == null)
                {
                    return null;
                }
                else
                {
                    var value = list_.GetArray().Where(j => j.Accomodation == CurrentAccomodationType);
                    return value.ToArray();
                }
            }
        }


    }
       


}
