namespace SIGC_TESChi
{
    partial class Historial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Historial));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.idHistorialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tablaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.llaveDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoAccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuarioBDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaAccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datosAnterioresDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datosNuevosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idUsuarioAppDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historialCambiosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBCONTRALORIADataSet3 = new SIGC_TESChi.DBCONTRALORIADataSet3();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.cmbTabla = new System.Windows.Forms.ComboBox();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.historialCambiosTableAdapter = new SIGC_TESChi.DBCONTRALORIADataSet3TableAdapters.HistorialCambiosTableAdapter();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.historialCambiosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBCONTRALORIADataSet3)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(938, 682);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvHistorial);
            this.panel3.Location = new System.Drawing.Point(2, 176);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(929, 500);
            this.panel3.TabIndex = 1;
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.AutoGenerateColumns = false;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idHistorialDataGridViewTextBoxColumn,
            this.tablaDataGridViewTextBoxColumn,
            this.llaveDataGridViewTextBoxColumn,
            this.tipoAccionDataGridViewTextBoxColumn,
            this.usuarioBDDataGridViewTextBoxColumn,
            this.fechaAccionDataGridViewTextBoxColumn,
            this.datosAnterioresDataGridViewTextBoxColumn,
            this.datosNuevosDataGridViewTextBoxColumn,
            this.idUsuarioAppDataGridViewTextBoxColumn});
            this.dgvHistorial.DataSource = this.historialCambiosBindingSource;
            this.dgvHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorial.Location = new System.Drawing.Point(0, 0);
            this.dgvHistorial.Margin = new System.Windows.Forms.Padding(2);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersWidth = 51;
            this.dgvHistorial.RowTemplate.Height = 24;
            this.dgvHistorial.Size = new System.Drawing.Size(929, 500);
            this.dgvHistorial.TabIndex = 0;
            // 
            // idHistorialDataGridViewTextBoxColumn
            // 
            this.idHistorialDataGridViewTextBoxColumn.DataPropertyName = "idHistorial";
            this.idHistorialDataGridViewTextBoxColumn.HeaderText = "idHistorial";
            this.idHistorialDataGridViewTextBoxColumn.Name = "idHistorialDataGridViewTextBoxColumn";
            this.idHistorialDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tablaDataGridViewTextBoxColumn
            // 
            this.tablaDataGridViewTextBoxColumn.DataPropertyName = "Tabla";
            this.tablaDataGridViewTextBoxColumn.HeaderText = "Tabla";
            this.tablaDataGridViewTextBoxColumn.Name = "tablaDataGridViewTextBoxColumn";
            this.tablaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // llaveDataGridViewTextBoxColumn
            // 
            this.llaveDataGridViewTextBoxColumn.DataPropertyName = "Llave";
            this.llaveDataGridViewTextBoxColumn.HeaderText = "Llave";
            this.llaveDataGridViewTextBoxColumn.Name = "llaveDataGridViewTextBoxColumn";
            this.llaveDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tipoAccionDataGridViewTextBoxColumn
            // 
            this.tipoAccionDataGridViewTextBoxColumn.DataPropertyName = "TipoAccion";
            this.tipoAccionDataGridViewTextBoxColumn.HeaderText = "TipoAccion";
            this.tipoAccionDataGridViewTextBoxColumn.Name = "tipoAccionDataGridViewTextBoxColumn";
            this.tipoAccionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usuarioBDDataGridViewTextBoxColumn
            // 
            this.usuarioBDDataGridViewTextBoxColumn.DataPropertyName = "UsuarioBD";
            this.usuarioBDDataGridViewTextBoxColumn.HeaderText = "UsuarioBD";
            this.usuarioBDDataGridViewTextBoxColumn.Name = "usuarioBDDataGridViewTextBoxColumn";
            this.usuarioBDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fechaAccionDataGridViewTextBoxColumn
            // 
            this.fechaAccionDataGridViewTextBoxColumn.DataPropertyName = "FechaAccion";
            this.fechaAccionDataGridViewTextBoxColumn.HeaderText = "FechaAccion";
            this.fechaAccionDataGridViewTextBoxColumn.Name = "fechaAccionDataGridViewTextBoxColumn";
            this.fechaAccionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // datosAnterioresDataGridViewTextBoxColumn
            // 
            this.datosAnterioresDataGridViewTextBoxColumn.DataPropertyName = "DatosAnteriores";
            this.datosAnterioresDataGridViewTextBoxColumn.HeaderText = "DatosAnteriores";
            this.datosAnterioresDataGridViewTextBoxColumn.Name = "datosAnterioresDataGridViewTextBoxColumn";
            this.datosAnterioresDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // datosNuevosDataGridViewTextBoxColumn
            // 
            this.datosNuevosDataGridViewTextBoxColumn.DataPropertyName = "DatosNuevos";
            this.datosNuevosDataGridViewTextBoxColumn.HeaderText = "DatosNuevos";
            this.datosNuevosDataGridViewTextBoxColumn.Name = "datosNuevosDataGridViewTextBoxColumn";
            this.datosNuevosDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idUsuarioAppDataGridViewTextBoxColumn
            // 
            this.idUsuarioAppDataGridViewTextBoxColumn.DataPropertyName = "idUsuarioApp";
            this.idUsuarioAppDataGridViewTextBoxColumn.HeaderText = "idUsuarioApp";
            this.idUsuarioAppDataGridViewTextBoxColumn.Name = "idUsuarioAppDataGridViewTextBoxColumn";
            this.idUsuarioAppDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // historialCambiosBindingSource
            // 
            this.historialCambiosBindingSource.DataMember = "HistorialCambios";
            this.historialCambiosBindingSource.DataSource = this.dBCONTRALORIADataSet3;
            // 
            // dBCONTRALORIADataSet3
            // 
            this.dBCONTRALORIADataSet3.DataSetName = "DBCONTRALORIADataSet3";
            this.dBCONTRALORIADataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnActualizar);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtpHasta);
            this.panel2.Controls.Add(this.cmbTabla);
            this.panel2.Controls.Add(this.dtpDesde);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(930, 161);
            this.panel2.TabIndex = 0;
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnActualizar.BackgroundImage")));
            this.btnActualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnActualizar.Location = new System.Drawing.Point(487, 94);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(2);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(49, 53);
            this.btnActualizar.TabIndex = 2;
            this.btnActualizar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscar.BackgroundImage")));
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscar.Location = new System.Drawing.Point(423, 94);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(49, 53);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Historial de Cambios ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(484, 66);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Hasta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(367, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tabla:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(532, 66);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(151, 20);
            this.dtpHasta.TabIndex = 5;
            // 
            // cmbTabla
            // 
            this.cmbTabla.FormattingEnabled = true;
            this.cmbTabla.Location = new System.Drawing.Point(415, 41);
            this.cmbTabla.Margin = new System.Windows.Forms.Padding(2);
            this.cmbTabla.Name = "cmbTabla";
            this.cmbTabla.Size = new System.Drawing.Size(151, 21);
            this.cmbTabla.TabIndex = 2;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(310, 66);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(151, 20);
            this.dtpDesde.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(224, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Fecha Desde:";
            // 
            // historialCambiosTableAdapter
            // 
            this.historialCambiosTableAdapter.ClearBeforeFill = true;
            // 
            // Historial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Historial";
            this.Size = new System.Drawing.Size(937, 681);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.historialCambiosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBCONTRALORIADataSet3)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTabla;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.DataGridViewTextBoxColumn idHistorialDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tablaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn llaveDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoAccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuarioBDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaAccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datosAnterioresDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datosNuevosDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idUsuarioAppDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource historialCambiosBindingSource;
        private DBCONTRALORIADataSet3 dBCONTRALORIADataSet3;
        private DBCONTRALORIADataSet3TableAdapters.HistorialCambiosTableAdapter historialCambiosTableAdapter;
    }
}
