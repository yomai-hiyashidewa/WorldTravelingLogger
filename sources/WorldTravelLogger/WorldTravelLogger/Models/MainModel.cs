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
        public event EventHandler ControlChanged_;
        public event EventHandler TransportationChanged_;
        public event EventHandler CalcCompleted_;

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
            isWorldMode_ = true;
            startDate_ = new DateTime(2022, 5, 16);
            endDate_ = new DateTime(2024, 5, 1);
            currentCountryType_ = CountryType.JPN;
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
        private bool isWorldMode_;

        private CountryType currentCountryType_;

        private DateTime startDate_;

        private DateTime endDate_;

        public bool IsWorldMode
        {
            get
            {
                return isWorldMode_;
            }
            set
            {
                if(isWorldMode_ != value)
                {
                    isWorldMode_ = value;
                    FireControlChangd();
                }
            }
        }

        public CountryType CurrentCountryType
        {
            get { return currentCountryType_; }
            set
            {
                if(currentCountryType_ != value)
                {
                    currentCountryType_ = value;
                    FireControlChangd();
                }
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDate_;
            }
            set
            {
                if(startDate_ != value)
                {
                    startDate_ = value;
                    FireControlChangd();
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endDate_;
            }
            set
            {
                if(endDate_ != value)
                {
                    endDate_ = value;
                    FireControlChangd();
                }
            }
        }

        public MajorCurrencytype CurrentMajorCurrencyType { get; set; }

        

       
        // control


        private void FireControlChangd()
        {
            accomodationList_.CalcModels(isWorldMode_, currentCountryType_, startDate_, endDate_);
            transpotationList_.CalcModels(isWorldMode_, currentCountryType_, startDate_, endDate_);
            sightSeeingList_.CalcModels(isWorldMode_, currentCountryType_, startDate_, endDate_);
            if (ControlChanged_ != null)
            {
                ControlChanged_.Invoke(this, EventArgs.Empty);
            }
        }

        public double CalcTotalCost()
        {
            return accomodationList_.TotalCost + transpotationList_.TotalCost + sightSeeingList_.TotalCost;
        }

       

        // accomodation

        public double CalcAccomodationTotalCost()
        {
            return accomodationList_.TotalCost;
        }

        public AccomodationTypeModel[] GetTypeAccomodations()
        {
            // controlに依存するので追記
            return accomodationList_.GetTypeArray();
            
        }

        public AccomodationModel[] GetAccomodations()
        {
            // controlに依存するので追記
            return accomodationList_.GetCalcArray();
        }

        // transportation
        public bool IsWithAirplane
        {
            get { return transpotationList_.IsWithAirplane; }
            set
            {
                if(transpotationList_.IsWithAirplane != value)
                {
                    transpotationList_.IsWithAirplane = value;
                    transpotationList_.CalcModels(isWorldMode_, currentCountryType_, startDate_, endDate_);
                    if(TransportationChanged_ != null)
                    {
                        TransportationChanged_.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool IsWithCrossBorder
        {
            get
            {
                return transpotationList_.IsWithCrossBorder;
            }
            set
            {
                if (transpotationList_.IsWithCrossBorder != value)
                {
                    transpotationList_.IsWithCrossBorder = value;
                    transpotationList_.CalcModels(isWorldMode_, currentCountryType_, startDate_, endDate_);
                    if (TransportationChanged_ != null)
                    {
                        TransportationChanged_.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public double CalcTransportationCost()
        {
            return transpotationList_.TotalCost;
        }

        public TransportationTypeModel[] GetTypeTransportations()
        {
            // controlに依存するので追記
            return transpotationList_.GetTypeArray();
        }

        public TransportationModel[] GetTransportations()
        {
            // controlに依存するので追記
            return transpotationList_.GetCalcArray();
        }

        public bool GetNeedingTransportationEndDate()
        {
            return transpotationList_.GetNeedingEndDate();
        }

        public double GetTotalDistance()
        {
            return transpotationList_.TotalDistance;
        }

        public int GetTotalTime()
        {
            return transpotationList_.TotalTime;
        }


        // Sightseeing

        public double CalcSightseeingCost()
        {
            return sightSeeingList_.TotalCost;
        }

        public SightseeingTypeModel[] GetTypeSightseeings()
        {
            // controlに依存するので追記
            return sightSeeingList_.GetTypeArray();
        }

        public SightseeingModel[] GetSightseeings()
        {
            // controlに依存するので追記
            return sightSeeingList_.GetCalcArray();
        }










    }



    
}
