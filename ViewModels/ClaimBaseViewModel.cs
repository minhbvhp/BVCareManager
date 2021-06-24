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
        private ClaimProgressRepository claimProgressRepository = new ClaimProgressRepository();
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ShowClaimOptions { get; set; }

        private string _searchText;
        public string SearchText {
            get
            {
                return _searchText;
            }
            set
            {
                SetProperty(ref _searchText, value);
                OnPropertyChanged("ListInsureds");
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

    #region Not Yet Closed Claim
        public ObservableCollection<Claim> ListNotYetClosedClaim {
            get
            {
                var _allClaims = from claim in claimRepository.FindAllClaims()
                                 select claim;

                var _listNotYetClosedClaim = new ObservableCollection<Claim>();

                foreach (var claim in _allClaims)
                {
                    if (claim.IsClosed == false)
                        _listNotYetClosedClaim.Add(claim);
                }
                    
                return new ObservableCollection<Claim>(_listNotYetClosedClaim);
            }
        }

        public string TotalNotYetClosedClaim 
        {
            get
            {
                string _totalNotYetClosedClaim;

                if (ListNotYetClosedClaim.Count > 0)
                {
                    _totalNotYetClosedClaim = String.Format("({0})", ListNotYetClosedClaim.Count);
                    return _totalNotYetClosedClaim;
                }

                return String.Empty;
            }
        }

        private Claim _selectedNotYetClosedClaim;
        public Claim SelectedNotYetClosedClaim {
            get
            {
                return _selectedNotYetClosedClaim;
            }
            set
            {
                SetProperty(ref _selectedNotYetClosedClaim, value);

                if (SelectedNotYetClosedClaim != null)
                {
                    Claim _tempNotYetClosedClaim = SelectedNotYetClosedClaim;

                    SelectedInsuredListBox = null;
                    OnPropertyChanged("SelectedInsuredListBox");

                    SelectedPaidClaim = null;
                    OnPropertyChanged("SelectedPaidClaim");

                    SelectedDeniedClaim = null;
                    OnPropertyChanged("SelectedDeniedClaim");

                    SelectedInsured = _tempNotYetClosedClaim.Policy.Insured;
                    OnPropertyChanged("SelectedInsured");


                    if (_tempNotYetClosedClaim.ExaminationDate != null)
                    {
                        SelectedClaimIdForView = _tempNotYetClosedClaim.Id;
                        OnPropertyChanged("SelectedClaimIdForView");

                        SelectedClaimId = _tempNotYetClosedClaim.Id;
                        OnPropertyChanged("SelectedClaimId");
                    }
                }                
            }
        }
        #endregion

    #region Paid Claim
        public ObservableCollection<Claim> ListPaidClaim
        {
            get
            {
                var _allClaims = from claim in claimRepository.FindAllClaims()
                                 select claim;

                var _listPaidClaim = new ObservableCollection<Claim>();

                foreach (var claim in _allClaims)
                {
                    if (claim.IsPaid)
                        _listPaidClaim.Add(claim);
                }

                return new ObservableCollection<Claim>(_listPaidClaim);
            }
        }

        public string TotalPaidClaim
        {
            get
            {
                string _totalPaidClaim;

                if (ListPaidClaim.Count > 0)
                {
                    _totalPaidClaim = String.Format("({0})", ListPaidClaim.Count);
                    return _totalPaidClaim;
                }

                return String.Empty;
            }
        }

        private Claim _selectedPaidClaim;
        public Claim SelectedPaidClaim
        {
            get
            {
                return _selectedPaidClaim;
            }
            set
            {
                SetProperty(ref _selectedPaidClaim, value);

                if (SelectedPaidClaim != null)
                {
                    Claim _tempPaidClaim = SelectedPaidClaim;

                    SelectedInsuredListBox = null;
                    OnPropertyChanged("SelectedInsuredListBox");

                    SelectedNotYetClosedClaim = null;
                    OnPropertyChanged("SelectedNotYetClosedClaim");

                    SelectedDeniedClaim = null;
                    OnPropertyChanged("SelectedDeniedClaim");

                    SelectedInsured = _tempPaidClaim.Policy.Insured;
                    OnPropertyChanged("SelectedInsured");

                    if (_tempPaidClaim.ExaminationDate != null)
                    {
                        SelectedClaimIdForView = _tempPaidClaim.Id;
                        OnPropertyChanged("SelectedClaimIdForView");

                        SelectedClaimId = _tempPaidClaim.Id;
                        OnPropertyChanged("SelectedClaimId");
                    }
                }
            }
        }
        #endregion

    #region Denied Claim
        public ObservableCollection<Claim> ListDeniedClaim
        {
            get
            {
                var _allClaims = from claim in claimRepository.FindAllClaims()
                                 select claim;

                var _listDeniedClaim = new ObservableCollection<Claim>();

                foreach (var claim in _allClaims)
                {
                    if (claim.IsDenied)
                        _listDeniedClaim.Add(claim);
                }

                return new ObservableCollection<Claim>(_listDeniedClaim);
            }
        }

        public string TotalDeniedClaim
        {
            get
            {
                string _totalDeniedClaim;

                if (ListDeniedClaim.Count > 0)
                {
                    _totalDeniedClaim = String.Format("({0})", ListDeniedClaim.Count);
                    return _totalDeniedClaim;
                }

                return String.Empty;
            }
        }

        private Claim _selectedDeniedClaim;
        public Claim SelectedDeniedClaim
        {
            get
            {
                return _selectedDeniedClaim;
            }
            set
            {
                SetProperty(ref _selectedDeniedClaim, value);

                if (SelectedDeniedClaim != null)
                {
                    Claim _tempDeniedClaim = SelectedDeniedClaim;

                    SelectedInsuredListBox = null;
                    OnPropertyChanged("SelectedInsuredListBox");

                    SelectedNotYetClosedClaim = null;
                    OnPropertyChanged("SelectedNotYetClosedClaim");

                    SelectedPaidClaim = null;
                    OnPropertyChanged("SelectedPaidClaim");

                    SelectedInsured = _tempDeniedClaim.Policy.Insured;
                    OnPropertyChanged("SelectedInsured");

                    if (_tempDeniedClaim.ExaminationDate != null)
                    {
                        SelectedClaimIdForView = _tempDeniedClaim.Id;
                        OnPropertyChanged("SelectedClaimIdForView");

                        SelectedClaimId = _tempDeniedClaim.Id;
                        OnPropertyChanged("SelectedClaimId");
                    }
                }
            }
        }
        #endregion

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
                OnPropertyChanged("ClaimListByInsured");

                SelectedClaimIdForView = 0;
                OnPropertyChanged("SelectedClaimIdForView");

                SelectedClaimId = 0;
                OnPropertyChanged("SelectedClaimId");
            }
        }

        private Insured _selectedInsuredListBox;
        public Insured SelectedInsuredListBox
        {
            get
            {
                return _selectedInsuredListBox;
            }
            set
            {
                SetProperty(ref _selectedInsuredListBox, value);

                SelectedNotYetClosedClaim = null;
                OnPropertyChanged("SelectedNotYetClosedClaim");

                SelectedPaidClaim = null;
                OnPropertyChanged("SelectedPaidClaim");

                SelectedDeniedClaim = null;
                OnPropertyChanged("SelectedDeniedClaim");

                SelectedInsured = SelectedInsuredListBox;
                OnPropertyChanged("SelectedInsured");

            }
        }

        public ObservableCollection<Policy> ListValidPolicies
        {
            get
            {
                var ValidPolicies = new ObservableCollection<Policy>();

                //fix DateTime min and max value between sql and c# 
                DateTime MinDateTimeOfSql = new DateTime(1753, 1, 1);
                DateTime MaxDateTimeOfSql = new DateTime(9999, 12, 31);

                if (NewExaminationDate > MinDateTimeOfSql && NewExaminationDate < MaxDateTimeOfSql)
                {
                    var _validPolicies = from policy in policyRepository.FindAllPolicies()
                                         where policy.Insured == SelectedInsured &&
                                               policy.FromDate <= NewExaminationDate &&
                                               policy.ToDate >= NewExaminationDate
                                         select policy;

                    foreach (var policy in _validPolicies)
                        ValidPolicies.Add(policy);
                }

                return ValidPolicies;
            }
        }

        public bool IsPolicySelected {
            get
            {
                if (ValidSelectedPolicy != 0)
                    return true;

                return false;
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
                OnPropertyChanged("IsPolicySelected");
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
                ErrorsList.Clear();
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
        public ObservableCollection<Claim> ClaimListByInsured
        {
            get
            {
                var AllClaims = new ObservableCollection<Claim>();

                if (SelectedInsured != null)
                {
                    var _allClaims = from claim in claimRepository.FindAllClaims()
                                     where claim.Policy.InsuredId == SelectedInsured.Id
                                     select claim;

                    foreach (var claim in _allClaims)
                        AllClaims.Add(claim);
                }           

                return AllClaims;
            }
        }

        public DateTime? ReceivedDateOfSelectedClaim {
            get
            {
                if (SelectedClaimId != 0)
                {
                    return claimRepository.GetClaimById(SelectedClaimId).ReceivedDate;
                }

                return null;
            }
        }

        public Claim SelectedClaim
        {
            get
            {
                return claimRepository.GetClaimById(SelectedClaimId);
            }
        }

        public bool IsClaimSelected {
            get
            {
                if (SelectedClaimId != 0)
                    return true;

                return false;
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
                ErrorsList.Clear();
                OnPropertyChanged("ContractIdOfSelectedClaim");
                OnPropertyChanged("PolicyNumberOfSelectedClaim");
                OnPropertyChanged("ClaimProgressList");
                OnPropertyChanged("SelectedClaim");
                OnPropertyChanged("IsClaimSelected");
                OnPropertyChanged("ReceivedDateOfSelectedClaim");
            }
        }

        public string ContractIdOfSelectedClaim {
            get
            {
                string _updateClaimContractId = String.Empty;
                Claim claim = new Claim();

                if (SelectedClaimId > 0)
                {
                    _updateClaimContractId = SelectedClaim.Policy.ContractId;
                }

                return _updateClaimContractId;
            }
        }

        public int PolicyNumberOfSelectedClaim
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

        private string _claimProgressRemarks;
        public string ClaimProgressRemarks {
            get
            {
                string afterTrim;

                if (String.IsNullOrEmpty(_claimProgressRemarks))
                {
                    afterTrim = String.Empty;
                }
                else
                {
                    afterTrim = _claimProgressRemarks.Trim();
                }

                return afterTrim;
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



        public bool IsClaimOnProgress {
            get
            {
                return !IsClaimClosed;
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

                if (IsClaimClosed)
                {
                    IsPaid = true;
                }
                else
                {
                    IsPaid = false;
                }

                OnPropertyChanged("IsPaid");
                OnPropertyChanged("IsClaimOnProgress");
            }
        }

        private bool _isPaid;
        public bool IsPaid
        {
            get
            {
                return _isPaid;
            }
            set
            {
                SetProperty(ref _isPaid, value);
            }
        }

        public ObservableCollection<ClaimsProgress> ClaimProgressList {
            get
            {
                var _allClaimProgressByClaimId = from claimProgress in claimProgressRepository.FindAllClaimsProgress()
                                                 where claimProgress.ClaimId == SelectedClaimId
                                                 orderby claimProgress.Date
                                                 select claimProgress;

                var AllClaimProgressByClaimId = new ObservableCollection<ClaimsProgress>();

                foreach (var claimProgress in _allClaimProgressByClaimId)
                    AllClaimProgressByClaimId.Add(claimProgress);

                return AllClaimProgressByClaimId;
            }
        }
        #endregion

    #region View Claim
        public Claim SelectedViewClaim
        {
            get
            {
                return claimRepository.GetClaimById(SelectedClaimIdForView);
            }
        }

        public bool IsViewClaimSelected
        {
            get
            {
                if (SelectedClaimIdForView != 0)
                    return true;

                return false;
            }
        }

        private int _selectedClaimIdForView;
        public int SelectedClaimIdForView
        {
            get
            {
                return _selectedClaimIdForView;
            }
            set
            {
                SetProperty(ref _selectedClaimIdForView, value);
                ErrorsList.Clear();

                OnPropertyChanged("ContractIdOfViewClaim");
                OnPropertyChanged("PolicyNumberOfViewClaim");
                OnPropertyChanged("SelectedViewClaim");
                OnPropertyChanged("IsViewClaimSelected");
                OnPropertyChanged("ReceivedDateOfSelectedViewClaim");
                OnPropertyChanged("ClaimProgressViewList");
            }
        }

        public string ContractIdOfViewClaim
        {
            get
            {
                string _viewClaimContractId = String.Empty;
                Claim claim = new Claim();

                if (SelectedClaimIdForView > 0)
                {
                    _viewClaimContractId = SelectedViewClaim.Policy.ContractId;
                }

                return _viewClaimContractId;
            }
        }

        public int PolicyNumberOfViewClaim
        {
            get
            {
                int _viewClaimPolicyNumber = 0;
                Claim claim = new Claim();

                if (SelectedClaimIdForView > 0)
                {
                    claim = claimRepository.GetClaimById(SelectedClaimIdForView);
                    _viewClaimPolicyNumber = claim.Policy.Number;
                }

                return _viewClaimPolicyNumber;
            }
        }

        public DateTime? ReceivedDateOfSelectedViewClaim
        {
            get
            {
                if (SelectedClaimIdForView != 0)
                {
                    return claimRepository.GetClaimById(SelectedClaimIdForView).ReceivedDate;
                }

                return null;
            }
        }

        public ObservableCollection<ClaimsProgress> ClaimProgressViewList
        {
            get
            {
                var _allClaimProgressByClaimId = from claimProgress in claimProgressRepository.FindAllClaimsProgress()
                                                 where claimProgress.ClaimId == SelectedClaimIdForView
                                                 orderby claimProgress.Date
                                                 select claimProgress;

                var AllClaimProgressByClaimId = new ObservableCollection<ClaimsProgress>();

                foreach (var claimProgress in _allClaimProgressByClaimId)
                    AllClaimProgressByClaimId.Add(claimProgress);

                return AllClaimProgressByClaimId;
            }
        }

        #endregion

    #region Delete Claim

        private bool _isDeleteDialogOpen;
        public bool IsDeleteDialogOpen 
        {
            get
            {
                return _isDeleteDialogOpen;
            }
            set
            {
                SetProperty(ref _isDeleteDialogOpen, value);
            }
        }
        #endregion


        public ClaimBaseViewModel()
        {
            IsDeleteDialogOpen = false;
            _errorsList.Clear();
            IsShowClaimOptions = false;

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

                OnPropertyChanged("ClaimListByInsured");
                OnPropertyChanged("ListNotYetClosedClaim");
                OnPropertyChanged("TotalNotYetClosedClaim");
                OnPropertyChanged("TotalPaidClaim");
                NewExaminationDate = null;
                ValidSelectedPolicy = 0;
                ClaimReceivedDate = null;
            });
        #endregion

        #region Update Command
            UpdateCommand = new RelayCommand<object>((p) =>
            {
                if (!IsClaimSelected)
                    return false;

                if (ClaimProgressDate == null)
                    return false;

                if (ClaimProgressDate < SelectedClaim.ExaminationDate)
                {
                    UpdateResultAsync(Result.HasError, "Ngày cập nhật/đóng hồ sơ phải sau ngày khám");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Ngày cập nhật/đóng hồ sơ phải sau ngày khám");
                }

                if (SelectedClaim.IsClosed)
                {
                    UpdateResultAsync(Result.HasError, "Hồ sơ này đã đóng, không thể cập nhật nữa");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Hồ sơ này đã đóng, không thể cập nhật nữa");
                }

                if (IsPaid && ClaimTotalPaid <= 0)
                {
                    UpdateResultAsync(Result.HasError, "Số tiền bồi thường phải lớn hơn 0");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Số tiền bồi thường phải lớn hơn 0");
                }

                if (!IsClaimClosed)
                {
                    if (String.IsNullOrEmpty(ClaimProgressRemarks) || String.IsNullOrWhiteSpace(ClaimProgressRemarks))
                        return false;
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
                ClaimsProgress newClaimsProgress = new ClaimsProgress();
                newClaimsProgress.ClaimId = SelectedClaimId;

                if (IsClaimClosed)
                {
                    if (IsPaid)
                    {
                        claimRepository.CloseClaim(newClaimsProgress, (DateTime)ClaimProgressDate, ClaimTotalPaid);
                    }
                    else
                    {
                        claimRepository.CloseClaim(newClaimsProgress, (DateTime)ClaimProgressDate, 0);
                    }

                    Success = "Đã đóng hồ sơ bồi thường";
                }
                else
                {                    
                    newClaimsProgress.Date = (DateTime)ClaimProgressDate;
                    newClaimsProgress.Remarks = ClaimProgressRemarks;

                    claimProgressRepository.Add(newClaimsProgress);
                    claimProgressRepository.Save();
                    Success = "Đã cập nhật hồ sơ bồi thường";
                }                              

                OnPropertyChanged("SelectedClaim");
                UpdateResultAsync(Result.Successful);

                SelectedClaimId = 0;
                ClaimProgressRemarks = String.Empty;
                ClaimProgressDate = null;
                IsClaimClosed = false;
                ClaimTotalPaid = 0;

                OnPropertyChanged("ClaimProgressList");

                OnPropertyChanged("ListNotYetClosedClaim");
                OnPropertyChanged("TotalNotYetClosedClaim");

                OnPropertyChanged("ListPaidClaim");
                OnPropertyChanged("TotalPaidClaim");

                OnPropertyChanged("ListDeniedClaim");
                OnPropertyChanged("TotalDeniedClaim");
            });
            #endregion

            #region Delete Command
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedViewClaim == null)
                    return false;

                return true;
            }, (p) =>
            {
                IsDeleteDialogOpen = false;

                claimRepository.Delete(SelectedViewClaim);
                claimRepository.Save();

                Success = "Đã xóa hồ sơ bồi thường";
                UpdateResultAsync(Result.Successful);

                SelectedClaimIdForView = 0;
                OnPropertyChanged("SelectedClaimIdForView");
                OnPropertyChanged("ListNotYetClosedClaim");
                OnPropertyChanged("ListPaidClaim");
                OnPropertyChanged("ListDeniedClaim");
                OnPropertyChanged("TotalNotYetClosedClaim");
                OnPropertyChanged("TotalPaidClaim");
                OnPropertyChanged("TotalDeniedClaim");
            });
            #endregion

        }
    }
}
