using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class RUsuarios : UserControl
    {
        private ToolTip toolTip;
        // 🔹 Cadena de conexión (ajústala si tu instancia/localdb es diferente)
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public RUsuarios()
        {
            InitializeComponent();

            // Enlazamos eventos manualmente para evitar fallas
            Load += RUsuarios_Load;
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click; 
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            TablaUsuarios.CellClick += TablaUsuarios_CellClick;
            btnBuscar.Click += btnBuscar_Click;

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;  // Visible 5 segundos
            toolTip.InitialDelay = 200;   // Aparece tras 0.2 segundos
            toolTip.ReshowDelay = 100;    // Retardo entre botones
            toolTip.ShowAlways = true;    // Siempre visible

            // Asignar eventos de mouse para mostrar tooltips
            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Registros", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Registros", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Registros", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);
        }

        private void RUsuarios_Load(object sender, EventArgs e)
        {
            CargarTiposUsuario();
            CargarUsuarios();
        }

        // Cargar ComboBox
        private void CargarTiposUsuario()
        {
            try
            {
                comboTipoUsuario.Items.Clear();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string q = "SELECT idTipoUsuario, dTipoUsuario FROM TipoUsuario ORDER BY idTipoUsuario ASC";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        comboTipoUsuario.Items.Add(dr["idTipoUsuario"].ToString() + " - " + dr["dTipoUsuario"].ToString());
                    }
                }

                comboTipoUsuario.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de usuario: " + ex.Message);
            }
        }


        // Cargar usuarios desde SQL (tabla: Usuario)
        private void CargarUsuarios()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = @"
                SELECT 
                    U.idUsuario,
                    U.Username,
                    U.Nombre,
                    U.Apaterno,
                    U.Amaterno,
                    U.idTipoUsuario,
                    T.dTipoUsuario AS TipoUsuario,
                    U.contrasena
                FROM Usuario U
                INNER JOIN TipoUsuario T ON U.idTipoUsuario = T.idTipoUsuario";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    TablaUsuarios.AutoGenerateColumns = true;
                    TablaUsuarios.DataSource = null;
                    TablaUsuarios.Columns.Clear();
                    TablaUsuarios.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }


        // Verificar si existe el usuario (por Username)
        private bool ExisteUsuario(string username)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT COUNT(1) FROM Usuario WHERE Username = @u";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@u", username);

                        object result = cmd.ExecuteScalar();

                        if (result == null || result == DBNull.Value)
                            return false;

                        if (int.TryParse(result.ToString(), out int count))
                            return count > 0;

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar usuario: " + ex.Message);
                return false;
            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" || txtNombreAcceso.Text == "" ||
                txtApaterno.Text == "" || txtAmaterno.Text == "" ||
                txtContrasena.Text == "" || comboTipoUsuario.SelectedIndex == -1)
            {
                MessageBox.Show("Llena todos los campos.");
                return;
            }

            string username = txtUsuario.Text.Trim();
            string nombre = txtNombreAcceso.Text.Trim();
            string apaterno = txtApaterno.Text.Trim();
            string amaterno = txtAmaterno.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            string tipo = comboTipoUsuario.SelectedItem.ToString().Split(' ')[0];

            if (ExisteUsuario(username))
            {
                MessageBox.Show("Ese nombre de usuario ya existe. Usa otro.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"
            INSERT INTO Usuario (Username, Nombre, Apaterno, Amaterno, idTipoUsuario, Contrasena)
            VALUES (@user, @n, @aP, @aM, @t, @c)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@n", nombre);
                cmd.Parameters.AddWithValue("@aP", apaterno);
                cmd.Parameters.AddWithValue("@aM", amaterno);
                cmd.Parameters.AddWithValue("@t", tipo);
                cmd.Parameters.AddWithValue("@c", contrasena);

                cmd.ExecuteNonQuery();
            }

            CargarUsuarios();
            LimpiarCampos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdentificador.Text))
            {
                MessageBox.Show("Selecciona un usuario.");
                return;
            }

            int idUsuario = int.Parse(txtIdentificador.Text.Trim());
            string username = txtUsuario.Text.Trim();
            string nombre = txtNombreAcceso.Text.Trim();
            string apaterno = txtApaterno.Text.Trim();
            string amaterno = txtAmaterno.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            string tipo = comboTipoUsuario.SelectedItem.ToString().Split(' ')[0];

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"
            UPDATE Usuario 
            SET Username=@user,
                Nombre=@n,
                Apaterno=@aP,
                Amaterno=@aM,
                Contrasena=@c,
                idTipoUsuario=@t
            WHERE idUsuario=@id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@n", nombre);
                cmd.Parameters.AddWithValue("@aP", apaterno);
                cmd.Parameters.AddWithValue("@aM", amaterno);
                cmd.Parameters.AddWithValue("@c", contrasena);
                cmd.Parameters.AddWithValue("@t", tipo);
                cmd.Parameters.AddWithValue("@id", idUsuario);

                cmd.ExecuteNonQuery();
            }

            CargarUsuarios();
            LimpiarCampos();
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdentificador.Text))
            {
                MessageBox.Show("Selecciona un usuario.");
                return;
            }

            int idUsuario = int.Parse(txtIdentificador.Text.Trim());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "DELETE FROM Usuario WHERE idUsuario=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", idUsuario);
                cmd.ExecuteNonQuery();
            }

            CargarUsuarios();
            LimpiarCampos();
        }


        // SELECCIONAR USUARIO DE LA TABLA
        private void TablaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = TablaUsuarios.Rows[e.RowIndex];

            // idUsuario en un TextBox oculto o aparte
            txtIdentificador.Text = row.Cells["idUsuario"].Value?.ToString();

            txtUsuario.Text = row.Cells["Username"].Value?.ToString();
            txtNombreAcceso.Text = row.Cells["Nombre"].Value?.ToString();
            txtApaterno.Text = row.Cells["Apaterno"].Value?.ToString();
            txtAmaterno.Text = row.Cells["Amaterno"].Value?.ToString();
            txtContrasena.Text = row.Cells["Contrasena"].Value?.ToString();

            string tipo = row.Cells["idTipoUsuario"].Value?.ToString();

            if (tipo == "1")
                comboTipoUsuario.SelectedItem = "1 - Administrador";
            else if (tipo == "2")
                comboTipoUsuario.SelectedItem = "2 - Licenciada";
            else if (tipo == "3")
                comboTipoUsuario.SelectedItem = "3 - Servicio";
        }


        // LIMPIAR CAMPOS
        // LIMPIAR CAMPOS
        private void LimpiarCampos()
        {
            
           
               
            txtIdentificador.Text = "";
            txtUsuario.Text = "";     // Username
            txtNombreAcceso.Text = "";      // Nombre
            txtApaterno.Text = "";
            txtAmaterno.Text = "";
            txtContrasena.Text = "";
            comboTipoUsuario.SelectedIndex = -1;

            // Opcional: limpiar selección de la tabla
            if (TablaUsuarios != null)
                TablaUsuarios.ClearSelection();
        }


        // BUSCAR USUARIOS (FILTROS FLEXIBLES)
        private void BuscarUsuarios()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = @"
                SELECT 
                    U.idUsuario,
                    U.Nombre,
                    U.Apaterno,
                    U.Amaterno,
                    T.dTipoUsuario AS TipoUsuario,
                    U.contrasena
                FROM Usuario U
                INNER JOIN TipoUsuario T ON U.idTipoUsuario = T.idTipoUsuario
                WHERE 1=1
            ";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    // FILTRAR por Nombre (txtUsuario)
                    if (!string.IsNullOrWhiteSpace(txtUsuario.Text))
                    {
                        query += " AND U.Nombre LIKE @nombre";
                        cmd.Parameters.AddWithValue("@nombre", "%" + txtUsuario.Text + "%");
                    }

                    // FILTRAR por Apellido Paterno
                    if (!string.IsNullOrWhiteSpace(txtApaterno.Text))
                    {
                        query += " AND U.Apaterno LIKE @apaterno";
                        cmd.Parameters.AddWithValue("@apaterno", "%" + txtApaterno.Text + "%");
                    }

                    // FILTRAR por Apellido Materno
                    if (!string.IsNullOrWhiteSpace(txtAmaterno.Text))
                    {
                        query += " AND U.Amaterno LIKE @amaterno";
                        cmd.Parameters.AddWithValue("@amaterno", "%" + txtAmaterno.Text + "%");
                    }

                    // FILTRAR por Tipo de Usuario desde ComboBox
                    if (comboTipoUsuario.SelectedIndex != -1)
                    {
                        string idTipo = comboTipoUsuario.SelectedItem.ToString().Split(' ')[0];
                        query += " AND U.idTipoUsuario = @tipo";
                        cmd.Parameters.AddWithValue("@tipo", idTipo);
                    }

                    cmd.CommandText = query;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    TablaUsuarios.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar usuarios: " + ex.Message);
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUsuarios();
        }
    }
}
