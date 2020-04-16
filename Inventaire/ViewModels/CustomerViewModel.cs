using app_models;
using BillingManagement.Business;
using BillingManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace BillingManagement.UI.ViewModels
{
    class CustomerViewModel : BaseViewModel
    {
        readonly CustomersDataService customersDataService = new CustomersDataService();

        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            private set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                StatusBar = $"Client sélectionné: " + (SelectedCustomer != null ? SelectedCustomer.Info : "");
                OnPropertyChanged();
            }
        }

        public CustomerViewModel()
        {
            InitValues();
        }

        private void InitValues()
        {
            Customers = new ObservableCollection<Customer>(customersDataService.GetAll());
            Debug.WriteLine(Customers.Count);
        }
    }
}
