using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVCareManager.Models;

namespace BVCareManager.Repository
{
    class ContractRepository
    {
        private BVCareManagerDataContext db = new BVCareManagerDataContext();

        //Query Methods
        public IQueryable<Contract> FindAllContracts()
        {
            return db.Contracts;
        }

        public Contract GetContract(string id)
        {
            return db.Contracts.SingleOrDefault(contract => contract.Id == id);
        }

        //Insert/Delete Methods
        public void Add(Contract contract)
        {
            db.Contracts.InsertOnSubmit(contract);
        }

        public void Delete(Contract contract)
        {
            db.Contracts.DeleteOnSubmit(contract);
            db.Policies.DeleteAllOnSubmit(contract.Policies);
        }

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}
