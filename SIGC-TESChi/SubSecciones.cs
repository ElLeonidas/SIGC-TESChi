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

            tablaSubsecciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaSubsecciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaSubsecciones.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaSubsecciones.Dock = DockStyle.Fill;

            Load += SubSecciones_Load;

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;  // Visible 5 segundos
            toolTip.InitialDelay = 200;   // Aparece tras 0.2 segundos
            toolTip.ReshowDelay = 100;    // Retardo entre botones
            toolTip.ShowAlways = true;    // Siempre visible

            // Asignar eventos de mouse para mostrar tooltips
            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nueva Subsección", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Subsección", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Subsección", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar Subsección", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);

            // Eventos
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            tablaSubsecciones.CellClick += tablaSubsecciones_CellClick;

            // Bloqueo total del ID
            txtID.ReadOnly = true;
            txtID.Enabled = false;
            txtID.TabStop = false;
        }

        private void SubSecciones_Load(object sender, EventArgs e)
        {
            CargarSubSecciones();
        }

        //EVENTOS 

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarSubSeccion();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarSubSeccion();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarSubSeccion();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarSubSeccion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarSubSecciones();
        }

        // MÉTODOS 
        private void CargarSubSecciones()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT idSubSeccion, claveSubSeccion, dSubSeccion FROM SubSeccion ORDER BY idSubSeccion";
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
            txtClaveSubseccion.Clear();
            txtSubseccion.Clear();
            txtClaveSubseccion.Focus();
        }

        private void AgregarSubSeccion()
        {
            if (string.IsNullOrWhiteSpace(txtClaveSubseccion.Text) ||
                string.IsNullOrWhiteSpace(txtSubseccion.Text))
            {
                MessageBox.Show("Ingresa la clave y la SubSección.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM SubSeccion WHERE claveSubSeccion = @clave";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@clave", txtClaveSubseccion.Text);

                    int existe = (int)checkCmd.ExecuteScalar();
                    if (existe > 0)
                    {
                        MessageBox.Show("⚠️ Esta SubSección ya existe.");
                        return;
                    }

                    string query = "INSERT INTO SubSeccion (claveSubSeccion, dSubSeccion) VALUES (@clave, @desc)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@clave", txtClaveSubseccion.Text);
                    cmd.Parameters.AddWithValue("@desc", txtSubseccion.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ SubSección agregada correctamente.");
                CargarSubSecciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ModificarSubSeccion()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una SubSección para modificar.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = @"UPDATE SubSeccion 
                                     SET claveSubSeccion = @clave, dSubSeccion = @desc
                                     WHERE idSubSeccion = @id";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@clave", txtClaveSubseccion.Text);
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
                        MessageBox.Show("⚠️ La SubSección no existe.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        private void EliminarSubSeccion()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una SubSección para eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Eliminar esta SubSección?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "DELETE FROM SubSeccion WHERE idSubSeccion = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("✅ SubSección eliminada.");
                        CargarSubSecciones();
                        LimpiarCampos();
                    }
                    else
                        MessageBox.Show("⚠️ No existe la SubSección.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void BuscarSubSeccion()
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

                    if (!string.IsNullOrWhiteSpace(txtClaveSubseccion.Text))
                    {
                        query += " AND claveSubSeccion LIKE @clave";
                        cmd.Parameters.AddWithValue("@clave", "%" + txtClaveSubseccion.Text + "%");
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

        private void tablaSubsecciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = tablaSubsecciones.Rows[e.RowIndex];

            txtID.Text = fila.Cells["idSubSeccion"].Value.ToString();
            txtClaveSubseccion.Text = fila.Cells["claveSubSeccion"].Value.ToString();
            txtSubseccion.Text = fila.Cells["dSubSeccion"].Value.ToString();
        }
    }
}
