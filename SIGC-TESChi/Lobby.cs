using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Lobby : UserControl
    {
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";
        private Timer alertaTimer;
        private ToolTip toolTip;
        private DataTable dtEventos;


        public Lobby()
        {
            InitializeComponent();

            toolTip = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 200,
                ReshowDelay = 100,
                ShowAlways = true
            };

            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Agregar evento", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Modificar evento", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Eliminar evento", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Limpiar formulario", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);

        }

        private void Lobby_Load(object sender, EventArgs e)
        {
            ConfigurarDGV();        // 1️⃣ primero
            CargarTiposEvento();
            CargarEventosDelMes();  // 2️⃣ luego datos
            AplicarTemaLobby();     // 3️⃣ al final el diseño

            ConfigurarDataGridViewOscuro(dgvEventos);

            dgvEventos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEventos.AllowUserToResizeColumns = false;
            dgvEventos.AllowUserToResizeRows = false;
            dgvEventos.RowHeadersVisible = false;

            txtUsuario.Text = SessionData.NombreCompleto;
            txtUsuario.ReadOnly = true;

            chkAlerta.Checked = true;
            nudMinutos.Value = 10;
            cmbModalidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModalidad.Items.AddRange(new string[] { "Presencial", "En línea" });

            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.ShowUpDown = true;

            mthCalendario.MaxSelectionCount = 1;
            mthCalendario.SelectionStart = DateTime.Today;


        }

        #region DISEÑO


        void ConfigurarDataGridViewOscuro(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;

            // Fondo general
            dgv.BackgroundColor = Color.FromArgb(30, 30, 30);
            dgv.GridColor = Color.FromArgb(45, 45, 48);

            // Filas
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgv.DefaultCellStyle.ForeColor = Color.Gainsboro;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Filas alternas
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(36, 36, 36);

            // Encabezados
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeight = 40;

            // Filas
            dgv.RowTemplate.Height = 36;
            dgv.RowHeadersVisible = false;

            // Comportamiento
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            // Auto ajuste
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void RedondearBoton(Button btn, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(btn.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(btn.Width - radio, btn.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, btn.Height - radio, radio, radio, 90, 90);
            path.CloseAllFigures();

            btn.Region = new Region(path);
        }

        private void EstiloBoton(Button btn, Color fondo)
        {
            btn.BackColor = fondo;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.UseVisualStyleBackColor = false;

            btn.ImageAlign = ContentAlignment.MiddleCenter;
            btn.Text = "";

            btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(fondo);
            btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(fondo);

            RedondearBoton(btn, 20);
        }



        private void AplicarTemaLobby()
        {
            // =========================
            // 🎨 COLORES BASE
            // =========================
            Color colorPrimario = Color.FromArgb(30, 58, 138);
            Color colorSecundario = Color.FromArgb(59, 130, 246);
            Color colorFondo = Color.FromArgb(243, 244, 246);
            Color colorTexto = Color.FromArgb(17, 24, 39);
            Color colorGris = Color.FromArgb(107, 114, 128);

            // =========================
            // 📦 PANEL PRINCIPAL
            // =========================
            pnlLobby.BackColor = colorFondo;

            // =========================
            // 🧾 HEADER
            // =========================
            //pnlTabla.Height = 60;
            //pnlTabla.BackColor = colorPrimario;

            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;


            // =========================
            // 📅 CALENDARIO
            // =========================
            mthCalendario.BackColor = Color.White;
            mthCalendario.ForeColor = colorTexto;

            // =========================
            // 🔤 LABELS
            // =========================
            Label[] labels =
            {
                label1, label2, label3, label4,
                label5, label6, label7
            };

            foreach (Label lbl in labels)
            {
                lbl.ForeColor = colorTexto;
                lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }

            // =========================
            // 🖱 BOTONES
            // =========================
            EstiloBoton(btnAgregar, colorSecundario);
            EstiloBoton(btnModificar, Color.FromArgb(245, 158, 11)); // Naranja
            EstiloBoton(btnEliminar, Color.FromArgb(239, 68, 68));   // Rojo
            EstiloBoton(btnLimpiar, colorGris);

            // =========================
            // 📊 DATAGRIDVIEW
            // =========================
            dgvEventos.BackgroundColor = colorFondo;
            dgvEventos.BorderStyle = BorderStyle.None;
            dgvEventos.EnableHeadersVisualStyles = false;
            dgvEventos.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            dgvEventos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEventos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvEventos.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvEventos.RowHeadersVisible = false;

            dgvEventos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEventos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEventos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEventos.Dock = DockStyle.Fill;

        }





        #endregion

        private void ConfigurarDGV()
        {
            dgvEventos.ReadOnly = true;
            dgvEventos.EditMode = DataGridViewEditMode.EditProgrammatically;

            dgvEventos.AllowUserToAddRows = false;
            dgvEventos.AllowUserToDeleteRows = false;
            dgvEventos.AllowUserToResizeColumns = false;
            dgvEventos.AllowUserToResizeRows = false;

            dgvEventos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEventos.MultiSelect = false;

            dgvEventos.RowHeadersVisible = false;
            dgvEventos.EnableHeadersVisualStyles = false;

            dgvEventos.CellBeginEdit += (s, e) => e.Cancel = true;
        }

        private void CargarTiposEvento()
        {
            cmbTipoEvento.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT DISTINCT tipo FROM Agenda ORDER BY tipo";
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    cmbTipoEvento.Items.Add(r["tipo"].ToString());
                }
            }
        }

        //VALIDACIÓN
        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("Ingrese el título del evento.");
                txtTitulo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbTipoEvento.Text))
            {
                MessageBox.Show("Ingrese el tipo de evento.");
                cmbTipoEvento.Focus();
                return false;
            }


            if (cmbModalidad.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione la modalidad.");
                cmbModalidad.Focus();
                return false;
            }

            return true;
        }

        // BOTONES 
        private void btnAgregar_Click(object sender, EventArgs e) => GuardarEvento();
        private void btnModificar_Click(object sender, EventArgs e) => ModificarEvento();
        private void btnEliminar_Click(object sender, EventArgs e) => EliminarEvento();
        private void btnLimpiar_Click(object sender, EventArgs e) => LimpiarFormulario();
        private void btnBuscar_Click(object sender, EventArgs e) => CargarEventosDelMes();

        //CRUD
        private void GuardarEvento()
        {
            if (!ValidarFormulario()) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"INSERT INTO Agenda
                    (titulo, fecha, hora, tipo, modalidad, idUsuarioCreador, idTipoUsuario, alertaActiva, minutosAntes)
                    VALUES (@titulo,@fecha,@hora,@tipo,@modalidad,@idUsuario,@idTipoUsuario,@alerta,@minutos)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@titulo", txtTitulo.Text.Trim());
                    cmd.Parameters.AddWithValue("@fecha", mthCalendario.SelectionStart.Date);
                    cmd.Parameters.AddWithValue("@hora", dtpHora.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@tipo", cmbTipoEvento.Text.Trim());
                    cmd.Parameters.AddWithValue("@modalidad", cmbModalidad.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@idUsuario", SessionData.IdUsuario);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", SessionData.IdTipoUsuario);
                    cmd.Parameters.AddWithValue("@alerta", chkAlerta.Checked);
                    cmd.Parameters.AddWithValue("@minutos", (int)nudMinutos.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Evento agregado correctamente.");
                CargarEventosDelMes();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ModificarEvento()
        {
            if (dgvEventos.SelectedRows.Count == 0) return;
            if (!ValidarFormulario()) return;

            int idEvento = Convert.ToInt32(dgvEventos.SelectedRows[0].Cells["idEvento"].Value);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"UPDATE Agenda SET
                    titulo=@titulo, fecha=@fecha, hora=@hora, tipo=@tipo, modalidad=@modalidad,
                    alertaActiva=@alerta, minutosAntes=@minutos
                    WHERE idEvento=@idEvento";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@titulo", txtTitulo.Text.Trim());
                    cmd.Parameters.AddWithValue("@fecha", mthCalendario.SelectionStart.Date);
                    cmd.Parameters.AddWithValue("@hora", dtpHora.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@tipo", cmbTipoEvento.Text.Trim());
                    cmd.Parameters.AddWithValue("@modalidad", cmbModalidad.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@alerta", chkAlerta.Checked);
                    cmd.Parameters.AddWithValue("@minutos", (int)nudMinutos.Value);
                    cmd.Parameters.AddWithValue("@idEvento", idEvento);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Evento modificado correctamente.");
                CargarEventosDelMes();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EliminarEvento()
        {
            if (dgvEventos.SelectedRows.Count == 0) return;

            int idEvento = Convert.ToInt32(dgvEventos.SelectedRows[0].Cells["idEvento"].Value);

            if (MessageBox.Show("¿Eliminar evento?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Agenda WHERE idEvento=@id", conn);
                cmd.Parameters.AddWithValue("@id", idEvento);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            CargarEventosDelMes();
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtTitulo.Clear();
            cmbTipoEvento.Text = "";
            cmbModalidad.SelectedIndex = -1;
            chkAlerta.Checked = true;
            nudMinutos.Value = 10;
            dtpHora.Value = DateTime.Now;
            dgvEventos.ClearSelection();
        }

        //CARGA
        private void CargarEventosDelMes()
        {
            int mes = mthCalendario.SelectionStart.Month;
            int anio = mthCalendario.SelectionStart.Year;

            DataTable dt = new DataTable();
            dt.Columns.Add("idEvento", typeof(int));
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Hora");
            dt.Columns.Add("Título");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Modalidad");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT idEvento, titulo, fecha, hora, tipo, modalidad
                       FROM Agenda
                       WHERE MONTH(fecha)=@mes AND YEAR(fecha)=@anio
                       ORDER BY fecha, hora";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mes", mes);
                cmd.Parameters.AddWithValue("@anio", anio);

                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    dt.Rows.Add(
                        r["idEvento"],
                        Convert.ToDateTime(r["fecha"]).ToString("dd/MM/yyyy"),
                        DateTime.Today.Add((TimeSpan)r["hora"]).ToString("HH:mm"),
                        r["titulo"],
                        r["tipo"],
                        r["modalidad"]
                    );
                }
            }

            dgvEventos.DataSource = dt;
            dgvEventos.Columns["idEvento"].Visible = false;
        }


        private void DgvEventos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0) return;

            DataGridViewRow r = dgvEventos.SelectedRows[0];

            txtTitulo.Text = r.Cells["Título"].Value.ToString();
            cmbTipoEvento.Text = r.Cells["Tipo"].Value.ToString();
            cmbModalidad.Text = r.Cells["Modalidad"].Value.ToString();
            dtpHora.Value = DateTime.Today.Add(TimeSpan.Parse(r.Cells["Hora"].Value.ToString()));
        }

        //ALERTAS
        private void IniciarAlertaTimer()
        {
            alertaTimer = new Timer { Interval = 60000 };
            alertaTimer.Tick += AlertaTimer_Tick;
            alertaTimer.Start();
        }

        private void AlertaTimer_Tick(object sender, EventArgs e)
        {
            DateTime ahora = DateTime.Now;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT titulo, fecha, hora, minutosAntes FROM Agenda WHERE alertaActiva=1", conn);

                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    DateTime f = Convert.ToDateTime(r["fecha"]).Add((TimeSpan)r["hora"])
                                 .AddMinutes(-Convert.ToInt32(r["minutosAntes"]));

                    if (ahora >= f && ahora < f.AddMinutes(1))
                        MessageBox.Show("Recordatorio: " + r["titulo"]);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                btnAgregar.PerformClick(); // Ejecuta el botón Agregar
                return true; // Indica que la tecla fue manejada
            }

            if (keyData == (Keys.Control | Keys.Delete))
            {
                btnEliminar.PerformClick(); // Ejecuta el botón Eliminar
                return true;
            }

            if (keyData == (Keys.Control | Keys.H))
            {
                btnModificar.PerformClick(); // Ejecuta el botón Modificar
                return true;
            }

            if (keyData == (Keys.Control | Keys.N))
            {
                btnLimpiar.PerformClick(); // Ejecuta el botón Limpiar
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtTitulo.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtTitulo.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtTitulo.Text != limpio)
            {
                txtTitulo.Text = limpio;
                txtTitulo.SelectionStart = Math.Min(cursor, txtTitulo.Text.Length);
            }
        }

        private void cmbModalidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbModalidad_Leave(object sender, EventArgs e)
        {
            string texto = cmbTipoEvento.Text.Trim();

            if (string.IsNullOrWhiteSpace(texto))
                return;

            if (!cmbTipoEvento.Items.Contains(texto))
            {
                cmbTipoEvento.Items.Add(texto);
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

