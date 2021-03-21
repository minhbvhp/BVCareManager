using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.ViewModels
{
    class NewContractViewModel : NewBaseViewModel
    {
        private string _inputId;
        public string InputId
        {
            get
            {
                return _inputId;
            }
            set
            {
                SetProperty(ref _inputId, value);
            }
        }

        private DateTime _inputFromDate;
        public DateTime InputFromDate
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

        private DateTime _inputToDate;
        public DateTime InputToDate
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

        private int _inputAnnualPremiumPerInsured = 0;
        public int InputAnnualPremiumPerInsured
        {
            get
            {
                return _inputAnnualPremiumPerInsured;
            }
            set
            {
                SetProperty(ref _inputAnnualPremiumPerInsured, value);
            }
        }

        public NewContractViewModel()
        {
            var now = DateTime.Now;
            DateTime defaultFromDate = new DateTime(now.Year, 1, 1);
            DateTime defaultToDate = new DateTime(now.Year, 12, 31);
            _inputFromDate = defaultFromDate;
            _inputToDate = defaultToDate;

            _errorsList.Clear();
            ContractRepository contractRepository = new ContractRepository();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(this.InputId))
                {
                    return false;
                }

                if (this.InputFromDate == null)
                {
                    InputFromDate = defaultFromDate;
                    return false;
                }

                if (this.InputToDate == null)
                {
                    InputToDate = defaultToDate;
                    return false;
                }


                //if (this.InputId.Length > 30)
                //{
                //    UpdateResultAsync(Result.HasError, "Độ dài tối đa của số CMT/CCCD là 30 ký tự");
                //}
                //else
                //{
                //    UpdateResultAsync(Result.ExcludeError, "Độ dài tối đa của số CMT/CCCD là 30 ký tự");
                //}

                //if (this.InputName.Length > 30)
                //{
                //    UpdateResultAsync(Result.HasError, "Độ dài tối đa của Họ tên nhân viên là 50 ký tự");
                //}
                //else
                //{
                //    UpdateResultAsync(Result.ExcludeError, "Độ dài tối đa của Họ tên nhân viên là 50 ký tự");
                //}

                var creatingContract = contractRepository.GetContract(InputId);
                if (creatingContract != null)
                {
                    UpdateResultAsync(Result.HasError, "Số Hợp đồng này đã tồn tại");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Số Hợp đồng này đã tồn tại");
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

                Contract newContract = new Contract();
                newContract.Id = this.InputId;
                newContract.FromDate = (DateTime)this.InputFromDate;
                newContract.ToDate = (DateTime)this.InputToDate;
                newContract.AnnualPremiumPerInsured = (int)this.InputAnnualPremiumPerInsured;

                contractRepository.Add(newContract);
                contractRepository.Save();

                UpdateResultAsync(Result.Successful);

                InputId = null;
                InputFromDate = defaultFromDate;
                InputToDate = defaultToDate;
                InputAnnualPremiumPerInsured = 0;

            });
        }

    }
}
