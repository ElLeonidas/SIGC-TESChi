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

namespace SIGC_TESChi
{
    public partial class Historial : UserControl
    {
        string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public Historial()
        {
            InitializeComponent();
        }

        private void Historial_Load(object sender, EventArgs e)
        {
            // Llenar ComboBox con las tablas reales de la base de datos
            LlenarComboTablas();

            // PASO 3: Columna botón 🔎
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

            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.ReadOnly = true;

            // Cargar datos iniciales
            CargarHistorial();
        }

        // 🔹 Llenar ComboBox dinámicamente con tablas existentes
        private void LlenarComboTablas()
        {
            try
            {
                cmbTabla.Items.Clear();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Traer nombres de las tablas de la base de datos
                    DataTable dt = conn.GetSchema("Tables");
                    cmbTabla.Items.Add("TODAS"); // opción por defecto

                    foreach (DataRow row in dt.Rows)
                    {
                        string tableName = row["TABLE_NAME"].ToString();
                        cmbTabla.Items.Add(tableName);
                    }
                    cmbTabla.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tablas: " + ex.Message);
            }
        }

        // 🔹 Cargar historial filtrando solo por tabla y rango de fechas
        private void CargarHistorial()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"SELECT idHistorial, Tabla, Llave, TipoAccion, UsuarioBD, FechaAccion, DatosAnteriores, DatosNuevos, idUsuarioApp
                                   FROM HistorialCambios
                                   WHERE (@tabla = 'TODAS' OR Tabla = @tabla)
                                     AND (FechaAccion BETWEEN @desde AND @hasta)
                                   ORDER BY FechaAccion DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tabla", cmbTabla.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@desde", dtpDesde.Value.Date);
                        cmd.Parameters.AddWithValue("@hasta", dtpHasta.Value.Date.AddDays(1).AddSeconds(-1));

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvHistorial.DataSource = dt;

                        // Ocultar columnas que no quieres mostrar
                        dgvHistorial.Columns["idHistorial"].Visible = false;
                        dgvHistorial.Columns["DatosAnteriores"].Visible = false;
                        dgvHistorial.Columns["DatosNuevos"].Visible = false;
                        dgvHistorial.Columns["idUsuarioApp"].Visible = false;

                        // Renombrar encabezados
                        dgvHistorial.Columns["Tabla"].HeaderText = "Tabla";
                        dgvHistorial.Columns["Llave"].HeaderText = "Llave";
                        dgvHistorial.Columns["TipoAccion"].HeaderText = "Acción";
                        dgvHistorial.Columns["UsuarioBD"].HeaderText = "Usuario";
                        dgvHistorial.Columns["FechaAccion"].HeaderText = "Fecha";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message);
            }
        }

        // 🔹 Botón Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarHistorial();
        }

        // 🔹 Botón Actualizar
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            cmbTabla.SelectedIndex = 0;
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;
            CargarHistorial();
        }

        // 🔹 Evento para abrir DetallesCambio
        private void dgvHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvHistorial.Columns[e.ColumnIndex].Name == "VerDetalle")
            {
                string tabla = dgvHistorial.Rows[e.RowIndex].Cells["Tabla"].Value.ToString();
                string llave = dgvHistorial.Rows[e.RowIndex].Cells["Llave"].Value.ToString();
                string accion = dgvHistorial.Rows[e.RowIndex].Cells["TipoAccion"].Value.ToString();
                string usuario = dgvHistorial.Rows[e.RowIndex].Cells["UsuarioBD"].Value.ToString();
                string fecha = dgvHistorial.Rows[e.RowIndex].Cells["FechaAccion"].Value.ToString();
                string datosAntes = dgvHistorial.Rows[e.RowIndex].Cells["DatosAnteriores"].Value.ToString();
                string datosDespues = dgvHistorial.Rows[e.RowIndex].Cells["DatosNuevos"].Value.ToString();

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
    }
}
