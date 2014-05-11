using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Models;

namespace DataAccessLayer
{
    public class SqliteDataProvider : IDataProvider
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Loads data from db
        /// </summary>
        public ICollection<AudioRecord> Load()
        {
            Collection<AudioRecord> records = new Collection<AudioRecord>();
            if (File.Exists(Path))
                using (var fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
                using (var zip = new GZipStream(fs, CompressionMode.Decompress, false))
                using (var br = new BinaryReader(zip, Encoding.UTF8))
                    try
                    {
                        var count = br.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            var item = new AudioRecord();
                            item.Load(br);
                            records.Add(item);
                        }
                    }
                    catch (EndOfStreamException) {/*end of stream*/}

            return records;
        }

        /// <summary>
        /// Stores the specified records.
        /// </summary>
        /// <param name="records"></param>
        public void Save(ICollection<AudioRecord> records)
        {
            var tempPath = Path + ".temp";

            using (var fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            using (var zip = new GZipStream(fs, CompressionMode.Compress, false))
            using (var buff = new BufferedStream(zip, 8192))
            using (var bw = new BinaryWriter(buff, Encoding.UTF8))
            {
                bw.Write(records.Count);
                foreach (AudioRecord record in records)
                {
                    record.Store(bw);
                }
            }

            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
            File.Move(tempPath, Path);
        }
    }
}
