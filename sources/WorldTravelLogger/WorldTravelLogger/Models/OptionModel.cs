using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class OptionModel
    {
        private string? accomodationPath_;

        private string? transportationPath_;

        private string? sightseeingPath_;

        private string? exchangeRatePath_;

        public event EventHandler AccomodationPathChanged;
        public event EventHandler TransportationPathChanged;
        public event EventHandler SightseeingPathChanged;
        public event EventHandler ExchangeRatePathChanged;

        

        public string? AccomodationPath
        {
            get { return accomodationPath_; }
            set
            {
                if (accomodationPath_ != value)
                {
                    accomodationPath_ = value;
                    if (AccomodationPathChanged != null)
                    {
                        AccomodationPathChanged.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public string? TransportationPath
        {
            get
            {
                return transportationPath_;
            }
            set
            {
                if (transportationPath_ != value)
                {
                    transportationPath_ = value;
                    if (TransportationPathChanged != null)
                    {
                        TransportationPathChanged.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public string? SightseeingPath 
        { 
            get
            {
                return sightseeingPath_;
            }
            set
            {
                if(sightseeingPath_ != value)
                {
                    sightseeingPath_ = value;
                    if(sightseeingPath_ != null)
                    {
                        SightseeingPathChanged.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public string? ExchangeRatePath 
        { 
            get
            {
                return exchangeRatePath_;
            }
            set
            {
                if(exchangeRatePath_ != value)
                {
                    exchangeRatePath_ = value;
                    if(ExchangeRatePathChanged != null)
                    {
                        ExchangeRatePathChanged.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public void Reload(ListType type)
        {
            string buff = "";
            switch (type)
            {
                case ListType.AccomodationList:
                    buff = accomodationPath_;
                    accomodationPath_ = null;
                    AccomodationPath = buff;
                    break;
                case ListType.TransportationList:
                    buff = transportationPath_;
                    transportationPath_ = null;
                    TransportationPath = buff;
                    break;
                case ListType.SightSeeingList:
                    buff = sightseeingPath_;
                    sightseeingPath_ = null;
                    SightseeingPath = buff;
                    break;
                case ListType.ExchangeRateList:
                    buff = exchangeRatePath_;
                    exchangeRatePath_ = null;
                    ExchangeRatePath = buff;
                    break;
            }
        }


    }
}
