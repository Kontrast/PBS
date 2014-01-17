using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Common;
using DataAccessLayer;
using Models;

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

        /// <summary>
        /// Gets or sets the data base.
        /// </summary>
        public static DataBase DataBase { get; set; }

        private PBSCore()
        {
            string dbPath = FileLocationProvider.DefaultDBPath;
            IsCollectionEmpty = !File.Exists(dbPath);
            DataBase = new DataBase()
            {
                DataProvider = new SqliteDataProvider() { Path = dbPath }
            };
            DataBase.Load();
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

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            DataBase.Save();
        }

        private void ScanPath(string path)
        {
            try
            {
                IEnumerable<string> audioIndexes = DataBase.Records.Select(r => r.FullPath);
                foreach (string filePath in DirectoryScanner.Scan(path, ApplicationConstants.SupportedAudioFileExtensions))
                {
                    if (!audioIndexes.Contains(filePath))
                    {
                        AudioRecord item = new AudioRecord()
                        {
                            FullPath = filePath
                        };

                        lock (DataBase.Records)
                        {
                            DataBase.Records.Add(item);
                        }
                        DataBase.IsChanged = true;
                    }
                }
            }
            catch (Exception e)
            {
                //TODO process possible exceptions
            }
        }
    }
}
