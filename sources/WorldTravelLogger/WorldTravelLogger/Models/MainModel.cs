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
        public MainModel()
        {
            exchangeRater_ = new ExchangeRater();
            option_ = new OptionModel();
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
