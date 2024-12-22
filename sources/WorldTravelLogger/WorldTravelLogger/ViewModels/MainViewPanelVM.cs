using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;

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
        }

        private void Model__FileLoaded_(object? sender, FileLoadedEventArgs e)
        {
         if(FileLoaded_ != null)
            {
                FileLoaded_.Invoke(sender, e);
            }
            if (model_.ReadyApplication)
            {
                this.RaisePropertyChanged("IsWithAirplane");
                this.RaisePropertyChanged("IsWithInsurance");
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
                    return false;
                }
            }
            set
            {
                // mot yet
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
            return new AccomodationViewModel(model_);
        }

        public TransportationViewModel GetTransporationViewModel()
        {
            return new TransportationViewModel(model_);
        }


        public SightSeeingViewModel GetSightseeingViewModel()
        {
            return new SightSeeingViewModel(model_);
        }

        public OtherViewModel GetOtherViewModel()
        {
            return new OtherViewModel(model_);
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
