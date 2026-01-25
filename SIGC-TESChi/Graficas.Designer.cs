namespace SIGC_TESChi
{
    partial class Graficas
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Graficas));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlGrafica = new System.Windows.Forms.Panel();
            this.chartGrafica = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel2 = new System.Windows.Forms.Panel();
            this.clbClasificacion = new System.Windows.Forms.CheckedListBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnExportarPDF = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTipoGrafica = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEstatus = new System.Windows.Forms.ComboBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnlGrafica.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartGrafica)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pnlGrafica);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(938, 682);
            this.panel1.TabIndex = 0;
            // 
            // pnlGrafica
            // 
            this.pnlGrafica.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlGrafica.Controls.Add(this.chartGrafica);
            this.pnlGrafica.Location = new System.Drawing.Point(2, 222);
            this.pnlGrafica.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlGrafica.Name = "pnlGrafica";
            this.pnlGrafica.Size = new System.Drawing.Size(930, 454);
            this.pnlGrafica.TabIndex = 1;
            // 
            // chartGrafica
            // 
            chartArea2.Name = "ChartArea1";
            this.chartGrafica.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartGrafica.Legends.Add(legend2);
            this.chartGrafica.Location = new System.Drawing.Point(-2, -2);
            this.chartGrafica.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chartGrafica.Name = "chartGrafica";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartGrafica.Series.Add(series2);
            this.chartGrafica.Size = new System.Drawing.Size(929, 453);
            this.chartGrafica.TabIndex = 0;
            this.chartGrafica.Text = "chart1";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.clbClasificacion);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.btnExportarPDF);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cmbTipoGrafica);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.cmbEstatus);
            this.panel2.Controls.Add(this.dtpFechaFin);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dtpFechaInicio);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(930, 204);
            this.panel2.TabIndex = 0;
            // 
            // clbClasificacion
            // 
            this.clbClasificacion.FormattingEnabled = true;
            this.clbClasificacion.Location = new System.Drawing.Point(472, 44);
            this.clbClasificacion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.clbClasificacion.Name = "clbClasificacion";
            this.clbClasificacion.Size = new System.Drawing.Size(200, 139);
            this.clbClasificacion.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscar.BackgroundImage")));
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscar.Location = new System.Drawing.Point(793, 67);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(45, 49);
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExportarPDF.BackgroundImage")));
            this.btnExportarPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExportarPDF.Location = new System.Drawing.Point(743, 67);
            this.btnExportarPDF.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(45, 49);
            this.btnExportarPDF.TabIndex = 11;
            this.btnExportarPDF.UseVisualStyleBackColor = true;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(88, 118);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Estatus:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(376, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Clasificación:";
            // 
            // cmbTipoGrafica
            // 
            this.cmbTipoGrafica.FormattingEnabled = true;
            this.cmbTipoGrafica.Location = new System.Drawing.Point(157, 156);
            this.cmbTipoGrafica.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbTipoGrafica.Name = "cmbTipoGrafica";
            this.cmbTipoGrafica.Size = new System.Drawing.Size(196, 21);
            this.cmbTipoGrafica.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(42, 159);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Tipo de Gráfica:";
            // 
            // cmbEstatus
            // 
            this.cmbEstatus.FormattingEnabled = true;
            this.cmbEstatus.Location = new System.Drawing.Point(157, 118);
            this.cmbEstatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbEstatus.Name = "cmbEstatus";
            this.cmbEstatus.Size = new System.Drawing.Size(196, 21);
            this.cmbEstatus.TabIndex = 6;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Location = new System.Drawing.Point(157, 80);
            this.dtpFechaFin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(196, 20);
            this.dtpFechaFin.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Fecha Final:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Location = new System.Drawing.Point(157, 44);
            this.dtpFechaInicio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(196, 20);
            this.dtpFechaInicio.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha Inicio:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Visualizacion de Graficas ";
            // 
            // Graficas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Graficas";
            this.Size = new System.Drawing.Size(937, 681);
            this.panel1.ResumeLayout(false);
            this.pnlGrafica.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartGrafica)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlGrafica;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTipoGrafica;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEstatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox clbClasificacion;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnExportarPDF;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGrafica;
    }
}
