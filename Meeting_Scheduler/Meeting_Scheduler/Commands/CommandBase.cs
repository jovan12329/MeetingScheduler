using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meeting_Scheduler.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        protected void OnCanExecutedChanged()
        {

            CanExecuteChanged?.Invoke(this, new EventArgs());

        }


        public abstract void Execute(object parameter);

    }
}
