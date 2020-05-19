using BillingManagement.Business;
using BillingManagement.Models;
using BillingManagement.UI.ViewModels.Commands;
using Inventaire;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BillingManagement.UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {
		private BaseViewModel _vm;
		private ObservableCollection<Customer> _customers;
		private ObservableCollection<Invoice> _invoices;

		public BillingManagementContext Database{ get; set; }

		public ObservableCollection<Customer> Customers
		{
			get => _customers;
			set
			{
				_customers = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<Invoice> Invoices
		{
			get => _invoices;
			set
			{
				_invoices = value;
				OnPropertyChanged();
			}
		}

		public BaseViewModel VM
		{
			get { return _vm; }
			set {
				_vm = value;
				OnPropertyChanged();
			}
		}

		private string searchCriteria;

		public string SearchCriteria
		{
			get { return searchCriteria; }
			set { 
				searchCriteria = value;
				OnPropertyChanged();
			}
		}

		public RelayCommand<Customer> SearchCommand { get; private set; }
		public DelegateCommand<object> CloseCommand { get; private set; }

		CustomerViewModel customerViewModel;
		InvoiceViewModel invoiceViewModel;

		public ChangeViewCommand ChangeViewCommand { get; set; }

		public DelegateCommand<object> AddNewItemCommand { get; private set; }

		public DelegateCommand<Invoice> DisplayInvoiceCommand { get; private set; }
		public DelegateCommand<Customer> DisplayCustomerCommand { get; private set; }

		public DelegateCommand<Customer> AddInvoiceToCustomerCommand { get; private set; }


		public MainViewModel()
		{
			this.Customers = new ObservableCollection<Customer>();
			this.Invoices = new ObservableCollection<Invoice>();
			this.Database = new BillingManagementContext();
			this.InitDatabase();

			ChangeViewCommand = new ChangeViewCommand(ChangeView);
			DisplayInvoiceCommand = new DelegateCommand<Invoice>(DisplayInvoice);
			DisplayCustomerCommand = new DelegateCommand<Customer>(DisplayCustomer);

			AddNewItemCommand = new DelegateCommand<object>(AddNewItem, CanAddNewItem);
			AddInvoiceToCustomerCommand = new DelegateCommand<Customer>(AddInvoiceToCustomer);

			this.CloseCommand = new DelegateCommand<object>(Close_Click);
			this.SearchCommand = new RelayCommand<Customer>(SearchContact, CanSearch);

			customerViewModel = new CustomerViewModel(Customers);
			invoiceViewModel = new InvoiceViewModel(Invoices);

			VM = customerViewModel;

		}

		private void ChangeView(string vm)
		{
			switch (vm)
			{
				case "customers":
					VM = customerViewModel;
					break;
				case "invoices":
					VM = invoiceViewModel;
					break;
			}
		}

		private void DisplayInvoice(Invoice invoice)
		{
			invoiceViewModel.SelectedInvoice = invoice;
			VM = invoiceViewModel;
		}

		private void DisplayCustomer(Customer customer)
		{
			customerViewModel.SelectedCustomer = customer;
			VM = customerViewModel;
		}

		private void AddInvoiceToCustomer(Customer c)
		{
			var invoice = new Invoice(c);
			c.Invoices.Add(invoice);
			DisplayInvoice(invoice);
		}

		private void AddNewItem (object item)
		{
			if (VM == customerViewModel)
			{
				var c = new Customer();
				customerViewModel.Customers.Add(c);
				customerViewModel.SelectedCustomer = c;
			}
		}

		private bool CanAddNewItem(object o)
		{
			bool result = false;

			result = VM == customerViewModel;
			return result;
		}

		private void Close_Click(object o)
		{
			App.Current.Shutdown();
		}

		private void InitDatabase()
		{
			if (Database.Customers.Count() == 0)
			{
				List<Customer> customers = new CustomersDataService().GetAll().ToList();
				List<Invoice> invoices = new InvoicesDataService(customers).GetAll().ToList();

				customers.ForEach(customer => Database.Customers.Add(customer));

				Database.SaveChanges();
			}

			Database.Customers.ToList().ForEach(c => Customers.Add(c));
			Database.Invoices.ToList().ForEach(i => Invoices.Add(i));
		}

		private void SearchContact(object parameter)
		{
			var input = this.searchCriteria as string;
			List<Customer> customers = this.Database.Customers.Where(c => c.LastName.StartsWith(input) || c.Name.StartsWith(input)).OrderBy(customer=> customer.LastName).ToList();
			if (input.Count() == 0) customers = this.Database.Customers.ToList();

			this.Customers.Clear();
			customers.ForEach(customer => this.Customers.Add(customer));
			this.customerViewModel.Customers = new ObservableCollection<Customer>(this.Customers);
			if(customers.Count() > 0) this.customerViewModel.SelectedCustomer = this.Customers.First();
		}

		private bool CanSearch(object c) => VM == customerViewModel;

	}
}
