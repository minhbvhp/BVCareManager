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
            DetailsGrid.Visibility = Visibility.Visible;
            DetailsBorder.Background = TotalPoliciesColor.Background;
            DetailsTotalPolicies.Visibility = Visibility.Visible;

            DetailsPremiumChange.Visibility = Visibility.Collapsed;
            DetailsLossRatio.Visibility = Visibility.Collapsed;

        }

        private void PremiumChangeGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DetailsGrid.Visibility = Visibility.Visible;
            DetailsBorder.Background = PremiumChangeColor.Background;
            DetailsPremiumChange.Visibility = Visibility.Visible;

            DetailsTotalPolicies.Visibility = Visibility.Collapsed;
            DetailsLossRatio.Visibility = Visibility.Collapsed;

        }

        private void LossRatioGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DetailsGrid.Visibility = Visibility.Visible;
            DetailsBorder.Background = LossRateColor.Background;
            DetailsLossRatio.Visibility = Visibility.Visible;

            DetailsTotalPolicies.Visibility = Visibility.Collapsed;
            DetailsPremiumChange.Visibility = Visibility.Collapsed;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            DetailsGrid.Visibility = Visibility.Collapsed;

        }

        private void ContractIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DetailsGrid.Visibility = Visibility.Collapsed;
        }

        private void PoliciesDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
