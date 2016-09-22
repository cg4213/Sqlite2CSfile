using System;
using System.Collections.Generic;
using System.Text;

namespace CSFileWriter
{


    /// <summary>
    /// CS类
    /// </summary>
    public class CSClass : CSObject
    {
        public string parentClass;
        public List<string> impledInterface = new List<string>();

        public List<CSClass> nestedClassList = new List<CSClass>();
        public List<CSField> varList = new List<CSField>();
        public List<CSFuncDeclare> funcDeclareList = new List<CSFuncDeclare>();

        public CSClass(string restrain, string name)
        {
            this.restrain = restrain;
            this.type = "class";
            this.name = name;
        }

        public CSField AddVar(string restrain, string type, string name, string comment = null)
        {
            CSField var = new CSField(restrain, type, name, comment);
            varList.Add(var);
            return var;
        }

        public CSClass AddNestedClass(string restrain, string name)
        {
            CSClass newClass = new CSClass(restrain, name);
            nestedClassList.Add(newClass);
            return newClass;
        }

        public void AddFuncDeclare(CSFuncDeclare declare)
        {
            funcDeclareList.Add(declare);
        }

        //public CSParseFunc AddParseFunc(string restrain, string type, string name)
        //{
        //    CSParseFunc newFunc = new CSParseFunc(restrain, type, name);
        //    parseFuncList.Add(newFunc);
        //    return newFunc;
        //}

        public void AppendContent(StringBuilder sb)
        {
            //declare
            sb.AppendLine(GenerateCommentString());

            if (string.IsNullOrEmpty(restrain))
            {
                sb.Append(type + " " + name);
            }
            else
            {
                sb.Append(restrain + " " + type + " " + name);
            }


            List<string> parents = new List<string>();
            if (!string.IsNullOrEmpty(parentClass))
                parents.Add(parentClass);

            parents.AddRange(impledInterface);
            if (parents.Count > 0)
            {
                sb.Append(":");

                for (int i = 0; i < parents.Count; ++i)
                {
                    sb.Append(parents[i]);
                    if (i < parents.Count - 1)
                        sb.Append(",");
                }
            }
            sb.AppendLine();
            sb.AppendLine("{");

            //var list
            foreach (CSField var in varList)
            {

                string comment = var.GenerateCommentString();
                if (!string.IsNullOrEmpty(comment))
                {

                }
                string attribute = var.GenerateAttributeString();
                if (!string.IsNullOrEmpty(attribute))
                {
                    sb.Append("\t");
                    sb.AppendLine(attribute);
                }
                sb.Append("\t");
                sb.AppendLine(var.GeneratVarString());
            }
            sb.AppendLine();

            // funcions
            foreach (CSFuncDeclare parse in funcDeclareList)
            {
                sb.AppendLine("\t" + parse.GenerateFuncDefineString());
            }

            sb.AppendLine("}");
        }
    }

}
