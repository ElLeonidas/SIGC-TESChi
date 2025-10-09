namespace SIGC_TESChi
{
    partial class Menu
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
            this.MenuControl = new System.Windows.Forms.Panel();
            this.Categorias = new System.Windows.Forms.Panel();
            this.MenuControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuControl
            // 
            this.MenuControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MenuControl.Controls.Add(this.Categorias);
            this.MenuControl.Location = new System.Drawing.Point(1, 1);
            this.MenuControl.Name = "MenuControl";
            this.MenuControl.Size = new System.Drawing.Size(1879, 1052);
            this.MenuControl.TabIndex = 0;
            // 
            // Categorias
            // 
            this.Categorias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Categorias.Location = new System.Drawing.Point(206, 45);
            this.Categorias.Name = "Categorias";
            this.Categorias.Size = new System.Drawing.Size(1672, 1006);
            this.Categorias.TabIndex = 0;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1882, 1055);
            this.Controls.Add(this.MenuControl);
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.MenuControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuControl;
        private System.Windows.Forms.Panel Categorias;
    }
}