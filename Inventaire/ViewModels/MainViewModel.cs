using BillingManagement.UI.ViewModels.commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingManagement.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _contentModel;
        private InvoiceViewModel _invoiceModel;
        private CustomerViewModel _customerModel;
        public ChangeViewCommand ChangeViewCommand { get; set; }

        public BaseViewModel ContentModel
        {
            get => _contentModel;
            set
            {
                _contentModel = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            ChangeViewCommand = new ChangeViewCommand(SetView);

            _contentModel = new BaseViewModel();
            _invoiceModel = new InvoiceViewModel();
            _customerModel = new CustomerViewModel();

            ContentModel = _customerModel;
        }

        private void SetView(string view)
        {
            switch (view)
            {
                case "invoices":
                    ContentModel = _invoiceModel;
                    break;
                case "customers":
                    ContentModel = _customerModel;
                    break;
            }
        }
    }
}
