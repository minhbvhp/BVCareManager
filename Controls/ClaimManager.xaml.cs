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
                ClaimActionButton.SetBinding(Button.CommandProperty, new Binding("AddCommand"));
            }
            else
            {
                NewClaimDatePicker.SelectedDate = null;
            }

            if (UpdateTabItem.IsSelected)
            {
                ClaimActionButton.SetBinding(Button.CommandProperty, new Binding("UpdateCommand"));
            }
            else
            {
                UpdateClaimComboBox.SelectedValue = 0;
            }

            if (ViewTabItem.IsSelected)
            {
                ClaimActionButton.SetBinding(Button.CommandProperty, new Binding("ViewCommand"));
            }
            else
            {
                ViewClaimComboBox.SelectedItem = null;
            }

        }
    }
}
