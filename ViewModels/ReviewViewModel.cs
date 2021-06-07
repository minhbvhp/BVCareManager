using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BVCareManager.ViewModels
{
    class ReviewViewModel : BaseViewModel
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

        public ReviewViewModel()
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
