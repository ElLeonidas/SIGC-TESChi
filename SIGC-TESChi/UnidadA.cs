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

            tablaUnidadA.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaUnidadA.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaUnidadA.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaUnidadA.Dock = DockStyle.Fill;

            // EVENTO LOAD
            Load += UnidadA_Load;

            // CONFIGURACIÓN TABLA
            tablaUnidadA.AutoGenerateColumns = true;
            tablaUnidadA.CellClick += tablaUnidadA_CellClick;

            // txtID BLOQUEADO
            txtID.ReadOnly = true;
            txtID.Enabled = false;
            txtID.TabStop = false;

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 200;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;

            // ToolTips
            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nueva Unidad Administrativa", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Unidad Administrativa", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Unidad Administrativa", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar Unidad Administrativa", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);

            // EVENTOS DE BOTONES
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
        }

        // LOAD
        private void UnidadA_Load(object sender, EventArgs e)
        {
            CargarUnidades();
        }

        //MÉTODOS 
        private void CargarUnidades()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT idUniAdmin, cUniAdmin, nUniAdmin FROM UnidadAdministrativa ORDER BY idUniAdmin";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaUnidadA.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar unidades: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtClaveUnidad.Clear();
            txtNombreUnidad.Clear();
            tablaUnidadA.ClearSelection();
        }

        private void AgregarUnidad()
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

                    string check = "SELECT COUNT(*) FROM UnidadAdministrativa WHERE cUniAdmin = @c";
                    SqlCommand chk = new SqlCommand(check, conn);
                    chk.Parameters.AddWithValue("@c", txtClaveUnidad.Text);

                    if ((int)chk.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("⚠️ Esta clave ya existe.");
                        return;
                    }

                    string query = "INSERT INTO UnidadAdministrativa (cUniAdmin, nUniAdmin) VALUES (@c, @n)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@c", txtClaveUnidad.Text);
                    cmd.Parameters.AddWithValue("@n", txtNombreUnidad.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Unidad agregada correctamente.");
                CargarUnidades();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }

        private void ModificarUnidad()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona un registro.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"UPDATE UnidadAdministrativa 
                                     SET cUniAdmin = @c, nUniAdmin = @n 
                                     WHERE idUniAdmin = @id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@c", txtClaveUnidad.Text);
                    cmd.Parameters.AddWithValue("@n", txtNombreUnidad.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Unidad actualizada.");
                CargarUnidades();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        private void EliminarUnidad()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona un registro.");
                return;
            }

            if (MessageBox.Show("¿Eliminar esta unidad?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM UnidadAdministrativa WHERE idUniAdmin = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("✅ Unidad eliminada.");
                    CargarUnidades();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void BuscarUnidad()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM UnidadAdministrativa WHERE 1=1";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

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

        //EVENTOS
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarUnidad();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarUnidad();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarUnidad();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUnidad();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarUnidades();
        }

        private void tablaUnidadA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = tablaUnidadA.Rows[e.RowIndex];
            txtID.Text = fila.Cells["idUniAdmin"].Value.ToString();
            txtClaveUnidad.Text = fila.Cells["cUniAdmin"].Value.ToString();
            txtNombreUnidad.Text = fila.Cells["nUniAdmin"].Value.ToString();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Función de exportar pendiente.");
        }
    }
}
