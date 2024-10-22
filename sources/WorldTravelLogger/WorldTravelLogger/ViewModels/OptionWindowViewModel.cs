using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WorldTravelLogger.Models;

namespace WorldTravelLogger.ViewModels
{
    public class OptionWindowViewModel : ViewModelBase
    {
        private OptionModel model_;
        public OptionWindowViewModel(OptionModel model)
        {
            model_ = model;
        }
    }
}
