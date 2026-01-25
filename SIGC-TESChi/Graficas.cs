using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Printing;

// PDF
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;

namespace SIGC_TESChi
{
    public partial class Graficas : UserControl
    {
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";
        private ToolTip toolTip;

        private PrintDocument printDocument1 = new PrintDocument();
        private Bitmap bmpGrafica;


        public Graficas()
        {
            InitializeComponent();
            CargarCombos();
            CargarClasificaciones();
            toolTip = new ToolTip();

            printDocument1.PrintPage += printDocument1_PrintPage;

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;  // Visible 5 segundos
            toolTip.InitialDelay = 200;   // Aparece tras 0.2 segundos
            toolTip.ReshowDelay = 100;    // Retardo entre botones
            toolTip.ShowAlways = true;    // Siempre visible

            // Asignar eventos de mouse para mostrar tooltips
            btnExportarPDF.MouseEnter += (s, e) => toolTip.Show("Boton para Exportar Gráfica a PDF", btnExportarPDF);
            btnExportarPDF.MouseLeave += (s, e) => toolTip.Hide(btnExportarPDF);
            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar la Grafica", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);
        }

        private void CapturarGrafica()
        {
            if (chartGrafica.Width <= 0 || chartGrafica.Height <= 0)
                return;

            bmpGrafica = new Bitmap(chartGrafica.Width, chartGrafica.Height);
            chartGrafica.DrawToBitmap(
                bmpGrafica,
                new Rectangle(0, 0, chartGrafica.Width, chartGrafica.Height)
            );
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (bmpGrafica == null)
            {
                e.HasMorePages = false;
                return;
            }

            Rectangle area = e.PageBounds;

            int margen = 20;
            area = new Rectangle(
                area.Left + margen,
                area.Top + margen,
                area.Width - margen * 2,
                area.Height - margen * 2
            );

            float ratioX = (float)area.Width / bmpGrafica.Width;
            float ratioY = (float)area.Height / bmpGrafica.Height;
            float ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(bmpGrafica.Width * ratio);
            int newHeight = (int)(bmpGrafica.Height * ratio);

            int posX = area.Left + (area.Width - newWidth) / 2;
            int posY = area.Top + (area.Height - newHeight) / 2;

            e.Graphics.DrawImage(
                bmpGrafica,
                new Rectangle(posX, posY, newWidth, newHeight)
            );

            e.HasMorePages = false;
        }


        // Paleta de colores
        private readonly Color[] colores = new Color[]
        {
            Color.FromArgb(52, 152, 219),
            Color.FromArgb(46, 204, 113),
            Color.FromArgb(230, 126, 34),
            Color.FromArgb(231, 76, 60),
            Color.FromArgb(155, 89, 182),
            Color.FromArgb(241, 196, 15),
            Color.FromArgb(26, 188, 156),
        };

        // LIMPIAR GRÁFICA
        private void LimpiarGrafica()
        {
            chartGrafica.Series.Clear();
            chartGrafica.Titles.Clear();
            chartGrafica.Legends.Clear();
        }

        // BOTÓN BUSCAR
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            GenerarGrafica();
        }

        // GENERAR GRÁFICA CON FILTROS
        private void GenerarGrafica()
        {
            if (cmbTipoGrafica.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona el tipo de gráfica.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpFechaFin.Value.Date < dtpFechaInicio.Value.Date)
            {
                MessageBox.Show("La fecha final no puede ser menor que la inicial.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> seleccionadas = clbClasificacion.CheckedItems.Cast<string>().ToList();
            bool usarTodas = seleccionadas.Count == 0;

            DataTable datos = ConsultarDatos();

            // Filtrar si seleccionaron clasificaciones
            if (!usarTodas && datos.Rows.Count > 0)
            {
                datos = datos.AsEnumerable()
                    .Where(r => seleccionadas.Contains(r.Field<string>("Clasificacion")))
                    .CopyToDataTable();
            }

            if (datos.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron datos con los filtros seleccionados.");
                LimpiarGrafica();
                return;
            }

            DibujarGrafica(datos);
        }

        // CONSULTAR DATOS
        private DataTable ConsultarDatos()
        {
            DataTable datos = new DataTable();
            datos.Columns.Add("Anio", typeof(int));
            datos.Columns.Add("Clasificacion");
            datos.Columns.Add("Cantidad", typeof(int));

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string q = @"
                        SELECT 
                            YEAR(ctrl.fApertura) AS Anio,
                            c.dClasificacion AS Clasificacion,
                            COUNT(*) AS Cantidad
                        FROM Control ctrl
                        INNER JOIN Clasificacion c ON ctrl.idClasificacion = c.idClasificacion
                        WHERE ctrl.fApertura >= @FechaInicio
                          AND ctrl.fApertura <= @FechaFin
                        GROUP BY YEAR(ctrl.fApertura), c.dClasificacion
                        ORDER BY Anio, Clasificacion";

                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@FechaInicio", dtpFechaInicio.Value.Date);
                    cmd.Parameters.AddWithValue("@FechaFin", dtpFechaFin.Value.Date);

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        datos.Rows.Add(
                            Convert.ToInt32(dr["Anio"]),
                            dr["Clasificacion"].ToString(),
                            Convert.ToInt32(dr["Cantidad"])
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar datos:\n" + ex.Message);
            }

            return datos;
        }

        // DIBUJAR GRÁFICA
        private void DibujarGrafica(DataTable datos)
        {
            LimpiarGrafica();

            Legend leyenda = new Legend("Leyenda")
            {
                Docking = Docking.Right,
                LegendStyle = LegendStyle.Table,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            chartGrafica.Legends.Add(leyenda);

            string tipo = cmbTipoGrafica.SelectedItem.ToString();

            var years = datos.AsEnumerable()
                             .Select(r => r.Field<int>("Anio"))
                             .Distinct()
                             .OrderBy(y => y)
                             .ToList();

            if (tipo == "Pastel")
            {
                if (years.Count > 1)
                    MessageBox.Show("Pastel solo puede mostrar un año. Se mostrará el primero.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                int anio = years.First();
                var rows = datos.AsEnumerable().Where(r => r.Field<int>("Anio") == anio);
                int total = rows.Sum(r => r.Field<int>("Cantidad"));

                Series serie = new Series($"Año {anio}")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true
                };

                int idx = 0;
                foreach (var r in rows)
                {
                    string clas = r.Field<string>("Clasificacion");
                    int cant = r.Field<int>("Cantidad");
                    double pct = Math.Round(cant * 100.0 / total, 2);

                    int p = serie.Points.AddXY(clas, cant);
                    serie.Points[p].Color = colores[idx % colores.Length];
                    serie.Points[p].Label = $"{cant} ({pct}%)";
                    serie.Points[p].LegendText = clas;
                    idx++;
                }

                chartGrafica.Series.Add(serie);
                chartGrafica.Titles.Add($"Reporte de Clasificación – Año {anio}");
                return;
            }

            // Barras o Áreas
            int colorIdx = 0;
            foreach (int anio in years)
            {
                Series serie = new Series($"Año {anio}")
                {
                    ChartType = tipo == "Barras" ? SeriesChartType.Column : SeriesChartType.Area,
                    Color = tipo == "Barras" ? colores[colorIdx % colores.Length] : Color.FromArgb(120, colores[colorIdx % colores.Length]),
                    IsValueShownAsLabel = true,
                    LegendText = $"Año {anio}"
                };

                var rows = datos.AsEnumerable().Where(r => r.Field<int>("Anio") == anio);

                foreach (var r in rows)
                {
                    string clas = r.Field<string>("Clasificacion");
                    int cant = r.Field<int>("Cantidad");
                    int total = rows.Sum(x => x.Field<int>("Cantidad"));
                    double pct = Math.Round(cant * 100.0 / total, 2);

                    int point = serie.Points.AddXY(clas, cant);
                    serie.Points[point].Label = $"{cant} ({pct}%)";
                }

                chartGrafica.Series.Add(serie);
                colorIdx++;
            }

            chartGrafica.Titles.Add(tipo == "Barras" ? "Reporte de Clasificación por Año" : "Reporte de Clasificación por Año");
        }

        // EXPORTAR PDF
        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = "Grafica.pdf"
                };

                if (save.ShowDialog() == DialogResult.OK)
                {
                    string temp = Path.GetTempFileName();
                    chartGrafica.SaveImage(temp, ChartImageFormat.Png);

                    PdfDocument pdf = new PdfDocument();
                    PdfPage page = pdf.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XImage img = XImage.FromFile(temp);

                    double availableWidth = page.Width.Point - 40;
                    double availableHeight = page.Height.Point - 40;
                    double ratio = Math.Min(availableWidth / img.PixelWidth, availableHeight / img.PixelHeight);
                    double width = img.PixelWidth * ratio;
                    double height = img.PixelHeight * ratio;

                    gfx.DrawImage(img, (page.Width.Point - width) / 2, 20, width, height);

                    pdf.Save(save.FileName);
                    pdf.Close();
                    File.Delete(temp);

                    MessageBox.Show("PDF exportado correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar PDF: " + ex.Message);
            }
        }

        // CARGAR COMBOS
        private void CargarCombos()
        {
            cmbEstatus.Items.Clear();
            cmbEstatus.Items.Add("Abierto");
            cmbEstatus.Items.Add("Cerrado");
            cmbEstatus.Items.Add("En proceso");

            cmbTipoGrafica.Items.Clear();
            cmbTipoGrafica.Items.Add("Barras");
            cmbTipoGrafica.Items.Add("Áreas");
            cmbTipoGrafica.Items.Add("Pastel");
        }

        // CARGAR CLASIFICACIONES
        private void CargarClasificaciones()
        {
            clbClasificacion.Items.Clear();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string q = "SELECT dClasificacion FROM Clasificacion ORDER BY idClasificacion";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clbClasificacion.Items.Add(dr["dClasificacion"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clasificaciones: " + ex.Message);
            }
        }

        private void btnExportarPDF_Click_1(object sender, EventArgs e)
        {
            if (chartGrafica.Series.Count == 0)
            {
                MessageBox.Show("No hay gráfica para imprimir.");
                return;
            }

            CapturarGrafica();

            if (bmpGrafica == null)
            {
                MessageBox.Show("No se pudo capturar la gráfica.");
                return;
            }

            PrintDialog pd = new PrintDialog();
            pd.Document = printDocument1;

            if (pd.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
    }
}
