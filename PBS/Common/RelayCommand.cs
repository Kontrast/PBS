﻿using System;
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

        private EventHandler canExecuteEventhandler;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                this.canExecuteEventhandler += value;
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                this.canExecuteEventhandler -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        /// <summary>
        /// Call this when you know something about this command's ability
        /// to execute changed.
        /// </summary>
        public void InvokeCanExecuteChanged()
        {
            if (this.canExecute != null)
            {
                if (this.canExecuteEventhandler != null)
                {
                    this.canExecuteEventhandler(this, EventArgs.Empty);
                }
            }
        }
    }
}
