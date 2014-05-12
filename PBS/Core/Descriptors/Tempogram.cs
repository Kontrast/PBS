using System.IO;
using Models;

namespace Core.Descriptors
{
    /// <summary>
    /// The tempogram
    /// </summary>
    public class Tempogram : IStorable
    {
        private const int size = 32;

        /// <summary>
        /// Describes main rithm
        /// </summary>
        public Histogram LongTempogram { get; private set; }

        /// <summary>
        /// Describes autocorrelation (short rithm)
        /// </summary>
        public Histogram ShortTempogram { get; private set; }

        /// <summary>
        /// Intensity (Hz), frequency of rithmical sounds
        /// </summary>
        public float Intensity { get; internal set; }

        /// <summary>
        /// Main rhythm (sec)
        /// </summary>
        public float LongRhythm { get; internal set; }

        /// <summary>
        /// Main rhythm level
        /// </summary>
        public float LongRhythmLevel { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tempogram"/> class.
        /// </summary>
        public Tempogram()
        {
            LongTempogram = new Histogram(size);
            ShortTempogram = new Histogram(size);
        }

        public void Store(BinaryWriter bw)
        {
            bw.Write(Intensity);
            bw.Write(LongRhythm);
            bw.Write(LongRhythmLevel);
            LongTempogram.Store(bw);
            ShortTempogram.Store(bw);
        }

        public void Load(BinaryReader br)
        {
            Intensity = br.ReadSingle();
            LongRhythm = br.ReadSingle();
            LongRhythmLevel = br.ReadSingle();
            LongTempogram.Load(br);
            ShortTempogram.Load(br);
        }

        public bool LongRhythmIsValid
        {
            get { return LongRhythmLevel > 0.2f && !float.IsPositiveInfinity(LongRhythm); }
        }

        public string Description
        {
            //get { return string.Format("Tmp {0:N2} Lvl {1:.000}", Tempo, RhythmLevel); }
            get
            {
                if (LongRhythmIsValid)
                    return string.Format("Ints {0:N2} Rtm {1:N1}", Intensity, LongRhythm);
                else
                    return string.Format("Ints {0:N2}", Intensity);
            }
        }


        public float Weight
        {
            get { return LongRhythmIsValid ? 1f : 0.1f; }
        }
    }
}
