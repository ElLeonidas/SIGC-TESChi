using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Historial : UserControl
    {
        // ⚠️ VERIFICA que esta cadena es la misma que usas en SSMS (servidor e instancia)
        // Si en SSMS usas (localdb)\MSSQLLocalDB y la BD se llama DBCONTRALORIA, esto está bien.
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public Historial()
        {
            InitializeComponent();
        }

        private void Historial_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Historial_Load se ejecutó", "DEBUG"); // DEBUG 1

            // Asegúrate que en el diseñador esté:
            // this.Load += new System.EventHandler(this.Historial_Load);

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
                MessageBox.Show("Entró a LlenarComboTablas", "DEBUG"); // DEBUG 2

                cmbTabla.Items.Clear();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Conexión abierta en LlenarComboTablas", "DEBUG"); // DEBUG 3

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

                MessageBox.Show($"Tablas cargadas en combo: {cmbTabla.Items.Count}", "DEBUG"); // DEBUG 4

                if (cmbTabla.Items.Count > 0)
                    cmbTabla.SelectedIndex = 0;
                else
                    MessageBox.Show("No se agregó ninguna tabla al ComboBox", "DEBUG");
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
                MessageBox.Show("Entró a CargarHistorial", "DEBUG"); // DEBUG 5

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Conexión abierta en CargarHistorial", "DEBUG"); // DEBUG 6

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

                        MessageBox.Show($"Tabla seleccionada: {tablaSeleccionada}", "DEBUG"); // DEBUG 7

                        cmd.Parameters.AddWithValue("@tabla", tablaSeleccionada);

                        DateTime desde = dtpDesde.Value.Date;
                        DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

                        MessageBox.Show($"Rango fechas: {desde} - {hasta}", "DEBUG"); // DEBUG 8

                        cmd.Parameters.AddWithValue("@desde", desde);
                        cmd.Parameters.AddWithValue("@hasta", hasta);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        MessageBox.Show($"Filas cargadas del historial: {dt.Rows.Count}", "DEBUG"); // DEBUG 9

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

                DetallesCambio frm = new DetallesCambio();
                frm.Tabla = tabla;
                frm.Llave = llave;
                frm.Accion = accion;
                frm.Usuario = usuario;
                frm.Fecha = fecha;
                frm.DatosAnteriores = datosAntes;
                frm.DatosNuevos = datosDespues;

                frm.ShowDialog();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
