using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;



namespace SIGC_TESChi
{
    public partial class CaratulaExpediente : UserControl
    {

        private PrintDocument printDocument1 = new PrintDocument();
        private Bitmap bmpCaratula; // aquí guardaremos la imagen del panel

        private Dictionary<Label, Color> backColorsLabels = new Dictionary<Label, Color>();
        private Dictionary<Label, BorderStyle> borderLabels = new Dictionary<Label, BorderStyle>();
        private Dictionary<Control, bool> estadoVisibilidad = new Dictionary<Control, bool>();




        string connectionString =
               @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public CaratulaExpediente()
        {
            InitializeComponent();

            this.AutoScroll = true;

            this.Width = 800;
            this.Height = 1600;

            printDocument1.PrintPage += printDocument1_PrintPage;

        }

        private void GuardarYOcultarControlesParaImpresion()
        {
            estadoVisibilidad.Clear();

            foreach (Control c in pnlCaratula.Controls)
            {
                estadoVisibilidad[c] = c.Visible;

                // SOLO ocultamos controles que YA tienen Label de impresión
                if (c is ComboBox || c is DateTimePicker)
                {
                    c.Visible = false;
                }
            }
        }


        private void RestaurarControlesDespuesImpresion()
        {
            foreach (var item in estadoVisibilidad)
            {
                item.Key.Visible = item.Value;
            }

            estadoVisibilidad.Clear();
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void CaratulaExpediente_Load(object sender, EventArgs e)
        {
            CargarSecciones();
            CargarSubsecciones();
            CargarCodigoUnidadAdministrativa();
            CargarNombreUnidadAdministrativa();
            CargarFondoDocumental();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigoUnidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void cboSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboSubSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormCaratula_Load(object sender, EventArgs e)
        {
            CargarSecciones();
            CargarSubsecciones();
        }

        private void CargarCodigoUnidadAdministrativa()
        {
            try
            {
                cboCodigoUnidadAdministrativa.Items.Clear();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string q = "SELECT cUniAdmin FROM UnidadAdministrativa ORDER BY cUniAdmin";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        // Sólo el ID
                        cboCodigoUnidadAdministrativa.Items.Add(dr["cUniAdmin"].ToString());
                    }
                }

                cboCodigoUnidadAdministrativa.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Codigo de Unidad Administrativa: " + ex.Message);
            }
        }

        private void CargarNombreUnidadAdministrativa()
        {
            try
            {
                cboNombreUnidadAdministrativa.Items.Clear();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string q = "SELECT nUniAdmin FROM UnidadAdministrativa ORDER BY nUniAdmin";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        // Sólo el ID
                        cboNombreUnidadAdministrativa.Items.Add(dr["nUniAdmin"].ToString());
                    }
                }

                cboNombreUnidadAdministrativa.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Nombre de Unidad Administrativa: " + ex.Message);
            }
        }

        private void CargarSecciones()
        {
            try
            {
                cboSeccion.Items.Clear();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string q = "SELECT claveSeccion FROM Seccion ORDER BY claveSeccion";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        // Sólo el ID
                        cboSeccion.Items.Add(dr["claveSeccion"].ToString());
                    }
                }

                cboSeccion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar secciones: " + ex.Message);
            }
        }



        private void CargarSubsecciones()
        {
            try
            {
                cboSubSeccion.Items.Clear();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string q = "SELECT claveSubSeccion FROM SubSeccion ORDER BY claveSubSeccion";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        // Sólo el ID
                        cboSubSeccion.Items.Add(dr["claveSubSeccion"].ToString());
                    }
                }

                cboSubSeccion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar subsecciones: " + ex.Message);
            }
        }

        private void CargarFondoDocumental()
        {
            try
            {
                cboFondoDocumental.Items.Clear();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string q = "SELECT dInstituto FROM Instituto ORDER BY dInstituto";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        // Sólo el ID
                        cboFondoDocumental.Items.Add(dr["dInstituto"].ToString());
                    }
                }

                cboFondoDocumental.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Fondo Documental: " + ex.Message);
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void CaratulaExpediente_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            PrepararVistaParaImpresion();
            PrepararLabelsParaImpresion();
            GuardarYOcultarControlesParaImpresion();  
            CapturarPanelCompleto();
            RestaurarLabelsDespuesImpresion();
            RestaurarControlesDespuesImpresion();      
            RestaurarVistaNormal();



            if (bmpCaratula == null)
            {
                MessageBox.Show("No se pudo capturar la carátula.");
                return;
            }

            PrintDialog pd = new PrintDialog();
            pd.Document = printDocument1;

            if (pd.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void CapturarPanelCompleto()
        {
            Rectangle contenido = pnlCaratula.DisplayRectangle;

            if (contenido.Width <= 0 || contenido.Height <= 0)
                return;

            bmpCaratula = new Bitmap(contenido.Width, contenido.Height);

            int scrollX = pnlCaratula.HorizontalScroll.Value;
            int scrollY = pnlCaratula.VerticalScroll.Value;

            pnlCaratula.AutoScrollPosition = new Point(0, 0);

            pnlCaratula.DrawToBitmap(bmpCaratula, new Rectangle(0, 0, contenido.Width, contenido.Height));

            pnlCaratula.AutoScrollPosition = new Point(scrollX, scrollY);
        }


        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (bmpCaratula == null)
            {
                e.HasMorePages = false;
                return;
            }

            // Usar casi TODA la hoja física
            Rectangle areaImprimible = e.PageBounds;

            // Margen mínimo manual (para no pegarlo totalmente al borde)
            int margen = 10; // puedes probar 5, 0, 15 según la impresora
            areaImprimible = new Rectangle(
                areaImprimible.Left + margen,
                areaImprimible.Top + margen,
                areaImprimible.Width - margen * 2,
                areaImprimible.Height - margen * 2
            );

            // Calcular escala para que quepa TODO
            float ratioX = (float)areaImprimible.Width / bmpCaratula.Width;
            float ratioY = (float)areaImprimible.Height / bmpCaratula.Height;
            float ratio = Math.Min(ratioX, ratioY);

            // Si quieres aún más “zoom” (opcional, coméntalo si se llega a recortar)
            // ratio *= 1.02f;

            int newWidth = (int)(bmpCaratula.Width * ratio);
            int newHeight = (int)(bmpCaratula.Height * ratio);

            // Aquí lo pegamos arriba-izquierda para que use al máximo el área
            int posX = areaImprimible.Left;
            int posY = areaImprimible.Top;

            // Si lo quieres centrado, usa esto en lugar de lo de arriba:
            // int posX = areaImprimible.Left + (areaImprimible.Width - newWidth) / 2;
            // int posY = areaImprimible.Top + (areaImprimible.Height - newHeight) / 2;

            e.Graphics.DrawImage(bmpCaratula, new Rectangle(posX, posY, newWidth, newHeight));

            e.HasMorePages = false;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Controles dentro de pnlCaratula:");
            foreach (Control c in pnlCaratula.Controls)
            {
                sb.AppendLine($"- {c.Name} ({c.GetType().Name})");
            }

            MessageBox.Show(sb.ToString());
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void pnlCaratula_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PrepararVistaParaImpresion()
        {
            // ====== SECCIÓN ======
            lblSeccionImpresion.Text = cboSeccion.SelectedItem != null
                ? cboSeccion.SelectedItem.ToString()
                : "";

            lblSeccionImpresion.Location = cboSeccion.Location;
            lblSeccionImpresion.Size = cboSeccion.Size;
            lblSeccionImpresion.BackColor = Color.Transparent;
            lblSeccionImpresion.BorderStyle = BorderStyle.None;
            lblSeccionImpresion.Visible = true;
            cboSeccion.Visible = false;

            // ====== SUBSECCIÓN ======
            lblSubSeccionImpresion.Text = cboSubSeccion.SelectedItem != null
                ? cboSubSeccion.SelectedItem.ToString()
                : "";

            lblSubSeccionImpresion.Location = cboSubSeccion.Location;
            lblSubSeccionImpresion.Size = cboSubSeccion.Size;
            lblSubSeccionImpresion.BackColor = Color.Transparent;
            lblSubSeccionImpresion.BorderStyle = BorderStyle.None;
            lblSubSeccionImpresion.Visible = true;
            cboSubSeccion.Visible = false;

            // ====== FECHA APERTURA ======
            lblFechaRecepcionImpresion.Text = dtpApertura.Value.ToString("dd/MM/yyyy");
            lblFechaRecepcionImpresion.Location = dtpApertura.Location;
            lblFechaRecepcionImpresion.Size = dtpApertura.Size;
            lblFechaRecepcionImpresion.BackColor = Color.Transparent;
            lblFechaRecepcionImpresion.BorderStyle = BorderStyle.None;
            lblFechaRecepcionImpresion.Visible = true;
            dtpApertura.Visible = false;

            // ====== FECHA CIERRE ======
            lblFechaDocumentoImpresion.Text = dtpCierre.Value.ToString("dd/MM/yyyy");
            lblFechaDocumentoImpresion.Location = dtpCierre.Location;
            lblFechaDocumentoImpresion.Size = dtpCierre.Size;
            lblFechaDocumentoImpresion.BackColor = Color.Transparent;
            lblFechaDocumentoImpresion.BorderStyle = BorderStyle.None;
            lblFechaDocumentoImpresion.Visible = true;
            dtpCierre.Visible = false;
        }




        private void RestaurarVistaNormal()
        {
            // ===== LABELS DE IMPRESIÓN =====
            lblSeccionImpresion.Visible = false;
            lblSubSeccionImpresion.Visible = false;
            lblFechaRecepcionImpresion.Visible = false;
            lblFechaDocumentoImpresion.Visible = false;

            // ===== COMBOS =====
            cboSeccion.Visible = true;
            cboSubSeccion.Visible = true;

            cboFondoDocumental.Visible = true;
            cboCodigoUnidadAdministrativa.Visible = true;
            cboNombreUnidadAdministrativa.Visible = true;

            // ===== TEXTBOX =====
            txtNombreExpediente.Visible = true;
            txtNoExpediente.Visible = true;
            txtNoLegajo.Visible = true;
            txtTLegajos.Visible = true;
            txtAsunto.Visible = true;
            txtTDocumentosCierre.Visible = true;
            txtSubfondoDocumental.Visible = true;
            txtSerieDocumental.Visible = true;
            txtSubserieDocumental.Visible = true;
            txtObservaciones.Visible = true;

            // ===== CHECK / COMBO =====
            cboxAdministrativo.Visible = true;
            cboxJuridicoLegal.Visible = true;
            cboContable.Visible = true;

            cboxArchivoTramite.Visible = true;
            cboxArchivoConcentracion.Visible = true;
            cboxArchivoHistorico.Visible = true;

            cboxReservada.Visible = true;
            cboxConfidencial.Visible = true;

            // ===== DATE =====
            dtpApertura.Visible = true;
            dtpCierre.Visible = true;
        }



        private void PrepararLabelsParaImpresion()
        {
            backColorsLabels.Clear();
            borderLabels.Clear();

            foreach (Control c in pnlCaratula.Controls)
            {
                if (c is Label lbl)
                {
                    // Guardar estado original
                    backColorsLabels[lbl] = lbl.BackColor;
                    borderLabels[lbl] = lbl.BorderStyle;

                    // Quitar cuadro
                    lbl.BackColor = Color.Transparent;
                    lbl.BorderStyle = BorderStyle.None;
                }
            }
        }


        private void RestaurarLabelsDespuesImpresion()
        {
            foreach (var lbl in backColorsLabels.Keys)
            {
                lbl.BackColor = Color.Transparent;
                lbl.BorderStyle = BorderStyle.None;
            }

            backColorsLabels.Clear();
            borderLabels.Clear();
        }




        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cboxArchivoTramite_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreExpediente_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
