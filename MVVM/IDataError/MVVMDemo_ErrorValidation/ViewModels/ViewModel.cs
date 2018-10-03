using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMDemo.Models; 
using System.Threading.Tasks;
using System.ComponentModel; 
namespace MVVMDemo.ViewModels
{
    class ViewModel : INotifyPropertyChanged
    {
        public Model Model { get; set; }

        public ViewModel()
        {
            Model = new Model()
            {
                CustomerName = "David"
            };
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    }

