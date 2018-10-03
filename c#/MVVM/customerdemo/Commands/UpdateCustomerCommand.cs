using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MVVMDemos1.Commands
{

    using System.Windows.Input;
    using MVVMDemos1.ViewModels;

    class UpdateCustomerCommand : System.Windows.Input.ICommand

    {
        private CustomerViewModel viewModel;

        public UpdateCustomerCommand(CustomerViewModel viewModel)
        {
            this.viewModel = viewModel;
        }


        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value; 
            }
            remove
            {
                CommandManager.RequerySuggested -= value; 
            }
        }
        public bool CanExecute(object parameter)
        {
            return String.IsNullOrWhiteSpace(viewModel.Customer.Error); 
        }

        public void Execute(object parameter)
        {
            viewModel.SaveChanges(); 
        }
    }
}
