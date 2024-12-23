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
        private ControlModel controllModel_;
        private ExchangeRater exchangeRater_;
        private AccomodationList accomodationList_;
        private TransportationList transportationList_;
        private SightSeeingList sightSeeingList_;
        private SightSeeingList otherList_;

        private Dictionary<CountryType, HashSet<string>> countriesAndRegions_;
        private Dictionary<CountryType, HashSet<CurrencyType>> countriesAndCurrencies_;
        private HashSet<CountryType> calcCountries_;





        private OptionModel option_;

        public event EventHandler<FileLoadedEventArgs> FileLoaded_;
        public event EventHandler ImageListReady_;
        public event EventHandler CalcCompleted_;

        private void SetOptionModel()
        {
            option_ = new OptionModel();
            option_.AccomodationPathChanged += Option__AccomodationPathChanged;
            option_.TransportationPathChanged += Option__TransportationPathChanged;
            option_.SightseeingPathChanged += Option__SightseeingPathChanged;
            option_.ExchangeRatePathChanged += Option__ExchangeRatePathChanged;
            option_.ImagePathChanged += Option__ImagePathChanged;
        }

    

        private void DeleteOptionModel()
        {
            option_.AccomodationPathChanged -= Option__AccomodationPathChanged;
            option_.TransportationPathChanged -= Option__TransportationPathChanged;
            option_.SightseeingPathChanged -= Option__SightseeingPathChanged;
            option_.ExchangeRatePathChanged -= Option__ExchangeRatePathChanged;
            option_.ImagePathChanged -= Option__ImagePathChanged;
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
                if (transportationList_.IsLoaded) { }
                transportationList_.ConvertAnotherCurrency(exchangeRater_);
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
                    CalcAccomodations();
                    CalcTransportations();
                    CalcSightseeings();
                    CalcOthes();
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
                    otherList_.ImportOthers(sightSeeingList_.ExportOthers());
                    ConvertSightSeeingPrices();
                    if (ReadyApplication)
                    {
                        SetCountries();
                        SetDate();
                    }
                    CalcSightseeings();
                    CalcOthes();
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
                transportationList_.Init();
                var result = transportationList_.Load(option_.TransportationPath, FileNames.TransportationFile);
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
                    CalcTransportations();
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

                if (result != ErrorTypes.None)
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
                    CalcAccomodations();
                }
                if (FileLoaded_ != null)
                {
                    FileLoaded_.Invoke(this, new FileLoadedEventArgs(ListType.AccomodationList, result));
                }
            }
        }

        private void Option__ImagePathChanged(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(option_.ImagePath) && Path.Exists(option_.ImagePath))
            {
                if (ImageListReady_ != null)
                {
                    ImageListReady_.Invoke(this, EventArgs.Empty);
                }

            }
        }

        private void InitParameter()
        {
            
        }

        public MainModel()
        {
            SetOptionModel();
            controllModel_ = new ControlModel();
            exchangeRater_ = new ExchangeRater();
            accomodationList_ = new AccomodationList();
            transportationList_ = new TransportationList();
            sightSeeingList_ = new SightSeeingList();
            otherList_ = new SightSeeingList();
            countriesAndRegions_ = new Dictionary<CountryType, HashSet<string>>();
            countriesAndCurrencies_ = new Dictionary<CountryType, HashSet<CurrencyType>>();
            calcCountries_ = new HashSet<CountryType>();
            controllModel_.InitSetDate(DateTime.Now, DateTime.Now);
            controllModel_.InitCalcDate(DateTime.Now, DateTime.Now);
            controllModel_.ControlChanged_ += ControllModel__ControlChanged_;
        }

        private void ControllModel__ControlChanged_(object? sender, EventArgs e)
        {
            CalcListAll();
        }

        public void Init()
        {
            exchangeRater_.Init();
            accomodationList_.Init();
            transportationList_.Init();
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

        public string ImageDir
        {
            get
            {
                return option_.ImagePath;
            }
        }

      
        // control
        private bool ReadyApplication
        {
            get
            {
                return accomodationList_.IsLoaded &&
                    transportationList_.IsLoaded &&
                    sightSeeingList_.IsLoaded &&
                    otherList_.IsLoaded &&
                     exchangeRater_.IsLoaded;
            }
        }

        private void SetCountries()
        {
            countriesAndRegions_.Clear();
            accomodationList_.SetCoutriesAndRegions(countriesAndRegions_);
            transportationList_.SetCoutriesAndRegions(countriesAndRegions_);
            sightSeeingList_.SetCoutriesAndRegions((countriesAndRegions_));
            otherList_.SetCoutriesAndRegions(countriesAndRegions_);

            // currency
            countriesAndCurrencies_.Clear();
            accomodationList_.SetCoutriesAndCurrencies(countriesAndCurrencies_);
            transportationList_.SetCoutriesAndCurrencies(countriesAndCurrencies_);
            sightSeeingList_.SetCoutriesAndCurrencies((countriesAndCurrencies_));
            otherList_.SetCoutriesAndCurrencies(countriesAndCurrencies_);

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
            foreach (var c in transportationList_.GetCalcCounties().Where(c => !calcCountries_.Contains(c)))
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
            controllModel_.InitSetDate(accomodationList_.StartDate, accomodationList_.EndDate);
            controllModel_.SetStartSetDate(transportationList_.StartDate);
            controllModel_.SetStartSetDate(sightSeeingList_.StartDate);
            controllModel_.SetStartSetDate(otherList_.StartDate);

            controllModel_.SetEndSetDate(transportationList_.EndDate);
            controllModel_.SetEndSetDate(sightSeeingList_.EndDate);
            controllModel_.SetEndSetDate(otherList_.EndDate);


            controllModel_.InitDate();
        }

        private void CalcDate()
        {
            controllModel_.InitCalcDate(accomodationList_.GetStartCalcDate(), accomodationList_.GetEndCalcDate());

            controllModel_.SetStartCalcDate(transportationList_.GetStartCalcDate());
            controllModel_.SetStartCalcDate(sightSeeingList_.GetStartCalcDate());
            controllModel_.SetStartCalcDate(otherList_.GetStartCalcDate());

            controllModel_.SetEndCalcDate(transportationList_.GetEndCalcDate());
            controllModel_.SetEndCalcDate(sightSeeingList_.GetEndCalcDate());
            controllModel_.SetEndCalcDate(otherList_.GetEndCalcDate());

            controllModel_.InitDateFromCalc();
        }

        private void CalcAccomodations()
        {
            if (ReadyAccomodations && !accomodationList_.IsReady)
            {
                accomodationList_.CalcModels(controllModel_);
            }
            CalcApplication();
        }

        private void CalcTransportations()
        {
            if (ReadyTransportations && !transportationList_.IsReady)
            {
                transportationList_.CalcModels(controllModel_);
            }
            CalcApplication();
        }

        private void CalcSightseeings()
        {
            if (ReadySightseeings && !sightSeeingList_.IsReady)
            {
                sightSeeingList_.CalcModels(controllModel_);
            }
            CalcApplication();
        }

        private void CalcOthes()
        {
            if (ReadyOthers && !otherList_.IsReady)
            {
                otherList_.CalcModels(controllModel_);
            }
            CalcApplication();

        }

        private void CalcApplication()
        {
            if (ReadyApplication)
            {
                CalcCountries();
                CalcDate();
                if (CalcCompleted_ != null)
                {
                    CalcCompleted_.Invoke(this, EventArgs.Empty);
                }
            }
        }



        private void CalcListAll()
        {
            if (ReadyAccomodations)
            {
                accomodationList_.CalcModels(controllModel_);
            }
            if (ReadyTransportations)
            {
                transportationList_.CalcModels(controllModel_);
            }
            if (ReadySightseeings)
            {
                sightSeeingList_.CalcModels(controllModel_);
            }
            if (ReadyOthers)
            {
                otherList_.CalcModels(controllModel_);
            }
            CalcApplication();

        }

        public CostModel GetCostModel()
        {
            var model = new CostModel(CurrencyType.JPY);
            model.Set(ListType.AccomodationList, accomodationList_.TotalCost);
            model.Set(ListType.TransportationList, transportationList_.TotalCost);
            model.Set(ListType.SightSeeingList, sightSeeingList_.TotalCost);
            model.Set(ListType.Other, otherList_.TotalCost);
            return model;
        }

        public MovingModel GetMovingModel()
        {
            return new MovingModel(transportationList_.TotalDistance, transportationList_.TotalTime);
        }

        public IEnumerable<CountryType> GetCountries()
        {
            foreach (var c in countriesAndRegions_)
            {
                yield return c.Key;
            }
        }

        public string[] GetCurrentRegions()
        {
            if (countriesAndRegions_.Count > 0 &&
                countriesAndRegions_.ContainsKey(controllModel_.CurrentCountryType))
            {
                return countriesAndRegions_[controllModel_.CurrentCountryType].ToArray();
            }
            else
            {
                return null;
            }
        }

        public int GetCurrentRegionCount()
        {
            if (countriesAndRegions_.Count > 0 &&
                countriesAndRegions_.ContainsKey(controllModel_.CurrentCountryType))
            {
                return countriesAndRegions_[controllModel_.CurrentCountryType].Count;
            }
            else
            {
                return 0;
            }
        }

        public ExchangeRateModel[] GetCurrentExchangeRates()
        {
            if (countriesAndCurrencies_.Count > 0 &&
                countriesAndCurrencies_.ContainsKey(controllModel_.CurrentCountryType))
            {
                var list = new List<ExchangeRateModel>();
                foreach(var currency in countriesAndCurrencies_[controllModel_.CurrentCountryType].Where(c => c != CurrencyType.JPY))
                {
                    ExchangeRateModel model = null;
                    if (controllModel_.EnableCalcDate)
                    {
                        model = new ExchangeRateModel(currency, exchangeRater_.GetAverageRate(currency, (DateTime)controllModel_.StartCalcDate, (DateTime)controllModel_.EndCalcDate));
                    }
                    else
                    {
                        model = new ExchangeRateModel(currency, exchangeRater_.GetAverageRate(currency,controllModel_.StartDate, controllModel_.EndDate));
                    }
                    list.Add(model);
                }
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }


        public int GetTotalRegionCount()
        {
            var sum = 0;
            foreach (var pair in countriesAndRegions_)
            {
                sum += pair.Value.Count;
            }
            return sum;

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
                var hSet = new HashSet<DateTime>();
                foreach(var date in accomodationList_.GetCalcDates(hSet))
                {
                    hSet.Add(date);
                }
                foreach (var date in transportationList_.GetCalcDates(hSet))
                {
                    hSet.Add(date);
                }
                foreach (var date in sightSeeingList_.GetCalcDates(hSet))
                {
                    hSet.Add(date);
                }
                foreach (var date in otherList_.GetCalcDates(hSet))
                {
                    hSet.Add(date);
                }
                return hSet.Count;
            }
        }

        // control
        public ControlModel GetControlModel()
        {
            return controllModel_;
        }

        public AccomodationList GetAccomodationList()
        {
            return accomodationList_;
        }
    

        public TransportationList GetTransportationList()
        {
            return transportationList_;
        }

        

        public SightSeeingList GetSightSeeingList()
        {
            return sightSeeingList_;
        }

        public SightSeeingList GetOtherList()
        {
            return otherList_;
        }

        public ExchangeRater GetExchanger()
        {
            return exchangeRater_;
        }

        // accomodation



        private bool ReadyAccomodations
        {
            get
            {
                return accomodationList_.IsLoaded && exchangeRater_.IsLoaded;
            }
        }

        // transportation

        private bool ReadyTransportations
        {
            get
            {
                return transportationList_.IsLoaded && exchangeRater_.IsLoaded;
            }
        }


        // Sightseeing

        private bool ReadySightseeings
        {
            get
            {
                return sightSeeingList_.IsLoaded && exchangeRater_.IsLoaded;
            }
        }

       

        private bool ReadyOthers
        {
            get
            {
                return otherList_.IsLoaded && exchangeRater_.IsLoaded;
            }
        }

       

     

        public string GetCountryImagePath()
        {
            var imageDir = option_.ImagePath;
            if (Path.Exists(imageDir))
            {
                return Path.Combine(imageDir, "Countries" , controllModel_.CurrentCountryType.ToString(), "zero.jpg");
            }
            else
            {
                return null;
            }
        }









    }




}
