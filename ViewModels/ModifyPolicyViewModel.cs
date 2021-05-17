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
        private ContractRepository contractRepository = new ContractRepository();

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
                    OnModifyingPolicyFromDate = value.FromDate;
                    OnModifyingPolicyToDate = value.ToDate;
                    OnModifyingPolicyInsuredId =  value.Insured.Id;
                }
                else
                {
                    OnModifyingPolicyContractId = String.Empty;
                    OnModifyingPolicyInsuredId = String.Empty;
                    OnModifyingPolicyFromDate = null;
                    OnModifyingPolicyToDate = null;
                    OnModifyingPolicyPremium = 0;
                }

                OnPropertyChanged("IsPolicySelected");
                OnPropertyChanged("OnModifyingPolicyInsuredId");
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
                ErrorsList.Clear();               

                if (!String.IsNullOrEmpty(OnModifyingPolicyContractId))
                {
                    Contract contract = contractRepository.GetContract(OnModifyingPolicyContractId);
                    Contract checkingContract = contract;

                    OnModifyingPolicyFromDate = contract.FromDate;
                    OnModifyingPolicyToDate = contract.ToDate;
                }
            }
        }

        public bool IsInsuredSelected {
            get
            {
                if (!String.IsNullOrEmpty(OnModifyingPolicyInsuredId))
                    return true;

                return false;
            }
        }

        private string _onModifyingPolicyInsuredId;
        public  string OnModifyingPolicyInsuredId {
            get
            {
                return _onModifyingPolicyInsuredId;
            }
            set 
            {
                SetProperty(ref _onModifyingPolicyInsuredId, value);
                OnPropertyChanged("IsInsuredSelected");
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

                if (OnModifyingPolicyInsuredId == null)
                    return false;

                if (OnModifyingPolicyFromDate == null)
                    return false;

                if (OnModifyingPolicyToDate == null)
                    return false;

                if (OnModifyingPolicyInsuredId == String.Empty)
                    return false;

                if (OnModifyingPolicyFromDate > OnModifyingPolicyToDate)
                {
                    UpdateResultAsync(Result.HasError, "Ngày bắt đầu hiệu lực phải trước ngày kết thúc");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Ngày bắt đầu hiệu lực phải trước ngày kết thúc");
                }

                Contract checkingContract = contractRepository.GetContract(OnModifyingPolicyContractId);

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
                policyRepository.UpdateContract(SelectedPolicy, OnModifyingPolicyContractId);
                policyRepository.UpdateInsured(SelectedPolicy, OnModifyingPolicyInsuredId);

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
