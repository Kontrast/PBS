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
        public IDataProvider DataProvider { get; set; }

        public ICollection<AudioRecord> Records { get; private set; }

        public bool IsChanged { get; set; }

        public DataBase(string fileName)
        {
            Records = new Collection<AudioRecord>();
            Load(fileName);
        }

        public  void Save(string fullPath)
        {
            
        }

        public void Load(string fileName)
        {
            
        }
    }
}
