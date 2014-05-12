using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;

namespace BassDecoder
{
    public class AudioDecoder :IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioDecoder" /> class.
        /// </summary>
        /// <exception cref="System.Exception">Bass_Init error!</exception>
        public AudioDecoder()
        {
            BassNet.Registration("pavel_torgashov@mail.ru", "2X25317232238");

            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_LATENCY, IntPtr.Zero))
            {
                BASS_INFO info = new BASS_INFO();
                Bass.BASS_GetInfo(info);
            }
            else
                throw new Exception("Bass_Init error!");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Bass.BASS_Free();
        }
    }
}
