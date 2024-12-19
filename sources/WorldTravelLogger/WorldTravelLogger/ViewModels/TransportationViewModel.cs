using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
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
            model_.FileLoaded_ += Model__FileLoaded_;
            model_.TransportationChanged_ += Model__TransportationChanged_;
        }

        private void Model__FileLoaded_(object? sender, FileLoadedEventArgs e)
        {
            if (e.Type == ListType.TransportationList || e.Type == ListType.ExchangeRateList)
            {
                if (model_.ReadyTransportations)
                {
                    UpdateAll();
                }
            }
        }

        private void Model__TransportationChanged_(object? sender, EventArgs e)
        {
            UpdateAll();
        }

        private void UpdateAll()
        {
            this.RaisePropertyChanged("TypeTransportations");
            this.RaisePropertyChanged("CurrentTransportationTypes");
            this.RaisePropertyChanged("CurrentTransportationType");
            this.RaisePropertyChanged("EnableCurrentTransportationType");
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

       

        public Transportationtype CurrentTransportationType
        {
            get 
            {
                if(model_ == null)
                {
                    return Transportationtype.Train;
                }
                else
                {
                    return model_.CurrentTransportationType;
                }
                 
            }
            set
            {
                if (model_.CurrentTransportationType != value)
                {
                    model_.CurrentTransportationType = value;
                    this.RaisePropertyChanged("Transportations");
                }
            }
        }

        public Transportationtype[] CurrentTransportationTypes
        {
            get
            {
                if (model_ == null)
                {
                    return [];
                }
                else
                {
                    var list = new List<Transportationtype>();
                    list.AddRange(model_.GetCurrentTransportationTypes());
                    return list.ToArray();
                }
            }
        }

        public bool EnableCurrentTransportationType
        {
            get
            {
                if (model_ == null)
                {
                    return false;
                }
                else
                {
                    return CurrentTransportationTypes.Count() > 0;
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
