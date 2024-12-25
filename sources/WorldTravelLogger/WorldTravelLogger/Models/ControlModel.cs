using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private bool isOnlyBorder_;
        private bool isRegionMode_;


        private bool isWithAirplane_;

        private bool isWithCrossBorder_;

        private bool isWithInsurance_;

        private CountryType currentCountryType_;

        private DateTime startDate_;
        private DateTime endDate_;

        private CurrencyType currentMajorCurrencyType_;
        private string currentRegion_;

        public event EventHandler ControlChanged_;
        public event EventHandler RegionChanged_;

        public ControlModel()
        {
            isWithAirplane_ = true;
            isWorldMode_ = true;
            isWithJapan_ = true;
            isWithInsurance_ = true;
            isWithCrossBorder_ = true;
            isOnlyBorder_ = false;
            isRegionMode_ = false;
            currentRegion_ = null;
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

        private void FireRegionChanged()
        {
            if (RegionChanged_ != null)
            {
                RegionChanged_.Invoke(this, EventArgs.Empty);
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
                    InitDate();
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
                    InitDate();
                    FireControlChanged();
                }
            }
        }

        public bool IsOnlyBorder
        {
            get { return isOnlyBorder_; }
            set
            {
                if (isOnlyBorder_ != value)
                {
                    isOnlyBorder_ = value;
                    FireControlChanged();
                }
            }
        }

        public bool IsRegion
        {
            get { return isRegionMode_; }
            set
            {
                if (isRegionMode_ != value)
                {
                    isRegionMode_ = value;
                    FireRegionChanged();
                }
            }
        }

        public bool IsWithJapan
        {
            get { return isWithJapan_; }
            set
            {
                if (isWithJapan_ != value)
                {
                    isWithJapan_ = value;
                    InitDate();
                    FireControlChanged();
                }
            }
        }

        public bool IsWithInsurance
        {
            get { return isWithInsurance_; }
            set
            {
                if (isWithInsurance_ != value)
                {
                    isWithInsurance_ = value;
                    InitDate();
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
                    InitDate();
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
                    InitDate();
                    FireControlChanged();
                }
            }
        }

        public string CurrentRegion
        {
            get
            {
                return currentRegion_;
            }
            set
            {
                if (currentRegion_ != value)
                {
                    currentRegion_ = value;
                    FireRegionChanged();
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

        public bool CheckControl(DateTime date, CountryType country)
        {
            if (isWorldMode_ && !isWithJapan_ && country == CountryType.JPN)
            {
                return false;
            }
            else if (!isWorldMode_ && currentCountryType_ != country)
            {
                return false;
            }
            return startDate_ <= date && date <= endDate_;

        }

        public bool CheckControl(Transportationtype tType, DateTime startDate, DateTime endDate, CountryType startCountry, CountryType endCountry)
        {
            DateTime date = startDate;
            CountryType country = startCountry;
            if (!isWithAirplane_ && tType == Transportationtype.AirPlane)
            {
                return false;
            }
            // 世界モードで国境のみの時は国が違ったら落とす
            else if (isWorldMode_ && isOnlyBorder_ && startCountry == endCountry)
            {
                return false;
            }
            // 国モードで国境越えがOFFの時国境越えは落とす
            else if (!isWorldMode_ && !isWithCrossBorder_ && startCountry != endCountry)
            {
                return false;
            }
            // 国モードで国境越えがONの時出発も到着も選択国でない時は落とす
            else if (!isWorldMode_ && IsWithCrossBorder && currentCountryType_ != startCountry && currentCountryType_ != endCountry)
            {
                return false;
            }
            // 国境越えで到着国が選択国なら到着国を選択
            else if (startCountry != endCountry && endCountry == currentCountryType_)
            {
                country = endCountry;
            }
            // 出発日と到着日が違う場合に到着日が選択範囲に入っているなら到着日を選択

            if (startDate != endDate)
            {
                if (startDate_ <= endDate && endDate <= endDate_)
                {
                    date = endDate;
                }
            }
            return CheckControl(date, startCountry);

        }

        public bool CheckControl(SightseeigType type, DateTime date, CountryType country)
        {
            if (!isWithInsurance_ && type == SightseeigType.Insurance)
            {
                return false;
            }
            return CheckControl(date, country);
        }
    }


}
