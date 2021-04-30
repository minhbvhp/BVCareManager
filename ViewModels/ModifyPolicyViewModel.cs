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
                    OnModifyingPolicyInsuredtId = value.Insured.Id;
                    OnModifyingPolicyFromDate = value.FromDate;
                    OnModifyingPolicyToDate = value.ToDate;
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
                OnPropertyChanged("OnModifyingPolicyContractId");
                OnPropertyChanged("OnModifyingPolicyInsuredtId");
                OnPropertyChanged("OnModifyingPolicyFromDate");
                OnPropertyChanged("OnModifyingPolicyToDate");
                OnPropertyChanged("OnModifyingPolicyPremium");
            }
        }

        public string OnModifyingPolicyContractId { get; set; }

        private string _selectedInsured;
        private string _selectedInsuredId;
        public string OnModifyingPolicyInsuredtId {
            get
            {
                Regex insuredIdRegex = new Regex(@"[^-]+$");

                if (SelectedPolicy != null)
                {
                    _selectedInsuredId = insuredIdRegex.Match(this._selectedInsured).ToString().Trim(' ');
                }
                else
                {
                    _selectedInsuredId = String.Empty;
                }

                return _selectedInsuredId;
            }
            set 
            {
                SetProperty(ref _selectedInsured, value);
            } 
        }
        public int OnModifyingPolicyPremium { get; set; }
        public DateTime? OnModifyingPolicyFromDate { get; set; }
        public DateTime? OnModifyingPolicyToDate { get; set; }

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

                if (OnModifyingPolicyFromDate == null)
                    return false;

                if (OnModifyingPolicyToDate == null)
                    return false;

                if (OnModifyingPolicyFromDate > OnModifyingPolicyToDate)
                {
                    UpdateResultAsync(Result.HasError, "Ngày bắt đầu hiệu lực phải trước ngày kết thúc");
                }

                if (OnModifyingPolicyPremium == 0)
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
                SelectedPolicy.FromDate = OnModifyingPolicyFromDate ?? SelectedPolicy.FromDate;
                SelectedPolicy.ToDate = OnModifyingPolicyToDate ?? SelectedPolicy.ToDate;

                policyRepository.Save();

                Success = "Đã sửa thông tin Hợp đồng";
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

                Success = "Đã xóa Hợp đồng";
                UpdateResultAsync(Result.Successful);

                SelectedPolicy = null;
                OnPropertyChanged("ListPolicies");

            });
        }
    }
}
