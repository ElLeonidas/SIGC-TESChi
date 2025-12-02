namespace SIGC_TESChi
{
    partial class UnidadA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnidadA));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtNombreUnidad = new System.Windows.Forms.TextBox();
            this.txtClaveUnidad = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tablaUnidadA = new System.Windows.Forms.DataGridView();
            this.dBCONTRALORIAUnidadAdministrativa = new SIGC_TESChi.DBCONTRALORIAUnidadAdministrativa();
            this.unidadAdministrativaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.unidadAdministrativaTableAdapter = new SIGC_TESChi.DBCONTRALORIAUnidadAdministrativaTableAdapters.UnidadAdministrativaTableAdapter();
            this.idUniAdminDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUniAdminDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUniAdminDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaUnidadA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBCONTRALORIAUnidadAdministrativa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unidadAdministrativaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1249, 838);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnLimpiar);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.btnModificar);
            this.panel2.Controls.Add(this.btnEliminar);
            this.panel2.Controls.Add(this.btnAgregar);
            this.panel2.Controls.Add(this.txtNombreUnidad);
            this.panel2.Controls.Add(this.txtClaveUnidad);
            this.panel2.Controls.Add(this.txtID);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1239, 203);
            this.panel2.TabIndex = 0;
            // 
            // txtNombreUnidad
            // 
            this.txtNombreUnidad.Location = new System.Drawing.Point(417, 145);
            this.txtNombreUnidad.Name = "txtNombreUnidad";
            this.txtNombreUnidad.Size = new System.Drawing.Size(357, 22);
            this.txtNombreUnidad.TabIndex = 1;
            // 
            // txtClaveUnidad
            // 
            this.txtClaveUnidad.Location = new System.Drawing.Point(417, 101);
            this.txtClaveUnidad.Name = "txtClaveUnidad";
            this.txtClaveUnidad.Size = new System.Drawing.Size(357, 22);
            this.txtClaveUnidad.TabIndex = 6;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(417, 54);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(357, 22);
            this.txtID.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(105, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(276, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Nombre de Unidad Administrativa:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(126, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(255, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Clave de Unidad Administrativa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Identificador de Unidad Administraviva:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Registro de  Unidad Administrativa";
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAgregar.BackgroundImage")));
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregar.Location = new System.Drawing.Point(845, 81);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(60, 60);
            this.btnAgregar.TabIndex = 7;
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.BackgroundImage")));
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminar.Location = new System.Drawing.Point(911, 82);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(60, 60);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscar.BackgroundImage")));
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscar.Location = new System.Drawing.Point(977, 81);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(60, 60);
            this.btnBuscar.TabIndex = 9;
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            this.btnModificar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModificar.BackgroundImage")));
            this.btnModificar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModificar.Location = new System.Drawing.Point(1043, 81);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(60, 60);
            this.btnModificar.TabIndex = 1;
            this.btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.BackgroundImage")));
            this.btnLimpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLimpiar.Location = new System.Drawing.Point(1109, 82);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 60);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.tablaUnidadA);
            this.panel3.Location = new System.Drawing.Point(3, 232);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1239, 599);
            this.panel3.TabIndex = 1;
            // 
            // tablaUnidadA
            // 
            this.tablaUnidadA.AllowUserToAddRows = false;
            this.tablaUnidadA.AllowUserToDeleteRows = false;
            this.tablaUnidadA.AutoGenerateColumns = false;
            this.tablaUnidadA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaUnidadA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idUniAdminDataGridViewTextBoxColumn,
            this.cUniAdminDataGridViewTextBoxColumn,
            this.nUniAdminDataGridViewTextBoxColumn});
            this.tablaUnidadA.DataSource = this.unidadAdministrativaBindingSource;
            this.tablaUnidadA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaUnidadA.Location = new System.Drawing.Point(0, 0);
            this.tablaUnidadA.Name = "tablaUnidadA";
            this.tablaUnidadA.ReadOnly = true;
            this.tablaUnidadA.RowHeadersWidth = 51;
            this.tablaUnidadA.RowTemplate.Height = 24;
            this.tablaUnidadA.Size = new System.Drawing.Size(1235, 595);
            this.tablaUnidadA.TabIndex = 0;
            // 
            // dBCONTRALORIAUnidadAdministrativa
            // 
            this.dBCONTRALORIAUnidadAdministrativa.DataSetName = "DBCONTRALORIAUnidadAdministrativa";
            this.dBCONTRALORIAUnidadAdministrativa.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // unidadAdministrativaBindingSource
            // 
            this.unidadAdministrativaBindingSource.DataMember = "UnidadAdministrativa";
            this.unidadAdministrativaBindingSource.DataSource = this.dBCONTRALORIAUnidadAdministrativa;
            // 
            // unidadAdministrativaTableAdapter
            // 
            this.unidadAdministrativaTableAdapter.ClearBeforeFill = true;
            // 
            // idUniAdminDataGridViewTextBoxColumn
            // 
            this.idUniAdminDataGridViewTextBoxColumn.DataPropertyName = "idUniAdmin";
            this.idUniAdminDataGridViewTextBoxColumn.HeaderText = "idUniAdmin";
            this.idUniAdminDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idUniAdminDataGridViewTextBoxColumn.Name = "idUniAdminDataGridViewTextBoxColumn";
            this.idUniAdminDataGridViewTextBoxColumn.ReadOnly = true;
            this.idUniAdminDataGridViewTextBoxColumn.Width = 125;
            // 
            // cUniAdminDataGridViewTextBoxColumn
            // 
            this.cUniAdminDataGridViewTextBoxColumn.DataPropertyName = "cUniAdmin";
            this.cUniAdminDataGridViewTextBoxColumn.HeaderText = "cUniAdmin";
            this.cUniAdminDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cUniAdminDataGridViewTextBoxColumn.Name = "cUniAdminDataGridViewTextBoxColumn";
            this.cUniAdminDataGridViewTextBoxColumn.ReadOnly = true;
            this.cUniAdminDataGridViewTextBoxColumn.Width = 125;
            // 
            // nUniAdminDataGridViewTextBoxColumn
            // 
            this.nUniAdminDataGridViewTextBoxColumn.DataPropertyName = "nUniAdmin";
            this.nUniAdminDataGridViewTextBoxColumn.HeaderText = "nUniAdmin";
            this.nUniAdminDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nUniAdminDataGridViewTextBoxColumn.Name = "nUniAdminDataGridViewTextBoxColumn";
            this.nUniAdminDataGridViewTextBoxColumn.ReadOnly = true;
            this.nUniAdminDataGridViewTextBoxColumn.Width = 125;
            // 
            // UnidadA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UnidadA";
            this.Size = new System.Drawing.Size(1249, 838);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablaUnidadA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBCONTRALORIAUnidadAdministrativa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unidadAdministrativaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNombreUnidad;
        private System.Windows.Forms.TextBox txtClaveUnidad;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView tablaUnidadA;
        private System.Windows.Forms.DataGridViewTextBoxColumn idUniAdminDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUniAdminDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUniAdminDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource unidadAdministrativaBindingSource;
        private DBCONTRALORIAUnidadAdministrativa dBCONTRALORIAUnidadAdministrativa;
        private DBCONTRALORIAUnidadAdministrativaTableAdapters.UnidadAdministrativaTableAdapter unidadAdministrativaTableAdapter;
    }
}
