using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace MVVMDemo.Models
{
    public class Model : INotifyPropertyChanged, IDataErrorInfo
    {
        private string customerName; 
        public string CustomerName
        {
            get { return customerName;  }
            set { customerName = value; OnPropertyChanged(nameof(CustomerName));  }
        }

        #region IDataErrorInfo

        public bool IsValid
        {
            get
            {
                foreach(string property in ValidatedProperties)
                {
                    if (GetValidationError(property) != null) return false; 



                }
                return true; 
            }
        }


        string IDataErrorInfo.this[string propertyName]
        {
          get { return GetValidationError(propertyName); }
        }

        public string GetValidationError(string propertyName)
        {
            
            
                string error = null;

                switch (propertyName)
                {
                    case nameof(CustomerName):
                        error = ValidateCustomerName();
                        break;
                }
                return error;
            
        }

        private string ValidateCustomerName()
        {
            if (String.IsNullOrWhiteSpace(CustomerName))
            {
                return "Customer name cannot be empty."; 
            }
            return null; 
        }

        string IDataErrorInfo.Error
        {
            get { return null; }
        }
        #endregion

        static readonly string[] ValidatedProperties =
            {
            nameof(CustomerName)
            }; 

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
#endregion
    }
}
