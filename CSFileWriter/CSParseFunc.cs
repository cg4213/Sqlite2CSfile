using System;
using System.Collections.Generic;
using System.Text;

namespace CSFileWriter
{


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

}
