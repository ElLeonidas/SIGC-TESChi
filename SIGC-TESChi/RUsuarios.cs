using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class RUsuarios : UserControl
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=SGCTESCHI;Trusted_Connection=True;";

        public RUsuarios()
        {
            InitializeComponent();

            // Enlazamos eventos manualmente para evitar fallas
            Load += RUsuarios_Load;
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;   // ← CORREGIDO
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            TablaUsuarios.CellClick += TablaUsuarios_CellClick;
        }

        private void RUsuarios_Load(object sender, EventArgs e)
        {
            CargarTiposUsuario();
            CargarUsuarios();
        }

        // ✅ Cargar ComboBox
        private void CargarTiposUsuario()
        {
            comboTipoUsuario.Items.Clear();
            comboTipoUsuario.Items.Add("1 - Administrador");
            comboTipoUsuario.Items.Add("2 - Servicio");
            comboTipoUsuario.Items.Add("3 - Licenciada");
            comboTipoUsuario.SelectedIndex = -1;
        }

        // ✅ Cargar usuarios desde SQL (tabla: Usuario)
        private void CargarUsuarios()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT idUsuario, idTipoUsuario, Contrasena FROM Usuario";
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

        // ✅ Verificar si existe el usuario (idUsuario = nombre)
        private bool ExisteUsuario(string idUsuario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string q = "SELECT COUNT(*) FROM Usuario WHERE idUsuario = @u";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@u", idUsuario);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // ✅ AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" || txtContrasena.Text == "" || comboTipoUsuario.SelectedIndex == -1)
            {
                MessageBox.Show("Llena todos los campos.");
                return;
            }

            string idUsuario = txtUsuario.Text.Trim();
            string tipo = comboTipoUsuario.SelectedItem.ToString().Split(' ')[0];

            if (ExisteUsuario(idUsuario))
            {
                MessageBox.Show("Ese nombre de usuario ya existe. Usa otro.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Usuario (idUsuario, Contrasena, idTipoUsuario) VALUES (@u, @c, @t)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@u", idUsuario);
                cmd.Parameters.AddWithValue("@c", txtContrasena.Text.Trim());
                cmd.Parameters.AddWithValue("@t", tipo);

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
    }
}
