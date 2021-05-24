using BVCareManager.Converter;
using BVCareManager.Models;
using BVCareManager.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BVCareManager.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual bool SetProperty<T> (ref T member, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(member, value))
            {
                return false;
            }

            member = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public class RelayCommand<T> : ICommand
        {
            private readonly Predicate<T> _canExecute;
            private readonly Action<T> _execute;

            public RelayCommand(Predicate<T> canExecute, Action<T> execute)
            {
                if (execute == null)
                    throw new ArgumentNullException("execute");
                _canExecute = canExecute;
                _execute = execute;
            }

            public bool CanExecute(object parameter)
            {
                try
                {
                    return _canExecute == null ? true : _canExecute((T)parameter);
                }
                catch
                {
                    return true;
                }
            }

            public void Execute(object parameter)
            {
                _execute((T)parameter);
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }

        protected bool _isOk;

        protected async void UpdateResultAsync(Result result, string errorMessage = null)
        {
            if (result == Result.Successful)
            {
                _isOk = true;
            }
            else if (result == Result.HasError)
            {
                if (!_errorsList.Contains(errorMessage))
                    _errorsList.Add(errorMessage);

                _isOk = false;

            }
            else if (result == Result.ExcludeError)
            {
                if (_errorsList.Contains(errorMessage))
                    _errorsList.Remove(errorMessage);

                _isOk = false;
            }

            OnPropertyChanged("IsOk");

            if (IsOk)
            {
                await Task.Delay(3000);
                IsOk = false;
            }
        }

        public bool IsOk
        {
            get
            {
                return _isOk;
            }
            set
            {
                SetProperty(ref _isOk, value);
            }

        }

        protected string _success;
        public string Success
        {
            get
            {
                return _success;
            }
            set
            {
                SetProperty(ref _success, value);
            }
        }

        protected ObservableCollection<string> _errorsList = new ObservableCollection<string>();
        public ObservableCollection<string> ErrorsList
        {
            get
            {
                return _errorsList;
            }
        }


        public ObservableCollection<Contract> ContractList
        {
            get
            {
                ContractRepository contractRepository = new ContractRepository();

                var _allContract = from contract in contractRepository.FindAllContracts()
                                   select contract;

                return new ObservableCollection<Contract>(_allContract);
            }
        }
        public ObservableCollection<Insured> InsuredList
        {
            get
            {
                InsuredRepository insuredRepository = new InsuredRepository();

                var _allInsured = from insured in insuredRepository.FindAllInsureds()
                                  select insured;

                return new ObservableCollection<Insured>(_allInsured);
            }
        }        
    }
}