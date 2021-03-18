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

        public ICommand AddCommand { get; set; }


        public NewInsuredViewModel()
        {
            InsuredRepository insuredRepository = new InsuredRepository();

            AddCommand = new RelayCommand<object>((p) =>
            {
                _errorsList.Clear();

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
                    UpdateResult(Result.HasError, "Độ dài tối đa của số CMT/CCCD là 30 ký tự");
                    return false;
                }

                if (this.InputName.Length > 30)
                {
                    UpdateResult(Result.HasError, "Độ dài tối đa của Họ tên nhân viên là 50 ký tự");
                    return false;
                }

                var creatingInsured = insuredRepository.GetInsured(this.InputId);
                if (creatingInsured != null)
                {
                    UpdateResult(Result.HasError, "Số CMT/CCCD này đã tồn tại");
                    return false;
                }

                return true;

            }, (p) =>
            {
                
                Insured newInsured = new Insured();
                newInsured.Id = this.InputId;
                newInsured.Name = this.InputName;

                insuredRepository.Add(newInsured);
                insuredRepository.Save();

                UpdateResult(Result.Successful);

                InputId = null;
                InputName = null;

            });
        }
    }
}
