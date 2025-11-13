using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class RUsuarios : UserControl
    {
        private ToolTip toolTip;
        string connectionString = @"Server=.\SQLEXPRESS;Database=DBCONTRALORIA;Trusted_Connection=True;";

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

        // ✅ Cargar ComboBox
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


        // ✅ Cargar usuarios desde SQL (tabla: Usuario)
        private void CargarUsuarios()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Consulta completa con INNER JOIN
                    string query = @"
                SELECT 
                    U.idUsuario,
                    U.Nombre,
                    U.Apaterno,
                    U.Amaterno,
                    T.dTipoUsuario AS TipoUsuario,
                    U.contrasena
                FROM Usuario U
                INNER JOIN TipoUsuario T ON U.idTipoUsuario = T.idTipoUsuario";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Cargar datos en el DataGridView
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

        // ✅ Verificar si existe el usuario (idUsuario = nombre)
        private bool ExisteUsuario(string idUsuario)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Usar COUNT(1) garantiza que ExecuteScalar() devuelva un número (0 o >0)
                    string query = "SELECT COUNT(1) FROM Usuario WHERE idUsuario = @u";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@u", idUsuario);

                        object result = cmd.ExecuteScalar();

                        // Comprobaciones de seguridad
                        if (result == null || result == DBNull.Value)
                        {
                            return false;
                        }

                        // Intentar convertir a entero de forma segura
                        if (int.TryParse(result.ToString(), out int count))
                        {
                            return count > 0;
                        }

                        // Si por alguna razón no se pudo convertir, asumimos que no existe
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Opcional: puedes loguear ex.Message
                MessageBox.Show("Error al verificar usuario: " + ex.Message);
                return false;
            }
        }

        // ✅ AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" || txtContrasena.Text == "" || comboTipoUsuario.SelectedIndex == -1 ||
       txtApaterno.Text == "" || txtAmaterno.Text == "")
            {
                MessageBox.Show("Llena todos los campos.");
                return;
            }

            string nombre = txtUsuario.Text.Trim();
            string apaterno = txtApaterno.Text.Trim();
            string amaterno = txtAmaterno.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            string tipo = comboTipoUsuario.SelectedItem.ToString().Split(' ')[0];

            // Si deseas verificar duplicados por nombre (no por ID)
            if (ExisteUsuario(nombre))
            {
                MessageBox.Show("Ese nombre de usuario ya existe. Usa otro.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"
            INSERT INTO Usuario (Nombre, Apaterno, Amaterno, idTipoUsuario, Contrasena)
            VALUES (@n, @aP, @aM, @t, @c)";

                SqlCommand cmd = new SqlCommand(query, con);
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

        // ✅ MODIFICAR (ACTUALIZAR)
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Selecciona un usuario.");
                return;
            }

            string idUsuario = txtUsuario.Text.Trim();
            string tipo = comboTipoUsuario.SelectedItem.ToString().Split(' ')[0];

            if (!ExisteUsuario(idUsuario))
            {
                MessageBox.Show("El usuario no existe en la base.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "UPDATE Usuario SET Contrasena=@c, idTipoUsuario=@t WHERE idUsuario=@u";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@c", txtContrasena.Text.Trim());
                cmd.Parameters.AddWithValue("@t", tipo);
                cmd.Parameters.AddWithValue("@u", idUsuario);

                cmd.ExecuteNonQuery();
            }

            CargarUsuarios();
            LimpiarCampos();
        }

        // ✅ ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Selecciona un usuario.");
                return;
            }

            string idUsuario = txtUsuario.Text.Trim();

            if (!ExisteUsuario(idUsuario))
            {
                MessageBox.Show("El usuario no existe.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "DELETE FROM Usuario WHERE idUsuario=@u";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@u", idUsuario);
                cmd.ExecuteNonQuery();
            }

            CargarUsuarios();
            LimpiarCampos();
        }

        // ✅ SELECCIONAR USUARIO DE LA TABLA
        private void TablaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = TablaUsuarios.Rows[e.RowIndex];

            txtUsuario.Text = row.Cells["idUsuario"].Value?.ToString();
            txtContrasena.Text = row.Cells["Contrasena"].Value?.ToString();

            string tipo = row.Cells["idTipoUsuario"].Value?.ToString();

            if (tipo == "1")
                comboTipoUsuario.SelectedItem = "1 - Administrador";
            else if (tipo == "2")
                comboTipoUsuario.SelectedItem = "2 - Servicio";
            else if (tipo == "3")
                comboTipoUsuario.SelectedItem = "3 - Licenciada";
        }

        // ✅ LIMPIAR CAMPOS
        private void LimpiarCampos()
        {
            txtUsuario.Text = "";
            txtContrasena.Text = "";
            comboTipoUsuario.SelectedIndex = -1;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
