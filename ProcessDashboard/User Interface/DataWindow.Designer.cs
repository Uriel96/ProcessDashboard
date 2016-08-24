namespace ProcessDashboard {
    partial class DataWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.DataDB = new System.Windows.Forms.DataGridView();
            this.BtnQuery = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CboStudent = new System.Windows.Forms.ComboBox();
            this.CboTable = new System.Windows.Forms.ComboBox();
            this.CboToProgram = new System.Windows.Forms.ComboBox();
            this.CbPreviousVersion = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataDB)).BeginInit();
            this.SuspendLayout();
            // 
            // DataDB
            // 
            this.DataDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataDB.Location = new System.Drawing.Point(12, 46);
            this.DataDB.Name = "DataDB";
            this.DataDB.Size = new System.Drawing.Size(833, 337);
            this.DataDB.TabIndex = 0;
            // 
            // BtnQuery
            // 
            this.BtnQuery.Location = new System.Drawing.Point(723, 10);
            this.BtnQuery.Name = "BtnQuery";
            this.BtnQuery.Size = new System.Drawing.Size(75, 23);
            this.BtnQuery.TabIndex = 1;
            this.BtnQuery.Text = "Query";
            this.BtnQuery.UseVisualStyleBackColor = true;
            this.BtnQuery.Click += new System.EventHandler(this.BtnQueryClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Alumno:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(360, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Hasta Programa:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tabla:";
            // 
            // CboStudent
            // 
            this.CboStudent.FormattingEnabled = true;
            this.CboStudent.Location = new System.Drawing.Point(63, 11);
            this.CboStudent.Name = "CboStudent";
            this.CboStudent.Size = new System.Drawing.Size(121, 21);
            this.CboStudent.TabIndex = 8;
            // 
            // CboTable
            // 
            this.CboTable.FormattingEnabled = true;
            this.CboTable.Location = new System.Drawing.Point(233, 11);
            this.CboTable.Name = "CboTable";
            this.CboTable.Size = new System.Drawing.Size(121, 21);
            this.CboTable.TabIndex = 9;
            // 
            // CboToProgram
            // 
            this.CboToProgram.FormattingEnabled = true;
            this.CboToProgram.Location = new System.Drawing.Point(452, 11);
            this.CboToProgram.Name = "CboToProgram";
            this.CboToProgram.Size = new System.Drawing.Size(121, 21);
            this.CboToProgram.TabIndex = 10;
            // 
            // CbPreviousVersion
            // 
            this.CbPreviousVersion.AutoSize = true;
            this.CbPreviousVersion.Location = new System.Drawing.Point(579, 13);
            this.CbPreviousVersion.Name = "CbPreviousVersion";
            this.CbPreviousVersion.Size = new System.Drawing.Size(138, 17);
            this.CbPreviousVersion.TabIndex = 11;
            this.CbPreviousVersion.Text = "Mostrar Versión Anterior";
            this.CbPreviousVersion.UseVisualStyleBackColor = true;
            // 
            // DataWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 418);
            this.Controls.Add(this.CbPreviousVersion);
            this.Controls.Add(this.CboToProgram);
            this.Controls.Add(this.CboTable);
            this.Controls.Add(this.CboStudent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnQuery);
            this.Controls.Add(this.DataDB);
            this.Name = "DataWindow";
            this.Text = "DataWindow";
            this.Load += new System.EventHandler(this.DataWindowLoad);
            ((System.ComponentModel.ISupportInitialize)(this.DataDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataDB;
        private System.Windows.Forms.Button BtnQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CboStudent;
        private System.Windows.Forms.ComboBox CboTable;
        private System.Windows.Forms.ComboBox CboToProgram;
        private System.Windows.Forms.CheckBox CbPreviousVersion;
    }
}