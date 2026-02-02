using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class FrmLogin : Form
    {
        // Cadena de conexión
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        // Límite de intentos
        private int intentosFallidos = 0;
        private const int MAX_INTENTOS = 2;

        public FrmLogin()
        {
            InitializeComponent();

            txtPassword.UseSystemPasswordChar = true;


            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.AcceptButton = btnLogin;

            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;

            pnlCard.Dock = DockStyle.Fill;
            pnlCard.BackgroundImageLayout = ImageLayout.Stretch;

            pnlLogin.BackColor = Color.FromArgb(235, 255, 255, 255);

            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.BackColor = Color.FromArgb(88, 63, 149);
            btnLogin.ForeColor = Color.White;
            btnLogin.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            txtPassword.UseSystemPasswordChar = true;

            btnOcultar.FlatStyle = FlatStyle.Flat;
            btnOcultar.FlatAppearance.BorderSize = 0;
            btnOcultar.Text = "👁";

            this.Load += FrmLogin_Load;

            btnCerrarPrograma.Click += BtnCerrarPrograma_Click;
            btnLogin.Click += BtnLogin_Click;
            btnOcultar.Click += btnOcultar_Click;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            CentrarPanelLogin();
        }

        private void CentrarPanelLogin()
        {
            pnlLogin.Left = (pnlCard.ClientSize.Width - pnlLogin.Width) / 2;
            pnlLogin.Top = (pnlCard.ClientSize.Height - pnlLogin.Height) / 2;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CentrarPanelLogin();
        }

        private void BtnCerrarPrograma_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ====================== LOGIN ======================
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Ingrese usuario y contraseña.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (intentosFallidos >= MAX_INTENTOS)
            {
                MessageBox.Show("Ha excedido el número máximo de intentos. Intente más tarde.", "Bloqueo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT idUsuario, Username, Nombre, Apaterno, Amaterno, idTipoUsuario, contrasena
                        FROM Usuario
                        WHERE Username = @user;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", usuario);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string hashAlmacenado = reader.GetString(reader.GetOrdinal("contrasena"));

                                if (VerificarHashPBKDF2(password, hashAlmacenado))
                                {
                                    intentosFallidos = 0; // Reset contador

                                    // Datos de usuario
                                    SessionData.IdUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario"));
                                    SessionData.Username = reader.GetString(reader.GetOrdinal("Username"));
                                    SessionData.NombreCompleto =
                                        reader.GetString(reader.GetOrdinal("Nombre")) + " " +
                                        reader.GetString(reader.GetOrdinal("Apaterno")) + " " +
                                        reader.GetString(reader.GetOrdinal("Amaterno"));

                                    SessionData.IdTipoUsuario = reader.GetInt32(reader.GetOrdinal("idTipoUsuario"));

                                    // Abrir menú
                                    Menu menu = new Menu();
                                    menu.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    intentosFallidos++;
                                    MessageBox.Show(
                                        $"Usuario o contraseña incorrectos. Intento {intentosFallidos} de {MAX_INTENTOS}.",
                                        "Acceso denegado",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error
                                    );
                                }
                            }
                            else
                            {
                                intentosFallidos++;
                                MessageBox.Show(
                                    $"Usuario o contraseña incorrectos. Intento {intentosFallidos} de {MAX_INTENTOS}.",
                                    "Acceso denegado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== MOSTRAR / OCULTAR CONTRASEÑA ==================
        private void btnOcultar_Click(object sender, EventArgs e)
        {
            bool oculto = txtPassword.UseSystemPasswordChar;

            txtPassword.UseSystemPasswordChar = !oculto;
            btnOcultar.Text = oculto ? "🚫" : "👁";

            txtPassword.Focus();
            txtPassword.SelectionStart = txtPassword.Text.Length;
        }

        // ================== PBKDF2 ==================
        private string CrearHashPBKDF2(string password, out byte[] salt)
        {
            salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                100000,
                HashAlgorithmName.SHA256
            );

            byte[] hash = pbkdf2.GetBytes(32);

            byte[] hashBytes = new byte[48];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);

            return Convert.ToBase64String(hashBytes);
        }

        

private bool VerificarHashPBKDF2(string password, string hashAlmacenado)
    {
        try
        {
            // Formato esperado: iteraciones:saltBase64:hashBase64
            string[] partes = hashAlmacenado.Split(':');
            if (partes.Length != 3)
                return false;

            int iteraciones = int.Parse(partes[0]);
            byte[] salt = Convert.FromBase64String(partes[1]);
            byte[] hashOriginal = Convert.FromBase64String(partes[2]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iteraciones))
            {
                byte[] hashCalculado = pbkdf2.GetBytes(hashOriginal.Length);

                // Comparación segura (byte a byte)
                for (int i = 0; i < hashOriginal.Length; i++)
                {
                    if (hashCalculado[i] != hashOriginal[i])
                        return false;
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }


    // ================== CREAR NUEVO USUARIO ==================
    public void CrearUsuario(string username, string nombre, string apaterno, string amaterno, string password, int idTipoUsuario)
        {
            string hash = CrearHashPBKDF2(password, out byte[] salt);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Usuario (Username, Nombre, Apaterno, Amaterno, idTipoUsuario, contrasena)
                        VALUES (@user, @nombre, @ap, @am, @tipo, @pass);";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@ap", apaterno);
                        cmd.Parameters.AddWithValue("@am", amaterno);
                        cmd.Parameters.AddWithValue("@tipo", idTipoUsuario);
                        cmd.Parameters.AddWithValue("@pass", hash);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear usuario:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== ACTUALIZAR TODAS LAS CONTRASEÑAS A PBKDF2 ==================
        public void ActualizarContraseñasABaseSegura()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Traer todos los usuarios con contraseña actual
                    string selectQuery = "SELECT idUsuario, contrasena FROM Usuario";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            var usuarios = new System.Collections.Generic.List<(int id, string pass)>();

                            while (reader.Read())
                            {
                                int idUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario"));
                                string contrasena = reader.GetString(reader.GetOrdinal("contrasena"));
                                usuarios.Add((idUsuario, contrasena));
                            }

                            reader.Close();

                            // Actualizar cada usuario con PBKDF2
                            foreach (var usuario in usuarios)
                            {
                                string hash = CrearHashPBKDF2(usuario.pass, out byte[] salt);

                                string updateQuery = "UPDATE Usuario SET contrasena = @hash WHERE idUsuario = @id";
                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@hash", hash);
                                    updateCmd.Parameters.AddWithValue("@id", usuario.id);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }

                            MessageBox.Show("Todas las contraseñas se actualizaron correctamente a PBKDF2.", "Actualización completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar contraseñas:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ActualizarContraseñasABaseSegura();
        }
    }
}
