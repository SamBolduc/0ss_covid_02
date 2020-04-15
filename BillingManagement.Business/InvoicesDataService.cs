using app_models;
using BillingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillingManagement.Business
{
    public class InvoicesDataService : IDataService<Invoice>
    {

        private CustomersDataService _customers = new CustomersDataService();
        public List<Invoice> invoices;

        public InvoicesDataService()
        {
            initValues();
        }
        private void initValues()
        {
            invoices = new List<Invoice>();
            Random rnd = new Random();

            foreach (var customer in _customers.GetAll())
            {
                int nbInvoices = rnd.Next(10);

                for (int i = 0; i < nbInvoices; i++)
                {
                    var invoice = new Invoice(customer);
                    invoice.SubTotal = rnd.NextDouble() * 100 + 50;
                    customer.Invoices.Add(invoice);
                    invoices.Add(invoice);
                }
            }
        }

        public IEnumerable<Invoice> GetAll()
        {
           foreach(Invoice i in invoices)
           {
                yield return i;
           }
        }
    }
}
