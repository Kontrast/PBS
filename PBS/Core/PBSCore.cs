using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Common;

namespace Core
{
    /// <summary>
    /// Singleton class. Implements core functions of application.
    /// </summary>
    public class PBSCore
    {
        private static PBSCore instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static PBSCore Instance
        {
            get { return instance ?? (instance = new PBSCore()); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [collection empty].
        /// </summary>
        public bool IsCollectionEmpty { get; private set; }

        private PBSCore()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string dbPath = Path.Combine(appDataPath, ApplicationConstants.PBSDbFileName);
            IsCollectionEmpty = !File.Exists(dbPath);
        }

        /// <summary>
        /// Analyzes the music collection.
        /// </summary>
        /// <param name="path">The folder path.</param>
        public void AnalyzeCollection(string path)
        {
            Thread worker = new Thread(() => ScanPath(path))
            {
                IsBackground = true
            };
            worker.Start();
        }

        private void ScanPath(string path)
        {
            try
            {
                foreach (string filePath in DirectoryScanner.Scan(path, ApplicationConstants.SupportedAudioFileExtensions))
                {
                }
            }
            catch(Exception e)
            {
                //TODO process possible exceptions
            }
        }
    }
}
