using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.ViewModels
{
    class InsuredViewModel
    {
        private string _inputId;
        public string InputId {
            get
            {
                return _inputId;
            }
            set
            {
                _inputId = value;

            }
        public string InputName { get; set; }

        
    }
}
