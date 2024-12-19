using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;

namespace WorldTravelLogger.ViewModels
{
    public class UpperViewModel: ViewModelBase
    {
        private MainModel? model_;
        public UpperViewModel()
        {
            // dummmy
        }

        public UpperViewModel(MainModel model)
        {
            model_ = model;
            model_.FileLoaded_ += Model__FileLoaded_;
            model_.ControlChanged_ += Model__ControlChanged_;
        }

        private void Model__ControlChanged_(object? sender, EventArgs e)
        {
            UpdateView();
        }

        private void Model__FileLoaded_(object? sender, FileLoadedEventArgs e)
        {
            if (model_.ReadyApplication)
            {
                UpdateView();
            }
        }

        private void UpdateView()
        {
            this.RaisePropertyChanged("StartDate");
            this.RaisePropertyChanged("EndDate");

            this.RaisePropertyChanged("StartCalctDate");
            this.RaisePropertyChanged("EndCalcDate");
            this.RaisePropertyChanged("TotalDays");
        }

        public DateTime StartDate
        {
            get
            {
                if (model_ == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return model_.StartDate;
                }
            }
            set
            {
                model_.StartDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                if (model_ == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return model_.EndDate;
                }
            }
            set
            {
                model_.EndDate = value;
            }
        }


        public string StartCalctDate
        {
            get
            {
                var date = DateTime.Now;
                if (model_ != null && model_.StartCalcDate != null)
                {
                    date = (DateTime)model_.StartCalcDate;
                }
                return date.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
            }
        }

        public string EndCalcDate
        {
            get
            {
                var date = DateTime.Now;
                if (model_ != null && model_.EndCalcDate != null)
                {
                    date = (DateTime)model_.EndCalcDate;
                }
                return date.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
            }
        }

        public string TotalDays
        {
            get
            {
                if (model_ == null)
                {
                    return "days";
                }
                else
                {
                    return model_.TotalCalcDays + " days";
                }
            }
        }
    }
}
