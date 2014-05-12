using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.AudioProcessing;
using Core.Descriptors;
using Core.Processors;

namespace Core
{
    /// <summary>
    /// The digital signal processors factory
    /// </summary>
    public class DSPFactory
    {
        public  IAudioDecoder CreateAudioDecoder()
        {
            return new AudioDecoder();
        }

        /// <summary>
        /// Enumerates signal samples processors
        /// </summary>
        public IEnumerable<ISampleProcessor> CreateSampleProcessors()
        {
            // build envelope
            yield return new EnvelopeProcessor(this);
            // build tempogram
            yield return new TempogramProcessor(this);
        }

        public Dictionary<int, Type> GetKnownTypes()
        {
            var result = new Dictionary<int, Type>();

            result.Add(0, typeof(Envelope));
            result.Add(1, typeof (Tempogram));

            return result;
        }

        /// <summary>
        /// Ctrates resampler
        /// </summary>
        public virtual Resampler CreateResampler()
        {
            return new Resampler();
        }
    }
}
