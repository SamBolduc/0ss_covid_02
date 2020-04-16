using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BillingManagement.UI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _statusBar = "";
        public string StatusBar { 
            get=>_statusBar; 
            set
            {
                _statusBar = value;
                OnPropertyChanged();
            } 
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
