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

        public IQueryable<Contract> SearchContracts(string inputText)
        {
            return db.Contracts.Where(contract => contract.Id.Contains(inputText));
        }

        public Contract GetContract(string id)
        {
            return db.Contracts.SingleOrDefault(contract => contract.Id == id);
        }

        public bool IsInForce(DateTime? fromDate, DateTime? toDate, Contract contract)
        {
            if (fromDate < contract.FromDate || toDate > contract.ToDate)
                return false;

            return true;
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
