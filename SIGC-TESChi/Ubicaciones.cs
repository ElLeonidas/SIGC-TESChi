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
        string connectionString = @"Server=.\SQLEXPRESS;Database=SGCTESCHI;Trusted_Connection=True;";

        public Ubicaciones()
        {
            InitializeComponent();
            Load += Ubicaciones_Load;

        }

        private void label1_Click(object sender, EventArgs e)
        {

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

                    // 🔍 Primero verificamos si la ubicación ya existe
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

                    // 🔹 Si no existe, procedemos a insertar
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
                // ⚠️ Error por ID duplicada
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

        private void tablaUbicaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow fila = tablaUbicaciones.Rows[e.RowIndex];

                // Asumiendo que las columnas son: 0=Numero, 1=ID, 2=Ubicación
                txtID.Text = fila.Cells[0].Value?.ToString() ?? "";
                txtUbicacion.Text = fila.Cells[1].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos en los campos: " + ex.Message);
            }
        }

        

        private void tablaUbicaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
