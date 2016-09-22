using System;
using System.Collections.Generic;
using System.Text;

namespace CSFileWriter
{

    public class CSFuncCall : CSObject
    {
        /// <summary>
        /// 调用的返回值赋值对象
        /// </summary>
        public CSVar returnVar;
        /// <summary>
        /// 方法所属的对象
        /// </summary>
        public CSVar targetObj;
        /// <summary>
        /// 参数表
        /// </summary>
        public List<CSVar> paramArray = new List<CSVar>();

        public CSFuncCall(string restrain, string type, string name)
        {
            this.restrain = restrain;
            this.type = type;
            this.name = name;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public CSVar AddParam(string type, string value)
        {
            CSVar param = new CSVar(type, "name", null);
            param.SetValue(value);
            paramArray.Add(param);
            return param;
        }

        public string GenerateFuncCallString()
        {
            string paramString = string.Empty;
            List<string> paramStringList = new List<string>();

            foreach (var param in this.paramArray)
            {
                paramStringList.Add(param.GenerateValueString());
            }
            paramString = string.Join(",", paramStringList.ToArray());

            if (targetObj != null)
            {
                if (returnVar != null)
                    return returnVar.name + " " + "=" + " " + targetObj.name + "." + name + "(" + paramString + ")" + ";";
                else
                    return targetObj.name + "." + name + "(" + paramString + ")" + ";";
            }
            else
            {
                if (returnVar != null)
                    return returnVar.name + " " + "=" + " " + name + "(" + paramString + ")" + ";";
                else
                    return name + "(" + paramString + ")" + ";";
            }
        }
    }

}
