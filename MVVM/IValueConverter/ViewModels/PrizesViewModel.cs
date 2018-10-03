using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMdemo.Model;
using System.Windows.Input;

namespace MVVMdemo.ViewModels
{
    class PrizesViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        private SumOfPrizes SumModel = new SumOfPrizes(1000); 

        public string Sum
        {
            get { return SumModel.Sum.ToString(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }

        public bool IsSumStringCorrect(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            decimal price;
            return decimal.TryParse(s, out price) ? SumModel.IsPriceCorrect(price) : false; 
            
        }

        private ICommand AddPriceCommand; 

        public ICommand AddPrice
        {
            get
            {
                if (AddPriceCommand == null)
                    AddPriceCommand = new RelayCommand(
                        (object argumet) =>
                        {
                            decimal price = decimal.Parse((string)argumet);
                            SumModel.Add(price);
                            OnPropertyChanged(nameof(Sum));
                        },
                        (object argument) =>
                        {
                            return IsSumStringCorrect((string)argument);
                        }
                        );
                return AddPriceCommand; 
            }
        }
    }
}
