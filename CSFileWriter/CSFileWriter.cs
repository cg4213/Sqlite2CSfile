using System;
using System.Collections.Generic;
using System.Text;

namespace Sqlite2Cs
{
    /// <summary>
    /// CS对象的基类
    /// </summary>
    public abstract class CSObject
    {
        public string restrain;
        public string type;
        public string name;
        public string comment;

        /// <summary>
        /// 
        /// </summary>
        public string GenerateCommentString()
        {
            if (string.IsNullOrEmpty(comment))
                return string.Empty;

            StringBuilder commentBuilder = new StringBuilder();
            string[] commentLines = comment.Split(new string[] { "/r/n" }, StringSplitOptions.None);
            commentBuilder.AppendLine("/// <summary>");
            for (int i = 0; i < commentLines.Length; ++i)
            {
                commentBuilder.Append("/// ");
                commentBuilder.AppendLine(commentLines[i]);
            }
            commentBuilder.AppendLine("/// <summary>");
            return commentBuilder.ToString();
        }
    }

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
    /// <summary>
    /// 解析方法
    /// 还没完成
    /// </summary>
    public class CSParseFunc : CSFuncDeclare
    {
        public List<CSFuncCall> parseFuncList = new List<CSFuncCall>();

        public CSParseFunc(string restrain, string type, string name) : base(restrain, type, name)
        {
        }

        public void AddParseCall(CSFuncCall call)
        {
            parseFuncList.Add(call);
        }
        protected override void GenerateFuncContent(StringBuilder sb)
        {

            foreach (var parse in parseFuncList)
            {
                sb.Append("\t");
                sb.AppendLine("\t" + parse.GenerateFuncCallString());
            }
            sb.AppendLine();
            sb.Append("\t");

            sb.Append("\t");
            sb.Append("return this;");
        }

    }
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

    /// <summary>
    /// CS文件
    /// </summary>
    public class CSFileWriter
    {
        /// <summary>
        /// all the usings
        /// </summary>
        public List<string> referencedLibList = new List<string>();

        /// <summary>
        /// Class in file
        /// </summary>
        public List<CSClass> classDeclList = new List<CSClass>();

        public void AddReferencedLib(string libName)
        {
            referencedLibList.Add(libName);
        }

        public CSClass AddCSClass(string restrain, string name, string[] impledInterface)
        {
            CSClass newClass = new CSClass(restrain, name);
            if (impledInterface != null)
                newClass.impledInterface = new List<string>(impledInterface);
            classDeclList.Add(newClass);
            return newClass;
        }

        public string GenerateFilsString()
        {
            StringBuilder content = new StringBuilder();

            foreach (string lib in referencedLibList)
            {
                content.AppendFormat("using {0};", lib);
                content.AppendLine();
            }

            content.AppendLine();
            foreach (var classDecl in classDeclList)
            {
                classDecl.AppendContent(content);
            }

            return content.ToString();
        }
    }
}