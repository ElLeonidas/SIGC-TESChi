namespace SIGC_TESChi
{
    partial class RUsuarios
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TablaUsuarios = new System.Windows.Forms.DataGridView();
            this.idUsuarioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idTipoUsuarioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contrasenaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuarioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sGCTESCHIDataSet1 = new SIGC_TESChi.SGCTESCHIDataSet1();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.usuarioTableAdapter = new SIGC_TESChi.SGCTESCHIDataSet1TableAdapters.UsuarioTableAdapter();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaUsuarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usuarioBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ayuda me siento de la verga ";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1356, 845);
            this.panel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Controls.Add(this.TablaUsuarios);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 348);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1346, 498);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // TablaUsuarios
            // 
            this.TablaUsuarios.AllowUserToAddRows = false;
            this.TablaUsuarios.AllowUserToDeleteRows = false;
            this.TablaUsuarios.AutoGenerateColumns = false;
            this.TablaUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idUsuarioDataGridViewTextBoxColumn,
            this.idTipoUsuarioDataGridViewTextBoxColumn,
            this.contrasenaDataGridViewTextBoxColumn});
            this.TablaUsuarios.DataSource = this.usuarioBindingSource;
            this.TablaUsuarios.Location = new System.Drawing.Point(3, 3);
            this.TablaUsuarios.Name = "TablaUsuarios";
            this.TablaUsuarios.ReadOnly = true;
            this.TablaUsuarios.RowHeadersWidth = 51;
            this.TablaUsuarios.RowTemplate.Height = 24;
            this.TablaUsuarios.Size = new System.Drawing.Size(604, 150);
            this.TablaUsuarios.TabIndex = 0;
            // 
            // idUsuarioDataGridViewTextBoxColumn
            // 
            this.idUsuarioDataGridViewTextBoxColumn.DataPropertyName = "idUsuario";
            this.idUsuarioDataGridViewTextBoxColumn.HeaderText = "idUsuario";
            this.idUsuarioDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idUsuarioDataGridViewTextBoxColumn.Name = "idUsuarioDataGridViewTextBoxColumn";
            this.idUsuarioDataGridViewTextBoxColumn.ReadOnly = true;
            this.idUsuarioDataGridViewTextBoxColumn.Width = 125;
            // 
            // idTipoUsuarioDataGridViewTextBoxColumn
            // 
            this.idTipoUsuarioDataGridViewTextBoxColumn.DataPropertyName = "idTipoUsuario";
            this.idTipoUsuarioDataGridViewTextBoxColumn.HeaderText = "idTipoUsuario";
            this.idTipoUsuarioDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idTipoUsuarioDataGridViewTextBoxColumn.Name = "idTipoUsuarioDataGridViewTextBoxColumn";
            this.idTipoUsuarioDataGridViewTextBoxColumn.ReadOnly = true;
            this.idTipoUsuarioDataGridViewTextBoxColumn.Width = 125;
            // 
            // contrasenaDataGridViewTextBoxColumn
            // 
            this.contrasenaDataGridViewTextBoxColumn.DataPropertyName = "contrasena";
            this.contrasenaDataGridViewTextBoxColumn.HeaderText = "contrasena";
            this.contrasenaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.contrasenaDataGridViewTextBoxColumn.Name = "contrasenaDataGridViewTextBoxColumn";
            this.contrasenaDataGridViewTextBoxColumn.ReadOnly = true;
            this.contrasenaDataGridViewTextBoxColumn.Width = 125;
            // 
            // usuarioBindingSource
            // 
            this.usuarioBindingSource.DataMember = "Usuario";
            this.usuarioBindingSource.DataSource = this.sGCTESCHIDataSet1;
            // 
            // sGCTESCHIDataSet1
            // 
            this.sGCTESCHIDataSet1.DataSetName = "SGCTESCHIDataSet1";
            this.sGCTESCHIDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1346, 323);
            this.panel2.TabIndex = 1;
            // 
            // usuarioTableAdapter
            // 
            this.usuarioTableAdapter.ClearBeforeFill = true;
            // 
            // RUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "RUsuarios";
            this.Size = new System.Drawing.Size(1356, 848);
            this.Load += new System.EventHandler(this.RUsuarios_Load_1);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TablaUsuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usuarioBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView TablaUsuarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn idUsuarioDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idTipoUsuarioDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contrasenaDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource usuarioBindingSource;
        private SGCTESCHIDataSet1 sGCTESCHIDataSet1;
        private System.Windows.Forms.BindingSource dataSet1BindingSource;
        private SGCTESCHIDataSet1TableAdapters.UsuarioTableAdapter usuarioTableAdapter;
    }
}
