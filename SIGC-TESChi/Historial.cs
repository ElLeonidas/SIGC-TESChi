using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Historial : UserControl
    {
        // Cadena de conexión
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public Historial()
        {
            InitializeComponent();
        }

        private void Historial_Load(object sender, EventArgs e)
        {
            // Configuración del DataGridView
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.ReadOnly = true;
            dgvHistorial.AutoGenerateColumns = true;

            // Rango de fechas inicial
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;

            LlenarComboTablas();
            CargarHistorial();
        }

        public static void RegistrarCambio(
    string tabla,
    string llave,
    string tipoAccion,
    string datosAnteriores,
    string datosNuevos)
        {
            try
            {
                string connectionString =
                    @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                INSERT INTO HistorialCambios
                (Tabla, Llave, TipoAccion, UsuarioBD, FechaAccion, DatosAnteriores, DatosNuevos, idUsuarioApp)
                VALUES
                (@tabla, @llave, @tipo, @usuarioBD, @fecha, @datosAnt, @datosNuevos, @idUsuarioApp)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tabla", tabla);
                        cmd.Parameters.AddWithValue("@llave", llave);
                        cmd.Parameters.AddWithValue("@tipo", tipoAccion);
                        cmd.Parameters.AddWithValue("@usuarioBD", SessionData.Username);
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@datosAnt", datosAnteriores ?? "");
                        cmd.Parameters.AddWithValue("@datosNuevos", datosNuevos ?? "");
                        cmd.Parameters.AddWithValue("@idUsuarioApp", SessionData.IdUsuario);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar cambio: " + ex.Message, "Historial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LlenarComboTablas()
        {
            try
            {
                cmbTabla.Items.Clear();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"SELECT TABLE_NAME 
                                   FROM INFORMATION_SCHEMA.TABLES
                                   WHERE TABLE_TYPE = 'BASE TABLE'
                                   ORDER BY TABLE_NAME";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        cmbTabla.Items.Add("TODAS");

                        while (dr.Read())
                        {
                            string tableName = dr.GetString(0);
                            cmbTabla.Items.Add(tableName);
                        }
                    }
                }

                if (cmbTabla.Items.Count > 0)
                    cmbTabla.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tablas: " + ex.Message);
            }
        }

        private void CargarHistorial()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
    SELECT h.idHistorial,
           h.Tabla,
           h.Llave,
           h.TipoAccion,
           h.UsuarioBD,
           h.FechaAccion,
           h.DatosAnteriores,
           h.DatosNuevos,
           u.Nombre + ' ' + u.Apaterno + ' ' + u.Amaterno AS UsuarioApp
    FROM HistorialCambios h
    LEFT JOIN Usuario u ON h.idUsuarioApp = u.idUsuario
    WHERE (@tabla = 'TODAS' OR h.Tabla = @tabla)
      AND (h.FechaAccion BETWEEN @desde AND @hasta)
    ORDER BY h.FechaAccion DESC";


                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        string tablaSeleccionada = (cmbTabla.SelectedItem != null)
                            ? cmbTabla.SelectedItem.ToString()
                            : "TODAS";

                        cmd.Parameters.AddWithValue("@tabla", tablaSeleccionada);

                        DateTime desde = dtpDesde.Value.Date;
                        DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

                        cmd.Parameters.AddWithValue("@desde", desde);
                        cmd.Parameters.AddWithValue("@hasta", hasta);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvHistorial.DataSource = dt;

                        if (dgvHistorial.Columns["idHistorial"] != null)
                            dgvHistorial.Columns["idHistorial"].Visible = false;
                        if (dgvHistorial.Columns["DatosAnteriores"] != null)
                            dgvHistorial.Columns["DatosAnteriores"].Visible = false;
                        if (dgvHistorial.Columns["DatosNuevos"] != null)
                            dgvHistorial.Columns["DatosNuevos"].Visible = false;
                        if (dgvHistorial.Columns["idUsuarioApp"] != null)
                            dgvHistorial.Columns["idUsuarioApp"].Visible = false;

                        if (!dgvHistorial.Columns.Contains("VerDetalle"))
                        {
                            DataGridViewButtonColumn btnVer = new DataGridViewButtonColumn();
                            btnVer.Name = "VerDetalle";
                            btnVer.HeaderText = "Ver";
                            btnVer.Text = "🔎";
                            btnVer.Width = 60;
                            btnVer.UseColumnTextForButtonValue = true;
                            dgvHistorial.Columns.Add(btnVer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarHistorial();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (cmbTabla.Items.Count > 0)
                cmbTabla.SelectedIndex = 0;

            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;
            CargarHistorial();
        }

        private void dgvHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvHistorial.Columns[e.ColumnIndex].Name == "VerDetalle")
            {
                var row = dgvHistorial.Rows[e.RowIndex];

                string tabla = row.Cells["Tabla"]?.Value?.ToString() ?? "";
                string llave = row.Cells["Llave"]?.Value?.ToString() ?? "";
                string accion = row.Cells["TipoAccion"]?.Value?.ToString() ?? "";
                string usuario = row.Cells["UsuarioBD"]?.Value?.ToString() ?? "";
                string fecha = row.Cells["FechaAccion"]?.Value?.ToString() ?? "";
                string datosAntes = row.Cells["DatosAnteriores"]?.Value?.ToString() ?? "";
                string datosDespues = row.Cells["DatosNuevos"]?.Value?.ToString() ?? "";

                DetallesCambio frm = new DetallesCambio
                {
                    Tabla = tabla,
                    Llave = llave,
                    Accion = accion,
                    Usuario = usuario,
                    Fecha = fecha,
                    DatosAnteriores = datosAntes,
                    DatosNuevos = datosDespues
                };

                frm.ShowDialog();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
