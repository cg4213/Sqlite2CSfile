using System;
using System.Collections.Generic;
using System.Text;

namespace CSFileWriter
{
    public class CSAttribute
    {
        public string attrName;

        List<CSVar> attParamList = new List<CSVar>();

        public void AddParam(string type, string name, object value)
        {
            CSVar param = new CSVar(type, name, null);
            param.SetValue(value);
            attParamList.Add(param);
        }

        public string GenerateAttributeString()
        {
            if (attParamList == null || attParamList.Count == 0)
                return string.Format("[" + attrName + "]");
            else
            {

                List<string> paramStringList = new List<string>();
                for (int i = 0; i < attParamList.Count; ++i)
                {

                    paramStringList.Add(attParamList[i].GenerateValueString());
                }
                string paramListString = string.Join(",", paramStringList.ToArray());
                return string.Format("[" + attrName + "(" + paramListString + ")]");
            }
        }
    }
}
