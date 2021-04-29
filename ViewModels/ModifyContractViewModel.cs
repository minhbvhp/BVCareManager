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
                    OnModifyingContractToDate = value.ToDate;
                    OnModifyingContractAnnualPremiumPerInsured = value.AnnualPremiumPerInsured;
                }
                else
                {
                    OnModifyingContractFromDate = DateTime.Today;
                    OnModifyingContractToDate = DateTime.Today;
                    OnModifyingContractAnnualPremiumPerInsured = 0;
                }

                OnPropertyChanged("IsContractSelected");
                OnPropertyChanged("OnModifyingContractFromDate");
                OnPropertyChanged("OnModifyingContractToDate");
                OnPropertyChanged("OnModifyingContractAnnualPremiumPerInsured");
            }
        }

        public int OnModifyingContractAnnualPremiumPerInsured { get; set; }
        public DateTime? OnModifyingContractFromDate { get; set; }
        public DateTime? OnModifyingContractToDate { get; set; }

        private IEnumerable<Contract> _listContracts;
        public ObservableCollection<Contract> ListContracts
        {
            get
            {
                string ContractSearchText = SearchText;
                if (!String.IsNullOrEmpty(ContractSearchText))
                {
                    _listContracts = contractRepository.SearchContracts(ContractSearchText);
                }
                else
                {
                    _listContracts = contractRepository.FindAllContracts();
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

                if (OnModifyingContractFromDate == null)
                    return false;

                if (OnModifyingContractToDate == null)
                    return false;

                if (OnModifyingContractFromDate > OnModifyingContractToDate)
                {
                    UpdateResultAsync(Result.HasError, "Ngày bắt đầu hiệu lực phải trước ngày kết thúc");
                }

                if (OnModifyingContractAnnualPremiumPerInsured == 0)
                {
                    UpdateResultAsync(Result.HasError, "Phí bảo hiểm phải lớn hơn 0");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Phí bảo hiểm phải lớn hơn 0");
                }

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
                SelectedContract.FromDate = OnModifyingContractFromDate ?? SelectedContract.FromDate;
                SelectedContract.ToDate = OnModifyingContractToDate ?? SelectedContract.ToDate;
                SelectedContract.AnnualPremiumPerInsured = OnModifyingContractAnnualPremiumPerInsured;

                contractRepository.Save();

                Success = "Đã sửa thông tin Hợp đồng";
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

                Success = "Đã xóa Hợp đồng";
                UpdateResultAsync(Result.Successful);

                SelectedContract = null;
                OnPropertyChanged("ListContracts");

            });
        }
    }
}
