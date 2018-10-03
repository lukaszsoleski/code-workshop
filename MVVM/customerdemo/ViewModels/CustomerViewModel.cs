using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MVVMDemos1.Models;
using MVVMDemos1.Commands;
using MVVMDemos1.Views;
namespace MVVMDemos1.ViewModels
{
    class CustomerViewModel
    {

        private Customer customer; 
        private CustomerInfoViewModel childViewModel; 

        /// <summary>
        /// Initializes a new instance of CustomerViewModel class.
        /// </summary>
        public CustomerViewModel()
        {
            this.Customer = new Customer("David");
            childViewModel = new CustomerInfoViewModel(); 
            UpdateCommand = new UpdateCustomerCommand(this);
        }

       

        /// <summary>
        /// Gets or private sets the customer instance. 
        /// </summary>
        public Customer Customer
        {
            private set { customer = value; }
            get { return customer; }
        }

        /// <summary>
        /// Gets the UpdateCommand for the ViewModel 
        /// </summary>
        public ICommand UpdateCommand
        {
            get;
            private set; 
        }
        /// <summary>
        /// Saves changes made to the Customer instance. 
        /// It's fake method now. 
        /// </summary>
        public void SaveChanges()
        {
            CustomerInfoView view = new CustomerInfoView()
            {
                DataContext = childViewModel
            };

            childViewModel.Info = Customer.Name + "was updated in the database.";

            view.ShowDialog(); 
        }



    }
}
