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

        public IQueryable<Policy> SearchPolicies(string inputText)
        {
            return db.Policies.Where(policy => policy.InsuredId.Contains(inputText) ||
                                               policy.ContractId.Contains(inputText) ||
                                               policy.Insured.Name.Contains(inputText));
        }

        public Policy GetPolicyById(int id)
        {
            return db.Policies.SingleOrDefault(policy => policy.Id == id);
        }

        public Policy GetPolicyByIndex(int policyNumber, string contractId)
        {
            return db.Policies.SingleOrDefault
                (policy => 
                (policy.Number == policyNumber && policy.ContractId == contractId));
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

        //Update Foreign Key Methods
        public void UpdateContract(string currentContractId, string updateContractId)
        {
            Policy currentPolicy = db.Policies.Single(policy => policy.ContractId == currentContractId);
            Contract updateContract = db.Contracts.Single(contract => contract.Id == updateContractId);

            currentPolicy.Contract = updateContract;
        }

        public void UpdateInsured(string currentInsuredId, string updateInsuredId)
        {
            Policy currentPolicy = db.Policies.Single(policy => policy.InsuredId == currentInsuredId);
            Insured updateInsured = db.Insureds.Single(insured => insured.Id == updateInsuredId);

            currentPolicy.Insured = updateInsured;
        }

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}
