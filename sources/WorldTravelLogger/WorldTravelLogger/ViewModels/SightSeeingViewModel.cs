using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;

namespace WorldTravelLogger.ViewModels
{
    public class SightSeeingViewModel : ViewModelBase
    {
        SightSeeingList list_;

        public SightSeeingViewModel()
        {
            // dummy
        }

        public SightSeeingViewModel(SightSeeingList list)
        {
            list_ = list;
            currentSightSeeingType_ = SightseeigType.Tour;
        }

        public SightseeingTypeModel[] TypeSightseeings
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

        private SightseeigType currentSightSeeingType_;

        public SightseeigType CurrentSightSeeingType
        {
            get { return currentSightSeeingType_; }
            set
            {
                if (currentSightSeeingType_ != value)
                {
                    currentSightSeeingType_ = value;
                    this.RaisePropertyChanged("TypeSightseeings");
                    this.RaisePropertyChanged("Sightseeings");
                }
            }
        }

        public SightseeingModel[] Sightseeings
        {
            get
            {
                if (list_ == null)
                {
                    return null;
                }
                else
                {
                    var value = list_.GetArray().Where(j => j.SightseeigType == currentSightSeeingType_);
                    return value.ToArray();

                }
            }
        }


    }
}
