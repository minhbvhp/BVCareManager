using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace BVCareManager.Models
{
    public partial class Policy
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }


        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Number.ToString()))
                yield return new RuleViolation("Không được để trống số Đơn", "Number");

            if (String.IsNullOrEmpty(ContractId))
                yield return new RuleViolation("Không được để trống số Hợp đồng", "ContractId");

            if (String.IsNullOrEmpty(InsuredId))
                yield return new RuleViolation("Không được để trống số CMND/CCCD", "InsuredId");

            if (String.IsNullOrEmpty(FromDate.ToString()))
                yield return new RuleViolation("Không được để trống ngày bắt đầu hiệu lực", "FromDate");

            if (String.IsNullOrEmpty(ToDate.ToString()))
                yield return new RuleViolation("Không được để trống ngày kết thúc hiệu lực", "ToDate");

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
