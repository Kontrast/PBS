using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Common
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> canExecute;

        private readonly Action<object> execute;

        private EventHandler canExecuteEventHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="System.ArgumentNullException">execute</exception>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                canExecuteEventHandler += value;
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                if (canExecuteEventHandler != null)
                {
                    canExecuteEventHandler -= value;
                }
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }

        /// <summary>
        /// Call this when you know something about this command's ability
        /// to execute changed.
        /// </summary>
        public void InvokeCanExecuteChanged()
        {
            if (canExecute != null)
            {
                if (canExecuteEventHandler != null)
                {
                    canExecuteEventHandler(this, EventArgs.Empty);
                }
            }
        }
    }
}
