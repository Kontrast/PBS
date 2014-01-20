using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AudioProcessing
{
    public class AudioDecoder : IAudioDecoder
    {
        /// <summary>
        /// Decodes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="targetBitRate">The target bit rate.</param>
        /// <param name="fileExt">The file ext.</param>
        /// <returns></returns>
        public AudioInfo Decode(System.IO.Stream stream, float targetBitRate, string fileExt)
        {
            return null;
        }
    }
}
