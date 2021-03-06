﻿using CSFileWriter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Xml2Cs
{
    public class Xml2CsConverter
    {
        //sqlite types
        const string SQLITE_TYPE_INT = "INTEGER";
        const string SQLITE_TYPE_REAL = "REAL";
        const string SQLITE_TYPE_TEXT = "TEXT";
        const string SQLITE_TYPE_BLOB = "BLOB";



        //read types
        const string CS_READ_INT = "ReadInt";
        const string CS_READ_FLOAT = "ReadFloat";
        const string CS_READ_STRING = "ReadUTF8";
        const string CS_READ_BYTEARRAY = "ReadBlob";
        const string CS_READ_LONG = "ReadLong";

        string HandleInteger(string[] sqliteTypeInfo)
        {
            if (sqliteTypeInfo.Length == 1)
                return CSFile.CS_TYPE_INT;

            int length = 0;
            if (int.TryParse(sqliteTypeInfo[1], out length))
            {
                if (length > 11)
                    return CSFile.CS_TYPE_LONG;
                else
                    return CSFile.CS_TYPE_INT;
            }
            else
                return CSFile.CS_TYPE_INT;
        }

        string HandleIntegerType(string[] sqliteTypeInfo)
        {
            if (sqliteTypeInfo.Length == 1)
                return CS_READ_INT;

            int length = 0;
            if (int.TryParse(sqliteTypeInfo[1], out length))
            {
                if (length > 11)
                    return CS_READ_LONG;
                else
                    return CS_READ_INT;
            }
            else
                return CS_READ_INT;
        }

        string SqliteType2CsType(string sqliteType)
        {
            string[] sqliteTypeInfo = sqliteType.Split(new string[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);


            switch (sqliteTypeInfo[0])
            {
                case SQLITE_TYPE_INT:
                    return HandleInteger(sqliteTypeInfo);
                case SQLITE_TYPE_REAL:
                    return CSFile.CS_TYPE_FLOAT;
                case SQLITE_TYPE_TEXT:
                    return CSFile.CS_TYPE_STRING;
                case SQLITE_TYPE_BLOB:
                    return CSFile.CS_TYPE_BYTEARRAY;
                default:
                    System.Diagnostics.Trace.WriteLine("Unhandled sqlite type [" + sqliteType + "]");
                    return string.Empty;
            }
        }

        string SqliteType2CsReadType(string sqliteType)
        {
            string[] sqliteTypeInfo = sqliteType.Split(new string[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);

            switch (sqliteTypeInfo[0])
            {
                case SQLITE_TYPE_INT:
                    return HandleIntegerType(sqliteTypeInfo);
                case SQLITE_TYPE_REAL:
                    return CS_READ_FLOAT;
                case SQLITE_TYPE_TEXT:
                    return CS_READ_STRING;
                case SQLITE_TYPE_BLOB:
                    return CS_READ_BYTEARRAY;
                default:
                    System.Diagnostics.Trace.WriteLine("Unhandled sqlite type [" + sqliteType + "]");
                    return string.Empty;
            }
        }

        //SQLiteConnection mSqliteConn;

        void CleanupOutput(string outputPath)
        {
            string[] outputFiles = Directory.GetFiles(outputPath);
            foreach (var file in outputFiles)
            {
                File.Delete(file);
            }
        }

        //Dictionary<string, string> GetTableInfo(string databaseName, string filterName)
        //{
        //    Dictionary<string, string> tableInfo = new Dictionary<string, string>();
        //    string sqlString = "select * from sqlite_master WHERE type = \"table\"";

        //    using (SQLiteCommand cmd = mSqliteConn.CreateCommand())
        //    {
        //        cmd.CommandText = sqlString;

        //        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        DataColumn nameCol = dt.Columns["name"];
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            string key = row[nameCol].ToString();
        //            if (filterName != "")
        //            {
        //                Regex regex = new Regex(@"^[" + filterName + "]");
        //                if (!regex.IsMatch(key))
        //                {
        //                    continue;
        //                }
        //            }
        //            tableInfo.Add(key, null);

        //            //System.Diagnostics.Trace.WriteLine("Unhandled sqlite type [" +  + "]");
        //        }
        //    }
        //    return tableInfo;
        //}

        //string GetString(SQLiteDataReader reader, string ordinal)
        //{
        //    return reader.GetString(reader.GetOrdinal(ordinal));
        //}

        ///// <summary>
        ///// Convsert specified xml file to cs file object
        ///// </summary>
        CSFile ConvertTable(string xmlFile, string[] impledInterface)
        {
            CSFile writer = new CSFile();
            writer.name = Path.GetFileName(xmlFile);
            //read xml file


            writer.AddReferencedLib("System");

            CSClass classObj = writer.AddCSClass("public", writer.name, impledInterface);
            using (XmlReader reader = XmlReader.Create(""))
            {
                XmlSchemaInference schema = new XmlSchemaInference();
                XmlSchemaSet schemaSet= schema.InferSchema(reader);

                //schemaSet.
            }

            //    string sqlString = string.Format("PRAGMA table_info([{0}]);", tableName);

            //    using (SQLiteCommand cmd = mSqliteConn.CreateCommand())
            //    {
            //        cmd.CommandText = sqlString;
            //        //using
            //        writer.AddReferencedLib("System");

            //        CSClass classObj = writer.AddCSClass("public", tableName, impledInterface);

            //        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            //        DataTable dt = new DataTable();
            //        da.Fill(dt);
            //        DataColumn nameCol = dt.Columns["name"];
            //        DataColumn typeCol = dt.Columns["type"];

            //        CSParseFunc parseFunc = new CSParseFunc("public", tableName, "ReadData");

            //        CSVar dataForm = parseFunc.AddParam(string.Empty, "ASteinGameDataHolder", "dataForm");
            //        classObj.AddFuncDeclare(parseFunc);

            //        foreach (DataRow row in dt.Rows)
            //        {
            //            CSField dataVar = classObj.AddVar("public", SqliteType2CsType(row[typeCol].ToString()), row[nameCol].ToString());

            //            CSFuncCall parseCall = new CSFuncCall(string.Empty, SqliteType2CsType(row[typeCol].ToString()), SqliteType2CsReadType(row[typeCol].ToString()));
            //            parseCall.AddParam("string", row[nameCol].ToString());
            //            parseCall.targetObj = dataForm;
            //            parseCall.returnVar = dataVar;

            //            parseFunc.AddParseCall(parseCall);
            //        }
            //    }

            //    //System.Diagnostics.Trace.WriteLine(writer.GenerateFilsString());

            return writer;
        }


        public void Covert(Xml2CsConfig config)
        {
            CleanupOutput(config.outputPath);
            //get all xml files
            string[] files =Directory.GetFiles(config.xmlFileDir,"*.xml",SearchOption.AllDirectories);
            foreach(string xmlFile in files)
            {
                CSFile csFile = ConvertTable(xmlFile, config.CSdataClassImpledInterface);

                string filePath = config.outputPath + "/" + csFile.name+ ".cs";
                if (File.Exists(filePath))
                {
                    Console.WriteLine("{0} already exists.", filePath);
                    return;
                }
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.Write(csFile.GenerateFilsString());
                    sw.Close();
                }
            }
      
            System.Windows.Forms.MessageBox.Show("写入完毕！！！");
        }

    }
}
