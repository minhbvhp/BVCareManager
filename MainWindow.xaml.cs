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
using BVCareManager.Models;
using BVCareManager.ViewModels;
using BVCareManager.Repository;
using BVCareManager.Controls;
using BVCareManager.Converter;

namespace BVCareManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        object createNewGroupBoxViewModel;
        object searchAndModifyViewModel;
        public MainWindow()
        {
            InitializeComponent();
            CreateNewListBox.SelectedIndex = 0;
            ComboBoxSearchCategory.SelectedIndex = 0;
        }

        private void CreateNewListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateNewDockPanel.Children.Clear();
            int createNewListBoxIndex = CreateNewListBox.SelectedIndex;
            
            switch (createNewListBoxIndex)
            {
                case 0:
                    CreateNewDockPanel.Children.Add(new InsuredNew());
                    createNewGroupBoxViewModel = new NewInsuredViewModel();                    
                    break;

                case 1:
                    CreateNewDockPanel.Children.Add(new ContractNew());
                    createNewGroupBoxViewModel = new NewContractViewModel();
                    break;

                case 2:
                    CreateNewDockPanel.Children.Add(new PolicyNew());
                    createNewGroupBoxViewModel = new NewPolicyViewModel();
                    break;

                default:
                    break;
            }

            CreateNewGrid.DataContext = createNewGroupBoxViewModel;
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            ModifyDockPanel.Children.Clear();
            int searchCategory = ComboBoxSearchCategory.SelectedIndex;
            string searchText = SearchTextBox.Text;

            switch (searchCategory)
            {
                case 0:
                    ModifyDockPanel.Children.Add(new InsuredModify());
                    searchAndModifyViewModel = new ModifyInsuredViewModel(searchText);
                    break;

                case 1:

                    break;

                case 2:

                    break;
            }

            ModifyGrid.DataContext = searchAndModifyViewModel;
        }
    }
}
