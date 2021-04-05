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
            }
        }

        private string _selectedInsuredId;
        public string SelectedInsuredId
        {
            get
            {
                return _selectedInsuredId;
            }
            set
            {
                SetProperty(ref _selectedInsuredId, value);
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

        public ObservableCollection<String> ContractList
        {
            get
            {
                ContractRepository contractRepository = new ContractRepository();

                var _allContract = from contract in contractRepository.FindAllContracts()
                                   select contract;

                var AllContract = new ObservableCollection<String>();

                foreach (var contract in _allContract)
                    AllContract.Add(contract.Id);

                return AllContract;
            }
        }
        public ObservableCollection<String> InsuredList
        {
            get
            {
                InsuredRepository insuredRepository = new InsuredRepository();

                var _allInsured = from insured in insuredRepository.FindAllInsureds()
                                    select insured;
                var AllInsured = new ObservableCollection<String>();

                foreach (var insured in _allInsured)
                    AllInsured.Add(insured.Name + " - " + insured.Id);

                return AllInsured;
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

                newPolicy.ContractId = this.SelectedContractId.Trim(' ');

                Regex insuredIdRegex = new Regex(@"[^-]+$");
                newPolicy.InsuredId = insuredIdRegex.Match(this.SelectedInsuredId).ToString().Trim(' ');

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
