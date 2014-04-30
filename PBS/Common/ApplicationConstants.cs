using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// Constant values, userd in application
    /// </summary>
    public static class ApplicationConstants
    {
        /// <summary>
        /// Gets the name of the PBS database file.
        /// </summary>
        public static string PBSDbFileName
        {
            get { return "PBS.db"; }
        }

        /// <summary>
        /// Gets the supported file extensions.
        /// </summary>
        public static string[] SupportedAudioFileExtensions
        {
            get { return new[] { ".mp3", ".wav" }; }
        }

        /// <summary>
        /// Gets the process interval.
        /// </summary>
        public static double ProcessInterval
        {
            get { return 1000; }
        }
    }
}
