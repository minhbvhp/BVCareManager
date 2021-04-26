using BVCareManager.Converter;
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
    class NewBaseViewModel : BaseViewModel
    {
        private bool _isStartOver;
        public bool IsStartOver {
            get
            {
                return _isStartOver;
            }
            set 
            {
                SetProperty(ref _isStartOver, value);
            }
        }

        public ICommand AddCommand { get; set; }
    }
}