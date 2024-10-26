using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    internal class MainModel
    {
        private ExchangeRater exchangeRater_;
        private OptionModel option_;

        public event EventHandler<ErrorTypes> ExchangeRateLoaded_;

        private void SetOptionModel()
        {
            option_ = new OptionModel();
            option_.AccomodationPathChanged += Option__AccomodationPathChanged;
            option_.TransportationPathChanged += Option__TransportationPathChanged;
            option_.SightseeingPathChanged += Option__SightseeingPathChanged;
            option_.ExchangeRatePathChanged += Option__ExchangeRatePathChanged;
        }

        private void DeleteOptionModel()
        {
            option_.AccomodationPathChanged -= Option__AccomodationPathChanged;
            option_.TransportationPathChanged -= Option__TransportationPathChanged;
            option_.SightseeingPathChanged -= Option__SightseeingPathChanged;
            option_.ExchangeRatePathChanged -= Option__ExchangeRatePathChanged;
            option_ = null;
        }

        private void Option__ExchangeRatePathChanged(object? sender, EventArgs e)
        {
           if(!string.IsNullOrWhiteSpace( option_.ExchangeRatePath))
            {
                var result = exchangeRater_.Load(option_.ExchangeRatePath);
                if(ExchangeRateLoaded_ != null)
                {
                    ExchangeRateLoaded_.Invoke(this, result);
                }
            }
        }

        private void Option__SightseeingPathChanged(object? sender, EventArgs e)
        {
            // not yet
        }

        private void Option__TransportationPathChanged(object? sender, EventArgs e)
        {
            // not yet
        }

        private void Option__AccomodationPathChanged(object? sender, EventArgs e)
        {
            // not yet
        }

        public MainModel()
        {
            SetOptionModel();
            exchangeRater_ = new ExchangeRater();
           
        }
        
        public void Init()
        {
            
        }

        public OptionModel GetOptionModel()
        {
            return option_;
        }
    }



    
}
