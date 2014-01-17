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
        /// Saves the specified full path.
        /// </summary>
        public void Save()
        {
            if (IsChanged)
            {
                DataProvider.Save(Records);
            }
        }

        /// <summary>
        /// Loads records.
        /// </summary>
        public void Load()
        {
            Records = DataProvider.Load();
        }
    }
}
