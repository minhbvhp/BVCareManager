using BVCareManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.Repository
{
    class ClaimRepository
    {
        private BVCareManagerDataContext db = new BVCareManagerDataContext();

        //Query Methods
        public IQueryable<Claim> FindAllClaims()
        {
            return db.Claims;
        }

        public Claim GetClaimById(int id)
        {
            return db.Claims.SingleOrDefault(Claim => Claim.Id == id);
        }

        //Insert/Delete Methods
        public void Add(Claim claim)
        {
            db.Claims.InsertOnSubmit(claim);
        }

        public void Delete(Claim claim)
        {
            db.Claims.DeleteOnSubmit(claim);
        }

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}
