using BVCareManager.Converter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BVCareManager.ViewModels
{
    class ModifyBaseViewModel : BaseViewModel
    {
        public List<String> Category
        {
            get
            {
                return new List<String> { "Nhân viên", "Đơn bảo hiểm", "Hợp đồng"};
            }
        }

        public string SearchText {get; set;}

        public ICommand ModifyCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        protected bool _isOk;

        protected async void UpdateResultAsync(Result result, string errorMessage = null)
        {
            if (result == Result.Successful)
            {
                _isOk = true;

            }
            else if (result == Result.HasError)
            {
                if (!_errorsList.Contains(errorMessage))
                    _errorsList.Add(errorMessage);

                _isOk = false;

            }
            else if (result == Result.ExcludeError)
            {
                if (_errorsList.Contains(errorMessage))
                    _errorsList.Remove(errorMessage);

                _isOk = false;
            }

            OnPropertyChanged("IsOk");

            if (IsOk)
            {
                await Task.Delay(3000);
                IsOk = false;
            }
        }

        public bool IsOk
        {
            get
            {
                return _isOk;
            }
            set
            {
                SetProperty(ref _isOk, value);
            }

        }

        public string Success { get { return "Đã sửa thành công"; } }

        protected ObservableCollection<string> _errorsList = new ObservableCollection<string>();
        public ObservableCollection<string> ErrorsList
        {
            get
            {
                return _errorsList;
            }
        }
    }
}
