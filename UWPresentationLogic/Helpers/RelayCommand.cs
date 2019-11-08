
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UWPresentationLogic.ViewModels.Helpers
{
    /// <summary>
    /// Rafal Stepien implementation
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;

        public RelayCommand(Action execute)
        {
            this.execute = parameter => execute();
        }

        public RelayCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public virtual void Execute(object parameter) => this.execute(parameter);
    }
}
