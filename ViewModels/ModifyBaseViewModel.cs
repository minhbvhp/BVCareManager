using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected string _searchText;
        public string SearchText {
            get
            {
                return _searchText;
            }
            set
            {
                SetProperty(ref _searchText, value);
            }
        }
    }
}
