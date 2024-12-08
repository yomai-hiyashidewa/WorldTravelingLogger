using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorldTravelLogger.Models;

namespace WorldTravelLogger.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CanExecuteChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var h = this.PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected string GetCurrencyStr(MajorCurrencytype type)
        {
            string culture = "ja-JP";
            if (type == MajorCurrencytype.USD)
            {
                culture = "en-US";
            }
            else if (type == MajorCurrencytype.EUR)
            {
                culture = "fr-FR";
            }
            return culture;
        }
    }
}
