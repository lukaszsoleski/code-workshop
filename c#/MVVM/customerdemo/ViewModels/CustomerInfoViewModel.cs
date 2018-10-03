using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMDemos1.ViewModels
{
    class CustomerInfoViewModel : INotifyPropertyChanged
    {
        private string info; 

        public string Info
        {
            get
            {
                return info; 
            }
            set
            {
                info = value;
                OnPropertyChanged(nameof(this.Info)); 
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
