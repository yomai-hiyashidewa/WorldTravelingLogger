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
        MainModel model_;

        public SightSeeingViewModel()
        {
            // dummy
        }

        public SightSeeingViewModel(MainModel model)
        {
            model_ = model;
            model.ControlChanged_ += Model_ControlChanged_;
            currentSightSeeingType_ = SightseeigType.Tour;
        }

        private void UpdeteAll()
        {
            this.RaisePropertyChanged("TypeSightseeings");
            this.RaisePropertyChanged("Sightseeings");
        }

        private void Model_ControlChanged_(object? sender, EventArgs e)
        {
            UpdeteAll();
        }

        public SightseeingTypeModel[] TypeSightseeings
        {
            get
            {
                if (model_ == null)
                {
                    return null;
                }
                else
                {
                    return model_.GetTypeSightseeings();

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
                    this.RaisePropertyChanged("Sightseeings");
                }
            }
        }

        public SightseeingModel[] Sightseeings
        {
            get
            {
                if (model_ == null)
                {
                    return null;
                }
                else
                {
                    var value = model_.GetSightseeings().Where(j => j.SightseeigType == currentSightSeeingType_);
                    return value.ToArray();

                }
            }
        }


    }
}
