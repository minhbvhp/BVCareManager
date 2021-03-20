using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVCareManager.Models;
using BVCareManager.Repository;
using System.Collections.ObjectModel;

namespace BVCareManager.ViewModels
{
    class MainViewModel
    {
        private InsuredRepository insuredRepository = new InsuredRepository();
        private ContractRepository contractRepository = new ContractRepository();
        private PolicyRepository policyRepository = new PolicyRepository();
        public IEnumerable<Insured> ListInsureds
        {
            get
            {
                return insuredRepository.FindAllInsureds().ToList();
            }
        }
        public IEnumerable<Contract> ListContracts
        {
            get
            {
                return contractRepository.FindAllContracts().ToList();
            }
        }

        public IEnumerable<Policy> ListPolicies
        {
            get
            {
                return policyRepository.FindAllPolicies().ToList();
            }
        }
    }
}
