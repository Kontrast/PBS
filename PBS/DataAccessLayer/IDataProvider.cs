using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace DataAccessLayer
{
    public interface IDataProvider
    {
        /// <summary>
        /// Loads data from db
        /// </summary>
        ICollection<AudioRecord> Load();

        /// <summary>
        /// Stores the specified records.
        /// </summary>
        void Save(ICollection<AudioRecord> records);
    }
}
