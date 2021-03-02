using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVCareManager.Models;

namespace BVCareManager.Repository
{
    class PolicyRepository
    {
        private BVCareManagerDataContext db = new BVCareManagerDataContext();

        //Query Methods
        public IQueryable<Policy> FindAllPolicies()
        {
            return db.Policies;
        }

        public Policy GetPolicy(int id)
        {
            return db.Policies.SingleOrDefault(policy => policy.Id == id);
        }

        //Insert/Delete Methods
        public void Add(Policy policy)
        {
            db.Policies.InsertOnSubmit(policy);
        }

        public void Delete(Policy policy)
        {
            db.Policies.DeleteOnSubmit(policy);
        }

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}
