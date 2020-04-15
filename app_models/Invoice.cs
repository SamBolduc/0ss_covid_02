using app_models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace BillingManagement.Models
{
    class Invoice : INotifyPropertyChanged
    {
        private static int _id;

        public int InvoiceId { get; private set; }
        public DateTime CreationDateTime { get; }

        private Customer _customer;
        public Customer Customer { 
            get=>_customer; 
            set {
                _customer = value;
                OnPropertyChanged();
            } 
        }

        private double _subTotal;
        public double SubTotal {
            get =>_subTotal;
            set
            {
                _subTotal = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FedTax));
                OnPropertyChanged(nameof(ProvTax));
                OnPropertyChanged(nameof(Total));
            }
        }

        public Invoice()
        {
            InvoiceId = Interlocked.Increment(ref _id);
            CreationDateTime = DateTime.Now;
        }

        public Invoice(Customer customer) : this()
        {
            this.Customer = customer;
        }

        public double FedTax => SubTotal * 1.05;
        public double ProvTax => SubTotal * 1.0975;
        public double Total => SubTotal + FedTax + ProvTax;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
