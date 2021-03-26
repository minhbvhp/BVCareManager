using BVCareManager.Converter;
using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<String> ListContractId
        {
            get
            {
                ContractRepository contractRepository = new ContractRepository();

                var _listContract = from contract in contractRepository.FindAllContracts()
                                   select contract;

                var ListContract = new ObservableCollection<String>();

                foreach (var lC in _listContract)
                    ListContract.Add(lC.Id + " - " + lC.AnnualPremiumPerInsured.ToString());

                return ListContract;
            }
        }
        public ObservableCollection<Insured> ListInsuredId
        {
            get
            {
                InsuredRepository insuredRepository = new InsuredRepository();

                var _listInsured = from insured in insuredRepository.FindAllInsureds()
                                    select insured;
                var ListInsured = new ObservableCollection<Insured>(_listInsured);
                return ListInsured;
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
                //newPolicy.ContractId = this.InputContractId;
                //newPolicy.InsuredId = this.InputInsuredId;
                newPolicy.FromDate = (DateTime)this.InputFromDate;
                newPolicy.ToDate = (DateTime)this.InputToDate;

                policyRepository.Add(newPolicy);
                policyRepository.Save();

                UpdateResultAsync(Result.Successful);

                InputNumber = 0;
                InputFromDate = defaultFromDate;
                InputToDate = defaultToDate;

            });
        }
    }
}
