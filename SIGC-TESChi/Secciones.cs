using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Secciones : UserControl
    {
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        private ToolTip toolTip;

        public Secciones()
        {
            InitializeComponent();
            Load += Secciones_Load;

            toolTip = new ToolTip();

            // Eventos
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;

            // Tooltip
            ConfigurarTooltip(btnAgregar, "Agregar Registro");
            ConfigurarTooltip(btnModificar, "Modificar Registro");
            ConfigurarTooltip(btnEliminar, "Eliminar Registro");
            ConfigurarTooltip(btnBuscar, "Buscar Sección");
            ConfigurarTooltip(btnLimpiar, "Limpiar Campos");

            tablaSecciones.CellClick += tablaSecciones_CellClick;
        }

        private void ConfigurarTooltip(Button boton, string mensaje)
        {
            boton.MouseEnter += (s, e) => toolTip.Show(mensaje, boton);
            boton.MouseLeave += (s, e) => toolTip.Hide(boton);
        }

        private void Secciones_Load(object sender, EventArgs e)
        {
            CargarSecciones();
        }

        private void CargarSecciones()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Seccion ORDER BY idSeccion ASC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    tablaSecciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Secciones: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtSeccion.Clear();
            txtID.Focus();
        }

        //                AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtSeccion.Text))
            {
                MessageBox.Show("Ingresa un ID y una sección.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Seccion WHERE dSeccion = @descripcion";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@descripcion", txtSeccion.Text);

                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("⚠️ Esta sección ya existe. Ingresa una diferente.");
                        return;
                    }

                    string query = "INSERT INTO Seccion (idSeccion, dSeccion) VALUES (@id, @desc)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@id", txtID.Text);   // CORREGIDO (VARCHAR)
                    cmd.Parameters.AddWithValue("@desc", txtSeccion.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Sección agregada correctamente.");
                CargarSecciones();
                LimpiarCampos();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                    MessageBox.Show("⚠️ El ID ya existe, elige otro.");
                else
                    MessageBox.Show("Error SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general: " + ex.Message);
            }
        }

        //                MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtSeccion.Text))
            {
                MessageBox.Show("Selecciona una sección primero.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Seccion SET dSeccion = @desc WHERE idSeccion = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@desc", txtSeccion.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);  // CORREGIDO (VARCHAR)

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("✅ Sección actualizada.");
                        CargarSecciones();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("⚠️ No se encontró la sección.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        //                ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una sección a eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Eliminar esta sección?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = "DELETE FROM Seccion WHERE idSeccion = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@id", txtID.Text);  // CORREGIDO (VARCHAR)

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            MessageBox.Show("✅ Sección eliminada.");
                            CargarSecciones();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("⚠️ La sección no existe.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        // Cargar datos al hacer clic en tabla
        private void tablaSecciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow fila = tablaSecciones.Rows[e.RowIndex];

                txtID.Text = fila.Cells["idSeccion"].Value?.ToString() ?? "";
                txtSeccion.Text = fila.Cells["dSeccion"].Value?.ToString() ?? "";
            }
            catch
            {
                txtID.Text = tablaSecciones.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                txtSeccion.Text = tablaSecciones.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
            }
        }

        // Limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        //                 BUSCAR
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Seccion WHERE 1=1";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    if (!string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        query += " AND idSeccion = @id";
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(txtSeccion.Text))
                    {
                        query += " AND dSeccion LIKE @desc";
                        cmd.Parameters.AddWithValue("@desc", "%" + txtSeccion.Text + "%");
                    }

                    cmd.CommandText = query;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    tablaSecciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }
    }
}
