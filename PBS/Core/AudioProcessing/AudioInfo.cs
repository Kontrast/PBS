using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Core.AudioProcessing
{
    public class AudioInfo
    {
        /// <summary>
        /// Gets or sets the samples.
        /// </summary>
        public Samples Samples { get; set; }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        public Dictionary<string, object> Tags { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioInfo"/> class.
        /// </summary>
        public AudioInfo()
        {
            Tags = new Dictionary<string, object>();
        }
    }
}
