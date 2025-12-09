using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SIGC_TESChi
{
    public partial class Ubicaciones : UserControl
    {
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public Ubicaciones()
        {
            InitializeComponent();

            //EVENTO LOAD
            Load += Ubicaciones_Load;

            //TABLA
            tablaUbicaciones.AutoGenerateColumns = true;
            tablaUbicaciones.CellClick += tablaUbicaciones_CellClick;

            //BLOQUEO TOTAL DEL TXT ID
            txtID.ReadOnly = true;
            txtID.Enabled = false;
            txtID.TabStop = false;

            //BOTONES CONECTADOS CORRECTAMENTE
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
        }

        private void Ubicaciones_Load(object sender, EventArgs e)
        {
            CargarUbicaciones();
        }

        // ✅ CARGAR
        private void CargarUbicaciones()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT idUbicacion, dUbicacion FROM Ubicacion ORDER BY idUbicacion";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaUbicaciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ubicaciones: " + ex.Message);
            }
        }

        // ✅ LIMPIAR CAMPOS
        private void LimpiarCampos()
        {
            txtID.Text = "";
            txtUbicacion.Text = "";
            txtUbicacion.Focus();
        }

        // ✅ BOTÓN LIMPIAR ✅✅✅
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarUbicaciones();
        }

        // ✅ AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                MessageBox.Show("Por favor ingresa una ubicación.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Ubicacion WHERE dUbicacion = @dUbicacion";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@dUbicacion", txtUbicacion.Text);

                    int existe = (int)checkCmd.ExecuteScalar();
                    if (existe > 0)
                    {
                        MessageBox.Show("⚠️ Esta ubicación ya está registrada.");
                        return;
                    }

                    string query = "INSERT INTO Ubicacion (dUbicacion) VALUES (@dUbicacion)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@dUbicacion", txtUbicacion.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Ubicación agregada.");
                CargarUbicaciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar ubicación:\n" + ex.Message);
            }
        }

        // ✅ MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtUbicacion.Text == "")
            {
                MessageBox.Show("Selecciona una ubicación y modifícala.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Ubicacion SET dUbicacion = @dUbicacion WHERE idUbicacion = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@dUbicacion", txtUbicacion.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Ubicación actualizada.");
                CargarUbicaciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar ubicación: " + ex.Message);
            }
        }

        // ✅ ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Selecciona una ubicación.");
                return;
            }

            if (MessageBox.Show("¿Seguro de eliminar?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Ubicacion WHERE idUbicacion = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("✅ Ubicación eliminada.");
                    CargarUbicaciones();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar ubicación: " + ex.Message);
                }
            }
        }

        // ✅ SELECCIÓN
        private void tablaUbicaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = tablaUbicaciones.Rows[e.RowIndex];
            txtID.Text = fila.Cells["idUbicacion"].Value.ToString();
            txtUbicacion.Text = fila.Cells["dUbicacion"].Value.ToString();
        }

        // ✅ BOTÓN BUSCAR ✅✅✅
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtUbicacion.Text == "")
            {
                CargarUbicaciones();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT idUbicacion, dUbicacion FROM Ubicacion WHERE dUbicacion LIKE @busqueda";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@busqueda", "%" + txtUbicacion.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaUbicaciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message);
            }
        }

        // ✅ EXPORTAR CSV
        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos CSV (*.csv)|*.csv";
            sfd.FileName = "Ubicaciones.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();

                foreach (DataGridViewColumn col in tablaUbicaciones.Columns)
                    sb.Append(col.HeaderText + ";");

                sb.AppendLine();

                foreach (DataGridViewRow row in tablaUbicaciones.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                            sb.Append(cell.Value + ";");

                        sb.AppendLine();
                    }
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("✅ Archivo CSV generado.");
            }
        }
    }
}
