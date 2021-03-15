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

namespace BVCareManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        List<string> Results = new List<string>();
        object createNewGroupBoxViewModel = new object();
        NewInsuredViewModel currentNewGroupBoxViewModel;
        public MainWindow()
        {
            InitializeComponent();
            CreateNewListBox.SelectedIndex = 0;
        }

        private void CreateNewListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateNewDockPanel.Children.Clear();
            int createNewListBoxIndex = CreateNewListBox.SelectedIndex;
            
            switch (createNewListBoxIndex)
            {
                case 0:
                    CreateNewDockPanel.Children.Add(new InsuredNew());
                    createNewGroupBoxViewModel = new NewInsuredViewModel() as NewInsuredViewModel;
                    
                    break;

                case 1:
                    CreateNewDockPanel.Children.Add(new ContractNew());
                    createNewGroupBoxViewModel = new NewContractViewModel()as NewContractViewModel;
                    break;

                case 2:
                    CreateNewDockPanel.Children.Add(new PolicyNew());
                    createNewGroupBoxViewModel = new NewPolicyViewModel() as NewPolicyViewModel;
                    break;

                default:
                    break;
            }

            CreateNewDockPanel.DataContext = createNewGroupBoxViewModel;
            CreateNewButton.DataContext = createNewGroupBoxViewModel;
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            DockPanelSearchResult.Children.Clear();
            int searchCategory = ComboBoxSearchCategory.SelectedIndex;

            switch (searchCategory)
            {
                case 0:
                    DockPanelSearchResult.Children.Add(new ListViewInsured());
                    break;

                case 1:
                    DockPanelSearchResult.Children.Add(new ListViewPolicy());
                    break;

                case 2:
                    DockPanelSearchResult.Children.Add(new ListViewContract());
                    break;
            }
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {
            Results.Clear();

            if (createNewGroupBoxViewModel is NewInsuredViewModel)
                currentNewGroupBoxViewModel = (NewInsuredViewModel)createNewGroupBoxViewModel;

            InsuredRepository insuredRepository = new InsuredRepository();

            Insured newInsured = new Insured();
            newInsured.Id = currentNewGroupBoxViewModel.InputId;
            newInsured.Name = currentNewGroupBoxViewModel.InputName;


            if (insuredRepository.GetInsured(newInsured.Id) == null)
            {
                insuredRepository.Add(newInsured);
                insuredRepository.Save();
            }
            else
            {
                Results.Add("Số CMT/CCCD này đã tồn tại");
            }

            if (Results.Count > 0)
            {
                foreach (string result in Results)
                    MessageBox.Show(result);
            }
        }
    }
}
