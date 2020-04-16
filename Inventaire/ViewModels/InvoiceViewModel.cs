using BillingManagement.Business;
using BillingManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace BillingManagement.UI.ViewModels
{
    class InvoiceViewModel : BaseViewModel
    {
        readonly InvoicesDataService invoicesDataService = new InvoicesDataService();

        private ObservableCollection<Invoice> _invoices;
        private Invoice _selectedInvoice;

        public ObservableCollection<Invoice> Invoices
        {
            get => _invoices;
            private set
            {
                _invoices = value;
                OnPropertyChanged();
            }
        }

        public Invoice SelectedInvoice
        {
            get => _selectedInvoice;
            set
            {
                _selectedInvoice = value;
                StatusBar = $"Facture sélectionnée: " + (SelectedInvoice != null ? SelectedInvoice.Info : "");
                OnPropertyChanged();
            }
        }

        public InvoiceViewModel()
        {
            InitValues();
        }

        private void InitValues()
        {
            Invoices = new ObservableCollection<Invoice>(invoicesDataService.GetAll());
            Debug.WriteLine(Invoices.Count);
        }
    }
}
