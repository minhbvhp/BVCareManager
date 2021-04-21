using System;
using System.Collections.Generic;
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
    }
}
