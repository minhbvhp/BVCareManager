using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.Models
{
    public partial class Policy
    {
        public int AdditionalPremium {
            get
            {
                int monthsCover = (this.Contract.ToDate.Year - this.FromDate.Year) * 12 + this.Contract.ToDate.Month - this.FromDate.Month + 1;
                double _additionalPremium = 0;
                int _premiumOfContract = this.Contract.AnnualPremiumPerInsured;

                if (monthsCover > 0 && monthsCover <= 3)
                {
                    _additionalPremium = _premiumOfContract * 30 / 100;
                }
                else if (monthsCover > 3 && monthsCover <= 6)
                {
                    _additionalPremium = _premiumOfContract * 60 / 100;
                }
                else if (monthsCover > 6 && monthsCover <= 9)
                {
                    _additionalPremium = _premiumOfContract * 85 / 100;
                }
                else
                {
                    _additionalPremium = _premiumOfContract;
                }

                return (int)Math.Round(_additionalPremium, MidpointRounding.AwayFromZero);
            }
        }

        public bool CouldBeRefunded
        {
            get
            {
                if (this.Claims.Count == 0)
                {
                    return true;
                }
                else
                {
                    foreach (Claim claim in this.Claims)
                    {
                        if (claim.TotalPaid != 0)
                            return false;                        
                    }
                    return true;
                }
            }
        }

        public int RefundPremium {
            get
            {
                if (CouldBeRefunded)
                {
                    double refundDays = (this.Contract.ToDate - this.ToDate).TotalDays;
                    double _refundPremium = this.Contract.AnnualPremiumPerInsured * refundDays / 365;

                    return (int)Math.Round(_refundPremium, MidpointRounding.AwayFromZero);
                }

                return 0;
            }
        }

        public int Premium {
            get
            {
                return AdditionalPremium - RefundPremium;
            }
            
        }

        public bool IsShortTerm {
            get
            {
                if (this.FromDate > this.Contract.FromDate || this.ToDate < this.Contract.ToDate)
                    return true;

                return false;
            }
        }
    }
}
