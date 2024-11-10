using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
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

        private void ReloadAccomodation()
        {
            model_.Reload(ListType.AccomodationList);
        }



        private DelegateCommand reloadAccomodationsCommand_;
        public DelegateCommand ReloadAccomodationsCommand
        {
            get
            {
                return reloadAccomodationsCommand_
                 ?? (reloadAccomodationsCommand_ = new DelegateCommand(
                 () =>
                 {
                     model_.Reload(ListType.AccomodationList);
                 }));
            }
        }

        private DelegateCommand reloadTransportsCommand_;
        public DelegateCommand ReloadTransportsCommand
        {
            get
            {
                return reloadTransportsCommand_
                 ?? (reloadTransportsCommand_ = new DelegateCommand(
                 () =>
                 {
                     model_.Reload(ListType.TransportationList);
                 }));
            }
        }

        private DelegateCommand reloadSightseeingsCommand_;
        public DelegateCommand ReloadSightseeingsCommand
        {
            get
            {
                return reloadSightseeingsCommand_
                 ?? (reloadSightseeingsCommand_ = new DelegateCommand(
                 () =>
                 {
                     model_.Reload(ListType.SightSeeingList);
                 }));
            }
        }

        private DelegateCommand reloadExchangeRatesCommand_;
        public DelegateCommand ReloadExchangeRatesCommand
        {
            get
            {
                return reloadExchangeRatesCommand_
                 ?? (reloadExchangeRatesCommand_ = new DelegateCommand(
                 () =>
                 {
                     model_.Reload(ListType.ExchangeRateList);
                 }));
            }
        }



        public string? AccomodationPath 
        { 
            get
            {
                if (!string.IsNullOrWhiteSpace(model_.AccomodationPath))
                {
                    return Path.GetFileName(model_.AccomodationPath);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                model_.AccomodationPath = value;
                this.RaisePropertyChanged("AccomodationPath");
            }
        }

        public string? TransportationPath
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(model_.TransportationPath))
                {
                    return Path.GetFileName(model_.TransportationPath);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                model_.TransportationPath = value;
                this.RaisePropertyChanged("TransportationPath");
            }
        }

        public string? SigntseeingPath
        {
            get
            {

                if (!string.IsNullOrWhiteSpace(model_.SightseeingPath))
                {
                    return Path.GetFileName( model_.SightseeingPath);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                model_.SightseeingPath = value;
                this.RaisePropertyChanged("SigntseeingPath");
            }
        }

        public string? ExchangeRatePath
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(model_.ExchangeRatePath))
                {
                    return Path.GetFileName( model_.ExchangeRatePath);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                model_.ExchangeRatePath = value;
                this.RaisePropertyChanged("ExchangeRatePath");
            }
        }
    }
}
