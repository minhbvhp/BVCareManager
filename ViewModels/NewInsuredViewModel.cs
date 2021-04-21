using BVCareManager.Converter;
using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BVCareManager.ViewModels
{
    class NewInsuredViewModel : NewBaseViewModel
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

        private string _inputName;
        public string InputName
        {
            get
            {
                return _inputName;
            }
            set
            {
                SetProperty(ref _inputName, value);
            }
        }

        public NewInsuredViewModel()
        {
            _errorsList.Clear();
            InsuredRepository insuredRepository = new InsuredRepository();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(this.InputId))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(this.InputName))
                {
                    return false;
                }

                if (this.InputId.Length > 30)
                {
                    UpdateResultAsync(Result.HasError, "Độ dài tối đa của số CMT/CCCD là 30 ký tự");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Độ dài tối đa của số CMT/CCCD là 30 ký tự");
                }

                if (this.InputName.Length > 30)
                {
                    UpdateResultAsync(Result.HasError, "Độ dài tối đa của Họ tên nhân viên là 50 ký tự");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Độ dài tối đa của Họ tên nhân viên là 50 ký tự");
                }

                var creatingInsured = insuredRepository.GetInsured(this.InputId);
                if (creatingInsured != null)
                {
                    UpdateResultAsync(Result.HasError, "Số CMT/CCCD này đã tồn tại");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Số CMT/CCCD này đã tồn tại");
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
                Insured newInsured = new Insured();
                newInsured.Id = this.InputId.Trim(' ');
                newInsured.Name = this.InputName.Trim(' ');

                insuredRepository.Add(newInsured);
                insuredRepository.Save();

                UpdateResultAsync(Result.Successful);

                InputId = null;
                InputName = null;

            });
        }
    }
}