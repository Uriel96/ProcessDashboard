namespace ProcessDashboard
{
    partial class ProcessDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessDashboard));
            this.LblDashboardDirectory = new System.Windows.Forms.Label();
            this.LblExcelFile = new System.Windows.Forms.Label();
            this.LblPrefix = new System.Windows.Forms.Label();
            this.TxtDashboardDirectory = new System.Windows.Forms.TextBox();
            this.TxtExcelFile = new System.Windows.Forms.TextBox();
            this.TxtPrefix = new System.Windows.Forms.TextBox();
            this.BtnDashboardDirectory = new System.Windows.Forms.Button();
            this.BtnExcelFile = new System.Windows.Forms.Button();
            this.BtnExecute = new System.Windows.Forms.Button();
            this.PbProcessGeneration = new System.Windows.Forms.ProgressBar();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.CbGenerateExcels = new System.Windows.Forms.CheckBox();
            this.CbSaveInDB = new System.Windows.Forms.CheckBox();
            this.CbDeleteExcels = new System.Windows.Forms.CheckBox();
            this.CbGenerateOne = new System.Windows.Forms.CheckBox();
            this.LblInformation = new System.Windows.Forms.Label();
            this.CbAcumulatedExcel = new System.Windows.Forms.CheckBox();
            this.RbToProgram = new System.Windows.Forms.RadioButton();
            this.RbAutomaticProgram = new System.Windows.Forms.RadioButton();
            this.PaToProgram = new System.Windows.Forms.Panel();
            this.LblToProgram = new System.Windows.Forms.Label();
            this.CboToProgram = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.PaToProgram.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblDashboardDirectory
            // 
            this.LblDashboardDirectory.AutoSize = true;
            this.LblDashboardDirectory.Location = new System.Drawing.Point(6, 17);
            this.LblDashboardDirectory.Name = "LblDashboardDirectory";
            this.LblDashboardDirectory.Size = new System.Drawing.Size(110, 13);
            this.LblDashboardDirectory.TabIndex = 0;
            this.LblDashboardDirectory.Text = "Directorio Dashboard:";
            // 
            // LblExcelFile
            // 
            this.LblExcelFile.AutoSize = true;
            this.LblExcelFile.Location = new System.Drawing.Point(26, 46);
            this.LblExcelFile.Name = "LblExcelFile";
            this.LblExcelFile.Size = new System.Drawing.Size(90, 13);
            this.LblExcelFile.TabIndex = 3;
            this.LblExcelFile.Text = "Archivo de Excel:";
            // 
            // LblPrefix
            // 
            this.LblPrefix.AutoSize = true;
            this.LblPrefix.Location = new System.Drawing.Point(77, 75);
            this.LblPrefix.Name = "LblPrefix";
            this.LblPrefix.Size = new System.Drawing.Size(39, 13);
            this.LblPrefix.TabIndex = 4;
            this.LblPrefix.Text = "Prefijo:";
            // 
            // TxtDashboardDirectory
            // 
            this.TxtDashboardDirectory.Location = new System.Drawing.Point(122, 14);
            this.TxtDashboardDirectory.Name = "TxtDashboardDirectory";
            this.TxtDashboardDirectory.ReadOnly = true;
            this.TxtDashboardDirectory.Size = new System.Drawing.Size(235, 20);
            this.TxtDashboardDirectory.TabIndex = 5;
            // 
            // TxtExcelFile
            // 
            this.TxtExcelFile.Location = new System.Drawing.Point(122, 43);
            this.TxtExcelFile.Name = "TxtExcelFile";
            this.TxtExcelFile.ReadOnly = true;
            this.TxtExcelFile.Size = new System.Drawing.Size(235, 20);
            this.TxtExcelFile.TabIndex = 6;
            // 
            // TxtPrefix
            // 
            this.TxtPrefix.Location = new System.Drawing.Point(122, 72);
            this.TxtPrefix.Name = "TxtPrefix";
            this.TxtPrefix.Size = new System.Drawing.Size(106, 20);
            this.TxtPrefix.TabIndex = 7;
            // 
            // BtnDashboardDirectory
            // 
            this.BtnDashboardDirectory.Location = new System.Drawing.Point(363, 12);
            this.BtnDashboardDirectory.Name = "BtnDashboardDirectory";
            this.BtnDashboardDirectory.Size = new System.Drawing.Size(75, 23);
            this.BtnDashboardDirectory.TabIndex = 8;
            this.BtnDashboardDirectory.Text = "Examinar";
            this.BtnDashboardDirectory.UseVisualStyleBackColor = true;
            this.BtnDashboardDirectory.Click += new System.EventHandler(this.BtnDashboardDirectoryClick);
            // 
            // BtnExcelFile
            // 
            this.BtnExcelFile.Location = new System.Drawing.Point(363, 41);
            this.BtnExcelFile.Name = "BtnExcelFile";
            this.BtnExcelFile.Size = new System.Drawing.Size(75, 23);
            this.BtnExcelFile.TabIndex = 9;
            this.BtnExcelFile.Text = "Examinar";
            this.BtnExcelFile.UseVisualStyleBackColor = true;
            this.BtnExcelFile.Click += new System.EventHandler(this.BtnExcelFileClick);
            // 
            // BtnExecute
            // 
            this.BtnExecute.Location = new System.Drawing.Point(282, 235);
            this.BtnExecute.Name = "BtnExecute";
            this.BtnExecute.Size = new System.Drawing.Size(75, 23);
            this.BtnExecute.TabIndex = 10;
            this.BtnExecute.Text = "Ejecutar";
            this.BtnExecute.UseVisualStyleBackColor = true;
            this.BtnExecute.Click += new System.EventHandler(this.BtnExecuteClick);
            // 
            // PbProcessGeneration
            // 
            this.PbProcessGeneration.Location = new System.Drawing.Point(12, 184);
            this.PbProcessGeneration.Name = "PbProcessGeneration";
            this.PbProcessGeneration.Size = new System.Drawing.Size(426, 23);
            this.PbProcessGeneration.TabIndex = 11;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(363, 235);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 12;
            this.BtnCancel.Text = "Cancelar";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // CbGenerateExcels
            // 
            this.CbGenerateExcels.AutoSize = true;
            this.CbGenerateExcels.Location = new System.Drawing.Point(12, 104);
            this.CbGenerateExcels.Name = "CbGenerateExcels";
            this.CbGenerateExcels.Size = new System.Drawing.Size(104, 17);
            this.CbGenerateExcels.TabIndex = 13;
            this.CbGenerateExcels.Text = "Generar Exceles";
            this.CbGenerateExcels.UseVisualStyleBackColor = true;
            // 
            // CbSaveInDB
            // 
            this.CbSaveInDB.AutoSize = true;
            this.CbSaveInDB.Location = new System.Drawing.Point(128, 104);
            this.CbSaveInDB.Name = "CbSaveInDB";
            this.CbSaveInDB.Size = new System.Drawing.Size(163, 17);
            this.CbSaveInDB.TabIndex = 14;
            this.CbSaveInDB.Text = "Guardar en la Base de Datos";
            this.CbSaveInDB.UseVisualStyleBackColor = true;
            // 
            // CbDeleteExcels
            // 
            this.CbDeleteExcels.AutoSize = true;
            this.CbDeleteExcels.Location = new System.Drawing.Point(302, 104);
            this.CbDeleteExcels.Name = "CbDeleteExcels";
            this.CbDeleteExcels.Size = new System.Drawing.Size(138, 17);
            this.CbDeleteExcels.TabIndex = 15;
            this.CbDeleteExcels.Text = "Borrar Exceles Antiguos";
            this.CbDeleteExcels.UseVisualStyleBackColor = true;
            // 
            // CbGenerateOne
            // 
            this.CbGenerateOne.AutoSize = true;
            this.CbGenerateOne.Location = new System.Drawing.Point(12, 127);
            this.CbGenerateOne.Name = "CbGenerateOne";
            this.CbGenerateOne.Size = new System.Drawing.Size(111, 17);
            this.CbGenerateOne.TabIndex = 16;
            this.CbGenerateOne.Text = "Generar Solo Uno";
            this.CbGenerateOne.UseVisualStyleBackColor = true;
            this.CbGenerateOne.CheckedChanged += new System.EventHandler(this.CbGenerateOneCheckedChanged);
            // 
            // LblInformation
            // 
            this.LblInformation.AutoSize = true;
            this.LblInformation.Location = new System.Drawing.Point(16, 215);
            this.LblInformation.Name = "LblInformation";
            this.LblInformation.Size = new System.Drawing.Size(16, 13);
            this.LblInformation.TabIndex = 17;
            this.LblInformation.Text = "...";
            // 
            // CbAcumulatedExcel
            // 
            this.CbAcumulatedExcel.AutoSize = true;
            this.CbAcumulatedExcel.Location = new System.Drawing.Point(128, 127);
            this.CbAcumulatedExcel.Name = "CbAcumulatedExcel";
            this.CbAcumulatedExcel.Size = new System.Drawing.Size(108, 17);
            this.CbAcumulatedExcel.TabIndex = 18;
            this.CbAcumulatedExcel.Text = "Excel Acumulado";
            this.CbAcumulatedExcel.UseVisualStyleBackColor = true;
            // 
            // RbToProgram
            // 
            this.RbToProgram.AutoSize = true;
            this.RbToProgram.Location = new System.Drawing.Point(213, 157);
            this.RbToProgram.Name = "RbToProgram";
            this.RbToProgram.Size = new System.Drawing.Size(14, 13);
            this.RbToProgram.TabIndex = 0;
            this.RbToProgram.UseVisualStyleBackColor = true;
            this.RbToProgram.CheckedChanged += new System.EventHandler(this.RbToProgramCheckedChanged);
            // 
            // RbAutomaticProgram
            // 
            this.RbAutomaticProgram.AutoSize = true;
            this.RbAutomaticProgram.Location = new System.Drawing.Point(13, 155);
            this.RbAutomaticProgram.Name = "RbAutomaticProgram";
            this.RbAutomaticProgram.Size = new System.Drawing.Size(148, 17);
            this.RbAutomaticProgram.TabIndex = 0;
            this.RbAutomaticProgram.Text = "Generar Automáticamente";
            this.RbAutomaticProgram.UseVisualStyleBackColor = true;
            // 
            // PaToProgram
            // 
            this.PaToProgram.Controls.Add(this.LblToProgram);
            this.PaToProgram.Controls.Add(this.CboToProgram);
            this.PaToProgram.Location = new System.Drawing.Point(228, 151);
            this.PaToProgram.Name = "PaToProgram";
            this.PaToProgram.Size = new System.Drawing.Size(209, 25);
            this.PaToProgram.TabIndex = 2;
            // 
            // LblToProgram
            // 
            this.LblToProgram.AutoSize = true;
            this.LblToProgram.Location = new System.Drawing.Point(5, 6);
            this.LblToProgram.Name = "LblToProgram";
            this.LblToProgram.Size = new System.Drawing.Size(79, 13);
            this.LblToProgram.TabIndex = 1;
            this.LblToProgram.Text = "Generar Hasta:";
            // 
            // CboToProgram
            // 
            this.CboToProgram.FormattingEnabled = true;
            this.CboToProgram.Location = new System.Drawing.Point(85, 2);
            this.CboToProgram.Name = "CboToProgram";
            this.CboToProgram.Size = new System.Drawing.Size(124, 21);
            this.CboToProgram.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Mostrar Base de Datos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BtnDataDBClick);
            // 
            // ProcessDashboard
            // 
            this.AccessibleName = "";
            this.ClientSize = new System.Drawing.Size(449, 266);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PaToProgram);
            this.Controls.Add(this.RbAutomaticProgram);
            this.Controls.Add(this.CbAcumulatedExcel);
            this.Controls.Add(this.RbToProgram);
            this.Controls.Add(this.LblInformation);
            this.Controls.Add(this.CbGenerateOne);
            this.Controls.Add(this.CbDeleteExcels);
            this.Controls.Add(this.CbSaveInDB);
            this.Controls.Add(this.CbGenerateExcels);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.PbProcessGeneration);
            this.Controls.Add(this.BtnExecute);
            this.Controls.Add(this.BtnExcelFile);
            this.Controls.Add(this.BtnDashboardDirectory);
            this.Controls.Add(this.TxtPrefix);
            this.Controls.Add(this.TxtExcelFile);
            this.Controls.Add(this.TxtDashboardDirectory);
            this.Controls.Add(this.LblExcelFile);
            this.Controls.Add(this.LblDashboardDirectory);
            this.Controls.Add(this.LblPrefix);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessDashboard";
            this.Text = "Process Dashboard";
            this.Load += new System.EventHandler(this.ProcessDashboardLoad);
            this.PaToProgram.ResumeLayout(false);
            this.PaToProgram.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LblDashboardDirectory;
        private System.Windows.Forms.Label LblExcelFile;
        private System.Windows.Forms.Label LblPrefix;
        private System.Windows.Forms.TextBox TxtDashboardDirectory;
        private System.Windows.Forms.TextBox TxtExcelFile;
        private System.Windows.Forms.TextBox TxtPrefix;
        private System.Windows.Forms.Button BtnDashboardDirectory;
        private System.Windows.Forms.Button BtnExcelFile;
        private System.Windows.Forms.Button BtnExecute;
        private System.Windows.Forms.ProgressBar PbProcessGeneration;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.CheckBox CbGenerateExcels;
        private System.Windows.Forms.CheckBox CbSaveInDB;
        private System.Windows.Forms.CheckBox CbDeleteExcels;
        private System.Windows.Forms.CheckBox CbGenerateOne;
        private System.Windows.Forms.Label LblInformation;
        private System.Windows.Forms.CheckBox CbAcumulatedExcel;
        private System.Windows.Forms.RadioButton RbToProgram;
        private System.Windows.Forms.RadioButton RbAutomaticProgram;
        private System.Windows.Forms.Panel PaToProgram;
        private System.Windows.Forms.Label LblToProgram;
        private System.Windows.Forms.ComboBox CboToProgram;
        private System.Windows.Forms.Button button1;
    }
}

