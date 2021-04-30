using BVCareManager.Converter;
using BVCareManager.Models;
using BVCareManager.Repository;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace BVCareManager.ViewModels
{
    class ModifyPolicyViewModel : ModifyBaseViewModel
    {
        private PolicyRepository policyRepository = new PolicyRepository();

        public bool IsPolicySelected
        {
            get
            {
                if (SelectedPolicy != null)
                    return true;

                return false;
            }
        }

        private Policy _selectedPolicy;
        public Policy SelectedPolicy
        {
            get
            {
                return _selectedPolicy;
            }
            set
            {
                SetProperty(ref _selectedPolicy, value);

                if (value != null)
                {
                    OnModifyingPolicyContractId = value.Contract.Id;
                    OnModifyingPolicyPremium = value.Premium;
                }
                else
                {
                    OnModifyingPolicyContractId = String.Empty;
                    OnModifyingPolicyInsuredtId = String.Empty;
                    OnModifyingPolicyFromDate = DateTime.Today;
                    OnModifyingPolicyToDate = DateTime.Today;
                    OnModifyingPolicyPremium = 0;
                }

                OnPropertyChanged("IsPolicySelected");
                OnPropertyChanged("OnModifyingPolicyInsuredtId");
            }
        }

        private string _onModifyingPolicyContractId;
        public string OnModifyingPolicyContractId {
            get 
            {
                return _onModifyingPolicyContractId;
            }
            set
            {
                SetProperty(ref _onModifyingPolicyContractId, value);

                ContractRepository contractRepository = new ContractRepository();

                if (!String.IsNullOrEmpty(OnModifyingPolicyContractId))
                {
                    Contract contract = contractRepository.GetContract(OnModifyingPolicyContractId);
                    Contract checkingContract = contract;
                    //ErrorsList.Clear();

                    OnModifyingPolicyFromDate = contract.FromDate;
                    OnModifyingPolicyToDate = contract.ToDate;
                }
            }
        }

        private string _selectedInsured;
        public  string OnModifyingPolicyInsuredtId {
            get
            {
                if (SelectedPolicy != null)
                {
                    _selectedInsured = SelectedPolicy.Insured.Name + " - " + SelectedPolicy.Insured.Id;
                }
                else
                {
                    _selectedInsured = String.Empty;
                }

                return _selectedInsured;
            }
            set 
            {
                SetProperty(ref _selectedInsured, value);
            } 
        }

        private int _onModifyingPolicyPremium;
        public int OnModifyingPolicyPremium
        {
            get
            {
                return _onModifyingPolicyPremium;
            }
            set
            {
                SetProperty(ref _onModifyingPolicyPremium, value);
            }
        }

        private DateTime? _onModifyingPolicyFromDate;
        public DateTime? OnModifyingPolicyFromDate
        {
            get
            {
                return _onModifyingPolicyFromDate;
            }
            set
            {
                SetProperty(ref _onModifyingPolicyFromDate, value);
            }
        }

        private DateTime? _onModifyingPolicyToDate;
        public DateTime? OnModifyingPolicyToDate
        {
            get
            {
                return _onModifyingPolicyToDate;
            }
            set
            {
                SetProperty(ref _onModifyingPolicyToDate, value);
            }
        }

        private IEnumerable<Policy> _listPolicies;
        public ObservableCollection<Policy> ListPolicies
        {
            get
            {
                string PolicySearchText = SearchText;
                if (!String.IsNullOrEmpty(PolicySearchText))
                {
                    _listPolicies = policyRepository.SearchPolicies(PolicySearchText);
                }
                else
                {
                    _listPolicies = policyRepository.FindAllPolicies();
                }

                return new ObservableCollection<Policy>(_listPolicies);

            }
        }

        public ModifyPolicyViewModel(string searchText)
        {
            IsModifyDialogOpen = false;
            _errorsList.Clear();
            this.SearchText = searchText;

            ModifyCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedPolicy == null)
                    return false;

                if (OnModifyingPolicyContractId == null)
                    return false;

                if (OnModifyingPolicyInsuredtId == null)
                    return false;

                if (OnModifyingPolicyFromDate == null)
                    return false;

                if (OnModifyingPolicyToDate == null)
                    return false;

                if (OnModifyingPolicyInsuredtId == String.Empty)
                    return false;

                if (OnModifyingPolicyFromDate > OnModifyingPolicyToDate)
                {
                    UpdateResultAsync(Result.HasError, "Ngày bắt đầu hiệu lực phải trước ngày kết thúc");
                }

                ContractRepository contractRepository = new ContractRepository();
                Contract checkingContract = contractRepository.GetContract(SelectedPolicy.ContractId);

                if (checkingContract != null)
                {
                    String fromDateContract = String.Format("{0:dd/MM/yyyy}", checkingContract.FromDate);
                    String toDateContract = String.Format("{0:dd/MM/yyyy}", checkingContract.ToDate);
                    string errorString = String.Format(
                        "Thời hạn của đơn bảo hiểm phải phù hợp thời hạn Hợp đồng ({0} - {1})",
                        fromDateContract, toDateContract);

                    if (!contractRepository.IsInForce(OnModifyingPolicyFromDate, OnModifyingPolicyToDate, checkingContract))
                    {
                        UpdateResultAsync(Result.HasError, errorString);
                    }
                    else
                    {
                        UpdateResultAsync(Result.ExcludeError, errorString);
                    }
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
                SelectedPolicy.FromDate = OnModifyingPolicyFromDate ?? SelectedPolicy.FromDate;
                SelectedPolicy.ToDate = OnModifyingPolicyToDate ?? SelectedPolicy.ToDate;
                SelectedPolicy.ContractId = OnModifyingPolicyContractId;

                policyRepository.Save();

                Success = "Đã sửa thông tin Đơn bảo hiểm";
                UpdateResultAsync(Result.Successful);

                SelectedPolicy = null;
                OnPropertyChanged("ListPolicies");

            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedPolicy == null)
                    return false;

                return true;
            }, (p) =>
            {
                IsModifyDialogOpen = false;

                policyRepository.Delete(SelectedPolicy);
                policyRepository.Save();

                Success = "Đã xóa Đơn bảo hiểm";
                UpdateResultAsync(Result.Successful);

                SelectedPolicy = null;
                OnPropertyChanged("ListPolicies");

            });
        }
    }
}
