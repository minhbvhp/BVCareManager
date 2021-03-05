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
        public IEnumerable<Insured> AllInsureds { get; set; }
        public Insured Insured
        {
            get
            {
                Insured insured = insuredRepository.GetInsured("3");
                return insured;
            }
        }

        public IEnumerable<Insured> Index()
        {
            var insureds = insuredRepository.FindAllInsureds().ToList();
            AllInsureds = insureds;
            return AllInsureds;
        }



    }
}
