using Core.AudioProcessing;
using Models;

namespace Core.Processors
{
    /// <summary>
    /// DSP processor interface
    /// </summary>
    public interface ISampleProcessor
    {
        void Process(AudioRecord record, AudioInfo info);
    }
}
