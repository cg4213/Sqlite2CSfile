using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Sqlite2Cs
{

    public partial class Sqlite2Cs : Form
    {

        Sqlite2CsConfig config;
        Sqlite2CsConverter converter = new Sqlite2CsConverter();

        public Sqlite2Cs()
        {
            InitializeComponent();
            InitWithConfig();
        }

        private void InitWithConfig()
        {
            string configPath = Directory.GetCurrentDirectory() + "/config.xml";

            XmlSerializer configSerializer = new XmlSerializer(typeof(Sqlite2CsConfig));
            XmlReaderSettings setting = new XmlReaderSettings();

            if (!File.Exists(configPath))
            {
                FileStream fs = File.Create(configPath);
                fs.Close();
                fs.Dispose();

            }

            using (XmlReader fs = XmlReader.Create(configPath))
            {
                try
                {
                    config = configSerializer.Deserialize(fs) as Sqlite2CsConfig;
                }
                catch
                {
                    //whatever happend ,create a new config
                    config = new Sqlite2CsConfig();
                }

            }

            if (!string.IsNullOrEmpty(config.databaseFilePath))
            {
                databasePath.Text = config.databaseFilePath;
            }

            if (!string.IsNullOrEmpty(config.outputPath))
            {
                outputPath.Text = config.outputPath;
            }

            if (!string.IsNullOrEmpty(config.databaseName))
            {
                dbNameBox.Text = config.databaseName;
            }

            if(!string.IsNullOrEmpty (config.filterName))
            {
                filterNameBox.Text = config.filterName;
            }

            if (config.CSdataClassImpledInterface != null)
            {
                for (int i = 0; i < config.CSdataClassImpledInterface.Length; ++i)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[0].Value = config.CSdataClassImpledInterface[i];
                    dataGridView1.Rows.Add(row);
                }
            }
        }

        private void SaveConfig()
        {
            string configPath = Directory.GetCurrentDirectory() + "/config.xml";
            XmlSerializer configSerializer = new XmlSerializer(typeof(Sqlite2CsConfig));
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.OmitXmlDeclaration = true;
            setting.Indent = true;
            setting.Encoding = System.Text.Encoding.UTF8;
            using (XmlWriter writer = XmlWriter.Create(configPath, setting))
            {
                configSerializer.Serialize(writer, config);
            }
        }

        private void ChangeDatabasePath(string newPath)
        {
            System.Diagnostics.Trace.WriteLine("ChangeDatabasePath[" + newPath + "]");
            if (config.databaseFilePath != newPath)
            {
                config.databaseFilePath = newPath;
                SaveConfig();
            }
        }

        private void ChangeOutputPath(string newPath)
        {
            System.Diagnostics.Trace.WriteLine("ChangeOutputPath[" + newPath + "]");
            if (config.outputPath != newPath)
            {
                config.outputPath = newPath;
                SaveConfig();
            }
        }

        private void ChangeDatabaseName(string newName)
        {
            System.Diagnostics.Trace.WriteLine("ChangeOutputPath[" + newName + "]");
            if (config.databaseName != newName)
            {
                config.databaseName = newName;
                SaveConfig();
            }
        }

        private void ChangeFilterName(string newName)
        {
            System.Diagnostics.Trace.WriteLine("ChangeFilterName["+newName+"]");
            if(config.filterName != newName)
            {
                config.filterName = newName;
                SaveConfig();
            }
        }

        private void databasePath_TextChanged(object sender, EventArgs e)
        {
            ChangeDatabasePath(databasePath.Text);
        }

        private void browseDatabasePath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                DialogResult result = dialog.ShowDialog();
                switch (result)
                {
                    case DialogResult.OK:
                        databasePath.Text = dialog.FileName; 
                        ChangeDatabasePath(dialog.FileName);
                        break;
                    case DialogResult.Cancel:
                        break;
                    default:
                        //TODO open a alert dialog
                        break;
                }
            }

        }

        private void ouputPath_TextChanged(object sender, EventArgs e)
        {
            ChangeOutputPath(outputPath.Text);
        }

        private void browseOutputPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                switch (result)
                {
                    case DialogResult.OK:
                        outputPath.Text = dialog.SelectedPath;
                        ChangeOutputPath(dialog.SelectedPath);
                        break;
                    case DialogResult.Cancel:
                        break;
                    default:
                        //TODO open a alert dialog
                        break;
                }
            }
        }

        private void generateCsFiles_Click(object sender, EventArgs e)
        {
            try
            {
                converter.Covert(config);
            }
            catch (System.Exception exception)
            {
                if(exception is DirectoryNotFoundException)
                {
                    MessageBox.Show("Unknown Path:" + exception.Message);
                }
                else
                MessageBox.Show("Unknown Exception" + exception.ToString());
            }
        }
        private void dbNameBox_TextChanged(object sender, EventArgs e)
        {
            ChangeDatabaseName(dbNameBox.Text);
        }

        private void filterNameBox_TextChanged(object sender, EventArgs e)
        {
            ChangeFilterName(filterNameBox.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            List<string> interfaceNames = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                    interfaceNames.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
            }
            config.CSdataClassImpledInterface = interfaceNames.ToArray();

            SaveConfig();
        }
    }
}