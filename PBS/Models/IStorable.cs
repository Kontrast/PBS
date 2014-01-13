using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Models
{
    /// <summary>
    /// Can save/load himself into/from stream
    /// </summary>
    public interface IStorable
    {
        void Store(BinaryWriter bw);
        void Load(BinaryReader br);
    }
}
