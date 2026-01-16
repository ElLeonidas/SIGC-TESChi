namespace SIGC_TESChi
{
    partial class Confi
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
            this.pnlInfo.Controls.Add(this.cboxModoOscuro);
            this.pnlInfo.Location = new System.Drawing.Point(4, 4);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(931, 675);
            this.pnlInfo.TabIndex = 1;
            // 
            // Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlInfo);
            this.Name = "Info";
            this.Size = new System.Drawing.Size(938, 682);
            this.Load += new System.EventHandler(this.Info_Load);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cboxModoOscuro;
        private System.Windows.Forms.Panel pnlInfo;
    }
}
