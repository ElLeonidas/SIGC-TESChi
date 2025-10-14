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
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnCArchivos = new System.Windows.Forms.Button();
            this.btnRUsuarios = new System.Windows.Forms.Button();
            this.btnLobby = new System.Windows.Forms.Button();
            this.Categorias = new System.Windows.Forms.Panel();
            this.MenuControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuControl
            // 
            this.MenuControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MenuControl.Controls.Add(this.btnCerrarSesion);
            this.MenuControl.Controls.Add(this.btnCArchivos);
            this.MenuControl.Controls.Add(this.btnRUsuarios);
            this.MenuControl.Controls.Add(this.btnLobby);
            this.MenuControl.Controls.Add(this.Categorias);
            this.MenuControl.Location = new System.Drawing.Point(1, 1);
            this.MenuControl.Name = "MenuControl";
            this.MenuControl.Size = new System.Drawing.Size(1428, 900);
            this.MenuControl.TabIndex = 0;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(1313, 9);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(101, 23);
            this.btnCerrarSesion.TabIndex = 0;
            this.btnCerrarSesion.Text = "Cerrar Sesion";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // btnCArchivos
            // 
            this.btnCArchivos.Location = new System.Drawing.Point(9, 121);
            this.btnCArchivos.Name = "btnCArchivos";
            this.btnCArchivos.Size = new System.Drawing.Size(75, 23);
            this.btnCArchivos.TabIndex = 0;
            this.btnCArchivos.Text = "Control de Archivos ";
            this.btnCArchivos.UseVisualStyleBackColor = true;
            this.btnCArchivos.Click += new System.EventHandler(this.btnCArchivos_Click);
            // 
            // btnRUsuarios
            // 
            this.btnRUsuarios.Location = new System.Drawing.Point(9, 83);
            this.btnRUsuarios.Name = "btnRUsuarios";
            this.btnRUsuarios.Size = new System.Drawing.Size(75, 23);
            this.btnRUsuarios.TabIndex = 0;
            this.btnRUsuarios.Text = "Usuarios";
            this.btnRUsuarios.UseVisualStyleBackColor = true;
            this.btnRUsuarios.Click += new System.EventHandler(this.btnRUsuarios_Click);
            // 
            // btnLobby
            // 
            this.btnLobby.Location = new System.Drawing.Point(9, 45);
            this.btnLobby.Name = "btnLobby";
            this.btnLobby.Size = new System.Drawing.Size(75, 23);
            this.btnLobby.TabIndex = 0;
            this.btnLobby.Text = "Lobby";
            this.btnLobby.UseVisualStyleBackColor = true;
            this.btnLobby.Click += new System.EventHandler(this.btnLobby_Click);
            // 
            // Categorias
            // 
            this.Categorias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Categorias.Location = new System.Drawing.Point(173, 45);
            this.Categorias.Name = "Categorias";
            this.Categorias.Size = new System.Drawing.Size(1248, 801);
            this.Categorias.TabIndex = 0;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1429, 853);
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
        private System.Windows.Forms.Button btnCArchivos;
        private System.Windows.Forms.Button btnRUsuarios;
        private System.Windows.Forms.Button btnLobby;
        private System.Windows.Forms.Button btnCerrarSesion;
    }
}