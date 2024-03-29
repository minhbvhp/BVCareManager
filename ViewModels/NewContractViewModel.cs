﻿using BVCareManager.Converter;
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
                return  _inputId;
            }
            set
            {
                SetProperty(ref _inputId, value);
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

        private int _inputAnnualPremiumPerInsured;
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
                    return false;
                }

                if (this.InputToDate == null)
                {
                    return false;
                }

                if (this.InputId.Length > 50)
                {
                    UpdateResultAsync(Result.HasError, "Độ dài tối đa của số Hợp đồng là 50 ký tự");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Độ dài tối đa của số Hợp đồng là 50 ký tự");
                }

                if (this.InputFromDate > this.InputToDate)
                {
                    UpdateResultAsync(Result.HasError, "Ngày bắt đầu hiệu lực phải trước ngày kết thúc");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Ngày bắt đầu hiệu lực phải trước ngày kết thúc");
                }

                if (this.InputAnnualPremiumPerInsured == 0)
                {
                    UpdateResultAsync(Result.HasError, "Phí bảo hiểm phải lớn hơn 0");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Phí bảo hiểm phải lớn hơn 0");
                }

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
                newContract.Id = this.InputId.Trim(' ').ToUpper();
                newContract.FromDate = (DateTime)this.InputFromDate;
                newContract.ToDate = (DateTime)this.InputToDate;
                newContract.AnnualPremiumPerInsured = this.InputAnnualPremiumPerInsured;

                contractRepository.Add(newContract);
                contractRepository.Save();

                Success = "Đã tạo hợp đồng";
                UpdateResultAsync(Result.Successful);

                InputId = null;
                InputFromDate = defaultFromDate;
                InputToDate = defaultToDate;
                InputAnnualPremiumPerInsured = 0;

                IsStartOver = true;
                OnPropertyChanged("IsStartOver");
                IsStartOver = false;

            });
        }

    }
}
