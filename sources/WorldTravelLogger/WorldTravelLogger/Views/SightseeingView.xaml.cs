﻿using System;
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
    /// SightseeingView.xaml の相互作用ロジック
    /// </summary>
    public partial class SightseeingView : UserControl
    {
        public SightseeingView()
        {
            InitializeComponent();
           
        }

        public void SetVM(SightseeingViewModel vm)
        {
            this.DataContext = vm;
        }
    }
}
