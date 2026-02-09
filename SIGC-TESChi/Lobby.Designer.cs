namespace SIGC_TESChi
{
    partial class Lobby
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lobby));
            this.pnlLobby = new System.Windows.Forms.Panel();
            this.pnlTabla = new System.Windows.Forms.Panel();
            this.dgvEventos = new System.Windows.Forms.DataGridView();
            this.pnlControles = new System.Windows.Forms.Panel();
            this.cmbTipoEvento = new System.Windows.Forms.ComboBox();
            this.mthCalendario = new System.Windows.Forms.MonthCalendar();
            this.nudMinutos = new System.Windows.Forms.NumericUpDown();
            this.chkAlerta = new System.Windows.Forms.CheckBox();
            this.cmbModalidad = new System.Windows.Forms.ComboBox();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.dtpHora = new System.Windows.Forms.DateTimePicker();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLobby.SuspendLayout();
            this.pnlTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).BeginInit();
            this.pnlControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutos)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLobby
            // 
            this.pnlLobby.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLobby.Controls.Add(this.pnlTabla);
            this.pnlLobby.Controls.Add(this.pnlControles);
            this.pnlLobby.Location = new System.Drawing.Point(0, 0);
            this.pnlLobby.Margin = new System.Windows.Forms.Padding(2);
            this.pnlLobby.Name = "pnlLobby";
            this.pnlLobby.Size = new System.Drawing.Size(938, 682);
            this.pnlLobby.TabIndex = 1;
            // 
            // pnlTabla
            // 
            this.pnlTabla.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTabla.Controls.Add(this.dgvEventos);
            this.pnlTabla.Location = new System.Drawing.Point(337, 2);
            this.pnlTabla.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTabla.Name = "pnlTabla";
            this.pnlTabla.Size = new System.Drawing.Size(596, 674);
            this.pnlTabla.TabIndex = 3;
            // 
            // dgvEventos
            // 
            this.dgvEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEventos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEventos.Location = new System.Drawing.Point(0, 0);
            this.dgvEventos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvEventos.Name = "dgvEventos";
            this.dgvEventos.RowHeadersWidth = 51;
            this.dgvEventos.RowTemplate.Height = 24;
            this.dgvEventos.Size = new System.Drawing.Size(592, 670);
            this.dgvEventos.TabIndex = 4;
            // 
            // pnlControles
            // 
            this.pnlControles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlControles.Controls.Add(this.cmbTipoEvento);
            this.pnlControles.Controls.Add(this.mthCalendario);
            this.pnlControles.Controls.Add(this.nudMinutos);
            this.pnlControles.Controls.Add(this.chkAlerta);
            this.pnlControles.Controls.Add(this.cmbModalidad);
            this.pnlControles.Controls.Add(this.txtTitulo);
            this.pnlControles.Controls.Add(this.dtpHora);
            this.pnlControles.Controls.Add(this.txtUsuario);
            this.pnlControles.Controls.Add(this.btnLimpiar);
            this.pnlControles.Controls.Add(this.btnModificar);
            this.pnlControles.Controls.Add(this.btnEliminar);
            this.pnlControles.Controls.Add(this.btnAgregar);
            this.pnlControles.Controls.Add(this.lblTitulo);
            this.pnlControles.Controls.Add(this.label7);
            this.pnlControles.Controls.Add(this.label6);
            this.pnlControles.Controls.Add(this.label5);
            this.pnlControles.Controls.Add(this.label4);
            this.pnlControles.Controls.Add(this.label3);
            this.pnlControles.Controls.Add(this.label2);
            this.pnlControles.Controls.Add(this.label1);
            this.pnlControles.Location = new System.Drawing.Point(2, 2);
            this.pnlControles.Margin = new System.Windows.Forms.Padding(2);
            this.pnlControles.Name = "pnlControles";
            this.pnlControles.Size = new System.Drawing.Size(328, 674);
            this.pnlControles.TabIndex = 2;
            // 
            // cmbTipoEvento
            // 
            this.cmbTipoEvento.FormattingEnabled = true;
            this.cmbTipoEvento.Location = new System.Drawing.Point(55, 435);
            this.cmbTipoEvento.Name = "cmbTipoEvento";
            this.cmbTipoEvento.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoEvento.TabIndex = 24;
            // 
            // mthCalendario
            // 
            this.mthCalendario.Location = new System.Drawing.Point(48, 128);
            this.mthCalendario.Margin = new System.Windows.Forms.Padding(7);
            this.mthCalendario.Name = "mthCalendario";
            this.mthCalendario.TabIndex = 0;
            // 
            // nudMinutos
            // 
            this.nudMinutos.Location = new System.Drawing.Point(141, 548);
            this.nudMinutos.Margin = new System.Windows.Forms.Padding(2);
            this.nudMinutos.Name = "nudMinutos";
            this.nudMinutos.Size = new System.Drawing.Size(46, 20);
            this.nudMinutos.TabIndex = 23;
            // 
            // chkAlerta
            // 
            this.chkAlerta.AutoSize = true;
            this.chkAlerta.Location = new System.Drawing.Point(64, 548);
            this.chkAlerta.Margin = new System.Windows.Forms.Padding(2);
            this.chkAlerta.Name = "chkAlerta";
            this.chkAlerta.Size = new System.Drawing.Size(35, 17);
            this.chkAlerta.TabIndex = 22;
            this.chkAlerta.Text = "Si";
            this.chkAlerta.UseVisualStyleBackColor = true;
            // 
            // cmbModalidad
            // 
            this.cmbModalidad.FormattingEnabled = true;
            this.cmbModalidad.Location = new System.Drawing.Point(55, 490);
            this.cmbModalidad.Margin = new System.Windows.Forms.Padding(2);
            this.cmbModalidad.Name = "cmbModalidad";
            this.cmbModalidad.Size = new System.Drawing.Size(196, 21);
            this.cmbModalidad.TabIndex = 20;
            this.cmbModalidad.SelectedIndexChanged += new System.EventHandler(this.cmbModalidad_SelectedIndexChanged);
            this.cmbModalidad.Leave += new System.EventHandler(this.cmbModalidad_Leave);
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(52, 320);
            this.txtTitulo.Margin = new System.Windows.Forms.Padding(2);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(196, 20);
            this.txtTitulo.TabIndex = 17;
            this.txtTitulo.TextChanged += new System.EventHandler(this.txtTitulo_TextChanged);
            // 
            // dtpHora
            // 
            this.dtpHora.Location = new System.Drawing.Point(52, 373);
            this.dtpHora.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHora.Name = "dtpHora";
            this.dtpHora.Size = new System.Drawing.Size(196, 20);
            this.dtpHora.TabIndex = 16;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Location = new System.Drawing.Point(53, 95);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(2);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(196, 20);
            this.txtUsuario.TabIndex = 14;
            this.txtUsuario.TextChanged += new System.EventHandler(this.txtUsuario_TextChanged);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.BackgroundImage")));
            this.btnLimpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.Location = new System.Drawing.Point(230, 602);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(56, 56);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModificar.BackgroundImage")));
            this.btnModificar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.Location = new System.Drawing.Point(164, 602);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(2);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(56, 56);
            this.btnModificar.TabIndex = 13;
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.BackgroundImage")));
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.Location = new System.Drawing.Point(98, 602);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(56, 56);
            this.btnEliminar.TabIndex = 12;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAgregar.BackgroundImage")));
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Location = new System.Drawing.Point(30, 602);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(56, 56);
            this.btnAgregar.TabIndex = 11;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Georgia", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(113, 22);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(76, 18);
            this.lblTitulo.TabIndex = 10;
            this.lblTitulo.Text = "Agenda ";
            this.lblTitulo.Click += new System.EventHandler(this.lblTitulo_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(138, 529);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "Minutos para Alerta:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(61, 529);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Alerta:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(50, 76);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Usuario:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 471);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Modalidad:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 415);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tipo de Evento:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 357);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hora :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 304);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Titulo:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLobby);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Lobby";
            this.Size = new System.Drawing.Size(937, 681);
            this.Load += new System.EventHandler(this.Lobby_Load);
            this.pnlLobby.ResumeLayout(false);
            this.pnlTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).EndInit();
            this.pnlControles.ResumeLayout(false);
            this.pnlControles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlLobby;
        private System.Windows.Forms.Panel pnlTabla;
        private System.Windows.Forms.Panel pnlControles;
        private System.Windows.Forms.MonthCalendar mthCalendario;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.CheckBox chkAlerta;
        private System.Windows.Forms.ComboBox cmbModalidad;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.DateTimePicker dtpHora;
        private System.Windows.Forms.NumericUpDown nudMinutos;
        private System.Windows.Forms.DataGridView dgvEventos;
        private System.Windows.Forms.ComboBox cmbTipoEvento;
        private System.Windows.Forms.Button btnEliminar;
    }
}
