using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace wpf_demo_phonebook
{
    public class ContactModel : INotifyPropertyChanged
    {
        public int ContactID { get; set; }

        private string _firstName;
        public string FirstName {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Info));
            } 
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Info));
            }
        }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Info => $"{LastName}, {FirstName}";

        public bool New { get; set; } = false;

        public ContactModel(bool isNew)
        {
            ContactID = new Random().Next(1, 500000);
            FirstName = "Undefined";
            LastName = "Undefined";
            Email = "Undefined";
            Phone = "Undefined";
            Mobile = "Undefined";
            New = isNew;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
