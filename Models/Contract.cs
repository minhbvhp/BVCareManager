using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.Models
{
    public partial class Contract
    {
        public int TotalPremium { 
            get {
                int totalPremium = 0;

                if (this.Policies.Count > 0)
                {
                    foreach(Policy policy in this.Policies)
                    {
                        totalPremium += policy.Premium;
                    }
                }
                else
                {
                    totalPremium = 0;
                }

                return totalPremium;
            } 
        }

        public int InitialTotalPremium
        {
            get
            {
                int initialTotalPremium = 0;

                if (this.Policies.Count > 0)
                {
                    foreach (Policy policy in this.Policies)
                    {
                        if (policy.FromDate == this.FromDate)
                            initialTotalPremium += policy.Premium;
                    }
                }
                else
                {
                    initialTotalPremium = 0;
                }

                return initialTotalPremium;
            }
        }
    }
}
