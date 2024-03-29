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
using BVCareManager.Models;
using BVCareManager.ViewModels;
using BVCareManager.Repository;
using BVCareManager.Controls;
using BVCareManager.Converter;
using MartinCostello.SqlLocalDb;

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
            CreateLocalDb();
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
                    searchAndModifyViewModel = new ModifyInsuredViewModel(searchText);
                    break;

                case "Hợp đồng":
                    ModifyDockPanel.Children.Add(new ContractModify());
                    searchAndModifyViewModel = new ModifyContractViewModel(searchText);
                    break;

                case "Đơn bảo hiểm":
                    ModifyDockPanel.Children.Add(new PolicyModify());
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
                ClaimManagerDockPanel.Children.Clear();
            }

            if (ClaimTab.IsSelected == true)
            {
                if (ClaimManagerDockPanel.Children.Count == 0)
                {
                    ClaimManagerDockPanel.Children.Add(new ClaimManager());
                    ClaimGrid.DataContext = new ClaimBaseViewModel();
                }                    
            }

            if (ReportTab.IsSelected == false)
            {
                ReportDockPanel.Children.Clear();
            }

            if (ReportTab.IsSelected == true)
            {
                if (ReportDockPanel.Children.Count == 0)
                {
                    ReportDockPanel.Children.Add(new Report());
                    ReportDockPanel.DataContext = new ReportViewModel();
                }
            }
        }

        private void TopControlBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CreateLocalDb()
        {
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["BVCareManager.Properties.Settings.BVCareManagerConnectionString"].ConnectionString;
            string localDbstring = @"(LocalDB)\";

            int pFrom = connection.IndexOf(localDbstring) + localDbstring.Length;
            int pTo = connection.LastIndexOf(";AttachDbFilename");

            string resultLocalDb = connection.Substring(pFrom, pTo - pFrom);

            #region SqlLocalDb Handle
            var localDb = new SqlLocalDbApi();

            ISqlLocalDbInstanceInfo localDbInstance = localDb.GetInstanceInfo(resultLocalDb);
            string localDbVersion = localDbInstance.LocalDbVersion.ToString().Substring(0, 2);

            if (localDbVersion == "0.")
            {
                try
                {
                    localDb.CreateInstance(resultLocalDb, "12.0");
                }
                catch
                {
                    MessageBox.Show("Phiên bản SQL Server LocalDB hiện tại không phù hợp. Hãy cài đặt phiên bản 12.0 (SQL Server 2014 Express LocalDb)");
                    Environment.Exit(1);
                }
            }
            else if (localDbVersion != "0." && localDbVersion != "12")
            {
                localDb.StopInstance(resultLocalDb);
                localDb.DeleteInstance(resultLocalDb);

                try
                {
                    localDb.CreateInstance(resultLocalDb, "12.0");
                }
                catch
                {
                    MessageBox.Show("Phiên bản SQL Server LocalDB hiện tại không phù hợp. Hãy cài đặt phiên bản 12.0 (SQL Server 2014 Express LocalDb)");
                    Environment.Exit(1);
                }
            }


            localDb.StartInstance(resultLocalDb);
            #endregion
        }
    }
}
