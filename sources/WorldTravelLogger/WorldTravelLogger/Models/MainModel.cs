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
        private SightSeeingList otherList_;

        private HashSet<CountryType> countries_;
        private HashSet<CountryType> calcCountries_;

    

        

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
                otherList_.ConvertAnotherCurrency(exchangeRater_);
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
                    if (ReadyApplication)
                    {
                        SetCountries();
                        SetDate();
                    }

                }
                CalcListAll();
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
                    otherList_.ImportOthers(sightSeeingList_.ExportOthers());
                    ConvertSightSeeingPrices();
                    if (ReadyApplication)
                    {
                        SetCountries();
                        SetDate();
                    }
                }
                CalcListAll();
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
                    if (ReadyApplication)
                    {
                        SetCountries();
                        SetDate();
                    }
                }
                CalcListAll();
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
                    if (ReadyApplication)
                    {
                        SetCountries();
                        SetDate();
                    }
                }
                CalcListAll();
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
            startSetDate_ = accomodationList_.StartDate;
            endSetDate_ = accomodationList_.EndDate;
            startCalcDate_ = accomodationList_.StartDate;
            endCalcDate_ = accomodationList_.EndDate;

            currentCountryType_ = CountryType.JPN;
            CurrentMajorCurrencyType = MajorCurrencytype.JPN;
        }

        public MainModel()
        {
            SetOptionModel();
           
            exchangeRater_ = new ExchangeRater();
            accomodationList_ = new AccomodationList();
            transpotationList_ = new TranspotationList();
            sightSeeingList_ = new SightSeeingList();
            otherList_ = new SightSeeingList();
            countries_ = new HashSet<CountryType>();
            calcCountries_ = new HashSet<CountryType>();
            InitParameter();
        }

        public void Init()
        {
            exchangeRater_.Init();
            accomodationList_.Init();
            transpotationList_.Init();
            sightSeeingList_.Init();
            otherList_.Init();
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

        private DateTime startSetDate_;
        private DateTime endSetDate_;

        private DateTime? startCalcDate_;
        private DateTime? endCalcDate_;


        public DateTime? StartCalcDate { get { return startCalcDate_; } }
        public DateTime? EndCalcDate { get { return endCalcDate_; } }

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
                    startDate_ = startSetDate_;
                    endDate_ = endSetDate_;
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
                if(startDate_ != value && value >= startSetDate_)
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
                if(endDate_ != value && value <= endSetDate_)
                {
                    endDate_ = value;
                    FireControlChangd();
                }
            }
        }

        public MajorCurrencytype CurrentMajorCurrencyType { get; set; }




        // control
        public bool ReadyApplication
        {
            get
            {
                return accomodationList_.IsLoaded &&
                    transpotationList_.IsLoaded &&
                    sightSeeingList_.IsLoaded &&
                    otherList_.IsLoaded &&
                     exchangeRater_.IsLoaded;
            }
        }

        private void SetCountries()
        {
            countries_.Clear();
            foreach(var c in accomodationList_.Countries)
            {
                if (!countries_.Contains(c))
                {
                    countries_.Add(c);
                }
            }
            foreach (var c in transpotationList_.Countries.Where(c => !countries_.Contains(c)))
            {
               countries_.Add(c); 
            }
            foreach (var c in sightSeeingList_.Countries.Where(c => !countries_.Contains(c)))
            {
                countries_.Add(c);
            }
            foreach (var c in otherList_.Countries.Where(c => !countries_.Contains(c)))
            {
                countries_.Add(c);
            }
        }

        private void CalcCountries()
        {
            calcCountries_.Clear();
           
            foreach (var c in accomodationList_.GetCalcCounties())
            {
                if (!calcCountries_.Contains(c))
                {
                    calcCountries_.Add(c);
                }
            }
            foreach (var c in transpotationList_.GetCalcCounties().Where(c => !calcCountries_.Contains(c)))
            {
                calcCountries_.Add(c);
            }
            foreach (var c in sightSeeingList_.GetCalcCounties().Where(c => !calcCountries_.Contains(c)))
            {
                calcCountries_.Add(c);
            }
            foreach (var c in otherList_.GetCalcCounties().Where(c => !calcCountries_.Contains(c)))
            {
                calcCountries_.Add(c);
            }
        }

        private void SetDate()
        {
            startSetDate_ = accomodationList_.StartDate;
            
            if (startSetDate_ > transpotationList_.StartDate)
            {
                startSetDate_ = transpotationList_.StartDate;
            }
            if (startSetDate_ > sightSeeingList_.StartDate)
            {
                startSetDate_ = sightSeeingList_.StartDate;
            }
            if (startSetDate_ > otherList_.StartDate)
            {
                startSetDate_ = otherList_.StartDate;
            }
            endSetDate_ = accomodationList_.EndDate;

           
            if (endSetDate_ > transpotationList_.EndDate)
            {
                endSetDate_ = transpotationList_.EndDate;
            }
            if (endSetDate_ > sightSeeingList_.EndDate)
            {
                endSetDate_ = sightSeeingList_.EndDate;
            }
            if (endSetDate_ > otherList_.EndDate)
            {
                endSetDate_ = otherList_.EndDate;
            }
            startDate_ = startSetDate_;
            endDate_ = endSetDate_;
        }

        private void CalcDate()
        {
            startCalcDate_ = accomodationList_.GetStartCalcDate();
            var tStartDate = transpotationList_.GetStartCalcDate();
            var sStartDate = sightSeeingList_.GetStartCalcDate();
            var oStartDate = otherList_.GetStartCalcDate();
            if(startCalcDate_ == null)
            {
                startCalcDate_ = tStartDate;
            }
            else if (startCalcDate_  > tStartDate)
            {
                startCalcDate_ = tStartDate;
            }

            if (startCalcDate_ == null)
            {
                startCalcDate_ = sStartDate;
            }
            else if (startCalcDate_ > sStartDate)
            {
                startCalcDate_ = sStartDate;
            }

            if (startCalcDate_ == null)
            {
                startCalcDate_ = oStartDate;
            }
            if (startCalcDate_ > oStartDate)
            {
                startCalcDate_ = oStartDate;
            }

            endCalcDate_ = accomodationList_.GetEndCalcDate(); ;
            var tEndtDate = transpotationList_.GetEndCalcDate();
            var sEndDate = sightSeeingList_.GetEndCalcDate();
            var oEndDate = otherList_.GetEndCalcDate();

            if (endCalcDate_ == null)
            {
                endCalcDate_ = tEndtDate;
            }
            else if (endCalcDate_ > tEndtDate)
            {
                endCalcDate_ = tEndtDate;
            }

            if (endCalcDate_ == null)
            {
                endCalcDate_ = sEndDate;
            }
            else if (endCalcDate_ > sEndDate)
            {
                endCalcDate_ = sEndDate;
            }

            if (endCalcDate_ == null)
            {
                endCalcDate_ = oEndDate;
            }
            else if (endCalcDate_ > oEndDate)
            {
                endCalcDate_ = oEndDate;
            }

            if (startCalcDate_ != null)
            {
                startDate_ = (DateTime)startCalcDate_;
            }
            if (endCalcDate_ != null)
            {
                endDate_ = (DateTime)endCalcDate_;
            }
        }


        private void CalcListAll()
        {
            if (ReadyAccomodations)
            {
                accomodationList_.CalcModels(isWorldMode_, currentCountryType_, startDate_, endDate_);
            }
            if (ReadyTransportations)
            {
                transpotationList_.CalcModels(isWorldMode_, currentCountryType_, startDate_, endDate_);
            }
            if (ReadySightseeings)
            {
                sightSeeingList_.CalcModels(isWorldMode_, currentCountryType_, startDate_, endDate_);
                otherList_.CalcModels(isWorldMode_, currentCountryType_, startDate_, endDate_);
            }
            if (ReadyApplication)
            {
                CalcCountries();
                CalcDate();
            }
            
        }

    

        private void FireControlChangd()
        {
            CalcListAll();
            if (ControlChanged_ != null)
            {
                ControlChanged_.Invoke(this, EventArgs.Empty);
            }
        }

        public double CalcTotalCost()
        {
            return accomodationList_.TotalCost + transpotationList_.TotalCost + sightSeeingList_.TotalCost;
        }

        public IEnumerable<CountryType> GetCountries()
        {
            foreach (var c in countries_)
            {
                yield return c;
            }
        }

        public int TotalCalcCountries
        {
            get
            {
                return calcCountries_.Count;
            }
        }

        public int TotalCalcDays
        {
            get
            {
                var count = accomodationList_.GetCalcDateCount();
                var tCount = transpotationList_.GetCalcDateCount();
                var sCount = sightSeeingList_.GetCalcDateCount();
                var oCount = otherList_.GetCalcDateCount();
                if (count < tCount)
                {
                    count = tCount;
                }
                if(count < sCount)
                {
                    count = sCount;
                }
                if(count < oCount)
                {
                    count = oCount;
                }
                return count;
            }
        }


       

        // accomodation

        public bool ReadyAccomodations
        {
            get
            {
                return accomodationList_.IsLoaded && exchangeRater_.IsLoaded;
            }
        }



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

        public IEnumerable<AccomodationType> GetCurrentAccomodationTypes()
        {
            return accomodationList_.GetCurrentAccomodationTypes();
        }

        public AccomodationType CurrentAccomodationtype
        {
            get { return accomodationList_.CurrentAccomodationtype; }
            set { accomodationList_.CurrentAccomodationtype = value; }
        }

        // transportation

        public bool ReadyTransportations
        {
            get
            {
                return transpotationList_.IsLoaded && exchangeRater_.IsLoaded;
            }
        }
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

        public IEnumerable<Transportationtype> GetCurrentTransportationTypes()
        {
            return transpotationList_.GetCurrentTransportationTypes();
        }

        public Transportationtype CurrentTransportationType
        {
            get { return transpotationList_.CurrentTransportationType; }
            set { transpotationList_.CurrentTransportationType = value; }
        }

        // Sightseeing

        public bool ReadySightseeings
        {
            get
            {
                return sightSeeingList_.IsLoaded && exchangeRater_.IsLoaded;
            }
        }

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

        public IEnumerable<SightseeigType> GetCurrentSightseeingTypes()
        {
            return sightSeeingList_.GetCurrentSightSeeingTypes();
        }

        public SightseeigType CurrentSightSeeingType
        {
            get { return sightSeeingList_.CurrentSightSeeingType; }
            set { sightSeeingList_.CurrentSightSeeingType = value; }
        }

        public bool ReadyOthers
        {
            get
            {
                return otherList_.IsLoaded && exchangeRater_.IsLoaded;
            }
        }

        public double CalcOtherCost()
        {
            return otherList_.TotalCost;
        }

        public SightseeingTypeModel[] GetTypeOthers()
        {
            // controlに依存するので追記
            return otherList_.GetTypeArray();
        }

        public SightseeingModel[] GetOthers()
        {
            // controlに依存するので追記
            return otherList_.GetCalcArray();
        }

        public IEnumerable<SightseeigType> GetCurrentOtherTypes()
        {
            return otherList_.GetCurrentSightSeeingTypes();
        }

        public SightseeigType CurrentOtherType
        {
            get { return otherList_.CurrentSightSeeingType; }
            set { otherList_.CurrentSightSeeingType = value; }
        }










    }



    
}
