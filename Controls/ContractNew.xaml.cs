using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
    /// Interaction logic for ContractNew.xaml
    /// </summary>
    public partial class ContractNew : UserControl
    {
        public ContractNew()
        {
            InitializeComponent();
        }

        private void TextBoxAnnualPremiumPerInsured_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (new Regex("[^0-9\\s]+").IsMatch(e.Text));
        }

        private void TextBoxAnnualPremiumPerInsured_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
