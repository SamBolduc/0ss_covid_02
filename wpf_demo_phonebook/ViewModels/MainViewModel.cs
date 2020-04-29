using System;
using System.Collections.ObjectModel;
using System.Windows;
using wpf_demo_phonebook.ViewModels.Commands;
using wpf_demo_phonebook.Views;

namespace wpf_demo_phonebook.ViewModels
{
    class MainViewModel : BaseViewModel
    {

        private BaseViewModel _vm;
        public BaseViewModel VM
        {
            get => _vm;
            set
            {
                _vm = value;
                OnPropertyChanged();
            }
        }

        ContactsViewModel contactsViewModel;

        public RelayCommand SearchContactCommand { get; set; }

        public MainViewModel()
        {
            SearchContactCommand = new RelayCommand(SearchContact);

            contactsViewModel = new ContactsViewModel();
            VM = contactsViewModel;
        }

        private void SearchContact(object parameter)
        {
            string input = parameter as string;
            int output;
            string searchMethod;
            if (!Int32.TryParse(input, out output))
            {
                searchMethod = "name";
            }
            else
            {
                searchMethod = "id";
            }

            switch (searchMethod)
            {
                case "id":
                    contactsViewModel.SelectedContact = PhoneBookBusiness.GetContactByID(output);
                    break;
                case "name":
                    if (input.Length == 0) contactsViewModel.Contacts = PhoneBookBusiness.GetAllContacts();
                    else contactsViewModel.Contacts = PhoneBookBusiness.GetContactsByName(input);
                    contactsViewModel.SelectedContact = contactsViewModel.Contacts.Count > 0 ? contactsViewModel.Contacts[0] : null;
                    break;
                default:
                    MessageBox.Show("Unkonwn search method");
                    break;
            }
        }
    }
}
