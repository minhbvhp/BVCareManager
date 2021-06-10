using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BVCareManager.ViewModels
{
    class ReportViewModel : BaseViewModel
    {
       
        public ICommand RefreshCommand { get; set; }

        private string _selectedContractId;
        public string SelectedContractId 
        {
            get
            {
                return _selectedContractId;
            }
            set
            {
                SetProperty(ref _selectedContractId, value);
                OnPropertyChanged("SelectedContract");
                OnPropertyChanged("TotalAdditionalPremium");
                OnPropertyChanged("TotalRefundPremium");
                OnPropertyChanged("Balance");
                OnPropertyChanged("FollowingAddedPolicies");
                OnPropertyChanged("EarlyResignedPolices");
            }
        }
        public Contract SelectedContract 
        {
            get
            {
                ContractRepository contractRepository = new ContractRepository();
                if (!String.IsNullOrEmpty(SelectedContractId))
                {
                    Contract contract = contractRepository.GetContract(SelectedContractId);
                    return contract;
                }

                return null;
            }
        }

        public int TotalAdditionalPremium {
            get
            {
                int _totalAdditionalPremium = 0;

                if (!String.IsNullOrEmpty(SelectedContractId))
                {
                    foreach (Policy policy in SelectedContract.Policies)
                    {
                        if (policy.IsFollowingAdded)
                            _totalAdditionalPremium = _totalAdditionalPremium + policy.AdditionalPremium;
                    }
                }

                return _totalAdditionalPremium;
            }
        }

        public int TotalRefundPremium
        {
            get
            {
                int _totalRefundPremium = 0;

                if (!String.IsNullOrEmpty(SelectedContractId))
                {
                    foreach (Policy policy in SelectedContract.Policies)
                    {
                        if (policy.IsEarlyResigned)
                            _totalRefundPremium = _totalRefundPremium + policy.RefundPremium;
                    }
                }

                return _totalRefundPremium;
            }
        }

        public int Balance
        {
            get
            {
                return TotalAdditionalPremium - TotalRefundPremium;
            }
        }

        public ObservableCollection<Policy> FollowingAddedPolicies
        {
            get
            {
                PolicyRepository policyRepository = new PolicyRepository();
                if (!String.IsNullOrEmpty(SelectedContractId))
                {
                    var _allPoliciesOfContract = from policy in policyRepository.FindAllPolicies()
                                                 where policy.ContractId == SelectedContractId
                                                 select policy;

                    var _followingAddedPolicies = new ObservableCollection<Policy>();

                    foreach (Policy policy in _allPoliciesOfContract)
                    {
                        if (policy.IsFollowingAdded)
                            _followingAddedPolicies.Add(policy);
                    }

                    return new ObservableCollection<Policy>(_followingAddedPolicies);
                }

                return null;
            }
        }

        public ObservableCollection<Policy> EarlyResignedPolices
        {
            get
            {
                PolicyRepository policyRepository = new PolicyRepository();
                if (!String.IsNullOrEmpty(SelectedContractId))
                {
                    var _allPoliciesOfContract = from policy in policyRepository.FindAllPolicies()
                                                 where policy.ContractId == SelectedContractId
                                                 select policy;

                    var _earlyAddedPolicies = new ObservableCollection<Policy>();

                    foreach (Policy policy in _allPoliciesOfContract)
                    {
                        if (policy.IsEarlyResigned)
                            _earlyAddedPolicies.Add(policy);
                    }

                    return new ObservableCollection<Policy>(_earlyAddedPolicies);
                }

                return null;
            }
        }
        public ReportViewModel()
        {
            #region RefreshCommand
            RefreshCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                OnPropertyChanged("ContractList");
            });

            #endregion
        }
    }
}
