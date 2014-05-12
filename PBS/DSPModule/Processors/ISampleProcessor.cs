using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPModule.Processors
{
    /// <summary>
    /// DSP processor interface
    /// </summary>
    public interface ISampleProcessor
    {
        void Process(AudioRecord)
    }
}
