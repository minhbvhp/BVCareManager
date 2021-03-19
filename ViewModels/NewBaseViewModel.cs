using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.ViewModels
{
    class NewBaseViewModel : BaseViewModel
    {
        protected bool _isOk;

        protected async void UpdateResultAsync(Result result, string errorMessage="Lỗi")
        {
            if (result == Result.Successful)
            {
                _isOk = true;
                
            }
            else if (result == Result.HasError)
            {
                _errorsList.Add(errorMessage);
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

        public string Success { get {return "Đã tạo thành công"; } }

        protected ObservableCollection<string> _errorsList = new ObservableCollection<string>();
        public ObservableCollection<string> ErrorsList
        {
            get
            {
                return _errorsList;
            }
            private set
            {

            }
        }
    }
}
