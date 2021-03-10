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
    /// Interaction logic for TopControlBar.xaml
    /// </summary>
    public partial class TopControlBar : UserControl
    {
        public TopControlBar()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window parrentWindown = Window.GetWindow(this);
            parrentWindown.Close();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window parrentWindown = Window.GetWindow(this);
            parrentWindown.WindowState = WindowState.Minimized;
        }
    }
}
