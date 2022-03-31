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
                OnPropertyChanged("LossRatio");
                OnPropertyChanged("FollowingAddedPolicies");
                OnPropertyChanged("EarlyResignedPolices");
                OnPropertyChanged("AllPolicies");
                OnPropertyChanged("ListPaidClaim");
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
                            _totalAdditionalPremium += policy.AdditionalPremium;
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
                            _totalRefundPremium += policy.RefundPremium;
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

        public float LossRatio
        {
            get
            {
                float lossRatio = 0;

                if (!String.IsNullOrEmpty(SelectedContractId))
                {
                    float totalPaid = 0;
                    ClaimRepository claimRepository = new ClaimRepository();

                    var _allClaim = from claim in claimRepository.FindAllClaims()
                                   select claim;

                    var _allClaimsOfContract = from claim in _allClaim
                                               where claim.Policy.ContractId == SelectedContract.Id
                                               select claim;

                    foreach (var claim in _allClaimsOfContract)
                    {
                        if (claim.IsClosed)
                            totalPaid += (int)claim.TotalPaid;
                    }

                    lossRatio = (totalPaid / SelectedContract.InitialTotalPremium) * 100;

                }

                return lossRatio;
            }
        }

        public List<Policy> AllPolicies
        {
            get
            {
                PolicyRepository policyRepository = new PolicyRepository();
                if (!String.IsNullOrEmpty(SelectedContractId))
                {
                    var _allPoliciesOfContract = from policy in policyRepository.FindAllPolicies()
                                                 where policy.ContractId == SelectedContractId
                                                 select policy;

                    return new List<Policy>(_allPoliciesOfContract);
                }

                return null;
            }
        }

        public List<Policy> FollowingAddedPolicies
        {
            get
            {
                PolicyRepository policyRepository = new PolicyRepository();
                if (!String.IsNullOrEmpty(SelectedContractId))
                {
                    var _allPoliciesOfContract = from policy in policyRepository.FindAllPolicies()
                                                 where policy.ContractId == SelectedContractId
                                                 select policy;

                    var _followingAddedPolicies = new List<Policy>();

                    foreach (Policy policy in _allPoliciesOfContract)
                    {
                        if (policy.IsFollowingAdded)
                            _followingAddedPolicies.Add(policy);
                    }

                    return new List<Policy>(_followingAddedPolicies);
                }

                return null;
            }
        }

        public List<Policy> EarlyResignedPolices
        {
            get
            {
                PolicyRepository policyRepository = new PolicyRepository();
                if (!String.IsNullOrEmpty(SelectedContractId))
                {
                    var _allPoliciesOfContract = from policy in policyRepository.FindAllPolicies()
                                                 where policy.ContractId == SelectedContractId
                                                 select policy;

                    var _earlyAddedPolicies = new List<Policy>();

                    foreach (Policy policy in _allPoliciesOfContract)
                    {
                        if (policy.IsEarlyResigned)
                            _earlyAddedPolicies.Add(policy);
                    }

                    return new List<Policy>(_earlyAddedPolicies);
                }

                return null;
            }
        }

        public List<Claim> ListPaidClaim
        {
            get
            {
                ClaimRepository claimRepository = new ClaimRepository();
                if (!String.IsNullOrEmpty(SelectedContractId))
                {
                    var _allClaims = from claim in claimRepository.FindAllClaims()
                                     where claim.Policy.ContractId == SelectedContractId
                                     select claim;

                    var _listPaidClaim = new List<Claim>();

                    foreach (var claim in _allClaims)
                    {
                        if (claim.IsPaid)
                            _listPaidClaim.Add(claim);
                    }

                    return new List<Claim>(_listPaidClaim);
                }

                return null;
            }
        }
    }
}
