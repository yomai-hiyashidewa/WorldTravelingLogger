using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.Models.Utility;
using WorldTravelLogger.ViewModels.Base;

namespace WorldTravelLogger.ViewModels
{
    internal class MainViewPanelVM : ViewModelBase
    {
        private MainModel model_;
        private ControlModel control_;

        public event EventHandler<FileLoadedEventArgs> FileLoaded_;

        public MainViewPanelVM()
        {
            model_ = new MainModel();
            control_ = model_.GetControlModel();
            model_.FileLoaded_ += Model__FileLoaded_;
            model_.CalcCompleted_ += Model__CalcCompleted_;
        }

        private void Model__CalcCompleted_(object? sender, EventArgs e)
        {
            this.RaisePropertyChanged("IsWithAirplane");
            this.RaisePropertyChanged("IsWithInsurance");
        }

        private void Model__FileLoaded_(object? sender, FileLoadedEventArgs e)
        {
         if(FileLoaded_ != null)
            {
                FileLoaded_.Invoke(sender, e);
            }
        }


        public bool IsWithAirplane
        {
            get
            {
                if (model_ == null)
                {
                    return false;
                }
                else
                {
                    return control_.IsWithAirplane;
                }
            }
            set
            {
                control_.IsWithAirplane = value;
            }
        }

     
        public bool IsWithInsurance
        {
            get
            {
                if(model_ == null)
                {
                    return false;
                }
                else
                {
                    return control_.IsWithInsurance;
                }
            }
            set
            {
                control_.IsWithInsurance = value;
            }
        }

        public void Init()
        {
            model_.Init();
        }

        public void Exit()
        {
            model_.Exit();
        }

        public OptionWindowViewModel GetOptionWindowViewModel()
        {
            return new OptionWindowViewModel(model_.GetOptionModel());
        }

        public DebugWinViewModel GetDebugWinViewModel()
        {
            return new DebugWinViewModel(model_);
        }

        public UpperViewModel GetUpperViewModel()
        {
            return new UpperViewModel(model_);
        }

        public SideViewModel GetSideViewModel()
        {
            return new SideViewModel(model_);
        }

        public AccomodationViewModel GetAccomodationViewModel()
        {
            return new AccomodationViewModel(model_.GetAccomodationList(),model_.GetControlModel());
        }

        public TransportationViewModel GetTransporationViewModel()
        {
            return new TransportationViewModel(model_.GetTransportationList(), model_.GetControlModel());
        }


        public SightSeeingViewModel GetSightseeingViewModel()
        {
            return new SightSeeingViewModel(model_.GetSightSeeingList(), model_.GetControlModel());
        }

        public OtherViewModel GetOtherViewModel()
        {
            return new OtherViewModel(model_.GetOtherList(), model_.GetControlModel());
        }


        public string GetFilename(ListType type)
        {
            switch(type)
            {
                case ListType.AccomodationList:
                    return FileNames.AccomodationFile;
                case ListType.TransportationList:
                    return FileNames.TransportationFile;
                case ListType.SightSeeingList:
                    return FileNames.SightseeingFile;
                case ListType.ExchangeRateList:
                    return FileNames.ExchangeRateFile;
                default:
                    return null;
            }    
        }
    }
}
