using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.Models
{
    public partial class Claim
    {
        public bool IsPaid
        {
            get
            {
                if (this.TotalPaid > 0)
                    return true;

                return false;
            }
        }

        public bool IsDenied
        {
            get
            {
                if (this.TotalPaid == 0 && this.PaidDate != null)
                    return true;

                return false;
            }
        }

        public bool IsClosed
        {
            get
            {
                if (this.IsPaid || this.IsDenied)
                    return true;

                return false;
            }
        }
    }
}
