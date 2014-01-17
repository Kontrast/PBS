using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// State of audio file in collection
    /// </summary>
    public enum RecordState : byte
    {
        /// <summary>
        /// File is unprocessed
        /// </summary>
        Unprocessed = 0,
        /// <summary>
        /// File is processed
        /// </summary>
        Processed = 1,
        /// <summary>
        /// Analyzing file was unsuccessful
        /// </summary>
        Bad = 2
    }
}
