using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AudioProcessing
{
    public interface IAudioDecoder : IDisposable
    {
        /// <summary>
        /// Decodes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="targetBitRate">The target bit rate.</param>
        /// <param name="fileExt">The file ext.</param>
        /// <returns></returns>
        AudioInfo Decode(Stream stream, float targetBitRate, string fileExt);

        /// <summary>
        /// Gets a value indicating whether [allows multithreading].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allows multiThreading]; otherwise, <c>false</c>.
        /// </value>
        bool AllowsMultiThreading { get; }
    }
}
