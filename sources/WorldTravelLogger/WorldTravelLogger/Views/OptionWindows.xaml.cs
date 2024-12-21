using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorldTravelLogger.Models;
using WorldTravelLogger.ViewModels;

namespace WorldTravelLogger.Views
{
    /// <summary>
    /// OptionWindows.xaml の相互作用ロジック
    /// </summary>
    public partial class OptionWindows : Window
    {

        private const string CSVFILE = "CSVファイル（*.csv）|*.csv";
        private const string FOLDER = "Folder|.";

        public OptionWindows(OptionWindowViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private OpenFileDialog CreateOpenFileDialog()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = CSVFILE;
            return ofd;
        }


        private void btn_accomodation_list_Click(object sender, RoutedEventArgs e)
        {

            var ofd = CreateOpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                var vm = (OptionWindowViewModel)this.DataContext;
                vm.AccomodationPath = ofd.FileName;
            }
        }

        private void btn_transportation_list_Click(object sender, RoutedEventArgs e)
        {
            var ofd = CreateOpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                var vm = (OptionWindowViewModel)this.DataContext;
                vm.TransportationPath = ofd.FileName;
            }
        }

        private void btn_sightseeing_list_Click(object sender, RoutedEventArgs e)
        {
            var ofd = CreateOpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                var vm = (OptionWindowViewModel)this.DataContext;
                vm.SigntseeingPath = ofd.FileName;
            }
        }

        private void btn_exchange_rate_list_Click(object sender, RoutedEventArgs e)
        {
            var ofd = CreateOpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                var vm = (OptionWindowViewModel)this.DataContext;
                vm.ExchangeRatePath = ofd.FileName;
            }
        }

        private void btn_image_list_Click(object sender, RoutedEventArgs e)
        {
            // Configure open folder dialog box
            var ofd = new OpenFolderDialog();

            ofd.Multiselect = false;
            ofd.Title = "Select a folder";
            if(ofd.ShowDialog() == true)
            {
                var vm = (OptionWindowViewModel)this.DataContext;
                vm.ImagePath = ofd.FolderName;
            }
        }
    }
}
