namespace SIGC_TESChi
{
    partial class SubSecciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubSecciones));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSubseccion = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.MaskedTextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.tablaSubsecciones = new System.Windows.Forms.DataGridView();
            this.dBCONTRALORIASubseccion = new SIGC_TESChi.DBCONTRALORIASubseccion();
            this.subSeccionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.subSeccionTableAdapter = new SIGC_TESChi.DBCONTRALORIASubseccionTableAdapters.SubSeccionTableAdapter();
            this.idSubSeccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dSubSeccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSubsecciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBCONTRALORIASubseccion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subSeccionBindingSource)).BeginInit();
            this.panel3.SuspendLayout();
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
            this.panel2.Controls.Add(this.btnModificar);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.btnEliminar);
            this.panel2.Controls.Add(this.btnAgregar);
            this.panel2.Controls.Add(this.txtID);
            this.panel2.Controls.Add(this.txtSubseccion);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1239, 203);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registro de  Subsecciones";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Identificador de SubSecciones:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(59, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Descripcion de Subseccion:";
            // 
            // txtSubseccion
            // 
            this.txtSubseccion.Location = new System.Drawing.Point(305, 119);
            this.txtSubseccion.Name = "txtSubseccion";
            this.txtSubseccion.Size = new System.Drawing.Size(357, 22);
            this.txtSubseccion.TabIndex = 3;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(305, 73);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(357, 22);
            this.txtID.TabIndex = 4;
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAgregar.BackgroundImage")));
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregar.Location = new System.Drawing.Point(747, 73);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(60, 60);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.BackgroundImage")));
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminar.Location = new System.Drawing.Point(813, 73);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(60, 60);
            this.btnEliminar.TabIndex = 6;
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscar.BackgroundImage")));
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscar.Location = new System.Drawing.Point(879, 72);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(60, 60);
            this.btnBuscar.TabIndex = 7;
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            this.btnModificar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModificar.BackgroundImage")));
            this.btnModificar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModificar.Location = new System.Drawing.Point(945, 73);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(60, 60);
            this.btnModificar.TabIndex = 8;
            this.btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.BackgroundImage")));
            this.btnLimpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLimpiar.Location = new System.Drawing.Point(1011, 73);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 60);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // tablaSubsecciones
            // 
            this.tablaSubsecciones.AllowUserToAddRows = false;
            this.tablaSubsecciones.AllowUserToDeleteRows = false;
            this.tablaSubsecciones.AutoGenerateColumns = false;
            this.tablaSubsecciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaSubsecciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idSubSeccionDataGridViewTextBoxColumn,
            this.dSubSeccionDataGridViewTextBoxColumn});
            this.tablaSubsecciones.DataSource = this.subSeccionBindingSource;
            this.tablaSubsecciones.Location = new System.Drawing.Point(-2, -2);
            this.tablaSubsecciones.Name = "tablaSubsecciones";
            this.tablaSubsecciones.ReadOnly = true;
            this.tablaSubsecciones.RowHeadersWidth = 51;
            this.tablaSubsecciones.RowTemplate.Height = 24;
            this.tablaSubsecciones.Size = new System.Drawing.Size(1239, 597);
            this.tablaSubsecciones.TabIndex = 0;
            // 
            // dBCONTRALORIASubseccion
            // 
            this.dBCONTRALORIASubseccion.DataSetName = "DBCONTRALORIASubseccion";
            this.dBCONTRALORIASubseccion.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // subSeccionBindingSource
            // 
            this.subSeccionBindingSource.DataMember = "SubSeccion";
            this.subSeccionBindingSource.DataSource = this.dBCONTRALORIASubseccion;
            // 
            // subSeccionTableAdapter
            // 
            this.subSeccionTableAdapter.ClearBeforeFill = true;
            // 
            // idSubSeccionDataGridViewTextBoxColumn
            // 
            this.idSubSeccionDataGridViewTextBoxColumn.DataPropertyName = "idSubSeccion";
            this.idSubSeccionDataGridViewTextBoxColumn.HeaderText = "idSubSeccion";
            this.idSubSeccionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idSubSeccionDataGridViewTextBoxColumn.Name = "idSubSeccionDataGridViewTextBoxColumn";
            this.idSubSeccionDataGridViewTextBoxColumn.ReadOnly = true;
            this.idSubSeccionDataGridViewTextBoxColumn.Width = 125;
            // 
            // dSubSeccionDataGridViewTextBoxColumn
            // 
            this.dSubSeccionDataGridViewTextBoxColumn.DataPropertyName = "dSubSeccion";
            this.dSubSeccionDataGridViewTextBoxColumn.HeaderText = "dSubSeccion";
            this.dSubSeccionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dSubSeccionDataGridViewTextBoxColumn.Name = "dSubSeccionDataGridViewTextBoxColumn";
            this.dSubSeccionDataGridViewTextBoxColumn.ReadOnly = true;
            this.dSubSeccionDataGridViewTextBoxColumn.Width = 125;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.tablaSubsecciones);
            this.panel3.Location = new System.Drawing.Point(3, 234);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1239, 597);
            this.panel3.TabIndex = 3;
            // 
            // SubSecciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "SubSecciones";
            this.Size = new System.Drawing.Size(1249, 838);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSubsecciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBCONTRALORIASubseccion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subSeccionBindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtID;
        private System.Windows.Forms.TextBox txtSubseccion;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView tablaSubsecciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSubSeccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dSubSeccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource subSeccionBindingSource;
        private DBCONTRALORIASubseccion dBCONTRALORIASubseccion;
        private DBCONTRALORIASubseccionTableAdapters.SubSeccionTableAdapter subSeccionTableAdapter;
    }
}
