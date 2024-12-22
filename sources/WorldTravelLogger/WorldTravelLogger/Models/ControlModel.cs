using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class ControlModel
    {
        private DateTime startSetDate_;
        private DateTime endSetDate_;

        private DateTime? startCalcDate_;
        private DateTime? endCalcDate_;



        private bool isWorldMode_;
        private bool isWithJapan_;


        private bool isWithAirplane_;

        private bool isWithCrossBorder_;

        private bool isWithInsurance_;

        private CountryType currentCountryType_;

        private DateTime startDate_;
        private DateTime endDate_;

        private CurrencyType currentMajorCurrencyType_;

        public event EventHandler ControlChanged_;

        public ControlModel()
        {
            isWithAirplane_ = true;
            isWorldMode_ = true;
            isWithJapan_ = true;
            isWithInsurance_ = true;
            isWithCrossBorder_ = true;
            startDate_ = new DateTime(2022, 5, 16);
            endDate_ = new DateTime(2024, 5, 1);
            currentCountryType_ = CountryType.JPN;
            currentMajorCurrencyType_ = CurrencyType.JPY;
        }

        private void FireControlChanged()
        {
            if (ControlChanged_ != null)
            {
                ControlChanged_.Invoke(this, EventArgs.Empty);
            }
        }

        public bool IsWithAirplane
        {
            get { return this.isWithAirplane_; }
            set
            {
                if (this.isWithAirplane_ != value)
                {
                    this.isWithAirplane_ = value;
                    FireControlChanged();
                }
            }
        }


        public bool IsWorldMode
        {
            get { return isWorldMode_; }
            set
            {
                if (isWorldMode_ != value)
                {
                    isWorldMode_ = value;
                    FireControlChanged();
                }
            }
        }

        public bool IsWithCrossBorder
        {
            get
            {
                return isWithCrossBorder_;
            }
            set
            {
                if (isWithCrossBorder_ != value)
                {
                    isWithCrossBorder_ = value;
                    FireControlChanged();
                }
            }
        }

        public CountryType CurrentCountryType
        {
            get { return currentCountryType_; }
            set
            {
                if (currentCountryType_ != value)
                {
                    currentCountryType_ = value;
                    startDate_ = startSetDate_;
                    endDate_ = endSetDate_;
                    FireControlChanged();
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
                if (startDate_ != value && value >= startSetDate_)
                {
                    startDate_ = value;
                    FireControlChanged();
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
                if (endDate_ != value && value <= endSetDate_)
                {
                    endDate_ = value;
                    FireControlChanged();
                }
            }
        }

        public DateTime? StartCalcDate { get { return startCalcDate_; } }
        public DateTime? EndCalcDate { get { return endCalcDate_; } }

        public bool EnableCalcDate
        {
            get
            {
                return startCalcDate_ != null && endCalcDate_ != null;
            }
        }



        public bool CheckControl(DateTime date, CountryType country)
        {
            if (!isWorldMode_ && currentCountryType_ != country)
            {
                return false;
            }
            return startDate_ <= date && date <= endDate_;

        }

        public void InitDate()
        {
            startDate_ = startSetDate_;
            endDate_ = endSetDate_;
        }

        public void InitDateFromCalc()
        {
            if (startCalcDate_ != null)
            {
                startDate_ = (DateTime)startCalcDate_;
            }
            if (endCalcDate_ != null)
            {
                endDate_ = (DateTime)endCalcDate_;
            }
        }

        public void InitSetDate(DateTime start, DateTime end)
        {
            startSetDate_ = start;
            endSetDate_ = end;
        }

        public void SetStartSetDate(DateTime date)
        {
            if (startSetDate_ > date)
            {
                startSetDate_ = date;
            }

        }

        public void SetEndSetDate(DateTime date)
        {
            if (endSetDate_ > date)
            {
                endSetDate_ = date;
            }
        }

        public void InitCalcDate(DateTime? start, DateTime? end)
        {
            startCalcDate_ = start;
            endCalcDate_ = end;
        }

        public void SetStartCalcDate(DateTime? date)
        {
            if (date == null)
            {
                return;
            }
            if (startCalcDate_ == null)
            {
                startCalcDate_ = date;
            }
            else if (startCalcDate_ > date)
            {
                startCalcDate_ = date;
            }
        }

        public void SetEndCalcDate(DateTime? date)
        {
            if (date == null)
            {
                return;
            }
            if (endCalcDate_ == null)
            {
                endCalcDate_ = date;
            }
            else if (endCalcDate_ < date)
            {
                endCalcDate_ = date;
            }

        }

        public bool CheckControl(Transportationtype tType, DateTime startDate, DateTime endDate, CountryType startCountry, CountryType endCountry)
        {
            if (!isWithAirplane_ && tType == Transportationtype.AirPlane)
            {
                return false;
            }
            else if (!isWorldMode_ && !isWithCrossBorder_ && startCountry != endCountry)
            {
                return false;
            }
            else if (!isWorldMode_ && currentCountryType_ != startCountry && currentCountryType_ != endCountry)
            {
                return false;
            }
            return CheckControl(startDate, startCountry);

        }
    }


}
