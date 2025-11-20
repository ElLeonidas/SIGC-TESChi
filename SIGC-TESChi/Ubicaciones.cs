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
    public partial class Ubicaciones : UserControl
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public Ubicaciones()
        {
            InitializeComponent();
            Load += Ubicaciones_Load;

            // <-- Muy importante: enlazar el evento CellClick para que se ejecute el manejador
            tablaUbicaciones.CellClick += tablaUbicaciones_CellClick;
        }

        private void Ubicaciones_Load(object sender, EventArgs e)
        {
            CargarUbicaciones();
        }

        private void CargarUbicaciones()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Ubicacion ORDER BY idUbicacion ASC";
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

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtUbicacion.Clear();
            txtID.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                MessageBox.Show("Por favor ingresa un ID y una ubicación.");
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
                        MessageBox.Show("⚠️ Esta ubicación ya está registrada. Ingresa una diferente.",
                                        "Ubicación duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string query = "INSERT INTO Ubicacion (idUbicacion, dUbicacion) VALUES (@idUbicacion, @dUbicacion)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@idUbicacion", Convert.ToInt32(txtID.Text));
                    cmd.Parameters.AddWithValue("@dUbicacion", txtUbicacion.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Ubicación agregada correctamente.");
                CargarUbicaciones();
                txtID.Clear();
                txtUbicacion.Clear();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("⚠️ El ID ingresado ya existe. Por favor ingresa un ID diferente.",
                                    "ID Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error SQL al agregar ubicación:\n" + ex.Message,
                                    "Error de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                MessageBox.Show("Por favor selecciona una ubicación y realiza los cambios.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Ubicacion SET dUbicacion = @dUbicacion WHERE idUbicacion = @idUbicacion";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@dUbicacion", txtUbicacion.Text);
                    cmd.Parameters.AddWithValue("@idUbicacion", Convert.ToInt32(txtID.Text));

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("✅ Ubicación actualizada correctamente.");
                        CargarUbicaciones();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("⚠️ No se encontró el registro que deseas modificar.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar ubicación: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Por favor selecciona una ubicación para eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Seguro que deseas eliminar esta ubicación?",
                                                   "Confirmar eliminación",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Ubicacion WHERE idUbicacion = @idUbicacion";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idUbicacion", Convert.ToInt32(txtID.Text));
                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            MessageBox.Show("✅ Ubicación eliminada correctamente.");
                            CargarUbicaciones();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("⚠️ No se encontró la ubicación especificada.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar ubicación: " + ex.Message);
                }
            }
        }

        // Manejador robusto: busca las columnas por DataPropertyName/Name/HeaderText y cae a índices seguros
        private void tablaUbicaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow fila = tablaUbicaciones.Rows[e.RowIndex];

                // Nombre esperados de columnas en la fuente de datos
                string colIdNames = "idUbicacion";
                string colDescNames = "dUbicacion";

                // Helper local para buscar el índice real de la columna por varios atributos
                int FindColumnIndex(string expectedName)
                {
                    // 1) Buscar por DataPropertyName (DataBinding)
                    for (int i = 0; i < tablaUbicaciones.Columns.Count; i++)
                    {
                        var col = tablaUbicaciones.Columns[i];
                        if (!string.IsNullOrEmpty(col.DataPropertyName) &&
                            string.Equals(col.DataPropertyName, expectedName, StringComparison.OrdinalIgnoreCase))
                            return i;
                    }

                    // 2) Buscar por Name
                    for (int i = 0; i < tablaUbicaciones.Columns.Count; i++)
                    {
                        var col = tablaUbicaciones.Columns[i];
                        if (!string.IsNullOrEmpty(col.Name) &&
                            string.Equals(col.Name, expectedName, StringComparison.OrdinalIgnoreCase))
                            return i;
                    }

                    // 3) Buscar por HeaderText (lo que ve el usuario)
                    for (int i = 0; i < tablaUbicaciones.Columns.Count; i++)
                    {
                        var col = tablaUbicaciones.Columns[i];
                        if (!string.IsNullOrEmpty(col.HeaderText) &&
                            string.Equals(col.HeaderText, expectedName, StringComparison.OrdinalIgnoreCase))
                            return i;
                    }

                    // 4) Si no se encontró, devolver -1
                    return -1;
                }

                int idxId = FindColumnIndex(colIdNames);
                int idxDesc = FindColumnIndex(colDescNames);

                // Si no encontró por nombre, intentar suponer posiciones comunes:
                if (idxId == -1 || idxDesc == -1)
                {
                    // Intentar por índices: (caso: columna extra 'Número' en la posición 0)
                    if (tablaUbicaciones.Columns.Count >= 3)
                    {
                        // suposición típica: 0 = Numero, 1 = id, 2 = descripcion
                        idxId = idxId == -1 ? 1 : idxId;
                        idxDesc = idxDesc == -1 ? 2 : idxDesc;
                    }
                    else if (tablaUbicaciones.Columns.Count == 2)
                    {
                        idxId = idxId == -1 ? 0 : idxId;
                        idxDesc = idxDesc == -1 ? 1 : idxDesc;
                    }
                }

                // Finalmente asignar (comprobando que existan esos índices)
                if (idxId >= 0 && idxId < fila.Cells.Count)
                    txtID.Text = fila.Cells[idxId].Value?.ToString() ?? "";
                else
                    txtID.Text = ""; // no encontrado, limpiar

                if (idxDesc >= 0 && idxDesc < fila.Cells.Count)
                    txtUbicacion.Text = fila.Cells[idxDesc].Value?.ToString() ?? "";
                else
                    txtUbicacion.Text = ""; // no encontrado, limpiar
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos en los campos: " + ex.Message);
            }
        }

        // BUSCAR UBICACIONES (FILTRO FLEXIBLE)
        private void BuscarUbicaciones()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Ubicacion WHERE 1=1";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    // FILTRAR POR ID
                    if (!string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        query += " AND idUbicacion = @id";
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                    }

                    // FILTRAR POR UBICACIÓN
                    if (!string.IsNullOrWhiteSpace(txtUbicacion.Text))
                    {
                        query += " AND dUbicacion LIKE @ubicacion";
                        cmd.Parameters.AddWithValue("@ubicacion", "%" + txtUbicacion.Text + "%");
                    }

                    cmd.CommandText = query;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    tablaUbicaciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar ubicaciones: " + ex.Message);
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUbicaciones();
        }
    }
}
