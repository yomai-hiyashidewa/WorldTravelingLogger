using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WorldTravelLogger.Models;
using WorldTravelLogger.Models.Context;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.Models.Interface;
using WorldTravelLogger.Models.List;
using WorldTravelLogger.ViewModels.Base;

namespace WorldTravelLogger.ViewModels
{
    public class OtherViewModel : BaseContextListViewModel
    {
        SightSeeingList list_;

        

        public OtherViewModel(SightSeeingList list,ControlModel control):
            base(control)
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
            this.RaisePropertyChanged("TypeOthers");
            this.RaisePropertyChanged("CurrentOtherTypes");
            this.RaisePropertyChanged("CurrentOtherType");
            this.RaisePropertyChanged("EnableCurrentOtherType");
            this.RaisePropertyChanged("Others");
           
        }

       
        private SightseeigType? ConvertType(string typeStr)
        {
            SightseeigType type;
            if (Enum.TryParse(typeStr, out type))
            {
                return type;
            }
            else
            {
                return null;
            }
        }

        


        public SightseeingTypeModel[] TypeOthers
        {
            get
            {
                return list_.TypeSightseeings;
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

        public bool EnableCurrentOtherType
        {
            get
            {
                return CurrentOtherTypes.Count() > 0;
            }
        }

        public SightseeigType[] CurrentOtherTypes
        {
            get
            {
                return list_.CurrentSightseeingTypes;
            }

        }

      

        public SightseeingModel[] Others
        {
            get
            {
                return list_.GetCalcs(control_.IsCountryRegion).OfType<SightseeingModel>().
                    Where(m => m.SightseeigType == list_.CurrentSightSeeingType).ToArray();
            }
        }
    }
}
