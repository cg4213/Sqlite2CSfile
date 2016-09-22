using System;
using System.Collections.Generic;
using System.Text;

namespace CSFileWriter
{
    /// <summary>
    /// CS文件
    /// </summary>
    public class CSFile
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