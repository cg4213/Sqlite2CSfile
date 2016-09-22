using System;
using System.Collections.Generic;
using System.Text;

namespace CSFileWriter
{
    /// <summary>
    /// the abstract base class for function/methods
    /// if you want to add a function in generated cs file follow these
    /// 1.extend this class
    /// 2.discribe the funtion in GenerateFuncContent 
    /// 3.add your CSFuncDeclare object to CSFile
    /// </summary>
    public abstract class CSFuncDeclare : CSObject
    {
        public List<CSVar> paramArray = new List<CSVar>();

        public CSFuncDeclare(string restrain, string type, string name)
        {
            this.restrain = restrain;
            this.type = type;
            this.name = name;
        }

        public CSVar AddParam(string restrain, string type, string name)
        {
            CSVar param = new CSVar(restrain, type, name, null);
            paramArray.Add(param);
            return param;
        }
        protected abstract void GenerateFuncContent(StringBuilder stringBuilder);

        public string GenerateFuncDefineString()
        {
            StringBuilder funcStringBuilder = new StringBuilder();

            //write func delc
            if (!string.IsNullOrEmpty(restrain))
            {
                funcStringBuilder.Append(restrain);
                funcStringBuilder.Append(" ");
            }
            funcStringBuilder.Append(type);
            funcStringBuilder.Append(" ");
            funcStringBuilder.Append(name);

            //func param list
            funcStringBuilder.Append("(");


            for (int i = 0; i < paramArray.Count; ++i)
            {
                CSVar param = paramArray[i];

                funcStringBuilder.Append(param.type);
                funcStringBuilder.Append(" ");
                funcStringBuilder.Append(param.name);
                if (i < paramArray.Count - 1)
                    funcStringBuilder.Append(",");
            }
            funcStringBuilder.Append(")");
            funcStringBuilder.AppendLine();

            //body
            funcStringBuilder.Append("\t");
            funcStringBuilder.Append("{");
            funcStringBuilder.AppendLine();

            GenerateFuncContent(funcStringBuilder);

            funcStringBuilder.AppendLine();
            funcStringBuilder.Append("\t");
            funcStringBuilder.Append("}");
            return funcStringBuilder.ToString();
        }
    }


}
