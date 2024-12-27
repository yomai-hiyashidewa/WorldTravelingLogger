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
    public class OtherViewModel : ViewModelBase
    {
        SightSeeingList list_;
        ControlModel control_;

        public OtherViewModel(SightSeeingList list,ControlModel control)
        {
            list_ = list;
            control_ = control;
            list_.ListChanged += List__ListChanged;
        }

        private void List__ListChanged(object? sender, EventArgs e)
        {
            UpdateAll();
        }

        private void UpdateAll()
        {
            this.RaisePropertyChanged("TypeOthers");
            this.RaisePropertyChanged("CurrentOtherTypes");
            this.RaisePropertyChanged("CurrentOtherType");
            this.RaisePropertyChanged("EnableCurrentOtherType");
            this.RaisePropertyChanged("Others");
           
        }


        public SightseeingTypeModel[] TypeOthers
        {
            get
            {
                return list_.GetTypeArray();
            }
        }

       

        public SightseeigType CurrentOtherType
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
                    this.RaisePropertyChanged("Others");
                }
            }
        }

        public SightseeigType[] CurrentOtherTypes
        {
            get
            {
                var list = new List<SightseeigType>();
                list.AddRange(list_.GetCurrentSightSeeingTypes());
                return list.ToArray();
            }
        }

        public bool EnableCurrentOtherType
        {
            get
            {
                return CurrentOtherTypes.Count() > 0;
            }
        }

        public SightseeingModel[] Others
        {
            get
            {
                var value = list_.GetCalcArray(control_.IsCountryRegion).Where(j => j.SightseeigType == CurrentOtherType);
                return value.ToArray();
            }
        }
    }
}
