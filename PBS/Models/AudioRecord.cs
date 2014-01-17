using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Models
{
    /// <summary>
    /// Represents audio record in database
    /// </summary>
    public class AudioRecord : IStorable
    {
        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        public virtual string FullPath { get; set; }

        /// <summary>
        /// Gets the short name.
        /// </summary>
        public virtual string ShortName
        {
            get { return Path.GetFileName(FullPath); }
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public virtual RecordState State { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public List<IStorable> Data { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioRecord"/> class.
        /// </summary>
        public AudioRecord()
        {
            Data = new List<IStorable>();
        }

        /// <summary>
        /// Stores the specified binary writer.
        /// </summary>
        /// <param name="bw">The binary writer.</param>
        public void Store(BinaryWriter bw)
        {
            bw.Write((byte)0);//version
            bw.Write(FullPath ?? "");
            bw.Write((byte)State);
        }

        /// <summary>
        /// Loads the specified binary reader.
        /// </summary>
        /// <param name="br">The binary reader.</param>
        public virtual void Load(BinaryReader br)
        {
            br.ReadByte();//version
            FullPath = br.ReadString();
            State = (RecordState)br.ReadByte();
        }
    }
}
