﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using wpf_demo_phonebook.ViewModels.Commands;

namespace wpf_demo_phonebook.ViewModels
{
    class ContactsViewModel : BaseViewModel
    {
        private ContactModel selectedContact;

        public ContactModel SelectedContact
        {
            get => selectedContact;
            set
            {
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        private string criteria;

        public string Criteria
        {
            get { return criteria; }
            set
            {
                criteria = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ContactModel> _contacts = new ObservableCollection<ContactModel>();
        public ObservableCollection<ContactModel> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveContactCommand { get; set; }
        public RelayCommand DeleteContactCommand { get; set; }

        public ContactsViewModel()
        {
            SaveContactCommand = new RelayCommand(SaveContact);
            DeleteContactCommand = new RelayCommand(DeleteContact);

            Contacts = PhoneBookBusiness.GetAllContacts();
            if (Contacts.Count > 0) SelectedContact = Contacts[0];
        }

        private void SaveContact(object c)
        {
            if (c is null) return;

            ContactModel contact = c as ContactModel;

            if(!contact.New)
            {
                PhoneBookBusiness.UpdateContact(contact);
                return;
            }

            contact.New = false;
            if (PhoneBookBusiness.InsertContact(contact) >= 0)
            {
                Contacts.Add(contact);
                OnPropertyChanged(nameof(Contacts));
            }
        }

        private void DeleteContact(object c)
        {
            if (c is null) return;

            ContactModel contact = c as ContactModel;


            MessageBoxResult confirmation = MessageBox.Show("Es-tu certain de vouloir supprimer ce contact?", "Confirmation", MessageBoxButton.YesNo);
            if (confirmation == MessageBoxResult.Yes)
            {
                if (PhoneBookBusiness.DeleteContact(c as ContactModel) > 0)
                {
                    Contacts.Remove(contact);
                    SelectedContact = null;
                    OnPropertyChanged("Contacts");
                }
            }
        }
    }
}
