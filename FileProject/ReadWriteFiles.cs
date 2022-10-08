using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
namespace FileProject
{
    class ReadWriteFiles
    { 
        public static void Read(DataSet tables)
        {
            tables.ReadXml("dataset.xml");
        }
        public static void write(DataSet tables)
        {
            tables.WriteXml("dataset.xml");
        }
    }
}
