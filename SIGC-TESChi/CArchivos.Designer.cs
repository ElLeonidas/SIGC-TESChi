namespace SIGC_TESChi
{
    partial class CArchivos
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
            this.TablaArchivos = new System.Windows.Forms.DataGridView();
            this.idcontrolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.añocontrolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUAcontrolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUAcontrolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noExpedienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nExpedienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nForjasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nLegajosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idSeccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idSubSeccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clasificacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idUbicacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observacionesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idEstatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idPersonalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.controlBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sGCTESCHIDataSet2 = new SIGC_TESChi.SGCTESCHIDataSet2();
            this.controlTableAdapter = new SIGC_TESChi.SGCTESCHIDataSet2TableAdapters.ControlTableAdapter();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaArchivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Temas muy Profundas";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.TablaArchivos);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1249, 838);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // TablaArchivos
            // 
            this.TablaArchivos.AutoGenerateColumns = false;
            this.TablaArchivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaArchivos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idcontrolDataGridViewTextBoxColumn,
            this.añocontrolDataGridViewTextBoxColumn,
            this.cUAcontrolDataGridViewTextBoxColumn,
            this.nUAcontrolDataGridViewTextBoxColumn,
            this.noExpedienteDataGridViewTextBoxColumn,
            this.nExpedienteDataGridViewTextBoxColumn,
            this.nForjasDataGridViewTextBoxColumn,
            this.nLegajosDataGridViewTextBoxColumn,
            this.idSeccionDataGridViewTextBoxColumn,
            this.idSubSeccionDataGridViewTextBoxColumn,
            this.clasificacionDataGridViewTextBoxColumn,
            this.idUbicacionDataGridViewTextBoxColumn,
            this.observacionesDataGridViewTextBoxColumn,
            this.idEstatusDataGridViewTextBoxColumn,
            this.idPersonalDataGridViewTextBoxColumn});
            this.TablaArchivos.DataSource = this.controlBindingSource;
            this.TablaArchivos.Location = new System.Drawing.Point(19, 420);
            this.TablaArchivos.Name = "TablaArchivos";
            this.TablaArchivos.RowHeadersWidth = 51;
            this.TablaArchivos.RowTemplate.Height = 24;
            this.TablaArchivos.Size = new System.Drawing.Size(1214, 150);
            this.TablaArchivos.TabIndex = 0;
            // 
            // idcontrolDataGridViewTextBoxColumn
            // 
            this.idcontrolDataGridViewTextBoxColumn.DataPropertyName = "id_control";
            this.idcontrolDataGridViewTextBoxColumn.FillWeight = 50F;
            this.idcontrolDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idcontrolDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idcontrolDataGridViewTextBoxColumn.Name = "idcontrolDataGridViewTextBoxColumn";
            this.idcontrolDataGridViewTextBoxColumn.ReadOnly = true;
            this.idcontrolDataGridViewTextBoxColumn.Width = 45;
            // 
            // añocontrolDataGridViewTextBoxColumn
            // 
            this.añocontrolDataGridViewTextBoxColumn.DataPropertyName = "año_control";
            this.añocontrolDataGridViewTextBoxColumn.HeaderText = "año_control";
            this.añocontrolDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.añocontrolDataGridViewTextBoxColumn.Name = "añocontrolDataGridViewTextBoxColumn";
            this.añocontrolDataGridViewTextBoxColumn.Width = 125;
            // 
            // cUAcontrolDataGridViewTextBoxColumn
            // 
            this.cUAcontrolDataGridViewTextBoxColumn.DataPropertyName = "CUA_control";
            this.cUAcontrolDataGridViewTextBoxColumn.HeaderText = "CUA_control";
            this.cUAcontrolDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cUAcontrolDataGridViewTextBoxColumn.Name = "cUAcontrolDataGridViewTextBoxColumn";
            this.cUAcontrolDataGridViewTextBoxColumn.Width = 125;
            // 
            // nUAcontrolDataGridViewTextBoxColumn
            // 
            this.nUAcontrolDataGridViewTextBoxColumn.DataPropertyName = "NUA_control";
            this.nUAcontrolDataGridViewTextBoxColumn.HeaderText = "NUA_control";
            this.nUAcontrolDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nUAcontrolDataGridViewTextBoxColumn.Name = "nUAcontrolDataGridViewTextBoxColumn";
            this.nUAcontrolDataGridViewTextBoxColumn.Width = 125;
            // 
            // noExpedienteDataGridViewTextBoxColumn
            // 
            this.noExpedienteDataGridViewTextBoxColumn.DataPropertyName = "noExpediente";
            this.noExpedienteDataGridViewTextBoxColumn.HeaderText = "noExpediente";
            this.noExpedienteDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.noExpedienteDataGridViewTextBoxColumn.Name = "noExpedienteDataGridViewTextBoxColumn";
            this.noExpedienteDataGridViewTextBoxColumn.Width = 125;
            // 
            // nExpedienteDataGridViewTextBoxColumn
            // 
            this.nExpedienteDataGridViewTextBoxColumn.DataPropertyName = "nExpediente";
            this.nExpedienteDataGridViewTextBoxColumn.HeaderText = "nExpediente";
            this.nExpedienteDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nExpedienteDataGridViewTextBoxColumn.Name = "nExpedienteDataGridViewTextBoxColumn";
            this.nExpedienteDataGridViewTextBoxColumn.Width = 125;
            // 
            // nForjasDataGridViewTextBoxColumn
            // 
            this.nForjasDataGridViewTextBoxColumn.DataPropertyName = "nForjas";
            this.nForjasDataGridViewTextBoxColumn.HeaderText = "nForjas";
            this.nForjasDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nForjasDataGridViewTextBoxColumn.Name = "nForjasDataGridViewTextBoxColumn";
            this.nForjasDataGridViewTextBoxColumn.Width = 125;
            // 
            // nLegajosDataGridViewTextBoxColumn
            // 
            this.nLegajosDataGridViewTextBoxColumn.DataPropertyName = "nLegajos";
            this.nLegajosDataGridViewTextBoxColumn.HeaderText = "nLegajos";
            this.nLegajosDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nLegajosDataGridViewTextBoxColumn.Name = "nLegajosDataGridViewTextBoxColumn";
            this.nLegajosDataGridViewTextBoxColumn.Width = 125;
            // 
            // idSeccionDataGridViewTextBoxColumn
            // 
            this.idSeccionDataGridViewTextBoxColumn.DataPropertyName = "idSeccion";
            this.idSeccionDataGridViewTextBoxColumn.HeaderText = "idSeccion";
            this.idSeccionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idSeccionDataGridViewTextBoxColumn.Name = "idSeccionDataGridViewTextBoxColumn";
            this.idSeccionDataGridViewTextBoxColumn.Width = 125;
            // 
            // idSubSeccionDataGridViewTextBoxColumn
            // 
            this.idSubSeccionDataGridViewTextBoxColumn.DataPropertyName = "idSubSeccion";
            this.idSubSeccionDataGridViewTextBoxColumn.HeaderText = "idSubSeccion";
            this.idSubSeccionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idSubSeccionDataGridViewTextBoxColumn.Name = "idSubSeccionDataGridViewTextBoxColumn";
            this.idSubSeccionDataGridViewTextBoxColumn.Width = 125;
            // 
            // clasificacionDataGridViewTextBoxColumn
            // 
            this.clasificacionDataGridViewTextBoxColumn.DataPropertyName = "Clasificacion";
            this.clasificacionDataGridViewTextBoxColumn.HeaderText = "Clasificacion";
            this.clasificacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.clasificacionDataGridViewTextBoxColumn.Name = "clasificacionDataGridViewTextBoxColumn";
            this.clasificacionDataGridViewTextBoxColumn.Width = 125;
            // 
            // idUbicacionDataGridViewTextBoxColumn
            // 
            this.idUbicacionDataGridViewTextBoxColumn.DataPropertyName = "idUbicacion";
            this.idUbicacionDataGridViewTextBoxColumn.HeaderText = "idUbicacion";
            this.idUbicacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idUbicacionDataGridViewTextBoxColumn.Name = "idUbicacionDataGridViewTextBoxColumn";
            this.idUbicacionDataGridViewTextBoxColumn.Width = 125;
            // 
            // observacionesDataGridViewTextBoxColumn
            // 
            this.observacionesDataGridViewTextBoxColumn.DataPropertyName = "Observaciones";
            this.observacionesDataGridViewTextBoxColumn.HeaderText = "Observaciones";
            this.observacionesDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.observacionesDataGridViewTextBoxColumn.Name = "observacionesDataGridViewTextBoxColumn";
            this.observacionesDataGridViewTextBoxColumn.Width = 125;
            // 
            // idEstatusDataGridViewTextBoxColumn
            // 
            this.idEstatusDataGridViewTextBoxColumn.DataPropertyName = "idEstatus";
            this.idEstatusDataGridViewTextBoxColumn.HeaderText = "idEstatus";
            this.idEstatusDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idEstatusDataGridViewTextBoxColumn.Name = "idEstatusDataGridViewTextBoxColumn";
            this.idEstatusDataGridViewTextBoxColumn.Width = 125;
            // 
            // idPersonalDataGridViewTextBoxColumn
            // 
            this.idPersonalDataGridViewTextBoxColumn.DataPropertyName = "idPersonal";
            this.idPersonalDataGridViewTextBoxColumn.HeaderText = "idPersonal";
            this.idPersonalDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idPersonalDataGridViewTextBoxColumn.Name = "idPersonalDataGridViewTextBoxColumn";
            this.idPersonalDataGridViewTextBoxColumn.Width = 125;
            // 
            // controlBindingSource
            // 
            this.controlBindingSource.DataMember = "Control";
            this.controlBindingSource.DataSource = this.sGCTESCHIDataSet2;
            // 
            // sGCTESCHIDataSet2
            // 
            this.sGCTESCHIDataSet2.DataSetName = "SGCTESCHIDataSet2";
            this.sGCTESCHIDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // controlTableAdapter
            // 
            this.controlTableAdapter.ClearBeforeFill = true;
            // 
            // CArchivos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "CArchivos";
            this.Size = new System.Drawing.Size(1255, 849);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaArchivos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView TablaArchivos;
        private System.Windows.Forms.BindingSource controlBindingSource;
        private SGCTESCHIDataSet2 sGCTESCHIDataSet2;
        private SGCTESCHIDataSet2TableAdapters.ControlTableAdapter controlTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idcontrolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn añocontrolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUAcontrolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUAcontrolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noExpedienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nExpedienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nForjasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nLegajosDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSeccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSubSeccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clasificacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idUbicacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn observacionesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEstatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPersonalDataGridViewTextBoxColumn;
    }
}
