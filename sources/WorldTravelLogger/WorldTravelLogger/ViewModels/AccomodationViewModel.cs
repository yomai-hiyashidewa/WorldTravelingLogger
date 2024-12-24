using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WorldTravelLogger.Models;

namespace WorldTravelLogger.ViewModels
{
    public class AccomodationViewModel : ViewModelBase
    {
        AccomodationList list_;

        public AccomodationViewModel(AccomodationList list)
        {
            list_ = list;
            list_.ListChanged += List_ListChanged;
        }



        private void List_ListChanged(object? sender, EventArgs e)
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
                return list_.GetTypeArray();
            }
        }



        public AccomodationType CurrentAccomodationType
        {

            get { return list_.CurrentAccomodationtype; }
            set {
                if(list_.CurrentAccomodationtype != value)
                {
                    list_.CurrentAccomodationtype = value;
                    this.RaisePropertyChanged("Accomodations");
                }
                 
            }

        }

        public bool EnableCurrentAccomodationType
        {
            get
            {
                return CurrentAccomodationTypes.Count() > 0;
            }
        }

        public AccomodationType[] CurrentAccomodationTypes
        {
            get
            {
                var list = new List<AccomodationType>();
                list.AddRange(list_.GetCurrentAccomodationTypes());
                return list.ToArray();
            }
        }

        public AccomodationModel[] Accomodations
        {
            get
            {
                var value = list_.GetCalcArray().Where(j => j.Accomodation == CurrentAccomodationType);
                return value.ToArray();

            }
        }
    }



}
