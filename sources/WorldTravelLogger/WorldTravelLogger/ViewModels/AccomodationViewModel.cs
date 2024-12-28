using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WorldTravelLogger.Models;
using WorldTravelLogger.Models.Base;
using WorldTravelLogger.Models.Context;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.Models.Interface;
using WorldTravelLogger.Models.List;
using WorldTravelLogger.ViewModels.Base;

namespace WorldTravelLogger.ViewModels
{
    public class AccomodationViewModel : BaseContextListViewModel
    {
        private AccomodationList list_;
        


        public AccomodationViewModel(AccomodationList list, ControlModel control): 
            base(control)
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
                return list_.TypeAccomodations;
            }
        }

        public AccomodationType CurrentAccomodationType
        {

            get { return list_.CurrentAccomodationtype; }
            set
            {
                if (list_.CurrentAccomodationtype != value)
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
                return list_.CurrentAccomodationTypes;
            }
        }

        public AccomodationModel[] Accomodations
        {
            get
            {
                return list_.GetCalcs(control_.IsCountryRegion).OfType<AccomodationModel>().
                    Where(m => m.Accomodation == CurrentAccomodationType).ToArray();


            }
        }


    }



}
