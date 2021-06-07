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

namespace BVCareManager.Controls
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : UserControl
    {
        public Report()
        {
            InitializeComponent();
        }

        private void TotalPoliciesGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DetailsBorder.Visibility = Visibility.Visible;
            DetailsBorder.Background = TotalPoliciesColor.Background;
        }

        private void PremiumChangeGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DetailsBorder.Visibility = Visibility.Visible;
            DetailsBorder.Background = PremiumChangeColor.Background;
        }

        private void LossRateGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DetailsBorder.Visibility = Visibility.Visible;
            DetailsBorder.Background = LossRateColor.Background;
        }
    }
}
