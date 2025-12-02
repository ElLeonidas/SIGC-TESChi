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

                    string sql = @"SELECT idHistorial, Tabla, Llave, TipoAccion, UsuarioBD, FechaAccion, 
                                          DatosAnteriores, DatosNuevos, idUsuarioApp
                                   FROM HistorialCambios
                                   WHERE (@tabla = 'TODAS' OR Tabla = @tabla)
                                     AND (FechaAccion BETWEEN @desde AND @hasta)
                                   ORDER BY FechaAccion DESC";

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
