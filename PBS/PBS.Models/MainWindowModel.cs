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
        private PBSCore core;

        public MainWindowModel()
        {
            core = PBSCore.Instance;
        }
    }
}
