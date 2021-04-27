using BVCareManager.Converter;
using BVCareManager.Models;
using BVCareManager.Repository;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BVCareManager.ViewModels
{
    class ModifyContractViewModel : ModifyBaseViewModel
    {
        private ContractRepository contractRepository = new ContractRepository();

        public bool IsContractSelected
        {
            get
            {
                if (SelectedContract != null)
                    return true;

                return false;
            }
        }

        private Contract _selectedContract;
        public Contract SelectedContract
        {
            get
            {
                return _selectedContract;
            }
            set
            {
                SetProperty(ref _selectedContract, value);

                if (value != null)
                {
                    OnModifyingContractFromDate = value.FromDate;
                }
                else
                {
                    OnModifyingContractFromDate = DateTime.Today;
                }

                OnPropertyChanged("IsContractSelected");
                OnPropertyChanged("OnModifyingContractName");
            }
        }
        public DateTime OnModifyingContractFromDate { get; set; }
        public DateTime OnModifyingContractToDate { get; set; }

        private IEnumerable<Contract> _listContracts;
        public ObservableCollection<Contract> ListContracts
        {
            get
            {
                string ContractSearchText = SearchText;
                if (!String.IsNullOrEmpty(ContractSearchText))
                {
                    //_listContracts = ContractRepository.SearchContracts(ContractSearchText);
                }
                else
                {
                    //_listContracts = ContractRepository.FindAllContracts();
                }

                return new ObservableCollection<Contract>(_listContracts);

            }
        }

        public ModifyContractViewModel(string searchText)
        {
            IsModifyDialogOpen = false;
            _errorsList.Clear();
            this.SearchText = searchText;

            ModifyCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedContract == null)
                    return false;

                //if (string.IsNullOrEmpty(OnModifyingContractFromDate))
                //    return false;

                //if (OnModifyingContractName.Length > 30)
                //{
                //    UpdateResultAsync(Result.HasError, "Độ dài tối đa của Họ tên nhân viên là 50 ký tự");
                //}
                //else
                //{
                //    UpdateResultAsync(Result.ExcludeError, "Độ dài tối đa của Họ tên nhân viên là 50 ký tự");
                //}

                if (_errorsList.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) =>
            {
                SelectedContract.FromDate = OnModifyingContractFromDate;
                SelectedContract.ToDate = OnModifyingContractToDate;
                contractRepository.Save();

                Success = "Đã sửa thông tin nhân viên";
                UpdateResultAsync(Result.Successful);

                SelectedContract = null;
                OnPropertyChanged("ListContracts");

            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedContract == null)
                    return false;

                return true;
            }, (p) =>
            {
                IsModifyDialogOpen = false;

                contractRepository.Delete(SelectedContract);
                contractRepository.Save();

                Success = "Đã xóa nhân viên";
                UpdateResultAsync(Result.Successful);

                SelectedContract = null;
                OnPropertyChanged("ListContracts");

            });
        }
    }
}
