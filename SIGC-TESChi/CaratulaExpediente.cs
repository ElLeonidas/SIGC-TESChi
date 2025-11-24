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
            CargarSubsecciones(); // Si quieres todas juntas al inicio
        }

        private void CargarSecciones()
        {
            try
            {
                cboSeccion.Items.Clear();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string q = "SELECT idSeccion FROM Seccion ORDER BY idSeccion";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        // Sólo el ID
                        cboSeccion.Items.Add(dr["idSeccion"].ToString());
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

                    string q = "SELECT idSubSeccion FROM SubSeccion ORDER BY idSubSeccion";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        // Sólo el ID
                        cboSubSeccion.Items.Add(dr["idSubSeccion"].ToString());
                    }
                }

                cboSubSeccion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar subsecciones: " + ex.Message);
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

            // 2. Capturar el panel completo ya con labels en lugar de combos
            CapturarPanelCompleto();

            // 3. Restaurar vista normal para el usuario
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
            // ====== COMBOS ======
            // Sección
            if (cboSeccion.SelectedItem != null)
                lblSeccionImpresion.Text = cboSeccion.SelectedItem.ToString();
            else
                lblSeccionImpresion.Text = "";

            lblSeccionImpresion.Location = cboSeccion.Location;
            lblSeccionImpresion.Size = cboSeccion.Size;
            lblSeccionImpresion.Visible = true;
            cboSeccion.Visible = false;

            // Subsección
            if (cboSubSeccion.SelectedItem != null)
                lblSubSeccionImpresion.Text = cboSubSeccion.SelectedItem.ToString();
            else
                lblSubSeccionImpresion.Text = "";

            lblSubSeccionImpresion.Location = cboSubSeccion.Location;
            lblSubSeccionImpresion.Size = cboSubSeccion.Size;
            lblSubSeccionImpresion.Visible = true;
            cboSubSeccion.Visible = false;

            // ====== DATE TIME PICKERS ======
            // Ejemplo: dtpFechaRecepcion
            // Formato de fecha como la quieras ver en el formato
            lblFechaRecepcionImpresion.Text = dtpApertura.Value.ToString("dd/MM/yyyy");
            lblFechaRecepcionImpresion.Location = dtpApertura.Location;
            lblFechaRecepcionImpresion.Size = dtpApertura.Size;
            lblFechaRecepcionImpresion.Visible = true;
            dtpApertura.Visible = false;

            // Ejemplo: dtpFechaDocumento
            lblFechaDocumentoImpresion.Text = dtpCierre.Value.ToString("dd/MM/yyyy");
            lblFechaDocumentoImpresion.Location = dtpCierre.Location;
            lblFechaDocumentoImpresion.Size = dtpCierre.Size;
            lblFechaDocumentoImpresion.Visible = true;
            dtpCierre.Visible = false;

            // Si tienes más DateTimePicker, repites el mismo patrón
        }


        private void RestaurarVistaNormal()
        {
            // Combos
            lblSeccionImpresion.Visible = false;
            cboSeccion.Visible = true;

            lblSubSeccionImpresion.Visible = false;
            cboSubSeccion.Visible = true;

            // DateTimePicker
            lblFechaRecepcionImpresion.Visible = false;
            dtpApertura.Visible = true;

            lblFechaDocumentoImpresion.Visible = false;
            dtpCierre.Visible = true;

            // Repite con los demás controles que “maquilles” para impresión
        }


        private void label35_Click(object sender, EventArgs e)
        {

        }
    }
}
