using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class SubSecciones : UserControl
    {
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        private ToolTip toolTip;

        public SubSecciones()
        {
            InitializeComponent();
            Load += SubSecciones_Load;

            toolTip = new ToolTip();

            // Eventos
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;

            tablaSubsecciones.CellClick += tablaSubsecciones_CellClick;

            // Tooltip
            ConfigurarTooltip(btnAgregar, "Agregar SubSección");
            ConfigurarTooltip(btnModificar, "Modificar SubSección");
            ConfigurarTooltip(btnEliminar, "Eliminar SubSección");
            ConfigurarTooltip(btnBuscar, "Buscar SubSección");
            ConfigurarTooltip(btnLimpiar, "Limpiar Campos");
        }

        private void ConfigurarTooltip(Button boton, string mensaje)
        {
            boton.MouseEnter += (s, e) => toolTip.Show(mensaje, boton);
            boton.MouseLeave += (s, e) => toolTip.Hide(boton);
        }

        private void SubSecciones_Load(object sender, EventArgs e)
        {
            CargarSubSecciones();
        }

        //              CARGAR DATOS
        private void CargarSubSecciones()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM SubSeccion ORDER BY idSubSeccion ASC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    tablaSubsecciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar SubSecciones: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtSubseccion.Clear();
            txtID.Focus();
        }

        //                  AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtSubseccion.Text))
            {
                MessageBox.Show("Ingresa un ID y una SubSección.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Validar duplicado
                    string checkQuery = "SELECT COUNT(*) FROM SubSeccion WHERE dSubSeccion = @descripcion";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@descripcion", txtSubseccion.Text);

                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("⚠️ Esta SubSección ya existe.");
                        return;
                    }

                    string query = "INSERT INTO SubSeccion (idSubSeccion, dSubSeccion) VALUES (@id, @desc)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@desc", txtSubseccion.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ SubSección agregada correctamente.");
                CargarSubSecciones();
                LimpiarCampos();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                    MessageBox.Show("⚠️ El ID ya existe.");
                else
                    MessageBox.Show("Error SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //                  MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una SubSección para modificar.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE SubSeccion SET dSubSeccion = @desc WHERE idSubSeccion = @id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@desc", txtSubseccion.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("✅ SubSección modificada.");
                        CargarSubSecciones();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("⚠️ La SubSección no existe.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        //                  ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una SubSección para eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Eliminar esta SubSección?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = "DELETE FROM SubSeccion WHERE idSubSeccion = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@id", txtID.Text);

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            MessageBox.Show("✅ SubSección eliminada.");
                            CargarSubSecciones();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("⚠️ No existe la SubSección.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        //       CARGAR DATOS AL SELECCIONAR FILA
        private void tablaSubsecciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow fila = tablaSubsecciones.Rows[e.RowIndex];

                txtID.Text = fila.Cells["idSubSeccion"].Value?.ToString() ?? "";
                txtSubseccion.Text = fila.Cells["dSubSeccion"].Value?.ToString() ?? "";
            }
            catch
            {
                txtID.Text = tablaSubsecciones.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                txtSubseccion.Text = tablaSubsecciones.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
            }
        }

        //                   LIMPIAR
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        //                    BUSCAR
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM SubSeccion WHERE 1=1";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    if (!string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        query += " AND idSubSeccion = @id";
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(txtSubseccion.Text))
                    {
                        query += " AND dSubSeccion LIKE @desc";
                        cmd.Parameters.AddWithValue("@desc", "%" + txtSubseccion.Text + "%");
                    }

                    cmd.CommandText = query;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    tablaSubsecciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }
    }
}
