using System;
using System.Collections.Generic;
using System.Text;

namespace CSFileWriter
{
    /// <summary>
    /// 变量
    /// </summary>
    public class CSVar : CSObject
    {
        object mValue;


        public CSVar(string type, string name, string comment)
        {
            this.type = type;
            this.name = name;
            this.comment = comment;
        }

        public CSVar(string restrain, string type, string name, string comment)
        {
            this.restrain = restrain;
            this.type = type;
            this.name = name;
            this.comment = comment;
        }
        public void SetValue(object value)
        {
            //if (value.GetType().ToString() != this.type)
            //{
            //    throw new SystemException("CSVar SetValue invalid type [" + value.GetType() + "][" + this.type + "]");

            //}
            mValue = value;
        }
        public string GeneratVarString()
        {
            if (string.IsNullOrEmpty(restrain))
            {
                return string.Format("{1} {2};", type, name);
            }
            else
            {
                return string.Format("{0} {1} {2};", restrain, type, name);
            }
        }

        public string GeneratParamString()
        {
            if (string.IsNullOrEmpty(restrain))
            {
                return type + " " + name;
            }
            else
            {
                return restrain + " " + type + " " + name;
            }
        }

        public string GenerateValueString()
        {
            switch (type)
            {
                case "string":
                    return "\"" + mValue.ToString() + "\"";
                case "int":
                    return mValue.ToString();
                default:
                    return mValue.ToString();
                    //return "Unhandled type [" + type + "]";
            }
        }
    }
}
