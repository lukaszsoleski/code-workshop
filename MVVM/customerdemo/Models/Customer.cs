using System;
using System.ComponentModel;

namespace MVVMDemos1.Models
{
    class Customer : INotifyPropertyChanged, IDataErrorInfo
    {

        /// <summary>
        /// Initializes a new instance of the Customer class
        /// </summary>
        /// <param name="customerName"></param>
        public Customer (string customerName)
        {
            this.Name = customerName; 
        }
        private string name; 
        /// <summary>
        /// Gets or sets the Customer's name. 
        /// </summary>
        public  string Name
        {
            get { return name; } 
            set
            {
                
                name = value;
                OnPropertyChanged(nameof(Name)); 
            }
            

  
        }

        public string Error
        {
            get;
            private set; 
        }
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(this.Name))
                {
                    if (String.IsNullOrWhiteSpace(Name))
                    {
                        Error = "Name cannot be null or empty.";
                    }
                    else Error = null; 
                }
                return Error; 
            }

        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
