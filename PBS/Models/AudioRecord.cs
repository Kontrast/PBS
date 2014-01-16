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
    public class AudioRecord
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
        public virtual AudioState State { get; set; }

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

        //public void Store(BinaryWriter bw)
        //{
        //    bw.Write((byte)0);//version
        //    bw.Write(FullPath ?? "");
        //    bw.Write((byte)State);
        //    //save Data
        //    bw.Write(Data.Count);
        //    foreach (var data in Data)
        //    {
        //        var id = DB.GetIdByType(data.GetType());//id of type
        //        bw.Write(id);
        //        data.Store(bw);
        //    }
        //}
    }

    /// <summary>
    /// State of audio file in collection
    /// </summary>
    public enum AudioState : byte
    {
        /// <summary>
        /// File is unprocessed
        /// </summary>
        Unprocessed,
        /// <summary>
        /// File is processed
        /// </summary>
        Processed,
        /// <summary>
        /// Analyzing file was unsuccessful
        /// </summary>
        Bad
    }
}
