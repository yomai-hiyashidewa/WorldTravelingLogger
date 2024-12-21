using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WorldTravelLogger.Models
{
    public class OptionModel
    {
        private string? accomodationPath_;

        private string? transportationPath_;

        private string? sightseeingPath_;

        private string? exchangeRatePath_;

        private string? imagePath_;

        public event EventHandler AccomodationPathChanged;
        public event EventHandler TransportationPathChanged;
        public event EventHandler SightseeingPathChanged;
        public event EventHandler ExchangeRatePathChanged;
        public event EventHandler ImagePathChanged;



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
                if (sightseeingPath_ != value)
                {
                    sightseeingPath_ = value;
                    if (sightseeingPath_ != null)
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
                if (exchangeRatePath_ != value)
                {
                    exchangeRatePath_ = value;
                    if (ExchangeRatePathChanged != null)
                    {
                        ExchangeRatePathChanged.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public string? ImagePath
        {
            get
            {
                return imagePath_;
            }
            set
            {
                if(imagePath_ != value)
                {
                    imagePath_ = value;
                    if(ImagePathChanged != null)
                    {
                        ImagePathChanged(this, EventArgs.Empty);
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

        public object[] GetSaveData()
        {
            var list = new List<string[]>();

            for (var i = 0; i < 5; i++)
            {
                var row = new List<string>();
                switch (i)
                {
                    case 0:
                        row.Add("files");
                        row.Add("path");
                        break;
                    case 1:
                        row.Add(FileNames.AccomodationFile);
                        row.Add(accomodationPath_);
                        break;
                    case 2:
                        row.Add(FileNames.TransportationFile);
                        row.Add(transportationPath_);
                        break;
                    case 3:
                        row.Add(FileNames.SightseeingFile);
                        row.Add(sightseeingPath_);
                        break;
                    case 4:
                        row.Add(FileNames.ExchangeRateFile);
                        row.Add(exchangeRatePath_);
                        break;
                }
                list.Add(row.ToArray());
            }
            return list.ToArray();
        }

        public bool Load(object[] arrays)
        {
            for (var i = 1; i < 5; i++)
            {
                string[] row = (string[])arrays[i];
                if (row.Length > 1)
                {
                    switch (i)
                    {
                        case 1:
                            AccomodationPath = row[1];
                            break;
                        case 2:
                            TransportationPath = row[1];
                            break;
                        case 3:
                            SightseeingPath = row[1];
                            break;
                        case 4:
                            ExchangeRatePath = row[1];
                            break;
                    }
                }
            }
            return true;
        }
    }
}
