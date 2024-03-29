﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ClaimManager.xaml
    /// </summary>
    public partial class ClaimManager : UserControl
    {
        public ClaimManager()
        {
            InitializeComponent();
        }

        private void TotalPaidTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (new Regex("[^0-9\\s]+").IsMatch(e.Text));
        }

        private void TotalPaidTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void ClaimTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTabs();
        }

        private void UpdateTabs()
        {
            if (NewTabItem.IsSelected)
            {
                ClaimActionButton.Visibility = Visibility.Visible;
                ClaimActionButton.IsEnabled = true;
                ClaimActionButton.SetBinding(Button.CommandProperty, new Binding("AddCommand"));
            }
            else
            {
                NewClaimDatePicker.SelectedDate = null;
            }

            if (UpdateTabItem.IsSelected)
            {
                ClaimActionButton.Visibility = Visibility.Visible;
                ClaimActionButton.IsEnabled = true;
                ClaimActionButton.SetBinding(Button.CommandProperty, new Binding("UpdateCommand"));
            }
            else
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }

            if (ViewTabItem.IsSelected)
            {
                ClaimActionButton.IsEnabled = false;
                ClaimActionButton.Visibility = Visibility.Hidden;
            }
            else
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }

        }

        private void NotYetClosedClaimDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            PaidDataGrid.UnselectAll();
            DeniedDataGrid.UnselectAll();
        }

        private void PaidDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            NotYetClosedClaimDataGrid.UnselectAll();
            DeniedDataGrid.UnselectAll();
        }

        private void DeniedDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            NotYetClosedClaimDataGrid.UnselectAll();
            PaidDataGrid.UnselectAll();
        }

        private void InsuredsResultDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            NotYetClosedClaimDataGrid.UnselectAll();
            PaidDataGrid.UnselectAll();
            DeniedDataGrid.UnselectAll();
        }
    }
}
