using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldTravelLogger.ViewModels;

namespace WorldTravelLogger.Views
{
    /// <summary>
    /// MainViewPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class MainViewPanel : UserControl
    {
        private OptionWindows optionWin_;

        public MainViewPanel()
        {
            InitializeComponent();
            this.DataContext = new MainViewPanelVM();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewPanelVM)this.DataContext;
            vm.Init();
        }

   
        private void OptionMenu_Click(object sender, RoutedEventArgs e)
        {
            if(optionWin_ == null)
            {
                var vm = (MainViewPanelVM)this.DataContext;
                optionWin_ = new OptionWindows(vm.GetOptionWindowViewModel());
                optionWin_.Show();
            }
            else
            {
                optionWin_.Activate();
            }
        }
    }
}
