using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models.Enumeration;

namespace WorldTravelLogger.Models.Utility
{
    public static class FileNames
    {
        public const string ImageDir = "Image";
        public const string ListDir = "List";
        public const string AccomodationFile = "accomodations";
        public const string TransportationFile = "transpotations";
        public const string SightseeingFile = "sightseeing";
        public const string ExchangeRateFile = "exchange_rates";

        public static string GetFileName(ListType type)
        {
            switch (type)
            {
                case ListType.AccomodationList:
                    return AccomodationFile;
                case ListType.TransportationList:
                    return TransportationFile;
                case ListType.SightSeeingList:
                    return SightseeingFile;
                case ListType.ExchangeRateList:
                    return ExchangeRateFile;

            }
            return null;
        }

    }
}
