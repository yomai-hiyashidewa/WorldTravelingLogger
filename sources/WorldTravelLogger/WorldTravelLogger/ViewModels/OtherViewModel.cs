using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;

namespace WorldTravelLogger.ViewModels
{
    public class OtherViewModel : ViewModelBase
    {
        MainModel model_;
        public OtherViewModel()
        {
            // dummy;
        }

        public OtherViewModel(MainModel model)
        {
            model_ = model;
            model_.CalcCompleted_ += Model__CalcCompleted_;
        }

        private void Model__CalcCompleted_(object? sender, EventArgs e)
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
                if (model_ == null)
                {
                    return null;
                }
                else
                {
                    return model_.GetTypeOthers();

                }
            }
        }

       

        public SightseeigType CurrentOtherType
        {
            get 
            {
                if(model_ == null)
                {
                    return SightseeigType.Accident;
                }
                else
                {
                    return model_.CurrentOtherType;
                }
            }
            set
            {
                if (model_.CurrentOtherType != value)
                {
                    model_.CurrentOtherType = value;
                    this.RaisePropertyChanged("Others");
                }
            }
        }

        public SightseeigType[] CurrentOtherTypes
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
                    list.AddRange(model_.GetCurrentOtherTypes());
                    return list.ToArray();
                }
            }
        }

        public bool EnableCurrentOtherType
        {
            get
            {
                if (model_ == null)
                {
                    return false;
                }
                else
                {
                    return CurrentOtherTypes.Count() > 0;
                }
            }
        }

        public SightseeingModel[] Others
        {
            get
            {
                if (model_ == null)
                {
                    return null;
                }
                else
                {
                    var value = model_.GetOthers().Where(j => j.SightseeigType == CurrentOtherType);
                    return value.ToArray();

                }
            }
        }
    }
}
