using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;

namespace WorldTravelLogger.ViewModels
{
    public class TransportationViewModel : ViewModelBase
    {
        TranspotationList list_;

        public TransportationViewModel()
        {
            // dummy
        }

        public TransportationViewModel(TranspotationList list)
        {
            list_ = list;
            currentTransportationType_ = Transportationtype.Train;
        }

        public TransportationTypeModel[] TypeTransportations
        {
            get
            {
                if (list_ == null)
                {
                    return null;
                }
                else
                {
                    return list_.GetTypeArray();
                    
                }
            }
        }

        private Transportationtype currentTransportationType_;

        public Transportationtype CurrentTransportationType
        {
            get { return currentTransportationType_; }
            set
            {
                if (currentTransportationType_ != value)
                {
                    currentTransportationType_ = value;
                    this.RaisePropertyChanged("TypeTransportations");
                    this.RaisePropertyChanged("Transportations");
                }
            }
        }

        public TransportationModel[] Transportations
        {
            get
            {
                if (list_ == null)
                {
                    return null;
                }
                else
                {
                    var value = list_.GetArray().Where(j => j.Transportationtype == CurrentTransportationType);
                    return value.ToArray();

                }
            }
        }


    }
}
