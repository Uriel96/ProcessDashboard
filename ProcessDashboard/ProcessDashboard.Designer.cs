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
            this.LblDirectorioDashboard = new System.Windows.Forms.Label();
            this.LblArchivoExcel = new System.Windows.Forms.Label();
            this.LblPrefijo = new System.Windows.Forms.Label();
            this.TxtDirectorioDashboard = new System.Windows.Forms.TextBox();
            this.TxtArchivoExcel = new System.Windows.Forms.TextBox();
            this.TxtPrefijo = new System.Windows.Forms.TextBox();
            this.BtnDirectorioDashboard = new System.Windows.Forms.Button();
            this.BtnArchivoExcel = new System.Windows.Forms.Button();
            this.BtnEjecutar = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.CbGenerarExcel = new System.Windows.Forms.CheckBox();
            this.CbGuardarBaseDatos = new System.Windows.Forms.CheckBox();
            this.CbBorrarExcel = new System.Windows.Forms.CheckBox();
            this.CbGenerarUno = new System.Windows.Forms.CheckBox();
            this.LblInformacion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblDirectorioDashboard
            // 
            this.LblDirectorioDashboard.AutoSize = true;
            this.LblDirectorioDashboard.Location = new System.Drawing.Point(12, 17);
            this.LblDirectorioDashboard.Name = "LblDirectorioDashboard";
            this.LblDirectorioDashboard.Size = new System.Drawing.Size(110, 13);
            this.LblDirectorioDashboard.TabIndex = 0;
            this.LblDirectorioDashboard.Text = "Directorio Dashboard:";
            // 
            // LblArchivoExcel
            // 
            this.LblArchivoExcel.AutoSize = true;
            this.LblArchivoExcel.Location = new System.Drawing.Point(32, 46);
            this.LblArchivoExcel.Name = "LblArchivoExcel";
            this.LblArchivoExcel.Size = new System.Drawing.Size(90, 13);
            this.LblArchivoExcel.TabIndex = 3;
            this.LblArchivoExcel.Text = "Archivo de Excel:";
            // 
            // LblPrefijo
            // 
            this.LblPrefijo.AutoSize = true;
            this.LblPrefijo.Location = new System.Drawing.Point(83, 75);
            this.LblPrefijo.Name = "LblPrefijo";
            this.LblPrefijo.Size = new System.Drawing.Size(39, 13);
            this.LblPrefijo.TabIndex = 4;
            this.LblPrefijo.Text = "Prefijo:";
            // 
            // TxtDirectorioDashboard
            // 
            this.TxtDirectorioDashboard.Location = new System.Drawing.Point(128, 14);
            this.TxtDirectorioDashboard.Name = "TxtDirectorioDashboard";
            this.TxtDirectorioDashboard.ReadOnly = true;
            this.TxtDirectorioDashboard.Size = new System.Drawing.Size(229, 20);
            this.TxtDirectorioDashboard.TabIndex = 5;
            // 
            // TxtArchivoExcel
            // 
            this.TxtArchivoExcel.Location = new System.Drawing.Point(128, 43);
            this.TxtArchivoExcel.Name = "TxtArchivoExcel";
            this.TxtArchivoExcel.ReadOnly = true;
            this.TxtArchivoExcel.Size = new System.Drawing.Size(229, 20);
            this.TxtArchivoExcel.TabIndex = 6;
            // 
            // TxtPrefijo
            // 
            this.TxtPrefijo.Location = new System.Drawing.Point(128, 72);
            this.TxtPrefijo.Name = "TxtPrefijo";
            this.TxtPrefijo.Size = new System.Drawing.Size(100, 20);
            this.TxtPrefijo.TabIndex = 7;
            // 
            // BtnDirectorioDashboard
            // 
            this.BtnDirectorioDashboard.Location = new System.Drawing.Point(363, 12);
            this.BtnDirectorioDashboard.Name = "BtnDirectorioDashboard";
            this.BtnDirectorioDashboard.Size = new System.Drawing.Size(75, 23);
            this.BtnDirectorioDashboard.TabIndex = 8;
            this.BtnDirectorioDashboard.Text = "Examinar";
            this.BtnDirectorioDashboard.UseVisualStyleBackColor = true;
            this.BtnDirectorioDashboard.Click += new System.EventHandler(this.BtnDirectorioDashboard_Click);
            // 
            // BtnArchivoExcel
            // 
            this.BtnArchivoExcel.Location = new System.Drawing.Point(363, 41);
            this.BtnArchivoExcel.Name = "BtnArchivoExcel";
            this.BtnArchivoExcel.Size = new System.Drawing.Size(75, 23);
            this.BtnArchivoExcel.TabIndex = 9;
            this.BtnArchivoExcel.Text = "Examinar";
            this.BtnArchivoExcel.UseVisualStyleBackColor = true;
            this.BtnArchivoExcel.Click += new System.EventHandler(this.BtnArchivoExcel_Click);
            // 
            // BtnEjecutar
            // 
            this.BtnEjecutar.Location = new System.Drawing.Point(282, 205);
            this.BtnEjecutar.Name = "BtnEjecutar";
            this.BtnEjecutar.Size = new System.Drawing.Size(75, 23);
            this.BtnEjecutar.TabIndex = 10;
            this.BtnEjecutar.Text = "Ejecutar";
            this.BtnEjecutar.UseVisualStyleBackColor = true;
            this.BtnEjecutar.Click += new System.EventHandler(this.BtnEjecutar_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 154);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(426, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Location = new System.Drawing.Point(365, 205);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(75, 23);
            this.BtnCancelar.TabIndex = 12;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            // 
            // CbGenerarExcel
            // 
            this.CbGenerarExcel.AutoSize = true;
            this.CbGenerarExcel.Location = new System.Drawing.Point(12, 108);
            this.CbGenerarExcel.Name = "CbGenerarExcel";
            this.CbGenerarExcel.Size = new System.Drawing.Size(104, 17);
            this.CbGenerarExcel.TabIndex = 13;
            this.CbGenerarExcel.Text = "Generar Exceles";
            this.CbGenerarExcel.UseVisualStyleBackColor = true;
            // 
            // CbGuardarBaseDatos
            // 
            this.CbGuardarBaseDatos.AutoSize = true;
            this.CbGuardarBaseDatos.Location = new System.Drawing.Point(128, 108);
            this.CbGuardarBaseDatos.Name = "CbGuardarBaseDatos";
            this.CbGuardarBaseDatos.Size = new System.Drawing.Size(163, 17);
            this.CbGuardarBaseDatos.TabIndex = 14;
            this.CbGuardarBaseDatos.Text = "Guardar en la Base de Datos";
            this.CbGuardarBaseDatos.UseVisualStyleBackColor = true;
            // 
            // CbBorrarExcel
            // 
            this.CbBorrarExcel.AutoSize = true;
            this.CbBorrarExcel.Location = new System.Drawing.Point(302, 108);
            this.CbBorrarExcel.Name = "CbBorrarExcel";
            this.CbBorrarExcel.Size = new System.Drawing.Size(138, 17);
            this.CbBorrarExcel.TabIndex = 15;
            this.CbBorrarExcel.Text = "Borrar Exceles Antiguos";
            this.CbBorrarExcel.UseVisualStyleBackColor = true;
            // 
            // CbGenerarUno
            // 
            this.CbGenerarUno.AutoSize = true;
            this.CbGenerarUno.Location = new System.Drawing.Point(12, 131);
            this.CbGenerarUno.Name = "CbGenerarUno";
            this.CbGenerarUno.Size = new System.Drawing.Size(111, 17);
            this.CbGenerarUno.TabIndex = 16;
            this.CbGenerarUno.Text = "Generar Solo Uno";
            this.CbGenerarUno.UseVisualStyleBackColor = true;
            // 
            // LblInformacion
            // 
            this.LblInformacion.AutoSize = true;
            this.LblInformacion.Location = new System.Drawing.Point(16, 185);
            this.LblInformacion.Name = "LblInformacion";
            this.LblInformacion.Size = new System.Drawing.Size(16, 13);
            this.LblInformacion.TabIndex = 17;
            this.LblInformacion.Text = "...";
            // 
            // ProcessDashboard
            // 
            this.ClientSize = new System.Drawing.Size(451, 238);
            this.Controls.Add(this.LblInformacion);
            this.Controls.Add(this.CbGenerarUno);
            this.Controls.Add(this.CbBorrarExcel);
            this.Controls.Add(this.CbGuardarBaseDatos);
            this.Controls.Add(this.CbGenerarExcel);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BtnEjecutar);
            this.Controls.Add(this.BtnArchivoExcel);
            this.Controls.Add(this.BtnDirectorioDashboard);
            this.Controls.Add(this.TxtPrefijo);
            this.Controls.Add(this.TxtArchivoExcel);
            this.Controls.Add(this.TxtDirectorioDashboard);
            this.Controls.Add(this.LblArchivoExcel);
            this.Controls.Add(this.LblDirectorioDashboard);
            this.Controls.Add(this.LblPrefijo);
            this.Name = "ProcessDashboard";
            this.Load += new System.EventHandler(this.ProcessDashboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LblDirectorioDashboard;
        private System.Windows.Forms.Label LblArchivoExcel;
        private System.Windows.Forms.Label LblPrefijo;
        private System.Windows.Forms.TextBox TxtDirectorioDashboard;
        private System.Windows.Forms.TextBox TxtArchivoExcel;
        private System.Windows.Forms.TextBox TxtPrefijo;
        private System.Windows.Forms.Button BtnDirectorioDashboard;
        private System.Windows.Forms.Button BtnArchivoExcel;
        private System.Windows.Forms.Button BtnEjecutar;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.CheckBox CbGenerarExcel;
        private System.Windows.Forms.CheckBox CbGuardarBaseDatos;
        private System.Windows.Forms.CheckBox CbBorrarExcel;
        private System.Windows.Forms.CheckBox CbGenerarUno;
        private System.Windows.Forms.Label LblInformacion;
    }
}

