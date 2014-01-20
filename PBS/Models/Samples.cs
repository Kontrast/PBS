using System;

namespace Models
{
    public class Samples : IStorable
    {
        /// <summary>
        /// The values
        /// </summary>
        private float[] values;

        /// <summary>
        /// The bit rate
        /// </summary>
        private float bitRate;

        /// <summary>
        /// Stores the specified bw.
        /// </summary>
        /// <param name="bw">The bw.</param>
        public void Store(System.IO.BinaryWriter bw)
        {
            bw.Write((byte)0);//version
            bw.Write(bitRate);
            bw.Write((int) values.Length);
            foreach (var v in values)
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
            br.ReadByte();//version
            bitRate = br.ReadSingle();
            var count = br.ReadInt32();
            values = new float[count];
            for (int i = 0; i < count; i++)
            {
                values[i] = br.ReadSingle();
            }
        }
    }
}
