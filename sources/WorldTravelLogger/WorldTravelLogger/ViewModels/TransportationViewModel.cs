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
        TransportationList list_;
        ControlModel control_;



        public TransportationViewModel(TransportationList list,ControlModel control)
        {
            list_ = list;
            control_ = control;
            list.ListChanged += List_ListChanged;


        }

        private void List_ListChanged(object? sender, EventArgs e)
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

        public TransportationTypeModel[] TypeTransportations
        {
            get
            {
                return list_.GetTypeArray();
            }
        }



        public Transportationtype CurrentTransportationType
        {
            get
            {
                return list_.CurrentTransportationType;
            }
            set
            {
                if (list_.CurrentTransportationType != value)
                {
                    list_.CurrentTransportationType = value;
                    this.RaisePropertyChanged("Transportations");
                }
            }
        }

        public Transportationtype[] CurrentTransportationTypes
        {
            get
            {
                var list = new List<Transportationtype>();
                list.AddRange(list_.GetCurrentTransportationTypes());
                return list.ToArray();
            }
        }

        public bool EnableCurrentTransportationType
        {
            get
            {
                return CurrentTransportationTypes.Count() > 0;
            }
        }

        public TransportationModel[] Transportations
        {
            get
            {
                var value = list_.GetCalcArray(control_.IsCountryRegion).Where(j => j.Transportationtype == CurrentTransportationType);
                return value.ToArray();
            }
        }





    }
}
