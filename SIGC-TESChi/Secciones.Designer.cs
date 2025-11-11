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
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.sGCTESCHIDataSet9 = new SIGC_TESChi.SGCTESCHIDataSet9();
            this.seccionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.seccionTableAdapter = new SIGC_TESChi.SGCTESCHIDataSet9TableAdapters.SeccionTableAdapter();
            this.sGCTESCHIDataSet10 = new SIGC_TESChi.SGCTESCHIDataSet10();
            this.subSeccionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.subSeccionTableAdapter = new SIGC_TESChi.SGCTESCHIDataSet10TableAdapters.SubSeccionTableAdapter();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seccionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subSeccionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1249, 835);
            this.panel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1239, 250);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(3, 278);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1239, 550);
            this.panel2.TabIndex = 2;
            // 
            // sGCTESCHIDataSet9
            // 
            this.sGCTESCHIDataSet9.DataSetName = "SGCTESCHIDataSet9";
            this.sGCTESCHIDataSet9.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // seccionBindingSource
            // 
            this.seccionBindingSource.DataMember = "Seccion";
            this.seccionBindingSource.DataSource = this.sGCTESCHIDataSet9;
            // 
            // seccionTableAdapter
            // 
            this.seccionTableAdapter.ClearBeforeFill = true;
            // 
            // sGCTESCHIDataSet10
            // 
            this.sGCTESCHIDataSet10.DataSetName = "SGCTESCHIDataSet10";
            this.sGCTESCHIDataSet10.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // subSeccionBindingSource
            // 
            this.subSeccionBindingSource.DataMember = "SubSeccion";
            this.subSeccionBindingSource.DataSource = this.sGCTESCHIDataSet10;
            // 
            // subSeccionTableAdapter
            // 
            this.subSeccionTableAdapter.ClearBeforeFill = true;
            // 
            // Secciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "Secciones";
            this.Size = new System.Drawing.Size(1249, 838);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seccionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sGCTESCHIDataSet10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subSeccionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource subSeccionBindingSource;
        private SGCTESCHIDataSet10 sGCTESCHIDataSet10;
        private System.Windows.Forms.BindingSource seccionBindingSource;
        private SGCTESCHIDataSet9 sGCTESCHIDataSet9;
        private SGCTESCHIDataSet9TableAdapters.SeccionTableAdapter seccionTableAdapter;
        private SGCTESCHIDataSet10TableAdapters.SubSeccionTableAdapter subSeccionTableAdapter;
    }
}
