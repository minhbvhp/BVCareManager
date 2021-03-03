using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.Models
{
    public partial class Contract
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }
        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Id))
                yield return new RuleViolation("Không được để trống số Hợp đồng", "Id");

            if (String.IsNullOrEmpty(FromDate.ToString()))
                yield return new RuleViolation("Không được để trống ngày bắt đầu hiệu lực", "FromDate");

            if (String.IsNullOrEmpty(ToDate.ToString()))
                yield return new RuleViolation("Không được để trống ngày kết thúc hiệu lực", "ToDate");

            if (AnnualPremiumPerInsured <= 0)
                yield return new RuleViolation("Số phí bảo hiểm phải lớn hơn 0", "AnnualPremiumPerInsured");

            if (FromDate >= ToDate)
                yield return new RuleViolation("Ngày bắt đầu hiệu lực phải trước ngày kết thúc", "FromDate");

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }
    }
}
