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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CArchivos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Control = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.txtnExpendiente = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.cboAño = new System.Windows.Forms.ComboBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cboClasificacion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEstatus = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cboSeccion = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtLegajos = new System.Windows.Forms.TextBox();
            this.cboSubSeccion = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cboInstituto = new System.Windows.Forms.ComboBox();
            this.txtFormulaClasificatoria = new System.Windows.Forms.TextBox();
            this.cboNombUniAdmin = new System.Windows.Forms.ComboBox();
            this.txtFojas = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboCodUnidAdmin = new System.Windows.Forms.ComboBox();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtnoExpediente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpfApertura = new System.Windows.Forms.DateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.dtpfCierre = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cboUbicacion = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvControl = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnBorrarDocumento = new System.Windows.Forms.Button();
            this.btnDescargarDocumento = new System.Windows.Forms.Button();
            this.btnSubirDocumento = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvDocumentos = new System.Windows.Forms.DataGridView();
            this.controlBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.controlBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.controlBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.controlBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.Control.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvControl)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.Control);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(913, 675);
            this.panel1.TabIndex = 1;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.CArchivos_Scroll);
            // 
            // Control
            // 
            this.Control.Controls.Add(this.tabPage1);
            this.Control.Controls.Add(this.tabPage2);
            this.Control.Controls.Add(this.tabPage3);
            this.Control.Location = new System.Drawing.Point(4, 3);
            this.Control.Name = "Control";
            this.Control.SelectedIndex = 0;
            this.Control.Size = new System.Drawing.Size(902, 648);
            this.Control.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnBuscar);
            this.tabPage1.Controls.Add(this.lblTitulo);
            this.tabPage1.Controls.Add(this.btnImportar);
            this.tabPage1.Controls.Add(this.btnExportar);
            this.tabPage1.Controls.Add(this.txtnExpendiente);
            this.tabPage1.Controls.Add(this.btnLimpiar);
            this.tabPage1.Controls.Add(this.cboAño);
            this.tabPage1.Controls.Add(this.btnEliminar);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.btnEditar);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnGuardar);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.cboClasificacion);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.cboEstatus);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.cboSeccion);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.txtID);
            this.tabPage1.Controls.Add(this.txtLegajos);
            this.tabPage1.Controls.Add(this.cboSubSeccion);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.cboInstituto);
            this.tabPage1.Controls.Add(this.txtFormulaClasificatoria);
            this.tabPage1.Controls.Add(this.cboNombUniAdmin);
            this.tabPage1.Controls.Add(this.txtFojas);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.cboCodUnidAdmin);
            this.tabPage1.Controls.Add(this.txtObservaciones);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.txtnoExpediente);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dtpfApertura);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.dtpfCierre);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.cboUbicacion);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(894, 622);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Registro";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscar.BackgroundImage")));
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(674, 219);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(56, 56);
            this.btnBuscar.TabIndex = 44;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Georgia", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(6, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(135, 16);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Registra archivos";
            // 
            // btnImportar
            // 
            this.btnImportar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImportar.BackgroundImage")));
            this.btnImportar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnImportar.Location = new System.Drawing.Point(674, 467);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(56, 56);
            this.btnImportar.TabIndex = 43;
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExportar.BackgroundImage")));
            this.btnExportar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExportar.Location = new System.Drawing.Point(674, 405);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(56, 56);
            this.btnExportar.TabIndex = 42;
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // txtnExpendiente
            // 
            this.txtnExpendiente.Location = new System.Drawing.Point(45, 252);
            this.txtnExpendiente.Name = "txtnExpendiente";
            this.txtnExpendiente.Size = new System.Drawing.Size(169, 20);
            this.txtnExpendiente.TabIndex = 25;
            this.txtnExpendiente.TextChanged += new System.EventHandler(this.txtnExpendiente_TextChanged);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.BackgroundImage")));
            this.btnLimpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLimpiar.Location = new System.Drawing.Point(674, 281);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(56, 56);
            this.btnLimpiar.TabIndex = 40;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // cboAño
            // 
            this.cboAño.FormattingEnabled = true;
            this.cboAño.Location = new System.Drawing.Point(160, 93);
            this.cboAño.Name = "cboAño";
            this.cboAño.Size = new System.Drawing.Size(54, 21);
            this.cboAño.TabIndex = 1;
            this.cboAño.SelectedIndexChanged += new System.EventHandler(this.cboAño_SelectedIndexChanged);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.BackgroundImage")));
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEliminar.Location = new System.Drawing.Point(674, 343);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(56, 56);
            this.btnEliminar.TabIndex = 39;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(42, 77);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(85, 14);
            this.label19.TabIndex = 41;
            this.label19.Text = "Identificador:";
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.Transparent;
            this.btnEditar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditar.BackgroundImage")));
            this.btnEditar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditar.Location = new System.Drawing.Point(674, 157);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(56, 56);
            this.btnEditar.TabIndex = 38;
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(158, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Año:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Transparent;
            this.btnGuardar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuardar.BackgroundImage")));
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGuardar.Location = new System.Drawing.Point(674, 95);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(56, 56);
            this.btnGuardar.TabIndex = 28;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(42, 289);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "Sección:";
            // 
            // cboClasificacion
            // 
            this.cboClasificacion.FormattingEnabled = true;
            this.cboClasificacion.Location = new System.Drawing.Point(331, 266);
            this.cboClasificacion.Name = "cboClasificacion";
            this.cboClasificacion.Size = new System.Drawing.Size(200, 21);
            this.cboClasificacion.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(42, 351);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 14);
            this.label6.TabIndex = 8;
            this.label6.Text = "SubSección:";
            // 
            // cboEstatus
            // 
            this.cboEstatus.FormattingEnabled = true;
            this.cboEstatus.Location = new System.Drawing.Point(331, 204);
            this.cboEstatus.Name = "cboEstatus";
            this.cboEstatus.Size = new System.Drawing.Size(200, 21);
            this.cboEstatus.TabIndex = 35;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(328, 241);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(69, 13);
            this.label18.TabIndex = 34;
            this.label18.Text = "Clasificacion:";
            // 
            // cboSeccion
            // 
            this.cboSeccion.FormattingEnabled = true;
            this.cboSeccion.Location = new System.Drawing.Point(45, 306);
            this.cboSeccion.Name = "cboSeccion";
            this.cboSeccion.Size = new System.Drawing.Size(121, 21);
            this.cboSeccion.TabIndex = 9;
            this.cboSeccion.SelectedIndexChanged += new System.EventHandler(this.cboSeccion_SelectedIndexChanged);
            this.cboSeccion.SelectionChangeCommitted += new System.EventHandler(this.cboSeccion_SelectionChangeCommitted);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(328, 180);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 13);
            this.label17.TabIndex = 33;
            this.label17.Text = "Estatus:";
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(42, 94);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(86, 20);
            this.txtID.TabIndex = 37;
            // 
            // txtLegajos
            // 
            this.txtLegajos.Location = new System.Drawing.Point(331, 484);
            this.txtLegajos.Name = "txtLegajos";
            this.txtLegajos.Size = new System.Drawing.Size(200, 20);
            this.txtLegajos.TabIndex = 30;
            this.txtLegajos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLegajos_KeyPress);
            // 
            // cboSubSeccion
            // 
            this.cboSubSeccion.FormattingEnabled = true;
            this.cboSubSeccion.Location = new System.Drawing.Point(45, 368);
            this.cboSubSeccion.Name = "cboSubSeccion";
            this.cboSubSeccion.Size = new System.Drawing.Size(122, 21);
            this.cboSubSeccion.TabIndex = 10;
            this.cboSubSeccion.SelectedIndexChanged += new System.EventHandler(this.cboSubSeccion_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(328, 468);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Legajos:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(328, 78);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 13);
            this.label16.TabIndex = 32;
            this.label16.Text = "Instituto:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(42, 175);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(226, 14);
            this.label11.TabIndex = 19;
            this.label11.Text = "Nombre de la Unidad Administrativa:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(328, 302);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(109, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Formula Clasificatoria:";
            // 
            // cboInstituto
            // 
            this.cboInstituto.FormattingEnabled = true;
            this.cboInstituto.Location = new System.Drawing.Point(331, 94);
            this.cboInstituto.Name = "cboInstituto";
            this.cboInstituto.Size = new System.Drawing.Size(200, 21);
            this.cboInstituto.TabIndex = 31;
            // 
            // txtFormulaClasificatoria
            // 
            this.txtFormulaClasificatoria.Enabled = false;
            this.txtFormulaClasificatoria.Location = new System.Drawing.Point(331, 318);
            this.txtFormulaClasificatoria.Name = "txtFormulaClasificatoria";
            this.txtFormulaClasificatoria.Size = new System.Drawing.Size(200, 20);
            this.txtFormulaClasificatoria.TabIndex = 26;
            // 
            // cboNombUniAdmin
            // 
            this.cboNombUniAdmin.FormattingEnabled = true;
            this.cboNombUniAdmin.Location = new System.Drawing.Point(45, 192);
            this.cboNombUniAdmin.Name = "cboNombUniAdmin";
            this.cboNombUniAdmin.Size = new System.Drawing.Size(258, 21);
            this.cboNombUniAdmin.TabIndex = 20;
            this.cboNombUniAdmin.SelectedIndexChanged += new System.EventHandler(this.cboNombUniAdmin_SelectedIndexChanged);
            // 
            // txtFojas
            // 
            this.txtFojas.Location = new System.Drawing.Point(331, 427);
            this.txtFojas.Name = "txtFojas";
            this.txtFojas.Size = new System.Drawing.Size(200, 20);
            this.txtFojas.TabIndex = 18;
            this.txtFojas.TextChanged += new System.EventHandler(this.txtFojas_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(42, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(188, 14);
            this.label12.TabIndex = 21;
            this.label12.Text = "Código Unidad Administrativa:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(328, 404);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Fojas:";
            // 
            // cboCodUnidAdmin
            // 
            this.cboCodUnidAdmin.FormattingEnabled = true;
            this.cboCodUnidAdmin.Location = new System.Drawing.Point(45, 144);
            this.cboCodUnidAdmin.Name = "cboCodUnidAdmin";
            this.cboCodUnidAdmin.Size = new System.Drawing.Size(258, 21);
            this.cboCodUnidAdmin.TabIndex = 22;
            this.cboCodUnidAdmin.SelectedIndexChanged += new System.EventHandler(this.cboCodUnidAdmin_SelectedIndexChanged);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(328, 368);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(203, 20);
            this.txtObservaciones.TabIndex = 14;
            this.txtObservaciones.TextChanged += new System.EventHandler(this.txtObservaciones_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(42, 227);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(144, 14);
            this.label21.TabIndex = 23;
            this.label21.Text = "Número del Expediente:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(328, 351);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 14);
            this.label8.TabIndex = 13;
            this.label8.Text = "Observaciones:";
            // 
            // txtnoExpediente
            // 
            this.txtnoExpediente.Location = new System.Drawing.Point(45, 442);
            this.txtnoExpediente.Multiline = true;
            this.txtnoExpediente.Name = "txtnoExpediente";
            this.txtnoExpediente.Size = new System.Drawing.Size(258, 39);
            this.txtnoExpediente.TabIndex = 16;
            this.txtnoExpediente.TextChanged += new System.EventHandler(this.txtnoExpediente_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(42, 495);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "Fecha de apertura:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(42, 545);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "Fecha de cierre:";
            // 
            // dtpfApertura
            // 
            this.dtpfApertura.Location = new System.Drawing.Point(45, 512);
            this.dtpfApertura.Name = "dtpfApertura";
            this.dtpfApertura.Size = new System.Drawing.Size(200, 20);
            this.dtpfApertura.TabIndex = 5;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(42, 413);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(143, 14);
            this.label20.TabIndex = 15;
            this.label20.Text = "Nombre del Expediente:";
            // 
            // dtpfCierre
            // 
            this.dtpfCierre.Location = new System.Drawing.Point(45, 563);
            this.dtpfCierre.Name = "dtpfCierre";
            this.dtpfCierre.Size = new System.Drawing.Size(200, 20);
            this.dtpfCierre.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(328, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Ubicación:";
            // 
            // cboUbicacion
            // 
            this.cboUbicacion.FormattingEnabled = true;
            this.cboUbicacion.Location = new System.Drawing.Point(331, 146);
            this.cboUbicacion.Name = "cboUbicacion";
            this.cboUbicacion.Size = new System.Drawing.Size(200, 21);
            this.cboUbicacion.TabIndex = 12;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvControl);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(894, 622);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Expedientes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvControl
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvControl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvControl.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvControl.Location = new System.Drawing.Point(9, 38);
            this.dgvControl.Margin = new System.Windows.Forms.Padding(2);
            this.dgvControl.Name = "dgvControl";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvControl.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvControl.RowHeadersWidth = 51;
            this.dgvControl.RowTemplate.Height = 24;
            this.dgvControl.Size = new System.Drawing.Size(880, 567);
            this.dgvControl.TabIndex = 0;
            this.dgvControl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvControl_CellClick);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(155, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Aqui la tabla con el visualizador";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnBorrarDocumento);
            this.tabPage3.Controls.Add(this.btnDescargarDocumento);
            this.tabPage3.Controls.Add(this.btnSubirDocumento);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(894, 622);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Archivos";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnBorrarDocumento
            // 
            this.btnBorrarDocumento.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBorrarDocumento.BackgroundImage")));
            this.btnBorrarDocumento.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBorrarDocumento.Location = new System.Drawing.Point(767, 364);
            this.btnBorrarDocumento.Name = "btnBorrarDocumento";
            this.btnBorrarDocumento.Size = new System.Drawing.Size(56, 56);
            this.btnBorrarDocumento.TabIndex = 47;
            this.btnBorrarDocumento.UseVisualStyleBackColor = true;
            this.btnBorrarDocumento.Click += new System.EventHandler(this.btnBorrarDocumento_Click);
            // 
            // btnDescargarDocumento
            // 
            this.btnDescargarDocumento.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDescargarDocumento.BackgroundImage")));
            this.btnDescargarDocumento.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDescargarDocumento.Location = new System.Drawing.Point(766, 255);
            this.btnDescargarDocumento.Name = "btnDescargarDocumento";
            this.btnDescargarDocumento.Size = new System.Drawing.Size(56, 56);
            this.btnDescargarDocumento.TabIndex = 46;
            this.btnDescargarDocumento.UseVisualStyleBackColor = true;
            this.btnDescargarDocumento.Click += new System.EventHandler(this.btnDescargarDocumento_Click);
            // 
            // btnSubirDocumento
            // 
            this.btnSubirDocumento.BackColor = System.Drawing.Color.Transparent;
            this.btnSubirDocumento.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSubirDocumento.BackgroundImage")));
            this.btnSubirDocumento.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSubirDocumento.Location = new System.Drawing.Point(766, 148);
            this.btnSubirDocumento.Name = "btnSubirDocumento";
            this.btnSubirDocumento.Size = new System.Drawing.Size(56, 56);
            this.btnSubirDocumento.TabIndex = 45;
            this.btnSubirDocumento.UseVisualStyleBackColor = false;
            this.btnSubirDocumento.Click += new System.EventHandler(this.btnSubirDocumento_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvDocumentos);
            this.panel2.Location = new System.Drawing.Point(16, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(691, 575);
            this.panel2.TabIndex = 48;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // dgvDocumentos
            // 
            this.dgvDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDocumentos.Location = new System.Drawing.Point(0, 0);
            this.dgvDocumentos.Name = "dgvDocumentos";
            this.dgvDocumentos.Size = new System.Drawing.Size(691, 575);
            this.dgvDocumentos.TabIndex = 0;
            this.dgvDocumentos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDocumentos_CellDoubleClick);
            // 
            // controlBindingSource3
            // 
            this.controlBindingSource3.DataMember = "Control";
            // 
            // controlBindingSource2
            // 
            this.controlBindingSource2.DataMember = "Control";
            // 
            // controlBindingSource1
            // 
            this.controlBindingSource1.DataMember = "Control";
            // 
            // controlBindingSource
            // 
            this.controlBindingSource.DataMember = "Control";
            // 
            // CArchivos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CArchivos";
            this.Size = new System.Drawing.Size(918, 661);
            this.Load += new System.EventHandler(this.CArchivos_Load_1);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.CArchivos_Scroll);
            this.panel1.ResumeLayout(false);
            this.Control.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvControl)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvControl;
        private System.Windows.Forms.BindingSource controlBindingSource;
        private System.Windows.Forms.BindingSource controlBindingSource1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.BindingSource controlBindingSource2;
        private System.Windows.Forms.ComboBox cboAño;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpfApertura;
        private System.Windows.Forms.DateTimePicker dtpfCierre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSubSeccion;
        private System.Windows.Forms.ComboBox cboSeccion;
        private System.Windows.Forms.ComboBox cboUbicacion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtnoExpediente;
        private System.Windows.Forms.TextBox txtFojas;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboCodUnidAdmin;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboNombUniAdmin;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtnExpendiente;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtFormulaClasificatoria;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtLegajos;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cboInstituto;
        private System.Windows.Forms.ComboBox cboClasificacion;
        private System.Windows.Forms.ComboBox cboEstatus;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.DataGridViewTextBoxColumn idControlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn anioControlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codUniAdmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomUniAdmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noExpedienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nExpedienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fAperturaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fCierreDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nForjasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nLegajosDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSeccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSubSeccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idInstitutoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn formClasificatoriaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idUbicacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn observacionesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEstatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idClasificacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource controlBindingSource3;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TabControl Control;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSubirDocumento;
        private System.Windows.Forms.Button btnDescargarDocumento;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvDocumentos;
        private System.Windows.Forms.Button btnBorrarDocumento;
        private System.Windows.Forms.Panel panel2;
    }
}
