using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BVCareManager.ViewModels
{
    class ModifyInsuredViewModel : ModifyBaseViewModel
    {
        private InsuredRepository insuredRepository = new InsuredRepository();

        public IEnumerable<Insured> ListInsureds
        {
            get
            {
                string insuredSearchText = base.SearchText;
                if (insuredSearchText != null)
                {
                    MessageBox.Show(insuredSearchText);
                    return insuredRepository.SearchInsureds(insuredSearchText);
                }
                else
                {
                    MessageBox.Show("Loi roi");
                    return null;
                }

            }
        }
    }
}
