using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace Core
{
    public static class DirectoryScanner
    {
        /// <summary>
        /// Scans the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileExtensions">The file extensions.</param>
        /// <returns></returns>
        public static IEnumerable<string> Scan(string path, string[] fileExtensions)
        {
            IEnumerable<string> files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).
                                                  Where(p => fileExtensions.Any(p.EndsWith));
            return files;
        }
    }
}
