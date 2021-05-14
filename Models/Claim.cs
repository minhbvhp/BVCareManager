using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.Models
{
    public partial class Claim
    {
        public bool IsClosed
        {
            get
            {
                if (this.TotalPaid > 0)
                    return true;

                return false;
            }
        }
    }
}
