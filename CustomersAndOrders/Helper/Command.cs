using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersAndOrders.Model
{
    public class Command:ICommand
    {
            private Action _action;

            public Command(Action p_action)
            {
                _action = p_action;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                if (_action != null)
                {
                    _action();
                }
            }
    }
}
