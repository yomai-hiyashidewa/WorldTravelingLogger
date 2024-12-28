using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models.Enumeration;

namespace WorldTravelLogger.Models.Context
{
    public class CountryModel
    {
        private CountryType countryType_;

        private string imageDir_;

        public CountryModel(CountryType countryType_, string imageDir_)
        {
            this.countryType_ = countryType_;
            this.imageDir_ = imageDir_;
        }

        public CountryType Type { get { return countryType_; } }

        public string ImagePath
        {
            get
            {
                if(!string.IsNullOrWhiteSpace(imageDir_) && Path.Exists(imageDir_))
                {
                    var path = Path.Combine(imageDir_, "Flags", countryType_.ToString() + ".png");
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }
                return null;
            }
        }
    }
}
