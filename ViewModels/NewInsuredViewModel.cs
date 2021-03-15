using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BVCareManager.ViewModels
{
    class NewInsuredViewModel : BaseViewModel
    {
        private List<string> _results = new List<string>();
        public List<string> Results { get { return _results; } }

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

                if (Results != null && Results.Count > 0)
                    foreach (string result in Results)
                        MessageBox.Show(result);
                else
                    return;

            });
        }
    }
}
