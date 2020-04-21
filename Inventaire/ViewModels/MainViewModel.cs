﻿using BillingManagement.Models;
using BillingManagement.UI.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingManagement.UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {
		private BaseViewModel _vm;

		public BaseViewModel VM
		{
			get { return _vm; }
			set {
				_vm = value;
				OnPropertyChanged();
			}
		}

		CustomerViewModel customerViewModel;
		InvoiceViewModel invoiceViewModel;

		public RelayCommand ChangeViewCommand { get; set; }
		public RelayCommand NewCustomerCommand { get; private set; }
		public RelayCommand DisplayInvoiceCommand { get; private set; }
		public RelayCommand DisplayCustomerCommand { get; private set; }
		public RelayCommand NewInvoiceCommand { get; private set; }

		public MainViewModel()
		{
			ChangeViewCommand = new RelayCommand(ChangeView);
			NewCustomerCommand = new RelayCommand(NewCustomer);
			DisplayInvoiceCommand = new RelayCommand(DisplayInvoice);
			DisplayCustomerCommand = new RelayCommand(DisplayCustomer);
			NewInvoiceCommand = new RelayCommand(NewInvoice, CanCreateInvoice);

			customerViewModel = new CustomerViewModel();
			invoiceViewModel = new InvoiceViewModel(customerViewModel.Customers);

			VM = customerViewModel;

		}

		private void ChangeView(object vm)
		{
			switch (vm as string)
			{
				case "customers":
					VM = customerViewModel;
					break;
				case "invoices":
					VM = invoiceViewModel;
					break;
			}
		}

		private void NewCustomer(object c)
		{
			var customer = new Customer();
			customerViewModel.Customers.Add(customer);
			customerViewModel.SelectedCustomer = customer;
			VM = customerViewModel;
		}

		private void DisplayInvoice(object c)
		{
			invoiceViewModel.SelectedInvoice = c as Invoice;
			VM = invoiceViewModel;
		}

		private void DisplayCustomer(object c)
		{
			customerViewModel.SelectedCustomer = c as Customer;
			VM = customerViewModel;
		}

		private void NewInvoice(object c)
		{
			var customer = c as Customer;
			var invoice = new Invoice(customer);
			customer.Invoices.Add(invoice);
			DisplayInvoice(invoice);
		}

		private bool CanCreateInvoice(object c) => c != null;
	}
}
