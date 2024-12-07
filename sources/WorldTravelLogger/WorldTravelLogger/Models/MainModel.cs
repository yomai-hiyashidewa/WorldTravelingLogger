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

        private const string SAVE_FILE_NAME = "WorldTravelLogger.csv";
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

        private void ConvertAccomodationPrices()
        {
            if (exchangeRater_ != null && exchangeRater_.IsLoaded)
            {
                if (accomodationList_.IsLoaded)
                {
                    accomodationList_.ConvertAnotherCurrency(exchangeRater_);
                }
            }
        }

        private void ConvertTransportationPrices()
        {
            if (exchangeRater_ != null && exchangeRater_.IsLoaded)
            {
                if (transpotationList_.IsLoaded) { }
                transpotationList_.ConvertAnotherCurrency(exchangeRater_);
            }
        }

        private void ConvertSightSeeingPrices()
        {
            if (exchangeRater_ != null && exchangeRater_.IsLoaded)
            {
                if (sightSeeingList_.IsLoaded) { }
                sightSeeingList_.ConvertAnotherCurrency(exchangeRater_);
            }
        }

        private void Option__ExchangeRatePathChanged(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(option_.ExchangeRatePath))
            {
                exchangeRater_.Init();
                var result = exchangeRater_.Load(option_.ExchangeRatePath, FileNames.ExchangeRateFile);
                if (result != ErrorTypes.None)
                {
                    option_.ExchangeRatePath = null;
                }
                else
                {
                    ConvertAccomodationPrices();
                    ConvertTransportationPrices();
                    ConvertSightSeeingPrices();
                }
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
                sightSeeingList_.Init();
                var result = sightSeeingList_.Load(option_.SightseeingPath, FileNames.SightseeingFile);
                if (result != ErrorTypes.None)
                {
                    option_.SightseeingPath = null;
                }
                else
                {
                    ConvertSightSeeingPrices();
                }
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
                transpotationList_.Init();
                var result = transpotationList_.Load(option_.TransportationPath, FileNames.TransportationFile);
                if (result != ErrorTypes.None)
                {
                    option_.TransportationPath = null;
                }
                else
                {
                    ConvertTransportationPrices();
                }
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
                accomodationList_.Init();
                var result = accomodationList_.Load(option_.AccomodationPath, FileNames.AccomodationFile);
              
                if(result != ErrorTypes.None)
                {
                    option_.AccomodationPath = null;
                }
                else
                {
                    ConvertAccomodationPrices();
                }
                if (FileLoaded_ != null)
                {
                    FileLoaded_.Invoke(this, new FileLoadedEventArgs(ListType.AccomodationList, result));
                }
            }
        }

        private void InitParameter()
        {
            IsWorldMode = true;
            StartDate = new DateTime(2022, 5, 16);
            EndDate = new DateTime(2024, 5, 1);
            CurrentCountryType = CountryType.JPN;
            CurrentMajorCurrencyType = MajorCurrencytype.JPN;
        }

        public MainModel()
        {
            SetOptionModel();
            InitParameter();
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
            this.Load();
        }

        private void Load()
        {
            var data = CSVReader.ReadCSV(SAVE_FILE_NAME);
            if (data != null)
            {
                option_.Load(data);
            }
        }



        public void Exit()
        {
            var data = option_.GetSaveData();
            CSVWriter.WriteCSV(SAVE_FILE_NAME, data);
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
                    return transpotationList_.GetArray();
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
                    return null;
                }
            }

        }

        public bool IsWorldMode { get; set; }

        public CountryType CurrentCountryType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public MajorCurrencytype CurrentMajorCurrencyType { get; set; }

        public AccomodationType CurrentAccomodationType { get; set; }

        public Transportationtype CurrentTransportationType { get; set; }

        public SightseeigType CurrentSightseeingType { get; set; }

        // accomodation

        public AccomodationList GetAccomodationList()
        {
            return accomodationList_;
        }

        // transportation

        public TranspotationList GetTransportationList()
        {
            return transpotationList_;
        }

      









    }



    
}
