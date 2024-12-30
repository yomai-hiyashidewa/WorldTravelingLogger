using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models.Context;
using WorldTravelLogger.ViewModels.Base;
using WorldTravelLogger.Views.Parts;

namespace WorldTravelLogger.ViewModels
{
    public class RouteViewModel : ViewModelBase
    {
        private RouteCountryViewModel arrivalsViewModel_;
        private RouteCountryViewModel departuresViewModel_;
        //private RouteRegionView regionsViewModel_;

        public RouteViewModel()
        {
            arrivalsViewModel_ = new RouteCountryViewModel(true);
            departuresViewModel_ = new RouteCountryViewModel(false);
            //regionsViewModel_ = new RouteRegionsViewPanel();
        }

        public void SetModel(CountryModel? model)
        {
            if (model != null)
            {
                arrivalsViewModel_.SetModel(model);
                departuresViewModel_.SetModel(model);
            }
        }


        public RouteCountryViewModel GetRouteCountryViewModel(bool isArrive)
        {
            if (isArrive)
            {
                return arrivalsViewModel_;
            }
            else
            {
                return departuresViewModel_;
            }
        }

        //public RouteRegionsViewPanel GetRegionsViewModel()
        //{
        //    return regionsViewModel_;
        //}

    }
}
