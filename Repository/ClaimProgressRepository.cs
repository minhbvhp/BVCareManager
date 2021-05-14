using BVCareManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.Repository
{
    class ClaimProgressRepository
    {
        private BVCareManagerDataContext db = new BVCareManagerDataContext();

        //Query Methods
        public IQueryable<ClaimsProgress> FindAllClaimsProgress()
        {
            return db.ClaimsProgresses;
        }

        public ClaimsProgress GetClaimProgressById(int id)
        {
            return db.ClaimsProgresses.SingleOrDefault(ClaimProgress => ClaimProgress.Id == id);
        }

        //Close Claim
        public void CloseClaim(ClaimsProgress claimsProgress, DateTime paidDate, int totalPaid)
        {
            Claim claim = db.Claims.SingleOrDefault(claim => claim.Id == claimsProgress.ClaimId);

            claim.PaidDate = paidDate;
            claim.TotalPaid = totalPaid;
        }

        //Insert/Delete Methods
        public void Add(ClaimsProgress claimProgress)
        {
            db.ClaimsProgresses.InsertOnSubmit(claimProgress);
        }

        public void Delete(ClaimsProgress claim)
        {
            db.ClaimsProgresses.DeleteOnSubmit(claim);
        }

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}
