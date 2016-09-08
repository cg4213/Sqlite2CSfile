using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Sqlite2Cs
{
    
    public class Sqlite2CsConfig
    {
        public string databaseFilePath;
        public string outputPath;
        public string databaseName;
        public string filterName;

        public string[] CSdataClassImpledInterface;
    }
}
