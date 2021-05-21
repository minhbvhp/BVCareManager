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
            MyTabs.SelectedIndex = 0;
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
                    if (!(createNewGroupBoxViewModel is NewInsuredViewModel))
                        createNewGroupBoxViewModel = new NewInsuredViewModel();                    
                    break;

                case 1:
                    CreateNewDockPanel.Children.Add(new ContractNew());
                    if (!(createNewGroupBoxViewModel is NewContractViewModel))
                        createNewGroupBoxViewModel = new NewContractViewModel();
                    break;

                case 2:
                    CreateNewDockPanel.Children.Add(new PolicyNew());
                    if (!(createNewGroupBoxViewModel is NewPolicyViewModel))
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
            string searchCategory = ComboBoxSearchCategory.SelectedItem.ToString();
            string searchText = SearchTextBox.Text;

            switch (searchCategory)
            {
                case "Nhân viên":
                    ModifyDockPanel.Children.Add(new InsuredModify());
                    if (!(searchAndModifyViewModel is ModifyInsuredViewModel))
                        searchAndModifyViewModel = new ModifyInsuredViewModel(searchText);
                    break;

                case "Hợp đồng":
                    ModifyDockPanel.Children.Add(new ContractModify());
                    if (!(searchAndModifyViewModel is ModifyContractViewModel))
                        searchAndModifyViewModel = new ModifyContractViewModel(searchText);
                    break;

                case "Đơn bảo hiểm":
                    ModifyDockPanel.Children.Add(new PolicyModify());
                    if (!(searchAndModifyViewModel is ModifyPolicyViewModel))
                        searchAndModifyViewModel = new ModifyPolicyViewModel(searchText);
                    break;
            }

            ModifyGrid.DataContext = searchAndModifyViewModel;
        }

        private void MyTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTabs();
        }

        private void UpdateTabs()
        {
            if (NewTab.IsSelected == false)
            {
                CreateNewListBox.SelectedIndex = 0;
                CreateNewGrid.DataContext = new NewInsuredViewModel();
            }

            if (ModifyTab.IsSelected == false)
            {
                ModifyDockPanel.Children.Clear();
                SearchTextBox.Text = String.Empty;
            }

            if (ClaimTab.IsSelected == false)
            {
                if (ClaimManagerDockPanel != null)
                {
                    ClaimManagerDockPanel.Children.Clear();
                    InsuredTextBox.Text = String.Empty;
                }
            }

        }

        private void TopControlBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void SearchInsured_Click(object sender, RoutedEventArgs e)
        {
            ClaimManagerDockPanel.Children.Clear();
            string searchText = SearchTextBox.Text;

            ClaimManagerDockPanel.Children.Add(new ClaimManager());
            ClaimGrid.DataContext = new ClaimBaseViewModel(searchText);
        }
    }
}
