﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
            model_.FileLoaded_ += Model__FileLoaded_;
            model.ControlChanged_ += Model_ControlChanged_;
        }

        private void Model__FileLoaded_(object? sender, FileLoadedEventArgs e)
        {
            if (e.Type == ListType.SightSeeingList || e.Type == ListType.ExchangeRateList)
            {
                if (model_.ReadySightseeings)
                {
                    UpdateAll();
                }
            }
        }

        private void UpdateAll()
        {
            this.RaisePropertyChanged("TypeSightseeings");
            this.RaisePropertyChanged("CurrentSightseeingTypes");
            this.RaisePropertyChanged("CurrentSightSeeingType");
            this.RaisePropertyChanged("EnableCurrentSightseeingType");
            this.RaisePropertyChanged("Sightseeings");
            
        }

      


        private void Model_ControlChanged_(object? sender, EventArgs e)
        {
            UpdateAll();
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

        

        public SightseeigType CurrentSightSeeingType
        {
            get 
            {
                if(model_ == null)
                {
                    return SightseeigType.HotSpring;
                }
                else
                {
                    return model_.CurrentSightSeeingType;
                }
            }
            set
            {
                if (model_.CurrentSightSeeingType != value)
                {
                    model_.CurrentSightSeeingType = value;
                    this.RaisePropertyChanged("Sightseeings");
                }
            }
        }

        public SightseeigType[] CurrentSightseeingTypes
        {
            get
            {
                if (model_ == null)
                {
                    return [];
                }
                else
                {
                    var list = new List<SightseeigType>();
                    list.AddRange(model_.GetCurrentSightseeingTypes());
                    return list.ToArray();
                }
            }
        }

        public bool EnableCurrentSightseeingType
        {
            get
            {
                if (model_ == null)
                {
                    return false;
                }
                else
                {
                    return CurrentSightseeingTypes.Count() > 0;
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
                    var value = model_.GetSightseeings().Where(j => j.SightseeigType == CurrentSightSeeingType);
                    return value.ToArray();

                }
            }
        }


    }
}
