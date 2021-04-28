using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.Models
{
    public partial class Policy
    {
        public int Premium {
            get
            {
                int monthsCover = (this.ToDate.Year - this.FromDate.Year) * 12 + this.ToDate.Month - this.FromDate.Month;
                int _premium = 0;
                int _premiumOfContract = this.Contract.AnnualPremiumPerInsured;

                if (monthsCover > 0 && monthsCover <= 3)
                {
                    _premium = _premiumOfContract * 30 / 100;
                } 
                else if (monthsCover > 3 && monthsCover <= 6)
                {
                    _premium = _premiumOfContract * 60 / 100;
                }
                else if (monthsCover > 6 && monthsCover <= 9)
                {
                    _premium = _premiumOfContract * 85 / 100;
                }
                else
                {
                    _premium = _premiumOfContract;
                }

                return _premium;
            }
            
        }
    }
}
