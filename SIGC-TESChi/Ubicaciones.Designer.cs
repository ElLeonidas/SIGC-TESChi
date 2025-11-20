namespace SIGC_TESChi
{
    partial class Ubicaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ubicaciones));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tablaUbicaciones = new System.Windows.Forms.DataGridView();
            this.ubicacionBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.sGCTESCHIDataSet8 = new SIGC_TESChi.SGCTESCHIDataSet8();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnModificar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.ubicacionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sGCTESCHIDataSet3 = new SIGC_TESChi.SGCTESCHIDataSet3();
            this.ubicacionTableAdapter = new SIGC_TESChi.SGCTESCHIDataSet3TableAdapters.UbicacionTableAdapter();
            this.sGCTESCHIDataSet7 = new SIGC_TESChi.SGCTESCHIDataSet7();
            this.ubicacionBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ubicacionTableAdapter1 = new SIGC_TESChi.SGCTESCHIDataSet7TableAdapters.UbicacionTableAdapter();
            this.ubicacionTableAdapter2 = new SIGC_TESChi.SGCTESCHIDataSet8TableAdapters.UbicacionTableAdapter();
            this.idUbicacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dUbicacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaUbicaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubicacionBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet8)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ubicacionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubicacionBindingSource1)).BeginInit();
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
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.tablaUbicaciones);
            this.panel3.Location = new System.Drawing.Point(3, 230);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1239, 601);
            this.panel3.TabIndex = 10;
            // 
            // tablaUbicaciones
            // 
            this.tablaUbicaciones.AllowUserToAddRows = false;
            this.tablaUbicaciones.AllowUserToDeleteRows = false;
            this.tablaUbicaciones.AutoGenerateColumns = false;
            this.tablaUbicaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaUbicaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idUbicacionDataGridViewTextBoxColumn,
            this.dUbicacionDataGridViewTextBoxColumn});
            this.tablaUbicaciones.DataSource = this.ubicacionBindingSource2;
            this.tablaUbicaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaUbicaciones.Location = new System.Drawing.Point(0, 0);
            this.tablaUbicaciones.Name = "tablaUbicaciones";
            this.tablaUbicaciones.ReadOnly = true;
            this.tablaUbicaciones.RowHeadersWidth = 51;
            this.tablaUbicaciones.RowTemplate.Height = 24;
            this.tablaUbicaciones.Size = new System.Drawing.Size(1235, 597);
            this.tablaUbicaciones.TabIndex = 0;
            // 
            // ubicacionBindingSource2
            // 
            this.ubicacionBindingSource2.DataMember = "Ubicacion";
            this.ubicacionBindingSource2.DataSource = this.sGCTESCHIDataSet8;
            // 
            // sGCTESCHIDataSet8
            // 
            this.sGCTESCHIDataSet8.DataSetName = "SGCTESCHIDataSet8";
            this.sGCTESCHIDataSet8.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtUbicacion);
            this.panel2.Controls.Add(this.btnLimpiar);
            this.panel2.Controls.Add(this.btnEliminar);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnModificar);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnAgregar);
            this.panel2.Controls.Add(this.txtID);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1239, 203);
            this.panel2.TabIndex = 9;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscar.BackgroundImage")));
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscar.Location = new System.Drawing.Point(808, 78);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(60, 60);
            this.btnBuscar.TabIndex = 9;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registro de Ubicaciones Fisicas ";
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUbicacion.Location = new System.Drawing.Point(258, 118);
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.Size = new System.Drawing.Size(357, 25);
            this.txtUbicacion.TabIndex = 4;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.BackgroundImage")));
            this.btnLimpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLimpiar.Location = new System.Drawing.Point(874, 78);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 60);
            this.btnLimpiar.TabIndex = 8;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.BackgroundImage")));
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminar.Location = new System.Drawing.Point(737, 78);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(60, 60);
            this.btnEliminar.TabIndex = 7;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 73);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(217, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Identificador de Ubicacion:";
            // 
            // btnModificar
            // 
            this.btnModificar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModificar.BackgroundImage")));
            this.btnModificar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModificar.Location = new System.Drawing.Point(940, 78);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(60, 60);
            this.btnModificar.TabIndex = 6;
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(38, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ubicacion Real de Archivos:";
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAgregar.BackgroundImage")));
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregar.Location = new System.Drawing.Point(666, 78);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(60, 60);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(258, 73);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(357, 25);
            this.txtID.TabIndex = 3;
            // 
            // ubicacionBindingSource
            // 
            this.ubicacionBindingSource.DataMember = "Ubicacion";
            this.ubicacionBindingSource.DataSource = this.sGCTESCHIDataSet3;
            // 
            // sGCTESCHIDataSet3
            // 
            this.sGCTESCHIDataSet3.DataSetName = "SGCTESCHIDataSet3";
            this.sGCTESCHIDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ubicacionTableAdapter
            // 
            this.ubicacionTableAdapter.ClearBeforeFill = true;
            // 
            // sGCTESCHIDataSet7
            // 
            this.sGCTESCHIDataSet7.DataSetName = "SGCTESCHIDataSet7";
            this.sGCTESCHIDataSet7.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ubicacionBindingSource1
            // 
            this.ubicacionBindingSource1.DataMember = "Ubicacion";
            this.ubicacionBindingSource1.DataSource = this.sGCTESCHIDataSet7;
            // 
            // ubicacionTableAdapter1
            // 
            this.ubicacionTableAdapter1.ClearBeforeFill = true;
            // 
            // ubicacionTableAdapter2
            // 
            this.ubicacionTableAdapter2.ClearBeforeFill = true;
            // 
            // idUbicacionDataGridViewTextBoxColumn
            // 
            this.idUbicacionDataGridViewTextBoxColumn.DataPropertyName = "idUbicacion";
            this.idUbicacionDataGridViewTextBoxColumn.HeaderText = "idUbicacion";
            this.idUbicacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idUbicacionDataGridViewTextBoxColumn.Name = "idUbicacionDataGridViewTextBoxColumn";
            this.idUbicacionDataGridViewTextBoxColumn.ReadOnly = true;
            this.idUbicacionDataGridViewTextBoxColumn.Width = 125;
            // 
            // dUbicacionDataGridViewTextBoxColumn
            // 
            this.dUbicacionDataGridViewTextBoxColumn.DataPropertyName = "dUbicacion";
            this.dUbicacionDataGridViewTextBoxColumn.HeaderText = "dUbicacion";
            this.dUbicacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dUbicacionDataGridViewTextBoxColumn.Name = "dUbicacionDataGridViewTextBoxColumn";
            this.dUbicacionDataGridViewTextBoxColumn.ReadOnly = true;
            this.dUbicacionDataGridViewTextBoxColumn.Width = 125;
            // 
            // Ubicaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "Ubicaciones";
            this.Size = new System.Drawing.Size(1249, 838);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablaUbicaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubicacionBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet8)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ubicacionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubicacionBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.BindingSource ubicacionBindingSource;
        private SGCTESCHIDataSet3 sGCTESCHIDataSet3;
        private SGCTESCHIDataSet3TableAdapters.UbicacionTableAdapter ubicacionTableAdapter;
        private SGCTESCHIDataSet7 sGCTESCHIDataSet7;
        private System.Windows.Forms.BindingSource ubicacionBindingSource1;
        private SGCTESCHIDataSet7TableAdapters.UbicacionTableAdapter ubicacionTableAdapter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView tablaUbicaciones;
        private System.Windows.Forms.BindingSource ubicacionBindingSource2;
        private SGCTESCHIDataSet8 sGCTESCHIDataSet8;
        private SGCTESCHIDataSet8TableAdapters.UbicacionTableAdapter ubicacionTableAdapter2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridViewTextBoxColumn idUbicacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dUbicacionDataGridViewTextBoxColumn;
    }
}
