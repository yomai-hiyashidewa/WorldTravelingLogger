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
        MainModel model_;

        public TransportationViewModel()
        {
            // dummy
        }

        public TransportationViewModel(MainModel model)
        {
            model_ = model;
            model.ControlChanged_ += Model_ControlChanged_;
            currentTransportationType_ = Transportationtype.Train;
        }

        private void UpdateAll()
        {
            this.RaisePropertyChanged("TypeTransportations");
            this.RaisePropertyChanged("Transportations");
        }

        private void Model_ControlChanged_(object? sender, EventArgs e)
        {
            UpdateAll();
        }

        public TransportationTypeModel[] TypeTransportations
        {
            get
            {
                if (model_ == null)
                {
                    return null;
                }
                else
                {
                    return model_.GetTypeTransportations();
                    
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
                if (model_ == null)
                {
                    return null;
                }
                else
                {
                    var value = model_.GetTransportations().Where(j => j.Transportationtype == CurrentTransportationType);
                    return value.ToArray();

                }
            }
        }


    }
}
