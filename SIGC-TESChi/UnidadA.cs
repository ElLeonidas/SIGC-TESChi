using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class UnidadA : UserControl
    {
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        private ToolTip toolTip;

        public UnidadA()
        {
            InitializeComponent();
            Load += UnidadA_Load;

            tablaUnidadA.AutoGenerateColumns = true;

            // 🔹 txtID no editable
            txtID.ReadOnly = true;
            txtID.Enabled = false;

            toolTip = new ToolTip();

            // Eventos de botones
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;

            ConfigurarTooltip(btnAgregar, "Agregar Unidad");
            ConfigurarTooltip(btnModificar, "Modificar Datos");
            ConfigurarTooltip(btnEliminar, "Eliminar Unidad");
            ConfigurarTooltip(btnBuscar, "Buscar Unidad");
            ConfigurarTooltip(btnLimpiar, "Limpiar Campos");

            tablaUnidadA.CellClick += tablaUnidadA_CellClick;
        }

        private void ConfigurarTooltip(Button boton, string mensaje)
        {
            boton.MouseEnter += (s, e) => toolTip.Show(mensaje, boton);
            boton.MouseLeave += (s, e) => toolTip.Hide(boton);
        }

        private void UnidadA_Load(object sender, EventArgs e)
        {
            CargarUnidadA();
        }

        // 🔹 CARGAR TABLA
        private void CargarUnidadA()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT idUniAdmin, cUniAdmin, nUniAdmin FROM UnidadAdministrativa ORDER BY idUniAdmin ASC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    tablaUnidadA.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtClaveUnidad.Clear();
            txtNombreUnidad.Clear();
            tablaUnidadA.ClearSelection();
        }

        // 🔹 AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClaveUnidad.Text) ||
                string.IsNullOrWhiteSpace(txtNombreUnidad.Text))
            {
                MessageBox.Show("Ingresa todos los datos.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Verificar si clave ya existe
                    string checkQuery = "SELECT COUNT(*) FROM UnidadAdministrativa WHERE cUniAdmin = @clave";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@clave", txtClaveUnidad.Text);

                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("⚠️ Esta clave ya existe.");
                        return;
                    }

                    // INSERT SIN ID (IDENTITY)
                    string query = "INSERT INTO UnidadAdministrativa (cUniAdmin, nUniAdmin) VALUES (@c, @n)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@c", txtClaveUnidad.Text);
                    cmd.Parameters.AddWithValue("@n", txtNombreUnidad.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Unidad agregada correctamente.");
                CargarUnidadA();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error SQL: " + ex.Message);
            }
        }

        // 🔹 MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona un registro primero.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE UnidadAdministrativa SET cUniAdmin = @c, nUniAdmin = @n WHERE idUniAdmin = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@c", txtClaveUnidad.Text);
                    cmd.Parameters.AddWithValue("@n", txtNombreUnidad.Text);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("✅ Unidad actualizada.");
                        CargarUnidadA();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("⚠️ No se encontró el registro.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        // 🔹 ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona un registro primero.");
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Eliminar esta unidad?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = "DELETE FROM UnidadAdministrativa WHERE idUniAdmin = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@id", txtID.Text);

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            MessageBox.Show("✅ Unidad eliminada.");
                            CargarUnidadA();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("⚠️ La unidad no existe.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        // 🔹 CLICK EN TABLA
        private void tablaUnidadA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow fila = tablaUnidadA.Rows[e.RowIndex];

            try
            {
                txtID.Text = fila.Cells["idUniAdmin"].Value?.ToString() ?? "";
                txtClaveUnidad.Text = fila.Cells["cUniAdmin"].Value?.ToString() ?? "";
                txtNombreUnidad.Text = fila.Cells["nUniAdmin"].Value?.ToString() ?? "";
            }
            catch
            {
                txtID.Text = fila.Cells[0].Value?.ToString() ?? "";
                txtClaveUnidad.Text = fila.Cells[1].Value?.ToString() ?? "";
                txtNombreUnidad.Text = fila.Cells[2].Value?.ToString() ?? "";
            }
        }

        // 🔹 LIMPIAR
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // 🔹 BUSCAR
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM UnidadAdministrativa WHERE 1=1";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    if (!string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        query += " AND idUniAdmin = @id";
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(txtClaveUnidad.Text))
                    {
                        query += " AND cUniAdmin LIKE @c";
                        cmd.Parameters.AddWithValue("@c", "%" + txtClaveUnidad.Text + "%");
                    }

                    if (!string.IsNullOrWhiteSpace(txtNombreUnidad.Text))
                    {
                        query += " AND nUniAdmin LIKE @n";
                        cmd.Parameters.AddWithValue("@n", "%" + txtNombreUnidad.Text + "%");
                    }

                    cmd.CommandText = query;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    tablaUnidadA.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }
    }
}
