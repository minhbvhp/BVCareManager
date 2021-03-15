using BVCareManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BVCareManager.ViewModels
{
    class NewInsuredViewModel : BaseViewModel
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

        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(InputId))
                yield return new RuleViolation("Không được để trống số CMND/CCCD", "Id");

            if (String.IsNullOrEmpty(InputName))
                yield return new RuleViolation("Không được để trống họ tên", "Name");

            yield break;
        }

    }
}
