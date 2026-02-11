using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SIGC_TESChi
{
    public partial class CaratulaExpediente : UserControl
    {

        // Cadena de conexión
        private static string connectionString => Program.ConnectionString;

        private PrintDocument printDocument1 = new PrintDocument();
        private Bitmap bmpCaratula; // aquí guardaremos la imagen del panel

        private Dictionary<Label, Color> backColorsLabels = new Dictionary<Label, Color>();
        private Dictionary<Label, BorderStyle> borderLabels = new Dictionary<Label, BorderStyle>();
        private Dictionary<Control, bool> estadoVisibilidad = new Dictionary<Control, bool>();

        private Dictionary<TextBox, BorderStyle> bordesTextBox = new Dictionary<TextBox, BorderStyle>();

        private Dictionary<TextBox, Color> fondosTextBox = new Dictionary<TextBox, Color>();

        private Dictionary<CheckBox, FlatStyle> estilosCheck = new Dictionary<CheckBox, FlatStyle>();

        private Dictionary<TextBox, bool> multilineTextBox =
    new Dictionary<TextBox, bool>();

        private Dictionary<MaskedTextBox, BorderStyle> bordesMasked =
    new Dictionary<MaskedTextBox, BorderStyle>();

        private Dictionary<MaskedTextBox, Color> fondosMasked =
            new Dictionary<MaskedTextBox, Color>();

        private ToolTip toolTip;

        public CaratulaExpediente()
        {
            InitializeComponent();

            
            this.AutoScroll = true;

            this.Width = 800;
            this.Height = 1600;

            printDocument1.PrintPage += printDocument1_PrintPage;

            //EVENTOS
            Load += CaratulaExpediente_Load;
            btnImprimir.Click += btnImprimir_Click;

            // ToolTips (IGUAL QUE UBICACIONES)
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 200;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;

            btnImprimir.MouseEnter += (s, e) => toolTip.Show("Boton para Imprimir Caratula", btnImprimir);
            btnImprimir.MouseLeave += (s, e) => toolTip.Hide(btnImprimir);

        }

        private IEnumerable<Control> ObtenerTodosLosControles(Control padre)
        {
            foreach (Control c in padre.Controls)
            {
                yield return c;

                if (c.HasChildren)
                {
                    foreach (Control hijo in ObtenerTodosLosControles(c))
                    {
                        yield return hijo;
                    }
                }
            }
        }


        private void PrepararControlesParaImpresion()
        {
            foreach (Control c in ObtenerTodosLosControles(pnlCaratula))
            {
                // ===== TEXTBOX =====
                if (c is TextBox txt)
                {
                    bordesTextBox[txt] = txt.BorderStyle;
                    fondosTextBox[txt] = txt.BackColor;

                    txt.BorderStyle = BorderStyle.None;
                    txt.BackColor = Color.White;
                    txt.ReadOnly = true;
                }

                // ===== MASKEDTEXTBOX (🔥 CLAVE 🔥) =====
                if (c is MaskedTextBox mtxt)
                {
                    bordesMasked[mtxt] = mtxt.BorderStyle;
                    fondosMasked[mtxt] = mtxt.BackColor;

                    mtxt.BorderStyle = BorderStyle.None;
                    mtxt.BackColor = Color.White;
                    mtxt.ReadOnly = true;
                }

                // ===== CHECKBOX =====
                if (c is CheckBox chk)
                {
                    estilosCheck[chk] = chk.FlatStyle;
                    chk.FlatStyle = FlatStyle.Flat;
                    chk.BackColor = Color.White;
                }
            }
        }



        private void RestaurarControlesDespuesImpresionAvanzada()
        {
            foreach (var txt in bordesTextBox.Keys)
            {
                txt.BorderStyle = bordesTextBox[txt];
                txt.BackColor = fondosTextBox[txt];
                txt.ReadOnly = false;
            }

            foreach (var mtxt in bordesMasked.Keys)
            {
                mtxt.BorderStyle = bordesMasked[mtxt];
                mtxt.BackColor = fondosMasked[mtxt];
                mtxt.ReadOnly = false;
            }

            foreach (var chk in estilosCheck.Keys)
            {
                chk.FlatStyle = estilosCheck[chk];
            }

            bordesTextBox.Clear();
            fondosTextBox.Clear();
            bordesMasked.Clear();
            fondosMasked.Clear();
            estilosCheck.Clear();
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

            using (var con = Db.CreateConnection())
            {
                con.Open();
                // consultas reales aquí
            }


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
            PrepararControlesParaImpresion();   // 🔥 AQUÍ
            GuardarYOcultarControlesParaImpresion();

            CapturarPanelCompleto();

            RestaurarLabelsDespuesImpresion();
            RestaurarControlesDespuesImpresionAvanzada(); // 🔥 AQUÍ
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

            // ====== CÓDIGO UNIDAD ADMINISTRATIVA ======
            lblCodigoUnidadImpresion.Text = cboCodigoUnidadAdministrativa.SelectedItem?.ToString() ?? "";
            lblCodigoUnidadImpresion.Location = cboCodigoUnidadAdministrativa.Location;
            lblCodigoUnidadImpresion.Size = cboCodigoUnidadAdministrativa.Size;
            lblCodigoUnidadImpresion.BackColor = Color.Transparent;
            lblCodigoUnidadImpresion.BorderStyle = BorderStyle.None;
            lblCodigoUnidadImpresion.Visible = true;
            cboCodigoUnidadAdministrativa.Visible = false;

            // ====== NOMBRE UNIDAD ADMINISTRATIVA ======
            lblNombreUnidadImpresion.Text = cboNombreUnidadAdministrativa.SelectedItem?.ToString() ?? "";
            lblNombreUnidadImpresion.Location = cboNombreUnidadAdministrativa.Location;
            lblNombreUnidadImpresion.Size = cboNombreUnidadAdministrativa.Size;
            lblNombreUnidadImpresion.BackColor = Color.Transparent;
            lblNombreUnidadImpresion.BorderStyle = BorderStyle.None;
            lblNombreUnidadImpresion.Visible = true;
            cboNombreUnidadAdministrativa.Visible = false;

            // ====== FONDO DOCUMENTAL ======
            lblFondoDocumentalImpresion.Text = cboFondoDocumental.SelectedItem?.ToString() ?? "";
            lblFondoDocumentalImpresion.Location = cboFondoDocumental.Location;
            lblFondoDocumentalImpresion.Size = cboFondoDocumental.Size;
            lblFondoDocumentalImpresion.BackColor = Color.Transparent;
            lblFondoDocumentalImpresion.BorderStyle = BorderStyle.None;
            lblFondoDocumentalImpresion.Visible = true;
            cboFondoDocumental.Visible = false;

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

            lblCodigoUnidadImpresion.Visible = false;
            lblNombreUnidadImpresion.Visible = false;
            lblFondoDocumentalImpresion.Visible = false;

            cboCodigoUnidadAdministrativa.Visible = true;
            cboNombreUnidadAdministrativa.Visible = true;
            cboFondoDocumental.Visible = true;

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
            int cursor = txtSubserieDocumental.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtSubserieDocumental.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtSubserieDocumental.Text != limpio)
            {
                txtSubserieDocumental.Text = limpio;
                txtSubserieDocumental.SelectionStart = Math.Min(cursor, txtSubserieDocumental.Text.Length);
            }
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
            int cursor = txtNombreExpediente.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtNombreExpediente.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtNombreExpediente.Text != limpio)
            {
                txtNombreExpediente.Text = limpio;
                txtNombreExpediente.SelectionStart = Math.Min(cursor, txtNombreExpediente.Text.Length);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.P))
            {
                btnImprimir.PerformClick(); // Ejecuta el botón Imprimir
                return true; // Indica que la tecla fue manejada
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtNoExpediente_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtNoExpediente.SelectionStart;

            // 1️⃣ Permitir SOLO números
            string limpio = Regex.Replace(
                txtNoExpediente.Text,
                @"[^0-9]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo (no afecta, pero se conserva)
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtNoExpediente.Text != limpio)
            {
                txtNoExpediente.Text = limpio;
                txtNoExpediente.SelectionStart =
                    Math.Min(cursor, txtNoExpediente.Text.Length);
            }

        }

        private void txtTLegajos_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtTLegajos.SelectionStart;

            // 1️⃣ Permitir SOLO números
            string limpio = Regex.Replace(
                txtTLegajos.Text,
                @"[^0-9]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo (no afecta, pero se conserva)
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtTLegajos.Text != limpio)
            {
                txtTLegajos.Text = limpio;
                txtTLegajos.SelectionStart =
                    Math.Min(cursor, txtTLegajos.Text.Length);
            }

        }

        private void txtTDocumentosCierre_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtTDocumentosCierre.SelectionStart;

            // 1️⃣ Permitir SOLO números
            string limpio = Regex.Replace(
                txtTDocumentosCierre.Text,
                @"[^0-9]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo (no afecta, pero se conserva)
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtTDocumentosCierre.Text != limpio)
            {
                txtTDocumentosCierre.Text = limpio;
                txtTDocumentosCierre.SelectionStart =
                    Math.Min(cursor, txtTDocumentosCierre.Text.Length);
            }

        }

        private void txtAsunto_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtAsunto.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtAsunto.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtAsunto.Text != limpio)
            {
                txtAsunto.Text = limpio;
                txtAsunto.SelectionStart = Math.Min(cursor, txtAsunto.Text.Length);
            }
        }

        private void txtSubfondoDocumental_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            int cursor = txtSubfondoDocumental.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtSubfondoDocumental.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtSubfondoDocumental.Text != limpio)
            {
                txtSubfondoDocumental.Text = limpio;
                txtSubfondoDocumental.SelectionStart = Math.Min(cursor, txtSubfondoDocumental.Text.Length);
            }
        }

        private void txtObservaciones_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtObservaciones.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtObservaciones.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtObservaciones.Text != limpio)
            {
                txtObservaciones.Text = limpio;
                txtObservaciones.SelectionStart = Math.Min(cursor, txtObservaciones.Text.Length);
            }
        }

        private void txtSubserieDocumental_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtSubserieDocumental.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtSubserieDocumental.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtSubserieDocumental.Text != limpio)
            {
                txtSubserieDocumental.Text = limpio;
                txtSubserieDocumental.SelectionStart = Math.Min(cursor, txtSubserieDocumental.Text.Length);
            }
        }

        private void txtSerieDocumental_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtSerieDocumental.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtSerieDocumental.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtSerieDocumental.Text != limpio)
            {
                txtSerieDocumental.Text = limpio;
                txtSerieDocumental.SelectionStart = Math.Min(cursor, txtSerieDocumental.Text.Length);
            }
        }

        private void txtSubfondoDocumental_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtSubfondoDocumental.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtSubfondoDocumental.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtSubfondoDocumental.Text != limpio)
            {
                txtSubfondoDocumental.Text = limpio;
                txtSubfondoDocumental.SelectionStart = Math.Min(cursor, txtSubfondoDocumental.Text.Length);
            }
        }

        private void txtNoLegajo_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtNoLegajo.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtNoLegajo.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtNoLegajo.Text != limpio)
            {
                txtNoLegajo.Text = limpio;
                txtNoLegajo.SelectionStart = Math.Min(cursor, txtNoLegajo.Text.Length);
            }
        }
    }
}
