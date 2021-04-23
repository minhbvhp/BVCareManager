using BVCareManager.Converter;
using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private IEnumerable<Insured> _listInsureds;
        public ObservableCollection<Insured> ListInsureds
        {
            get
            {
                string insuredSearchText = SearchText;
                if (!String.IsNullOrEmpty(insuredSearchText))
                {
                    _listInsureds = insuredRepository.SearchInsureds(insuredSearchText);
                }
                else
                {
                    _listInsureds = insuredRepository.FindAllInsureds();
                }

                return new ObservableCollection<Insured>(_listInsureds);

            }
        }

        public ModifyInsuredViewModel(string searchText)
        {
            _errorsList.Clear();
            this.SearchText = searchText;

            ModifyCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(SelectedInsured.Name))
                    return false;

                if (SelectedInsured.Name.Length > 30)
                {
                    UpdateResultAsync(Result.HasError, "Độ dài tối đa của Họ tên nhân viên là 50 ký tự");
                }
                else
                {
                    UpdateResultAsync(Result.ExcludeError, "Độ dài tối đa của Họ tên nhân viên là 50 ký tự");
                }

                if (_errorsList.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) =>
            {
                insuredRepository.Save();

                SelectedInsured = null;
                OnPropertyChanged("ListInsureds");
                
            });
        }

    }
}
