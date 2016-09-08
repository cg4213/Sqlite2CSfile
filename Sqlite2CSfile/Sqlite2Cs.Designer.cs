namespace Sqlite2Cs
{
    partial class Sqlite2Cs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.databasePath = new System.Windows.Forms.TextBox();
            this.browseDatabasePath = new System.Windows.Forms.Button();
            this.generateCsFiles = new System.Windows.Forms.Button();
            this.outputPath = new System.Windows.Forms.TextBox();
            this.browseOutputPath = new System.Windows.Forms.Button();
            this.dbNameBox = new System.Windows.Forms.TextBox();
            this.filterNameBox = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Interface = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "导出的数据库";
            //
            //label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "选择相应的导出项";
            // 
            // databasePath
            // 
            this.databasePath.Location = new System.Drawing.Point(27, 31);
            this.databasePath.Name = "databasePath";
            this.databasePath.Size = new System.Drawing.Size(393, 21);
            this.databasePath.TabIndex = 2;
            this.databasePath.TextChanged += new System.EventHandler(this.databasePath_TextChanged);
            // 
            // browseDatabasePath
            // 
            this.browseDatabasePath.Location = new System.Drawing.Point(446, 31);
            this.browseDatabasePath.Name = "browseDatabasePath";
            this.browseDatabasePath.Size = new System.Drawing.Size(146, 23);
            this.browseDatabasePath.TabIndex = 3;
            this.browseDatabasePath.Text = "选择数据库文件";
            this.browseDatabasePath.UseVisualStyleBackColor = true;
            this.browseDatabasePath.Click += new System.EventHandler(this.browseDatabasePath_Click);
            // 
            // generateCsFiles
            // 
            this.generateCsFiles.Location = new System.Drawing.Point(535, 225);
            this.generateCsFiles.Name = "generateCsFiles";
            this.generateCsFiles.Size = new System.Drawing.Size(75, 23);
            this.generateCsFiles.TabIndex = 4;
            this.generateCsFiles.Text = "生成CS文件";
            this.generateCsFiles.UseVisualStyleBackColor = true;
            this.generateCsFiles.Click += new System.EventHandler(this.generateCsFiles_Click);
            // 
            // outputPath
            // 
            this.outputPath.Location = new System.Drawing.Point(27, 76);
            this.outputPath.Name = "outputPath";
            this.outputPath.Size = new System.Drawing.Size(393, 21);
            this.outputPath.TabIndex = 5;
            this.outputPath.TextChanged += new System.EventHandler(this.ouputPath_TextChanged);
            // 
            // browseOutputPath
            // 
            this.browseOutputPath.Location = new System.Drawing.Point(446, 76);
            this.browseOutputPath.Name = "browseOutputPath";
            this.browseOutputPath.Size = new System.Drawing.Size(146, 23);
            this.browseOutputPath.TabIndex = 6;
            this.browseOutputPath.Text = "选择CS文件输出路径";
            this.browseOutputPath.UseVisualStyleBackColor = true;
            this.browseOutputPath.Click += new System.EventHandler(this.browseOutputPath_Click);
            // 
            // dbNameBox
            // 
            this.dbNameBox.Location = new System.Drawing.Point(150, 137);
            this.dbNameBox.Name = "dbNameBox";
            this.dbNameBox.Size = new System.Drawing.Size(100, 21);
            this.dbNameBox.TabIndex = 7;
            //this.dbNameBox.Text = "main";
            this.dbNameBox.TextChanged += new System.EventHandler(this.dbNameBox_TextChanged);
            //
            //filterNameBox
            //
            this.filterNameBox.Location = new System.Drawing.Point(150,167);
            this.filterNameBox.Name = "filterNameBox";
            this.filterNameBox.Size = new System.Drawing.Size(100,21);
            this.filterNameBox.TabIndex = 8;
            //this.filterNameBox.Text = "t";
            this.filterNameBox.TextChanged += new System.EventHandler(this.filterNameBox_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Interface});
            this.dataGridView1.Location = new System.Drawing.Point(41, 197);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // Interface
            // 
            this.Interface.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Interface.HeaderText = "Interface";
            this.Interface.Name = "Interface";
            this.Interface.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Sqlite2Cs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 370);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dbNameBox);
            this.Controls.Add(this.filterNameBox);
            this.Controls.Add(this.browseOutputPath);
            this.Controls.Add(this.outputPath);
            this.Controls.Add(this.generateCsFiles);
            this.Controls.Add(this.browseDatabasePath);
            this.Controls.Add(this.databasePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "Sqlite2Cs";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox databasePath;
        private System.Windows.Forms.Button browseDatabasePath;
        private System.Windows.Forms.Button generateCsFiles;
        private System.Windows.Forms.TextBox outputPath;
        private System.Windows.Forms.Button browseOutputPath;
        private System.Windows.Forms.TextBox dbNameBox;
        private System.Windows.Forms.TextBox filterNameBox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interface;
    }
}

