using System;
using System.Data;
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
            toolTip = new ToolTip();

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;  // Visible 5 segundos
            toolTip.InitialDelay = 200;   // Aparece tras 0.2 segundos
            toolTip.ReshowDelay = 100;    // Retardo entre botones
            toolTip.ShowAlways = true;    // Siempre visible

            // Asignar eventos de mouse para mostrar tooltips
            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nuevo Evento", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Evento", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Evento", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);
        }

        private void Lobby_Load(object sender, EventArgs e)
        {
            // Usuario logueado
            txtUsuario.Text = SessionData.NombreCompleto;
            txtUsuario.ReadOnly = true;

            // Configuración controles
            chkAlerta.Checked = true;
            nudMinutos.Value = 10;
            txtTipo.MaxLength = 100;
            cmbModalidad.Items.AddRange(new string[] { "Presencial", "En línea" });

            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.ShowUpDown = true;

            mthCalendario.MaxSelectionCount = 1;
            mthCalendario.TodayDate = DateTime.Today;
            mthCalendario.SelectionStart = DateTime.Today;

            ConfigurarDGV();

            // Mostrar eventos del mes por defecto
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

        // ======================= BOTONES =======================
        private void btnAgregar_Click(object sender, EventArgs e) => GuardarEvento();
        private void btnModificar_Click(object sender, EventArgs e) => ModificarEvento();
        private void btnEliminar_Click(object sender, EventArgs e) => EliminarEvento();
        private void btnLimpiar_Click(object sender, EventArgs e) => LimpiarFormulario();
        private void btnBuscar_Click(object sender, EventArgs e) => CargarEventosDelMes();

        // ======================= MÉTODOS CRUD =======================
        private void GuardarEvento()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"INSERT INTO Agenda 
                                   (titulo, fecha, hora, tipo, modalidad, idUsuarioCreador, idTipoUsuario, alertaActiva, minutosAntes)
                                   VALUES (@titulo, @fecha, @hora, @tipo, @modalidad, @idUsuario, @idTipoUsuario, @alertaActiva, @minutosAntes)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@titulo", txtTitulo.Text);
                    cmd.Parameters.AddWithValue("@fecha", mthCalendario.SelectionStart.Date);
                    cmd.Parameters.AddWithValue("@hora", dtpHora.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@tipo", txtTipo.Text);
                    cmd.Parameters.AddWithValue("@modalidad", cmbModalidad.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@idUsuario", SessionData.IdUsuario);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", SessionData.IdTipoUsuario);
                    cmd.Parameters.AddWithValue("@alertaActiva", chkAlerta.Checked);
                    cmd.Parameters.AddWithValue("@minutosAntes", (int)nudMinutos.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Evento agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarEventosDelMes();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar evento: " + ex.Message);
            }
        }

        private void ModificarEvento()
        {
            if (dgvEventos.SelectedRows.Count == 0) return;
            int idEvento = Convert.ToInt32(dgvEventos.SelectedRows[0].Cells["idEvento"].Value);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"UPDATE Agenda SET
                                   titulo=@titulo, fecha=@fecha, hora=@hora, tipo=@tipo, modalidad=@modalidad,
                                   alertaActiva=@alertaActiva, minutosAntes=@minutosAntes
                                   WHERE idEvento=@idEvento";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@titulo", txtTitulo.Text);
                    cmd.Parameters.AddWithValue("@fecha", mthCalendario.SelectionStart.Date);
                    cmd.Parameters.AddWithValue("@hora", dtpHora.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@tipo", txtTipo.Text);
                    cmd.Parameters.AddWithValue("@modalidad", cmbModalidad.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@alertaActiva", chkAlerta.Checked);
                    cmd.Parameters.AddWithValue("@minutosAntes", (int)nudMinutos.Value);
                    cmd.Parameters.AddWithValue("@idEvento", idEvento);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Evento modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarEventosDelMes();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar evento: " + ex.Message);
            }
        }

        private void EliminarEvento()
        {
            if (dgvEventos.SelectedRows.Count == 0) return;
            int idEvento = Convert.ToInt32(dgvEventos.SelectedRows[0].Cells["idEvento"].Value);

            DialogResult resp = MessageBox.Show("¿Desea eliminar este evento?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resp == DialogResult.No) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = "DELETE FROM Agenda WHERE idEvento=@idEvento";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@idEvento", idEvento);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Evento eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarEventosDelMes();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar evento: " + ex.Message);
            }
        }

        private void LimpiarFormulario()
        {
            txtTitulo.Clear();
            txtTipo.Clear();
            dtpHora.Value = DateTime.Now;
            cmbModalidad.SelectedIndex = -1;
            chkAlerta.Checked = true;
            nudMinutos.Value = 10;
            dgvEventos.ClearSelection();
        }

        // ======================= CARGAR EVENTOS =======================
        private void CargarEventosDelMes()
        {
            dgvEventos.Rows.Clear();

            int mes = mthCalendario.SelectionStart.Month;
            int año = mthCalendario.SelectionStart.Year;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"SELECT idEvento, titulo, fecha, hora, tipo, modalidad, alertaActiva
                                   FROM Agenda
                                   WHERE MONTH(fecha) = @mes AND YEAR(fecha) = @anio
                                   ORDER BY fecha, hora";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@mes", mes);
                    cmd.Parameters.AddWithValue("@anio", año);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TimeSpan hora = (TimeSpan)reader["hora"];
                        string horaStr = DateTime.Today.Add(hora).ToString("HH:mm");
                        string fechaStr = Convert.ToDateTime(reader["fecha"]).ToString("dd/MM/yyyy");

                        int index = dgvEventos.Rows.Add(
                            reader["idEvento"],
                            fechaStr,
                            horaStr,
                            reader["titulo"],
                            reader["tipo"],
                            reader["modalidad"]
                        );

                        if ((bool)reader["alertaActiva"])
                            dgvEventos.Rows[index].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                    }
                }

                ResaltarDiasConEventos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar eventos: " + ex.Message);
            }
        }

        private void DgvEventos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvEventos.SelectedRows[0];
            txtTitulo.Text = row.Cells["titulo"].Value.ToString();
            txtTipo.Text = row.Cells["tipo"].Value.ToString();
            dtpHora.Value = DateTime.Today.Add(TimeSpan.Parse(row.Cells["hora"].Value.ToString()));
            cmbModalidad.SelectedItem = row.Cells["modalidad"].Value.ToString();
        }

        private void mthCalendario_DateChanged(object sender, DateRangeEventArgs e)
        {
            // Automáticamente mostrar eventos del mes
            CargarEventosDelMes();
        }

        // ======================= ALERTAS =======================
        private void IniciarAlertaTimer()
        {
            alertaTimer = new Timer();
            alertaTimer.Interval = 60000; // cada minuto
            alertaTimer.Tick += AlertaTimer_Tick;
            alertaTimer.Start();
        }

        private void AlertaTimer_Tick(object sender, EventArgs e)
        {
            DateTime ahora = DateTime.Now;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"SELECT titulo, fecha, hora, minutosAntes 
                                   FROM Agenda WHERE alertaActiva = 1";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                        TimeSpan hora = (TimeSpan)reader["hora"];
                        int minutosAntes = Convert.ToInt32(reader["minutosAntes"]);

                        DateTime horaAlerta = fecha.Add(hora).AddMinutes(-minutosAntes);

                        if (ahora >= horaAlerta && ahora < horaAlerta.AddMinutes(1))
                        {
                            MessageBox.Show($"⚠️ Recordatorio: {reader["titulo"]}",
                                            "Alerta de evento",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch { }
        }

        private void ResaltarDiasConEventos()
        {
            mthCalendario.RemoveAllBoldedDates();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = "SELECT DISTINCT fecha FROM Agenda";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        mthCalendario.AddBoldedDate(Convert.ToDateTime(reader["fecha"]));
                    }
                }

                mthCalendario.UpdateBoldedDates();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al resaltar fechas: " + ex.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

