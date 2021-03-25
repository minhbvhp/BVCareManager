using BVCareManager.Converter;
using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.ViewModels
{
    class NewPolicyViewModel : NewBaseViewModel
    {
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

        private string _inputContractId;
        public string InputContractId
        {
            get
            {
                return _inputContractId;
            }
            set
            {
                SetProperty(ref _inputContractId, value);
            }
        }

        private string _inputInsuredId;
        public string InputInsuredId
        {
            get
            {
                return _inputInsuredId;
            }
            set
            {
                SetProperty(ref _inputInsuredId, value);
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
            DateTime defaultFromDate = new DateTime(now.Year, 1, 1);
            DateTime defaultToDate = new DateTime(now.Year, 12, 31);
            _inputFromDate = defaultFromDate;
            _inputToDate = defaultToDate;

            _errorsList.Clear();
            PolicyRepository policyRepository = new PolicyRepository();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (this.InputNumber == 0)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(this.InputContractId))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(this.InputInsuredId))
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

                if (this.InputNumber <=0)
                {
                    UpdateResultAsync(Result.HasError, "Số đơn phải là số và lớn hơn 0");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Số đơn phải là số và lớn hơn 0");
                }

                if (this.InputFromDate > this.InputToDate)
                {
                    UpdateResultAsync(Result.HasError, "Ngày bắt đầu hiệu lực phải trước ngày kết thúc");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Ngày bắt đầu hiệu lực phải trước ngày kết thúc");
                }


                #region Checking Insured/Contract in Database
                insuredRepository.GetInsured()
                #endregion


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
                newPolicy.ContractId = this.InputContractId;
                newPolicy.InsuredId = this.InputInsuredId;
                newPolicy.FromDate = (DateTime)this.InputFromDate;
                newPolicy.ToDate = (DateTime)this.InputToDate;

                policyRepository.Add(newPolicy);
                policyRepository.Save();

                UpdateResultAsync(Result.Successful);

                InputNumber = 0;
                InputContractId = null;
                InputInsuredId = null;
                InputFromDate = defaultFromDate;
                InputToDate = defaultToDate;

            });
        }
    }
}
