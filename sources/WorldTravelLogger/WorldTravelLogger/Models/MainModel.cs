using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class MainModel
    {
        private ExchangeRater exchangeRater_;
        private AccomodationList accomodationList_;
        private TranspotationList transpotationList_;
        private SightSeeingList sightSeeingList_;

        private OptionModel option_;

        private ListType currentListType_;

        public event EventHandler<FileLoadedEventArgs> FileLoaded_;

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
            if (!string.IsNullOrWhiteSpace(option_.ExchangeRatePath))
            {
                var result = exchangeRater_.Load(option_.ExchangeRatePath, FileNames.ExchangeRateFile);
                if (FileLoaded_ != null)
                {
                    FileLoaded_.Invoke(this, new FileLoadedEventArgs(ListType.ExchangeRateList, result));
                }
            }
        }

        private void Option__SightseeingPathChanged(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(option_.SightseeingPath))
            {
                var result = sightSeeingList_.Load(option_.SightseeingPath, FileNames.SightseeingFile);
                if (FileLoaded_ != null)
                {
                    FileLoaded_.Invoke(this, new FileLoadedEventArgs(ListType.SightSeeingList, result));
                }
            }
        }

        private void Option__TransportationPathChanged(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(option_.TransportationPath))
            {
                var result = transpotationList_.Load(option_.TransportationPath, FileNames.TransportationFile);
                if (FileLoaded_ != null)
                {
                    FileLoaded_.Invoke(this, new FileLoadedEventArgs(ListType.TransportationList, result));
                }
            }
        }

        private void Option__AccomodationPathChanged(object? sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(option_.AccomodationPath))
            {
                var result = accomodationList_.Load(option_.AccomodationPath, FileNames.AccomodationFile);
                if (FileLoaded_ != null)
                {
                    FileLoaded_.Invoke(this, new FileLoadedEventArgs(ListType.AccomodationList, result));
                }
            }
        }

        public MainModel()
        {
            SetOptionModel();
            exchangeRater_ = new ExchangeRater();
            accomodationList_ = new AccomodationList();
            transpotationList_ = new TranspotationList();
            sightSeeingList_ = new SightSeeingList();
        }

        public void Init()
        {
            exchangeRater_.Init();
            accomodationList_.Init();
            transpotationList_.Init();
            sightSeeingList_.Init();
        }

        public OptionModel GetOptionModel()
        {
            return option_;
        }

        // debug
        public ListType CurrentListType
        {
            get { return currentListType_; }
            set
            {
                currentListType_ = value;
            }
        }

        public object[] Accomodations
        {
            get
            {
                if (accomodationList_.IsError)
                {
                    return accomodationList_.GetErrorArray();
                }
                else
                {
                    return accomodationList_.GetArray();
                }
            }
        }

        public object[] Transportations
        {
            get
            {
                if (transpotationList_.IsError)
                {
                    return transpotationList_.GetErrorArray();
                }
                else
                {
                    transpotationList_.GetArray();
                }
                return null;
            }
        }

        public object[] Sightseeings
        {
            get
            {
                if (sightSeeingList_.IsError)
                {
                    return sightSeeingList_.GetErrorArray();
                }
                else
                {
                    return sightSeeingList_.GetArray();
                }
            }
        }

        public object[] ExchangeRates
        {
            get
            {
                if (exchangeRater_.IsError)
                {
                    return exchangeRater_.GetErrorArray();
                }
                else
                {
                    return [];
                }
            }

     
        }







    }



    
}
