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
    class NewInsuredViewModel : BaseViewModel
    {
        private bool _isOk;
        public bool IsOk
        {
            get
            {
                return _isOk;
            }
         }
        public string Success { get
            {
                return "Đã tạo thành công";
            } }
        private ObservableCollection<string> _errorsList = new ObservableCollection<string>();
        public ObservableCollection<string> ErrorsList {
            get
            {
                return _errorsList;
            }
            private set
            {

            }
        }

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
            ErrorsList = new ObservableCollection<string>();
            InsuredRepository insuredRepository = new InsuredRepository();

            AddCommand = new RelayCommand<object>((p) =>
            {
                _errorsList.Clear();

                if (string.IsNullOrEmpty(this.InputId))
                {
                    _isOk = false;
                    return false;
                }

                if (string.IsNullOrEmpty(this.InputName))
                {
                    _isOk = false;
                    return false;
                }

                var creatingInsured = insuredRepository.GetInsured(this.InputId);
                if (creatingInsured != null)
                {
                    _isOk = false;
                    _errorsList.Add("Nhân viên này đã tồn tại");
                    return false;
                }


                OnPropertyChanged("IsOk");
                return true;

            }, (p) =>
            {
                
                Insured newInsured = new Insured();
                newInsured.Id = this.InputId;
                newInsured.Name = this.InputName;

                insuredRepository.Add(newInsured);
                insuredRepository.Save();

                _isOk = true;

                OnPropertyChanged("IsOk");

                InputId = null;
                InputName = null;

            });
        }
    }
}
