using Core.AudioProcessing;
using Core.Descriptors;
using Models;

namespace Core.Processors
{
    /// <summary>
    /// Builds amplitude envelope
    /// </summary>
    public class EnvelopeProcessor : ISampleProcessor
    {
        private readonly DSPFactory factory;

        const int EnvelopeLength = 64;

        public EnvelopeProcessor(DSPFactory factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// Amplitude envelope builder
        /// </summary>
        public Samples Build(Samples source, int targetBitRate = 20, bool differentiate = false)
        {
            float k = 1f * targetBitRate / source.BitRate;
            var values = source.Values;
            var resValues = new float[(int)(values.Length * k) + 1];

            for (int i = values.Length - 2; i >= 0; i--)
            {
                var ii = (int)(i * k);
                var prev = resValues[ii];
                var f = values[i];
                if (differentiate) f = values[i + 1] - f;
                if (f < 0) f = -f;

                if (prev < f) resValues[ii] = f;
            }

            return new Samples() { BitRate = targetBitRate, Values = resValues };
        }

        public virtual void Process(AudioRecord item, AudioInfo info)
        {
            //build amplitude envelope
            var s = Build(info.Samples);
            //resample
            var resampler = factory.CreateResampler();
            var resampled = resampler.Resample(s, info.Samples.BitRate * ((float)EnvelopeLength / info.Samples.Values.Length));

            //build packed array
            var envelope = new Envelope(resampled);
            //save into audio item
            item.Data.Add(envelope);
        }
    }
}
