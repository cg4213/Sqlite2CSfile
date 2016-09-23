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


        //cs types
        public const string CS_TYPE_INT = "int";
        public const string CS_TYPE_SHORT = "short";
        public const string CS_TYPE_LONG = "long";
        public const string CS_TYPE_STRING = "string";
        public const string CS_TYPE_FLOAT = "float";
        public const string CS_TYPE_DOUBLE = "double";
        public const string CS_TYPE_DATE = "Date";
        public const string CS_TYPE_BYTEARRAY = "byte[]";
        public const string CS_TYPE_STRARRAY = "string[]";

        /// <summary>
        /// file name
        /// </summary>
        public string name;

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