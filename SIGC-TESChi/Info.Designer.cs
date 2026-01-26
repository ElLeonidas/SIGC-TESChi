namespace SIGC_TESChi
{
    partial class Info
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboxModoOscuro = new System.Windows.Forms.CheckBox();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.cmbFuentes = new System.Windows.Forms.ComboBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboxModoOscuro
            // 
            this.cboxModoOscuro.AutoSize = true;
            this.cboxModoOscuro.Location = new System.Drawing.Point(388, 101);
            this.cboxModoOscuro.Name = "cboxModoOscuro";
            this.cboxModoOscuro.Size = new System.Drawing.Size(90, 17);
            this.cboxModoOscuro.TabIndex = 0;
            this.cboxModoOscuro.Text = "Modo Oscuro";
            this.cboxModoOscuro.UseVisualStyleBackColor = true;
            this.cboxModoOscuro.CheckedChanged += new System.EventHandler(this.cboxModoOscuro_CheckedChanged);
            // 
            // pnlInfo
            // 
            this.pnlInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlInfo.Controls.Add(this.btnRestaurar);
            this.pnlInfo.Controls.Add(this.btnBackup);
            this.pnlInfo.Controls.Add(this.cmbFuentes);
            this.pnlInfo.Controls.Add(this.cboxModoOscuro);
            this.pnlInfo.Location = new System.Drawing.Point(3, 3);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(930, 673);
            this.pnlInfo.TabIndex = 1;
            // 
            // cmbFuentes
            // 
            this.cmbFuentes.FormattingEnabled = true;
            this.cmbFuentes.Location = new System.Drawing.Point(387, 145);
            this.cmbFuentes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbFuentes.Name = "cmbFuentes";
            this.cmbFuentes.Size = new System.Drawing.Size(151, 21);
            this.cmbFuentes.TabIndex = 1;
            this.cmbFuentes.SelectedIndexChanged += new System.EventHandler(this.cmbFuentes_SelectedIndexChanged);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(672, 103);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(75, 23);
            this.btnBackup.TabIndex = 2;
            this.btnBackup.Text = "Respaldo";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(672, 158);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(75, 23);
            this.btnRestaurar.TabIndex = 3;
            this.btnRestaurar.Text = "Restaurar";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.pnlInfo);
            this.Name = "Info";
            this.Size = new System.Drawing.Size(935, 678);
            this.Load += new System.EventHandler(this.Info_Load);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cboxModoOscuro;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.ComboBox cmbFuentes;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnRestaurar;
    }
}
