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
                return new List<String> { "Nhân viên", "Đơn bảo hiểm", "Hợp đồng" };
            }
        }

        public string SearchText { get; set; }

        private bool _isModifyDialogOpen;
        public bool IsModifyDialogOpen
        {
            get
            {
                return _isModifyDialogOpen;
            }
            set
            {
                SetProperty(ref _isModifyDialogOpen, value);
            }
        }

        public ICommand ModifyCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        
    }
}
