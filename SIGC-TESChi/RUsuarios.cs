using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;


namespace SIGC_TESChi
{
    public partial class RUsuarios : UserControl
    {
        private ToolTip toolTip;

        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public RUsuarios()
        {
            InitializeComponent();

            txtContrasena.UseSystemPasswordChar = true;

            TablaUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TablaUsuarios.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TablaUsuarios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TablaUsuarios.Dock = DockStyle.Fill;

            // Eventos
            Load += RUsuarios_Load;
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnBuscar.Click += btnBuscar_Click;
            TablaUsuarios.CellClick += TablaUsuarios_CellClick;

            // txtID no editable
            txtIdentificador.ReadOnly = true;

            // ToolTips (IGUAL QUE UBICACIONES)
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 200;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;

            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nuevo Usuario", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);

            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Usuario", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);

            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Usuario", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);

            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);

            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar Usuario", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);
        }

        private void RUsuarios_Load(object sender, EventArgs e)
        {

            if (SessionData.IdTipoUsuario != 1)
            {
                MessageBox.Show("No tienes permisos para acceder a este módulo");
                this.Enabled = false;
            }


            CargarTiposUsuario();
            CargarUsuarios();

        }

        //  CARGAS 
        private void CargarTiposUsuario()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string q = "SELECT idTipoUsuario, dTipoUsuario FROM TipoUsuario";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboTipoUsuario.DataSource = dt;
                comboTipoUsuario.DisplayMember = "dTipoUsuario"; // texto visible
                comboTipoUsuario.ValueMember = "idTipoUsuario";  // INT real
            }

            comboTipoUsuario.SelectedIndex = -1;
        }



        private void CargarUsuarios()
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
            T.dTipoUsuario AS TipoUsuario
        FROM Usuario U
        INNER JOIN TipoUsuario T ON U.idTipoUsuario = T.idTipoUsuario";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                TablaUsuarios.DataSource = null;
                TablaUsuarios.Columns.Clear();
                TablaUsuarios.AutoGenerateColumns = true;
                TablaUsuarios.DataSource = dt;

                // 🔹 CAMBIAR TÍTULOS DE COLUMNAS
                TablaUsuarios.Columns["idUsuario"].HeaderText = "Identificador";
                TablaUsuarios.Columns["Username"].HeaderText = "Username";
                TablaUsuarios.Columns["Nombre"].HeaderText = "Nombre";
                TablaUsuarios.Columns["Amaterno"].HeaderText = "Apellido Materno";
                TablaUsuarios.Columns["Apaterno"].HeaderText = "Apellido Paterno";
                TablaUsuarios.Columns["TipoUsuario"].HeaderText = "Tipo de Usuario";

                // 🔒 Ocultar ID
                TablaUsuarios.Columns["idTipoUsuario"].Visible = false;
            }
        }




        public static string CrearHashPBKDF2(string password)
        {
            int iterations = 100000;
            byte[] salt = new byte[16];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                return iterations + ":" +
                       Convert.ToBase64String(salt) + ":" +
                       Convert.ToBase64String(hash);
            }
        }


        private void AgregarUsuario()
        {
            if (CamposVaciosAgregar())
            {
                MessageBox.Show("⚠️ Llena todos los campos.");
                return;
            }

            if (ExisteUsuario(txtNombreAcceso.Text.Trim()))
            {
                MessageBox.Show("⚠️ Este usuario ya está registrado.");
                return;
            }

            string tipo = comboTipoUsuario.SelectedItem.ToString().Split(' ')[0];

            string hashPassword = CrearHashPBKDF2(txtContrasena.Text.Trim());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string q = @"
INSERT INTO Usuario
(Username, Nombre, Apaterno, Amaterno, Contrasena, idTipoUsuario)
VALUES (@u,@n,@ap,@am,@c,@t)";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@u", txtNombreAcceso.Text.Trim());
                cmd.Parameters.AddWithValue("@n", txtUsuario.Text.Trim());
                cmd.Parameters.AddWithValue("@ap", txtApaterno.Text.Trim());
                cmd.Parameters.AddWithValue("@am", txtAmaterno.Text.Trim());
                cmd.Parameters.AddWithValue("@c", hashPassword); // 🔐 HASH
                cmd.Parameters.AddWithValue("@t", tipo);
                cmd.ExecuteNonQuery();
            }

            string datosNuevos =
                $"Username={txtNombreAcceso.Text}, Nombre={txtUsuario.Text}, " +
                $"Apaterno={txtApaterno.Text}, Amaterno={txtAmaterno.Text}, Tipo={tipo}";

            HistorialHelper.RegistrarCambio(
                "Usuario",
                txtNombreAcceso.Text.Trim(),
                "INSERT",
                null,
                datosNuevos
            );

            MessageBox.Show("✅ Usuario agregado.");
            CargarUsuarios();
            LimpiarCampos();
        }

        private void ModificarUsuario()
        {
            if (string.IsNullOrWhiteSpace(txtIdentificador.Text))
            {
                MessageBox.Show("⚠️ Selecciona un usuario.");
                return;
            }

            if (CamposVaciosModificar())
            {
                MessageBox.Show("⚠️ Llena todos los campos obligatorios.");
                return;
            }

            int tipo = Convert.ToInt32(comboTipoUsuario.SelectedValue);
            string datosAnteriores = "";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string select = @"
        SELECT Username, Nombre, Apaterno, Amaterno, idTipoUsuario
        FROM Usuario WHERE idUsuario=@id";

                SqlCommand selCmd = new SqlCommand(select, con);
                selCmd.Parameters.AddWithValue("@id", txtIdentificador.Text);

                using (SqlDataReader dr = selCmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        datosAnteriores =
                            $"Username={dr["Username"]}, Nombre={dr["Nombre"]}, " +
                            $"Apaterno={dr["Apaterno"]}, Amaterno={dr["Amaterno"]}, Tipo={dr["idTipoUsuario"]}";
                    }
                }

                string q = @"
        UPDATE Usuario SET
            Username=@u,
            Nombre=@n,
            Apaterno=@ap,
            Amaterno=@am,
            idTipoUsuario=@t";

                bool cambiarPassword =
                    !string.IsNullOrWhiteSpace(txtContrasena.Text) &&
                    txtContrasena.Text != "********";

                if (cambiarPassword)
                    q += ", Contrasena=@c";

                q += " WHERE idUsuario=@id";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@u", txtNombreAcceso.Text.Trim());
                cmd.Parameters.AddWithValue("@n", txtUsuario.Text.Trim());
                cmd.Parameters.AddWithValue("@ap", txtApaterno.Text.Trim());
                cmd.Parameters.AddWithValue("@am", txtAmaterno.Text.Trim());
                cmd.Parameters.AddWithValue("@t", tipo);
                cmd.Parameters.AddWithValue("@id", txtIdentificador.Text);

                if (cambiarPassword)
                {
                    string hashPassword = CrearHashPBKDF2(txtContrasena.Text.Trim());
                    cmd.Parameters.AddWithValue("@c", hashPassword);
                }

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("✅ Usuario modificado.");
            CargarUsuarios();
            LimpiarCampos();
        }




        private void EliminarUsuario()
        {
            if (string.IsNullOrWhiteSpace(txtIdentificador.Text))
            {
                MessageBox.Show("⚠️ Selecciona un usuario.");
                return;
            }

            if (MessageBox.Show("¿Seguro de eliminar?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            string datosAnteriores =
                $"Username={txtNombreAcceso.Text}, Nombre={txtUsuario.Text}, " +
                $"Apaterno={txtApaterno.Text}, Amaterno={txtAmaterno.Text}";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Usuario WHERE idUsuario=@id", con);
                cmd.Parameters.AddWithValue("@id", txtIdentificador.Text);
                cmd.ExecuteNonQuery();
            }

            // 🔴 HISTORIAL (DELETE)
            HistorialHelper.RegistrarCambio(
                "Usuario",
                txtIdentificador.Text,
                "DELETE",
                datosAnteriores,
                null
            );

            MessageBox.Show("✅ Usuario eliminado.");
            CargarUsuarios();
            LimpiarCampos();
        }



        //AGREGAR 
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarUsuario();
        }       

        //MODIFICAR 
        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarUsuario();
        }
         
        // ELIMINAR 
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarUsuario();
        }      

        // BUSCAR 
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUsuarios();
        }

        private void BuscarUsuarios()
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
                    T.dTipoUsuario AS TipoUsuario
                FROM Usuario U
                INNER JOIN TipoUsuario T ON U.idTipoUsuario = T.idTipoUsuario
                WHERE 1=1";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                if (!string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    query += " AND U.Nombre LIKE @n";
                    cmd.Parameters.AddWithValue("@n", "%" + txtUsuario.Text + "%");
                }

                if (comboTipoUsuario.SelectedIndex != -1)
                {
                    string tipo = comboTipoUsuario.SelectedItem.ToString().Split(' ')[0];
                    query += " AND U.idTipoUsuario=@t";
                    cmd.Parameters.AddWithValue("@t", tipo);
                }

                cmd.CommandText = query;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                TablaUsuarios.DataSource = dt;
            }
        }

        //TABLA 
        private void TablaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = TablaUsuarios.Rows[e.RowIndex];

            txtIdentificador.Text = fila.Cells["idUsuario"].Value.ToString();
            txtNombreAcceso.Text = fila.Cells["Username"].Value.ToString();
            txtUsuario.Text = fila.Cells["Nombre"].Value.ToString();
            txtApaterno.Text = fila.Cells["Apaterno"].Value.ToString();
            txtAmaterno.Text = fila.Cells["Amaterno"].Value.ToString();

            // 🔐 Mostrar máscara
            txtContrasena.Text = "********";

            // 👤 Seleccionar tipo usando el ID REAL
            comboTipoUsuario.SelectedValue = fila.Cells["idTipoUsuario"].Value;
        }

        private bool ExisteUsuario(string username)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Usuario WHERE Username=@u", con);
                cmd.Parameters.AddWithValue("@u", username);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private bool CamposVaciosAgregar()
        {
            return string.IsNullOrWhiteSpace(txtNombreAcceso.Text) ||
                   string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                   string.IsNullOrWhiteSpace(txtApaterno.Text) ||
                   string.IsNullOrWhiteSpace(txtAmaterno.Text) ||
                   string.IsNullOrWhiteSpace(txtContrasena.Text) ||
                   comboTipoUsuario.SelectedIndex == -1;
        }

        private bool CamposVaciosModificar()
        {
            return string.IsNullOrWhiteSpace(txtNombreAcceso.Text) ||
                   string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                   string.IsNullOrWhiteSpace(txtApaterno.Text) ||
                   string.IsNullOrWhiteSpace(txtAmaterno.Text) ||
                   comboTipoUsuario.SelectedIndex == -1;
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarUsuarios();
        }

        private void LimpiarCampos()
        {
            txtIdentificador.Clear();
            txtNombreAcceso.Clear();
            txtUsuario.Clear();
            txtApaterno.Clear();
            txtAmaterno.Clear();
            txtContrasena.Clear();
            comboTipoUsuario.SelectedIndex = -1;
            TablaUsuarios.ClearSelection();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                btnAgregar.PerformClick(); // Ejecuta el botón Agregar
                return true; // Indica que la tecla fue manejada
            }

            if (keyData == (Keys.Control | Keys.Delete))
            {
                btnEliminar.PerformClick(); // Ejecuta el botón Eliminar
                return true;
            }

            if (keyData == (Keys.Control | Keys.F))
            {
                btnBuscar.PerformClick(); // Ejecuta el botón Buscar
                return true;
            }

            if (keyData == (Keys.Control | Keys.H))
            {
                btnModificar.PerformClick(); // Ejecuta el botón Modificar
                return true;
            }

            if (keyData == (Keys.Control | Keys.N))
            {
                btnLimpiar.PerformClick(); // Ejecuta el botón Limpiar
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
