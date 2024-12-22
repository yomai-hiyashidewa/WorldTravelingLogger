using System;
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
        SightSeeingList list_;


        public SightSeeingViewModel(SightSeeingList list)
        {
            list_ = list;
            list_.ListChanged += List__ListChanged;
        }

        private void List__ListChanged(object? sender, EventArgs e)
        {
            UpdateAll();
        }




        private void UpdateAll()
        {
            this.RaisePropertyChanged("TypeSightseeings");
            this.RaisePropertyChanged("CurrentSightseeingTypes");
            this.RaisePropertyChanged("CurrentSightSeeingType");
            this.RaisePropertyChanged("EnableCurrentSightseeingType");
            this.RaisePropertyChanged("Sightseeings");

        }

        public SightseeingTypeModel[] TypeSightseeings
        {
            get
            {
                return list_.GetTypeArray();
            }
        }



        public SightseeigType CurrentSightSeeingType
        {
            get
            {
                return list_.CurrentSightSeeingType;
            }
            set
            {
                if (list_.CurrentSightSeeingType != value)
                {
                    list_.CurrentSightSeeingType = value;
                    this.RaisePropertyChanged("Sightseeings");
                }
            }
        }

        public SightseeigType[] CurrentSightseeingTypes
        {
            get
            {
                var list = new List<SightseeigType>();
                list.AddRange(list_.GetCurrentSightSeeingTypes());
                return list.ToArray();
            }
        }

        public bool EnableCurrentSightseeingType
        {
            get
            {
                return CurrentSightseeingTypes.Count() > 0;
            }
        }


        public SightseeingModel[] Sightseeings
        {
            get
            {
                var value = list_.GetCalcArray().Where(j => j.SightseeigType == CurrentSightSeeingType);
                return value.ToArray();
            }
        }


    }
}
