using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Historial : UserControl
    {
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public Historial()
        {
            InitializeComponent();
        }

        private void Historial_Load(object sender, EventArgs e)
        {
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.ReadOnly = true;
            dgvHistorial.AutoGenerateColumns = true;

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
                            cmbTabla.Items.Add(dr.GetString(0));
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
           u.Nombre + ' ' + u.Apaterno + ' ' + u.Amaterno AS UsuarioApp
    FROM HistorialCambios h
    LEFT JOIN Usuario u ON h.idUsuarioApp = u.idUsuario
    WHERE (@tabla = 'TODAS' OR h.Tabla = @tabla)
    ORDER BY h.FechaAccion DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        string tablaSeleccionada = (cmbTabla.SelectedItem != null)
                            ? cmbTabla.SelectedItem.ToString()
                            : "TODAS";

                        cmd.Parameters.AddWithValue("@tabla", tablaSeleccionada);

                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);

                        dgvHistorial.DataSource = dt;

                        // Ocultar columnas internas
                        if (dgvHistorial.Columns["idHistorial"] != null)
                            dgvHistorial.Columns["idHistorial"].Visible = false;
                        if (dgvHistorial.Columns["Llave"] != null)
                            dgvHistorial.Columns["Llave"].Visible = false;
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
    }
}
