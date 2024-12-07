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
            model_.FileLoaded_ += Model__FileLoaded_;
            model_.ControlChanged_ += Model__ControlChanged_;
            currentAccomodationtype_ = AccomodationType.Domitory;
        }

     
        private void UpdateAll()
        {
            this.RaisePropertyChanged("TypeAccomodations");
            this.RaisePropertyChanged("Accomodations");
        }

        private void Model__FileLoaded_(object? sender, FileLoadedEventArgs e)
        {
            //this.UpdateAll();
        }

        private void Model__ControlChanged_(object? sender, EventArgs e)
        {
            this.UpdateAll();
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
                    return model_.GetTypeAccomodations();
                }
            }
        }

        private AccomodationType currentAccomodationtype_;

        public AccomodationType CurrentAccomodationType
        {
            get { return currentAccomodationtype_; }
            set
            {
                if (currentAccomodationtype_ != value)
                {
                    currentAccomodationtype_ = value;
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
                    var value = model_.GetAccomodations().Where(j => j.Accomodation == CurrentAccomodationType);
                    return value.ToArray();
                }
            }
        }


    }
       


}
