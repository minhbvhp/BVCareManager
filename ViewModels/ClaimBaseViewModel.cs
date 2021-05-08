using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
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
        public ICommand AddCommand { get; set; }
        public ICommand ModifyCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        //private bool _isValidInsured;
        //public bool IsValidInsured
        //{
        //    get
        //    {
        //        if (!String.IsNullOrEmpty(this.SelectedInsuredId))
        //        {
        //            _isValidInsured = false;
        //            //InsuredRepository insuredRepository = new InsuredRepository();
        //            //if (insuredRepository.GetInsured(SelectedInsuredId) != null) 
        //            //    return true;
        //        }
        //        else
        //        {
        //            _isValidInsured = true;
        //        }

        //        return _isValidInsured;
        //    }
        //    set
        //    {
        //        SetProperty(ref _isValidInsured, value);
        //    }
        //}

        private string _selectedInsuredId;
        public string SelectedInsuredId
        {
            get
            {
                return _selectedInsuredId;
            }
            set
            {
                SetProperty(ref _selectedInsuredId, value);
            }
        }
    }
}
