using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelLogger.Models;

namespace WorldTravelLogger.ViewModels
{
    internal class SideViewModel : ViewModelBase
    {
        private MainModel model_;
        public SideViewModel(MainModel model)
        {
            model_ = model;
        }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsCurrencyJPY { get; set; }

        public bool IsCurrencyUSD { get; set; }

        public bool IsCurrencyEUR { get; set; }

        public int Countries { get; set; }

        public int TotalDays { get; set; }

        public int TotalCost { get; set; }

        public int TotalMovingDistance { get; set; }

        public int TotalMovingTime { get; set; }



    }
}
