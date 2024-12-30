using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models.Context
{
    public class RegionModel
    {

        private string region_;

        private DateTime startDate_;

        private DateTime endDate_;

        private TransportationModel? arrival_;

        private TransportationModel? departure_;

        private List<AccomodationModel> accomodations_;

        private List<TransportationModel> transportations_;

        private List<SightseeingModel> sightseeings_;

        private List<OtherModel> others_;

        public string Region { get { return region_; } }

        public string StartEndDate
        {
            get
            {
                return startDate_.ToString("yyyy/MM/dd") + endDate_.ToString("yyyy/MM/dd");
            }
        }


        public RegionModel(string region) 
        {
            region_ = region;

          
            arrival_ = null;
            departure_ = null;

            accomodations_ = new List<AccomodationModel>();
            transportations_ = new List<TransportationModel>();
            sightseeings_ = new List<SightseeingModel>();
            others_ = new List<OtherModel>();

        }

        public TransportationModel Arrival
        {
            get { return arrival_; }
        }

        public TransportationModel Departure
        {
            get { return departure_; }
        }



        public bool SetTransportation(TransportationModel model)
        {
            bool end = false;
            if(model.IsSameStartRegion(region_) && model.IsSameEndRegion(region_))
            {
                transportations_.Add(model);
            }
            else if(model.IsSameStartRegion(region_) && !model.IsSameEndRegion(region_))
            {
                departure_ = model;
                end = true;
            }
            else
            {
                arrival_ = model;
            }
            return end;
        }

    }
}
