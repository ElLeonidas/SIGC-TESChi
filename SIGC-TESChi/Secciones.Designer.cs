namespace SIGC_TESChi
{
    partial class Secciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Secciones));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtSeccion = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tablaSecciones = new System.Windows.Forms.DataGridView();
            this.idSeccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dSeccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seccionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBCONTRALORIASeccionesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBCONTRALORIASecciones = new SIGC_TESChi.DBCONTRALORIASecciones();
            this.seccionTableAdapter = new SIGC_TESChi.DBCONTRALORIASeccionesTableAdapters.SeccionTableAdapter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSecciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seccionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBCONTRALORIASeccionesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBCONTRALORIASecciones)).BeginInit();
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
            this.panel2.Controls.Add(this.txtSeccion);
            this.panel2.Controls.Add(this.txtID);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1239, 203);
            this.panel2.TabIndex = 2;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.BackgroundImage")));
            this.btnLimpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLimpiar.Location = new System.Drawing.Point(991, 75);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 60);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            this.btnModificar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModificar.BackgroundImage")));
            this.btnModificar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModificar.Location = new System.Drawing.Point(925, 75);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(60, 60);
            this.btnModificar.TabIndex = 8;
            this.btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscar.BackgroundImage")));
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscar.Location = new System.Drawing.Point(859, 75);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(60, 60);
            this.btnBuscar.TabIndex = 7;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.BackgroundImage")));
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminar.Location = new System.Drawing.Point(793, 75);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(60, 60);
            this.btnEliminar.TabIndex = 6;
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAgregar.BackgroundImage")));
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregar.Location = new System.Drawing.Point(727, 75);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(60, 60);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // txtSeccion
            // 
            this.txtSeccion.Location = new System.Drawing.Point(284, 113);
            this.txtSeccion.Name = "txtSeccion";
            this.txtSeccion.Size = new System.Drawing.Size(357, 22);
            this.txtSeccion.TabIndex = 4;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(284, 71);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(357, 22);
            this.txtID.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(49, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Identificador de Secciones:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Descripcion de Seccion:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registro de Secciones";
            // 
            // tablaSecciones
            // 
            this.tablaSecciones.AllowUserToAddRows = false;
            this.tablaSecciones.AllowUserToDeleteRows = false;
            this.tablaSecciones.AutoGenerateColumns = false;
            this.tablaSecciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaSecciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idSeccionDataGridViewTextBoxColumn,
            this.dSeccionDataGridViewTextBoxColumn});
            this.tablaSecciones.DataSource = this.seccionBindingSource;
            this.tablaSecciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaSecciones.Location = new System.Drawing.Point(0, 0);
            this.tablaSecciones.Name = "tablaSecciones";
            this.tablaSecciones.ReadOnly = true;
            this.tablaSecciones.RowHeadersWidth = 51;
            this.tablaSecciones.RowTemplate.Height = 24;
            this.tablaSecciones.Size = new System.Drawing.Size(1235, 596);
            this.tablaSecciones.TabIndex = 0;
            // 
            // idSeccionDataGridViewTextBoxColumn
            // 
            this.idSeccionDataGridViewTextBoxColumn.DataPropertyName = "idSeccion";
            this.idSeccionDataGridViewTextBoxColumn.HeaderText = "idSeccion";
            this.idSeccionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idSeccionDataGridViewTextBoxColumn.Name = "idSeccionDataGridViewTextBoxColumn";
            this.idSeccionDataGridViewTextBoxColumn.ReadOnly = true;
            this.idSeccionDataGridViewTextBoxColumn.Width = 125;
            // 
            // dSeccionDataGridViewTextBoxColumn
            // 
            this.dSeccionDataGridViewTextBoxColumn.DataPropertyName = "dSeccion";
            this.dSeccionDataGridViewTextBoxColumn.HeaderText = "dSeccion";
            this.dSeccionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dSeccionDataGridViewTextBoxColumn.Name = "dSeccionDataGridViewTextBoxColumn";
            this.dSeccionDataGridViewTextBoxColumn.ReadOnly = true;
            this.dSeccionDataGridViewTextBoxColumn.Width = 125;
            // 
            // seccionBindingSource
            // 
            this.seccionBindingSource.DataMember = "Seccion";
            this.seccionBindingSource.DataSource = this.dBCONTRALORIASeccionesBindingSource;
            // 
            // dBCONTRALORIASeccionesBindingSource
            // 
            this.dBCONTRALORIASeccionesBindingSource.DataSource = this.dBCONTRALORIASecciones;
            this.dBCONTRALORIASeccionesBindingSource.Position = 0;
            // 
            // dBCONTRALORIASecciones
            // 
            this.dBCONTRALORIASecciones.DataSetName = "DBCONTRALORIASecciones";
            this.dBCONTRALORIASecciones.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // seccionTableAdapter
            // 
            this.seccionTableAdapter.ClearBeforeFill = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.tablaSecciones);
            this.panel3.Location = new System.Drawing.Point(3, 231);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1239, 600);
            this.panel3.TabIndex = 3;
            // 
            // Secciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "Secciones";
            this.Size = new System.Drawing.Size(1249, 838);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSecciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seccionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBCONTRALORIASeccionesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBCONTRALORIASecciones)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSeccion;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView tablaSecciones;
        private System.Windows.Forms.BindingSource dBCONTRALORIASeccionesBindingSource;
        private DBCONTRALORIASecciones dBCONTRALORIASecciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSeccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dSeccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource seccionBindingSource;
        private DBCONTRALORIASeccionesTableAdapters.SeccionTableAdapter seccionTableAdapter;
        private System.Windows.Forms.Panel panel3;
    }
}
