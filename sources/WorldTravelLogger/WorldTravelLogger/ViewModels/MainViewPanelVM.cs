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

        public event EventHandler<FileLoadedEventArgs> FileLoaded_;

        public MainViewPanelVM()
        {
            model_ = new MainModel();
            model_.FileLoaded_ += Model__FileLoaded_; 
        }

        private void Model__FileLoaded_(object? sender, FileLoadedEventArgs e)
        {
         if(FileLoaded_ != null)
            {
                FileLoaded_.Invoke(sender, e);
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
