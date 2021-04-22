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

        public bool IsInsuredSelected
        {
            get
            {
                if (SelectedInsured != null)
                    return true;

                return false;
            }
        }

        private Insured _selectedInsured;
        public Insured SelectedInsured {
            get 
            {
                return _selectedInsured;
            }
            set 
            {
                SetProperty(ref _selectedInsured, value);
                OnPropertyChanged("IsInsuredSelected");
            }
        }
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

            ModifyCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                insuredRepository.Delete(SelectedInsured);
                insuredRepository.Save();

                SelectedInsured = null;
                
            });
        }

    }
}
