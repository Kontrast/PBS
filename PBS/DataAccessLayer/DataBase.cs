using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Models;

namespace DataAccessLayer
{
    public class DataBase
    {
        /// <summary>
        /// Gets or sets the data provider.
        /// </summary>
        public IDataProvider DataProvider { private get; set; }

        /// <summary>
        /// Gets the records.
        /// </summary>
        public ICollection<AudioRecord> Records { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether database [is changed].
        /// </summary>
        public bool IsChanged { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBase"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public DataBase(string fileName)
        {
            Records = new Collection<AudioRecord>();
            Load(fileName);
        }

        public void Save(string fullPath)
        {
            DataProvider.Save(Records);
        }

        public void Load(string fileName)
        {
        }
    }
}
