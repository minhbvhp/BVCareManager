using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.ViewModels
{
    class ModifyInsuredViewModel : ModifyBaseViewModel
    {
        private InsuredRepository insuredRepository = new InsuredRepository();

        public IEnumerable<Insured> ListInsureds
        {
            get
            {
                string insuredSearchText = SearchText;
                if (!String.IsNullOrEmpty(insuredSearchText))
                {
                    return insuredRepository.SearchInsureds(insuredSearchText);
                }
                else
                {
                    return insuredRepository.FindAllInsureds();
                }

            }
        }

        public ModifyInsuredViewModel(string searchText)
        {
            this.SearchText = searchText;
        }

    }
}
