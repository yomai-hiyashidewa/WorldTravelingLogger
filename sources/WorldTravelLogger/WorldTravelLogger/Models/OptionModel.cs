using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using WorldTravelLogger.Models.Enumeration;
using WorldTravelLogger.Models.Utility;

namespace WorldTravelLogger.Models
{
    public class OptionModel
    {
        private string? accomodationPath_;

        private string? transportationPath_;

        private string? sightseeingPath_;

        private string? exchangeRatePath_;

        private string? imagePath_;

        public event EventHandler<FileLoadedEventArgs> FilePathChanged;

        private void FireFilePathChanged(ListType type)
        {
            if (FilePathChanged != null)
            {
                FilePathChanged.Invoke(this,new FileLoadedEventArgs(type, ErrorTypes.None));
            }

        }




        public string? AccomodationPath
        {
            get { return accomodationPath_; }
            set
            {
                if (accomodationPath_ != value)
                {
                    accomodationPath_ = value;
                    FireFilePathChanged(ListType.AccomodationList);
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
                    FireFilePathChanged(ListType.TransportationList);
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
                    FireFilePathChanged(ListType.SightSeeingList);
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
                    FireFilePathChanged(ListType.ExchangeRateList);
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
                    FireFilePathChanged(ListType.ImageList);
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

            for (var i = 0; i < (int)ListType.Length +1; i++)
            {
                var row = new List<string>();
                switch (i)
                {
                    case 0:
                        row.Add("files");
                        row.Add("path");
                        break;
                    case 1:
                        row.Add(FileNames.ImageDir);
                        row.Add(imagePath_);
                        break;
                    case 2:
                        row.Add(FileNames.AccomodationFile);
                        row.Add(accomodationPath_);
                        break;
                    case 3:
                        row.Add(FileNames.TransportationFile);
                        row.Add(transportationPath_);
                        break;
                    case 4:
                        row.Add(FileNames.SightseeingFile);
                        row.Add(sightseeingPath_);
                        break;
                    case 5:
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
            for (var i = 1; i < (int)ListType.Length + 1; i++)
            {
                string[] row = (string[])arrays[i];
                if (row.Length > 1)
                {
                    switch (i)
                    {
                        case 1:
                            ImagePath = row[1];
                            break;
                        case 2:
                            AccomodationPath = row[1];
                            break;
                        case 3:
                            TransportationPath = row[1];
                            break;
                        case 4:
                            SightseeingPath = row[1];
                            break;
                        case 5:
                            ExchangeRatePath = row[1];
                            break;
                    }
                }
            }
            return true;
        }

        public string GetFilePath(ListType type)
        {
            string path = null;
            switch (type)
            {
                case ListType.AccomodationList:
                    path = accomodationPath_;
                    break;
                case ListType.TransportationList:
                    path = transportationPath_;
                    break;
                case ListType.SightSeeingList:
                    path = sightseeingPath_;
                    break;
                case ListType.ExchangeRateList:
                    path = exchangeRatePath_;
                    break;
                case ListType.ImageList:
                    path = imagePath_;
                    break;
            }
            return path;
        }

        public void SetFilePath(ListType type, string path)
        {
           
            switch (type)
            {
                case ListType.AccomodationList:
                    accomodationPath_ =path;
                    break;
                case ListType.TransportationList:
                    transportationPath_ = path;
                    break;
                case ListType.SightSeeingList:
                    sightseeingPath_ = path;
                    break;
                case ListType.ExchangeRateList:
                    exchangeRatePath_ = path;
                    break;
                case ListType.ImageList:
                    imagePath_ = path;
                    break;
            }
            
        }

    }
}
