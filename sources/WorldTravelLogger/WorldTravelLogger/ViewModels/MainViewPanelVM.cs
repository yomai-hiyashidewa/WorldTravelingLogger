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
        public MainViewPanelVM()
        {
            model_ = new MainModel();
        }

        

        public void Init()
        {
            model_.Init();
        }

        public OptionWindowViewModel GetOptionWindowViewModel()
        {
            return new OptionWindowViewModel(model_.GetOptionModel());
        }
    }
}
