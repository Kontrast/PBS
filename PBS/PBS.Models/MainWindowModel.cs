using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace PBS.Models
{
    public class MainWindowModel
    {
        /// <summary>
        /// The core
        /// </summary>
        private PBSCore core;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowModel"/> class.
        /// </summary>
        public MainWindowModel()
        {
            core = PBSCore.Instance;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            core.SaveChanges();
        }
    }
}
