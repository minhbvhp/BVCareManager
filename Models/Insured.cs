using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.Models
{
    public partial class Insured
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0);  }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Id))
                yield return new RuleViolation("Không được để trống số CMND/CCCD", "Id");

            if (String.IsNullOrEmpty(Name))
                yield return new RuleViolation("Không được để trống họ tên", "Name");

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }
    }
}
