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

namespace BVCareManager.ViewModels
{
    class NewPolicyViewModel : NewBaseViewModel
    {
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
                ErrorsList.Clear();

                ContractRepository contractRepository = new ContractRepository();

                if (!String.IsNullOrEmpty(value))
                {
                    Contract contract = contractRepository.GetContract(value);
                    Contract checkingContract = contract;
                    ErrorsList.Clear();

                    InputFromDate = contract.FromDate;
                    InputToDate = contract.ToDate;
                }
            }
        }

        private string _selectedInsured;
        private string _selectedInsuredId;
        public string SelectedInsuredId
        {
            get
            {
                Regex insuredIdRegex = new Regex(@"[^-]+$");

                if (_selectedInsured != null)
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

        private int _inputNumber;
        public int InputNumber
        {
            get
            {
                return _inputNumber;
            }
            set
            {
                SetProperty(ref _inputNumber, value);
            }
        }

        private DateTime? _inputFromDate;
        public DateTime? InputFromDate
        {
            get
            {
                return _inputFromDate;
            }
            set
            {
                SetProperty(ref _inputFromDate, value);
            }
        }

        private DateTime? _inputToDate;
        public DateTime? InputToDate
        {
            get
            {
                return _inputToDate;
            }
            set
            {
                SetProperty(ref _inputToDate, value);
            }
        }

        public NewPolicyViewModel()
        {
            InsuredRepository insuredRepository = new InsuredRepository();
            ContractRepository contractRepository = new ContractRepository();

            var now = DateTime.Now;

            InputFromDate = null;
            InputToDate = null;
            

            _errorsList.Clear();
            PolicyRepository policyRepository = new PolicyRepository();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (this.InputNumber == 0)
                {
                    return false;
                }

                if (this.InputFromDate == null)
                {
                    return false;
                }

                if (this.InputToDate == null)
                {
                    return false;
                }

                if (this.SelectedInsuredId == String.Empty)
                    return false;

                if (this.InputFromDate > this.InputToDate)
                {
                    UpdateResultAsync(Result.HasError, "Ngày bắt đầu hiệu lực phải trước ngày kết thúc");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Ngày bắt đầu hiệu lực phải trước ngày kết thúc");
                }

                Contract checkingContract = contractRepository.GetContract(this.SelectedContractId);

                if (checkingContract != null)
                {
                    String fromDateContract = String.Format("{0:dd/MM/yyyy}", checkingContract.FromDate);
                    String toDateContract = String.Format("{0:dd/MM/yyyy}", checkingContract.ToDate);
                    string errorString = String.Format(
                        "Thời hạn của đơn bảo hiểm phải phù hợp thời hạn Hợp đồng ({0} - {1})",
                        fromDateContract, toDateContract);

                    if (!contractRepository.IsInForce(this.InputFromDate, this.InputToDate, checkingContract))
                    {
                        UpdateResultAsync(Result.HasError, errorString);
                    }
                    else
                    {
                        UpdateResultAsync(Result.ExcludeError, errorString);
                    }
                }

                if (policyRepository.GetPolicyByIndex(this.InputNumber, this.SelectedContractId) != null)
                {
                    UpdateResultAsync(Result.HasError, "Đơn bảo hiểm này đã tồn tại");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Đơn bảo hiểm này đã tồn tại");
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

                Policy newPolicy = new Policy();
                newPolicy.Number = this.InputNumber;

                newPolicy.ContractId = this.SelectedContractId.Trim(' ');

                newPolicy.InsuredId = this.SelectedInsuredId;

                newPolicy.FromDate = (DateTime)this.InputFromDate;
                newPolicy.ToDate = (DateTime)this.InputToDate;

                policyRepository.Add(newPolicy);
                policyRepository.Save();

                Success = "Đã tạo đơn bảo hiểm";
                UpdateResultAsync(Result.Successful);

                InputNumber = 0;

                IsStartOver = true;
                OnPropertyChanged("IsStartOver");
                IsStartOver = false;
            });
        }
    }
}
