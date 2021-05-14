using BVCareManager.Converter;
using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BVCareManager.ViewModels
{
    public class ClaimBaseViewModel : BaseViewModel
    {
        private InsuredRepository insuredRepository = new InsuredRepository();
        private ClaimRepository claimRepository = new ClaimRepository();
        private PolicyRepository policyRepository = new PolicyRepository();
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ViewCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ShowClaimOptions { get; set; }
        public string SearchText { get; set; }

    #region New Claim
        private bool _isShowClaimOptions;
        public bool IsShowClaimOptions
        {
            get
            {
                return _isShowClaimOptions;
            }
            set
            {
                SetProperty(ref _isShowClaimOptions, value);
            }
        }

        private IEnumerable<Insured> _listInsureds;
        public ObservableCollection<Insured> ListInsureds
        {
            get
            {
                string insuredSearchText = SearchText;
                if (!String.IsNullOrEmpty(insuredSearchText))
                {
                    _listInsureds = insuredRepository.SearchInsureds(insuredSearchText);
                }
                else
                {
                    _listInsureds = insuredRepository.FindAllInsureds();
                }

                return new ObservableCollection<Insured>(_listInsureds);

            }
        }

        public bool IsInsuredSelected
        {
            get
            {
                if (SelectedInsured != null)
                    return true;

                return false;
            }
        }

        private Insured _selectedInsured;
        public Insured SelectedInsured
        {
            get
            {
                return _selectedInsured;
            }
            set
            {
                SetProperty(ref _selectedInsured, value);
                OnPropertyChanged("IsInsuredSelected");
                OnPropertyChanged("ListValidPolicies");
            }
        }

        public ObservableCollection<Policy> ListValidPolicies
        {
            get
            {
                var _validPolicies = from policy in policyRepository.FindAllPolicies()
                                        where policy.Insured == SelectedInsured &&
                                              policy.FromDate <= NewExaminationDate &&
                                              policy.ToDate >= NewExaminationDate
                                        select policy;

                var ValidPolicies = new ObservableCollection<Policy>();

                foreach (var policy in _validPolicies)
                    ValidPolicies.Add(policy);

                return ValidPolicies;
            }
        }

        private int _validSelectedPolicy;
        public int ValidSelectedPolicy
        {
            get
            {
                return _validSelectedPolicy;
            }
            set
            {
                SetProperty(ref _validSelectedPolicy, value);
            }
        }

        private DateTime? _newExaminationDate;
        public DateTime? NewExaminationDate {
            get
            {
                return _newExaminationDate;
            }
            set
            {
                SetProperty(ref _newExaminationDate, value);
                OnPropertyChanged("ListValidPolicies");
                OnPropertyChanged("IsNewExaminationEntered");
            }
        }

        public bool IsNewExaminationEntered {
            get
            {
                if (NewExaminationDate != null)
                    return true;

                return false;
            }
        }

        private DateTime? _claimReceivedDate;
        public DateTime? ClaimReceivedDate {
            get
            {
                return _claimReceivedDate;
            }
            set
            {
                SetProperty(ref _claimReceivedDate, value);
            }
        }
    #endregion

    #region Update Claim
        public ObservableCollection<Claim> ClaimList
        {
            get
            {
                var _allClaims = from claim in claimRepository.FindAllClaims()
                                     select claim;

                var AllClaims = new ObservableCollection<Claim>();

                foreach (var claim in _allClaims)
                    AllClaims.Add(claim);

                return AllClaims;
            }
        }

        private int _selectedClaimId;
        public int SelectedClaimId
        {
            get
            {
                return _selectedClaimId;
            }
            set
            {
                SetProperty(ref _selectedClaimId, value);

                OnPropertyChanged("UpdateClaimContractId");
                OnPropertyChanged("IsUpdateExaminationEntered");
                OnPropertyChanged("UpdateClaimPolicyNumber");
            }
        }

        public string UpdateClaimContractId {
            get
            {
                string _updateClaimContractId = String.Empty;
                Claim claim = new Claim();

                if (SelectedClaimId > 0)
                {
                    claim = claimRepository.GetClaimById(SelectedClaimId);
                    _updateClaimContractId = claim.Policy.ContractId;
                }

                return _updateClaimContractId;
            }
        }

        public int UpdateClaimPolicyNumber
        {
            get
            {
                int _updateClaimPolicyNumber = 0;
                Claim claim = new Claim();

                if (SelectedClaimId > 0)
                {
                    claim = claimRepository.GetClaimById(SelectedClaimId);
                    _updateClaimPolicyNumber = claim.Policy.Number;
                }

                return _updateClaimPolicyNumber;
            }
        }

        public bool IsUpdateExaminationEntered
        {
            get
            {
                if (SelectedClaimId > 0)
                    return true;

                return false;
            }
        }

        private string _claimProgressRemarks;
        public string ClaimProgressRemarks {
            get
            {
                return _claimProgressRemarks;
            }
            set
            {
                SetProperty(ref _claimProgressRemarks, value);
            }
        }

        private DateTime? _claimProgressDate;
        public DateTime? ClaimProgressDate {
            get
            {
                return _claimProgressDate;
            }
            set
            {
                SetProperty(ref _claimProgressDate, value);
            }
        }

        private int _claimTotalPaid;
        public int ClaimTotalPaid {
            get
            {
                return _claimTotalPaid;
            }
            set
            {
                if (IsClaimClosed)
                {
                    SetProperty(ref _claimTotalPaid, value);
                }
                else
                {
                    SetProperty(ref _claimTotalPaid, 0);
                }

            }
        }

        private bool _isClaimClosed;
        public bool IsClaimClosed {
            get
            {
                return _isClaimClosed;
            }
            set
            {
                SetProperty(ref _isClaimClosed, value);
            }
        }

        #endregion


        public ClaimBaseViewModel(string searchText)
        {
            _errorsList.Clear();
            IsShowClaimOptions = false;
            this.SearchText = searchText;

            ShowClaimOptions = new RelayCommand<object>((p) =>
            {
                if (IsInsuredSelected == false)
                    return false;

                return true;
            }, (p) =>
            {
                IsShowClaimOptions = true;
            });

        #region Add Command
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (NewExaminationDate == null)
                    return false;

                if (ClaimReceivedDate == null)
                    return false;

                if (ValidSelectedPolicy < 1)
                    return false;

                if (ClaimReceivedDate < NewExaminationDate)
                {
                    UpdateResultAsync(Result.HasError, "Ngày nhận hồ sơ phải trùng hoặc sau ngày khám");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Ngày nhận hồ sơ phải trùng hoặc sau ngày khám");
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
                Claim newClaim = new Claim();
                newClaim.PolicyId = ValidSelectedPolicy;
                newClaim.ExaminationDate = (DateTime)NewExaminationDate;
                newClaim.ReceivedDate = (DateTime)ClaimReceivedDate;

                claimRepository.Add(newClaim);
                claimRepository.Save();

                Success = "Đã lập hồ sơ bồi thường";
                UpdateResultAsync(Result.Successful);

                OnPropertyChanged("ClaimList");
                NewExaminationDate = null;
                ValidSelectedPolicy = 0;
                ClaimReceivedDate = null;
            });
            #endregion

        #region Update Command
            UpdateCommand = new RelayCommand<object>((p) =>
            {
                if (!IsUpdateExaminationEntered)
                    return false;

                if (String.IsNullOrEmpty(ClaimProgressRemarks) || String.IsNullOrWhiteSpace(ClaimProgressRemarks))
                    return false;

                if (ClaimProgressDate == null)
                    return false;

                Claim claim = claimRepository.GetClaimById(SelectedClaimId);
                if (ClaimProgressDate < claim.ExaminationDate)
                {
                    UpdateResultAsync(Result.HasError, "Ngày cập nhật phải sau ngày khám");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Ngày cập nhật phải sau ngày khám");
                }

                if (IsClaimClosed && ClaimTotalPaid <= 0)
                {
                    UpdateResultAsync(Result.HasError, "Số tiền bồi thường phải lớn hơn 0");
                } 
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Số tiền bồi thường phải lớn hơn 0");
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
                
            });
        #endregion

        }
    }
}
