using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Lobby : UserControl
    {
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";
        private Timer alertaTimer;
        private ToolTip toolTip;

        public Lobby()
        {
            InitializeComponent();

            dgvEventos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEventos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEventos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEventos.Dock = DockStyle.Fill;

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
            txtUsuario.Text = SessionData.NombreCompleto;
            txtUsuario.ReadOnly = true;

            chkAlerta.Checked = true;
            nudMinutos.Value = 10;
            txtTipo.MaxLength = 100;
            cmbModalidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModalidad.Items.AddRange(new string[] { "Presencial", "En línea" });

            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.ShowUpDown = true;

            mthCalendario.MaxSelectionCount = 1;
            mthCalendario.SelectionStart = DateTime.Today;

            ConfigurarDGV();
            CargarEventosDelMes();
            IniciarAlertaTimer();
        }

        private void ConfigurarDGV()
        {
            dgvEventos.Columns.Clear();
            dgvEventos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEventos.MultiSelect = false;
            dgvEventos.ReadOnly = true;
            dgvEventos.AllowUserToAddRows = false;

            dgvEventos.Columns.Add("idEvento", "ID");
            dgvEventos.Columns.Add("fecha", "Fecha");
            dgvEventos.Columns.Add("hora", "Hora");
            dgvEventos.Columns.Add("titulo", "Título");
            dgvEventos.Columns.Add("tipo", "Tipo");
            dgvEventos.Columns.Add("modalidad", "Modalidad");

            dgvEventos.Columns["idEvento"].Visible = false;
            dgvEventos.SelectionChanged += DgvEventos_SelectionChanged;
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

            if (string.IsNullOrWhiteSpace(txtTipo.Text))
            {
                MessageBox.Show("Ingrese el tipo de evento.");
                txtTipo.Focus();
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
                    cmd.Parameters.AddWithValue("@tipo", txtTipo.Text.Trim());
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
                    cmd.Parameters.AddWithValue("@tipo", txtTipo.Text.Trim());
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
            txtTipo.Clear();
            cmbModalidad.SelectedIndex = -1;
            chkAlerta.Checked = true;
            nudMinutos.Value = 10;
            dtpHora.Value = DateTime.Now;
            dgvEventos.ClearSelection();
        }

        //CARGA
        private void CargarEventosDelMes()
        {
            dgvEventos.Rows.Clear();

            int mes = mthCalendario.SelectionStart.Month;
            int anio = mthCalendario.SelectionStart.Year;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT idEvento, titulo, fecha, hora, tipo, modalidad, alertaActiva
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
                    string fecha = Convert.ToDateTime(r["fecha"]).ToString("dd/MM/yyyy");
                    string hora = DateTime.Today.Add((TimeSpan)r["hora"]).ToString("HH:mm");

                    int i = dgvEventos.Rows.Add(r["idEvento"], fecha, hora, r["titulo"], r["tipo"], r["modalidad"]);

                    if ((bool)r["alertaActiva"])
                        dgvEventos.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                }
            }
        }

        private void DgvEventos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0) return;

            var r = dgvEventos.SelectedRows[0];
            txtTitulo.Text = r.Cells["titulo"].Value.ToString();
            txtTipo.Text = r.Cells["tipo"].Value.ToString();
            cmbModalidad.SelectedItem = r.Cells["modalidad"].Value.ToString();
            dtpHora.Value = DateTime.Today.Add(TimeSpan.Parse(r.Cells["hora"].Value.ToString()));
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

    }
}

