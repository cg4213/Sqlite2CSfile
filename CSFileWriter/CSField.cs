using System;
using System.Collections.Generic;
using System.Text;

namespace CSFileWriter
{
    public class CSField : CSVar
    {

        public CSAttribute attribute;
        public CSField(string type, string name, string comment) : base(type, name, comment)
        {
        }

        public CSField(string restrain, string type, string name, string comment) : base(restrain, type, name, comment)
        {
        }

        public string GenerateAttributeString()
        {
            if (attribute == null)
                return string.Empty;
            else
                return attribute.GenerateAttributeString();
        }
    }
}
