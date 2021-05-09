using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BVCareManager.ViewModels
{
    public class ClaimBaseViewModel : BaseViewModel
    {
        private InsuredRepository insuredRepository = new InsuredRepository();
        public ICommand AddCommand { get; set; }
        public ICommand ModifyCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ShowClaimOptions { get; set; }
        public string SearchText { get; set; }

        private bool _isShowClaimOptions;
        public bool IsShowClaimOptions
        {
            get
            {
                return _isShowClaimOptions;
            }
            set
            {
                SetProperty(ref _isShowClaimOptions, value);
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
        public Insured SelectedInsured
        {
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

        public ClaimBaseViewModel(string searchText)
        {
            IsShowClaimOptions = false;
            this.SearchText = searchText;

            ShowClaimOptions = new RelayCommand<object>((p) =>
            {
                if (IsInsuredSelected == false)
                    return false;

                return true;
            }, (p) =>
            {
                IsShowClaimOptions = true;
            });
        }
    }
}
