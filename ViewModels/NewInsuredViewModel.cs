using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BVCareManager.ViewModels
{
    class NewInsuredViewModel : BaseViewModel
    {
        private string _added;
        private ObservableCollection<string> _results = new ObservableCollection<string>();
        public ObservableCollection<string> Results {
            get
            {

                return _results;
            }
            private set
            {

            }
        }

        private string _inputId;
        public string InputId
        {
            get
            {
                return _inputId;
            }
            set
            {                
                SetProperty(ref _inputId, value);
            }
        }

        private string _inputName;
        public string InputName 
        {
            get
            {
                return _inputName;
            }
            set
            {
                SetProperty(ref _inputName, value);
            }
        }

        public ICommand AddCommand { get; set; }


        public NewInsuredViewModel()
        {
            Results = new ObservableCollection<string>();
            InsuredRepository insuredRepository = new InsuredRepository();

            AddCommand = new RelayCommand<object>((p) =>
            {
                _results.Clear();

                if (string.IsNullOrEmpty(this.InputId))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(this.InputName))
                {
                    return false;
                }

                var creatingInsured = insuredRepository.GetInsured(this.InputId);
                if (creatingInsured != null)
                {                  
                    _results.Add("Nhân viên này đã tồn tại");
                    return false;
                }

                return true;

            }, (p) =>
            {
                
                Insured newInsured = new Insured();
                newInsured.Id = this.InputId;
                newInsured.Name = this.InputName;

                insuredRepository.Add(newInsured);
                insuredRepository.Save();

                InputId = null;
                InputName = null;

            });
        }
    }
}
