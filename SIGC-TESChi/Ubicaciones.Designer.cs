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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.tablaUbicaciones = new System.Windows.Forms.DataGridView();
            this.sGCTESCHIDataSet3 = new SIGC_TESChi.SGCTESCHIDataSet3();
            this.ubicacionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ubicacionTableAdapter = new SIGC_TESChi.SGCTESCHIDataSet3TableAdapters.UbicacionTableAdapter();
            this.idUbicacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dUbicacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaUbicaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubicacionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnLimpiar);
            this.panel1.Controls.Add(this.btnEliminar);
            this.panel1.Controls.Add(this.btnModificar);
            this.panel1.Controls.Add(this.btnAgregar);
            this.panel1.Controls.Add(this.txtUbicacion);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(25, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1201, 403);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tablaUbicaciones);
            this.panel2.Location = new System.Drawing.Point(402, 465);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(435, 292);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datos y Operaciones";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ubicacion";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(157, 158);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 22);
            this.txtID.TabIndex = 3;
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.Location = new System.Drawing.Point(157, 216);
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.Size = new System.Drawing.Size(238, 22);
            this.txtUbicacion.TabIndex = 4;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(665, 54);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(100, 50);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar Ubicacion";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(800, 53);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(100, 50);
            this.btnModificar.TabIndex = 6;
            this.btnModificar.Text = "Modificar Ubicacion";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(665, 145);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 50);
            this.btnEliminar.TabIndex = 7;
            this.btnEliminar.Text = "Eliminar Ubicacion";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(800, 145);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(100, 50);
            this.btnLimpiar.TabIndex = 8;
            this.btnLimpiar.Text = "Limpiar Campos";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // tablaUbicaciones
            // 
            this.tablaUbicaciones.AllowUserToOrderColumns = true;
            this.tablaUbicaciones.AutoGenerateColumns = false;
            this.tablaUbicaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaUbicaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idUbicacionDataGridViewTextBoxColumn,
            this.dUbicacionDataGridViewTextBoxColumn});
            this.tablaUbicaciones.DataSource = this.ubicacionBindingSource;
            this.tablaUbicaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaUbicaciones.Location = new System.Drawing.Point(0, 0);
            this.tablaUbicaciones.Name = "tablaUbicaciones";
            this.tablaUbicaciones.RowHeadersWidth = 51;
            this.tablaUbicaciones.RowTemplate.Height = 24;
            this.tablaUbicaciones.Size = new System.Drawing.Size(433, 290);
            this.tablaUbicaciones.TabIndex = 0;
            this.tablaUbicaciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaUbicaciones_CellClick);
            this.tablaUbicaciones.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaUbicaciones_CellContentClick);
            // 
            // sGCTESCHIDataSet3
            // 
            this.sGCTESCHIDataSet3.DataSetName = "SGCTESCHIDataSet3";
            this.sGCTESCHIDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ubicacionBindingSource
            // 
            this.ubicacionBindingSource.DataMember = "Ubicacion";
            this.ubicacionBindingSource.DataSource = this.sGCTESCHIDataSet3;
            // 
            // ubicacionTableAdapter
            // 
            this.ubicacionTableAdapter.ClearBeforeFill = true;
            // 
            // idUbicacionDataGridViewTextBoxColumn
            // 
            this.idUbicacionDataGridViewTextBoxColumn.DataPropertyName = "idUbicacion";
            this.idUbicacionDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idUbicacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idUbicacionDataGridViewTextBoxColumn.Name = "idUbicacionDataGridViewTextBoxColumn";
            this.idUbicacionDataGridViewTextBoxColumn.Width = 125;
            // 
            // dUbicacionDataGridViewTextBoxColumn
            // 
            this.dUbicacionDataGridViewTextBoxColumn.DataPropertyName = "dUbicacion";
            this.dUbicacionDataGridViewTextBoxColumn.HeaderText = "Ubicacion";
            this.dUbicacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dUbicacionDataGridViewTextBoxColumn.Name = "dUbicacionDataGridViewTextBoxColumn";
            this.dUbicacionDataGridViewTextBoxColumn.Width = 125;
            // 
            // Ubicaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Ubicaciones";
            this.Size = new System.Drawing.Size(1249, 838);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablaUbicaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubicacionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView tablaUbicaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn idUbicacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dUbicacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource ubicacionBindingSource;
        private SGCTESCHIDataSet3 sGCTESCHIDataSet3;
        private SGCTESCHIDataSet3TableAdapters.UbicacionTableAdapter ubicacionTableAdapter;
    }
}
