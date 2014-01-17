using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class FileLocationProvider
    {
        /// <summary>
        /// Gets the default database path.
        /// </summary>
        public static string DefaultDBPath
        {
            get
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dbPath = Path.Combine(appDataPath, ApplicationConstants.PBSDbFileName);
                return dbPath;
            }
        } 
    }
}
