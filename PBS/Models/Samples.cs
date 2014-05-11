using System;

namespace Models
{
    public class Samples : IStorable
    {
        /// <summary>
        /// Gets or sets the bit rate.
        /// </summary>
        public float BitRate { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        public float[] Values { get; set; }

        /// <summary>
        /// Returns sample for any point (with linear interpolation)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual float this[float index]
        {
            get
            {
                var intIndex = (int)index;
                if (intIndex < 0) return 0;
                if (index > Values.Length) return 0;
                if (intIndex == Values.Length) return Values[intIndex - 1];

                var rest = index - intIndex;
                return (1 - rest) * Values[intIndex] + rest * Values[intIndex + 1];//linear interpolation
            }
        }

        /// <summary>
        /// Normalizes amplitude (by default from -1 to +1)
        /// </summary>
        unsafe public void Normalize(float k = 1f)
        {
            var l = Values.Length;

            //find abs max sample
            float max = 0;
            fixed (float* valuesPtr = Values)
            {
                var ptr = valuesPtr;
                for (int i = 0; i < l; i++)
                {
                    var v = *ptr > 0 ? *ptr : -*ptr;
                    if (v > max)
                        max = v;
                    ptr++;
                }
            }

            //normalize
            if (max > float.Epsilon)
                Scale(k / max);
        }

        /// <summary>
        /// Scales amplitude of samples
        /// </summary>
        unsafe public void Scale(float volumeKoeff)
        {
            var l = Values.Length;

            fixed (float* valuesPtr = Values)
            {
                var ptr = valuesPtr;
                for (int i = 0; i < l; i++)
                {
                    *ptr *= volumeKoeff;
                    ptr++;
                }
            }
        }

        /// <summary>
        /// Stores the specified bw.
        /// </summary>
        /// <param name="bw">The bw.</param>
        public void Store(System.IO.BinaryWriter bw)
        {
            bw.Write(BitRate);
            bw.Write((int) Values.Length);
            foreach (var v in Values)
            {
                bw.Write(v);
            }
        }

        /// <summary>
        /// Loads the specified br.
        /// </summary>
        /// <param name="br">The br.</param>
        public void Load(System.IO.BinaryReader br)
        {
            BitRate = br.ReadSingle();
            var count = br.ReadInt32();
            Values = new float[count];
            for (int i = 0; i < count; i++)
            {
                Values[i] = br.ReadSingle();
            }
        }
    }
}
