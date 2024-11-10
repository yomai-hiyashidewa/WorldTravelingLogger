using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;

namespace WorldTravelLogger.ViewModels
{

    public class DebugWinViewModel : ViewModelBase
    {
        private MainModel model_;

        public DebugWinViewModel(MainModel model_)
        {
            this.model_ = model_;
            model_.FileLoaded_ += Model__FileLoaded_;
        }

        private void Model__FileLoaded_(object? sender, FileLoadedEventArgs e)
        {
            switch (e.Type)
            {
                case ListType.AccomodationList:
                    this.RaisePropertyChanged("Accomodations");
                    break;
                case ListType.TransportationList:
                    this.RaisePropertyChanged("Transportations");
                    break;
                case ListType.SightSeeingList:
                    this.RaisePropertyChanged("Sightseeings");
                    break;
                case ListType.ExchangeRateList:
                    this.RaisePropertyChanged("ExchangeRates");
                    break;
                default:
                    break;
            }
        }

        public void Delete()
        {
            model_.FileLoaded_ -= Model__FileLoaded_;
        }

        public ListType[] Lists
        {
            get
            {
                return (ListType[])Enum.GetValues(typeof(ListType));
            }
        }

        public ListType CurrentListType
        {
            get { return model_.CurrentListType; }
            set
            {
                model_.CurrentListType = value; ;
            }
        }

        public object[] Accomodations
        {
            get
            {
                return model_.Accomodations;
            }
        }

        public object[] Transportations
        {
            get
            {
                return model_.Transportations;
            }
        }

        public object[] Sightseeings
        {
            get
            {
                return model_.Sightseeings;
            }
        }

        public object[] ExchangeRates
        {
            get
            {
                return model_.ExchangeRates;
            }
        }

        


    }
}
