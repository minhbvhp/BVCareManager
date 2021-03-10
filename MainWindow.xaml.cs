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
using BVCareManager.Repository;
using BVCareManager.Controls;

namespace BVCareManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
                    break;

                case 1:
                    CreateNewDockPanel.Children.Add(new ContractNew());
                    break;

                case 2:
                    CreateNewDockPanel.Children.Add(new PolicyNew());
                    break;
            }
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
    }
}
