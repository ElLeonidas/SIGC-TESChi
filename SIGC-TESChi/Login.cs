using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
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

            diseñologin();

            txtUsuario.Enter += (s, e) => pnlUsuario.Invalidate();
            txtUsuario.Leave += (s, e) => pnlUsuario.Invalidate();

            DiseñoBoton();

            btnLogin.TabStop = false;

            PanelCard();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.AcceptButton = btnLogin;

            label1.BackColor = Color.Transparent;

            pnlCard.Dock = DockStyle.Fill;
            pnlCard.BackgroundImageLayout = ImageLayout.Stretch;


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

        #region DISEÑO



        private void ControlCard(Control control, int radio)
        {
            GraphicsPath path = new GraphicsPath();

            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(control.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(control.Width - radio, control.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, control.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();

            control.Region = new Region(path);
            control.Tag = path;
        }

        private void PanelCard()
        {
            pnlCard.Size = new Size(449, 310);
            pnlLogin.BackColor = Color.FromArgb(245, 255, 255, 255); // blanco suave
            pnlLogin.Padding = new Padding(25);

            ControlCard(pnlLogin, 25);

        }

        private void ControlPildora(Control control)
        {
            int radio = control.Height;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(control.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(control.Width - radio, control.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, control.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();

            control.Region = new Region(path);
            control.Tag = path;
        }

        private void ControlConBorde(object sender, PaintEventArgs e)
        {
            Control control = sender as Control;
            if (control == null) return;

            GraphicsPath path = control.Tag as GraphicsPath;
            if (path == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 1))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }


        private void DiseñoBoton()
        {
            btnLogin.Size = new Size(320, 42);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.BackColor = Color.FromArgb(88, 63, 149); // morado elegante
            btnLogin.ForeColor = Color.White;
            btnLogin.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;

            btnLogin.Left = (pnlLogin.Width - btnLogin.Width) / 2;

            Color colorNormal = Color.FromArgb(88, 63, 149);
            Color colorHover = Color.FromArgb(105, 78, 175);
            Color colorClick = Color.FromArgb(70, 50, 120);

            btnLogin.MouseEnter += (s, e) =>
            {
                btnLogin.BackColor = colorHover;
            };

            btnLogin.MouseLeave += (s, e) =>
            {
                btnLogin.BackColor = colorNormal;
            };

            btnLogin.MouseDown += (s, e) =>
            {
                btnLogin.BackColor = colorClick;
            };

            btnLogin.MouseUp += (s, e) =>
            {
                btnLogin.BackColor = colorHover;
            };

            btnLogin.EnabledChanged += (s, e) =>
            {
                btnLogin.BackColor = btnLogin.Enabled
                    ? colorNormal
                    : Color.LightGray;
            };


        }

        private void PanelConBorde(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null) return;

            GraphicsPath path = panel.Tag as GraphicsPath;
            if (path == null) return;


            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.LightGray, 1))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void PanelPildora(Panel panel)
        {
            int radio = panel.Height;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(panel.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(panel.Width - radio, panel.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, panel.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();

            panel.Region = new Region(path);

            // 👇 Guardamos el path para el Paint
            panel.Tag = path;
        }

            private bool passwordVisible = false;
            private const string PLACEHOLDER_USER = "Username";
            private const string PLACEHOLDER_PASS = "Password";

        public void diseñologin()
        {
            txtUsuario.Text = PLACEHOLDER_USER;
            txtUsuario.ForeColor = Color.Gray;

            txtUsuario.Enter += (s, e) =>
            {
                if (txtUsuario.Text == PLACEHOLDER_USER)
                {
                    txtUsuario.Text = "";
                    txtUsuario.ForeColor = Color.Black;
                }
            };

            txtUsuario.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    txtUsuario.Text = PLACEHOLDER_USER;
                    txtUsuario.ForeColor = Color.Gray;
                }
            };

            txtPassword.Text = PLACEHOLDER_PASS;
            txtPassword.ForeColor = Color.Gray;
            txtPassword.UseSystemPasswordChar = false;

            txtPassword.Enter += (s, e) =>
            {
                if (txtPassword.Text == PLACEHOLDER_PASS)
                {
                    txtPassword.Text = "";
                    txtPassword.ForeColor = Color.Black;
                    txtPassword.UseSystemPasswordChar = !passwordVisible;
                }
            };

            txtPassword.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    txtPassword.UseSystemPasswordChar = false;
                    txtPassword.Text = PLACEHOLDER_PASS;
                    txtPassword.ForeColor = Color.Gray;
                }
            };

            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Location = new Point(10, (pnlUsuario.Height - txtUsuario.Height) / 2);
            txtUsuario.Width = pnlUsuario.Width - 20;

            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Location = new Point(10, (pnlPassword.Height - txtPassword.Height) / 2);
            txtPassword.Width = pnlPassword.Width - 45; // deja espacio al ojo

            pnlUsuario.BackColor = Color.White;
            pnlPassword.BackColor = Color.White;

            pnlUsuario.Paint += PanelConBorde;
            pnlPassword.Paint += PanelConBorde;

            int espacioOjo = 35;

            // =======================
            // USUARIO
            // =======================
            picUsuario.Left = 12;

            txtUsuario.Left = picUsuario.Right + 8;
            txtUsuario.Width = pnlUsuario.Width
                               - txtUsuario.Left
                               - 10;


            // =======================
            // PASSWORD
            // =======================
            picPassword.Left = 12;

            txtPassword.Left = picPassword.Right + 8;
            txtPassword.Width = pnlPassword.Width
                                - txtPassword.Left
                                - espacioOjo;



            //Redondear(txtUsuario, txtUsuario.Height);
            //Redondear(txtPassword, txtPassword.Height);

        }

        #endregion

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            CentrarPanelLogin();
            PanelPildora(pnlUsuario);
            PanelPildora(pnlPassword);

            ControlPildora(pnlUsuario);
            ControlPildora(pnlPassword);
            ControlPildora(btnLogin);

            pnlUsuario.Paint += ControlConBorde;
            pnlPassword.Paint += ControlConBorde;
            // btnLogin normalmente no necesita borde

        }

        private void CentrarPanelLogin()
        {
            //pnlLogin.Left = (pnlCard.ClientSize.Width - pnlLogin.Width) / 2;
            //pnlLogin.Top = (pnlCard.ClientSize.Height - pnlLogin.Height) / 2;
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

        #region LOGIN

        public static bool VerificarHashPBKDF2(string password, string hashAlmacenado)
        {
            var partes = hashAlmacenado.Split(':');
            if (partes.Length != 3) return false;

            int iterations = int.Parse(partes[0]);
            byte[] salt = Convert.FromBase64String(partes[1]);
            byte[] hashOriginal = Convert.FromBase64String(partes[2]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hashCalculado = pbkdf2.GetBytes(hashOriginal.Length);

                for (int i = 0; i < hashOriginal.Length; i++)
                    if (hashCalculado[i] != hashOriginal[i])
                        return false;
            }
            return true;
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

        #endregion

        // ================== MOSTRAR / OCULTAR CONTRASEÑA ==================
        private void btnOcultar_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == PLACEHOLDER_PASS)
                return; // ⛔ no hacer nada si es placeholder

            passwordVisible = !passwordVisible;
            txtPassword.UseSystemPasswordChar = !passwordVisible;

            btnOcultar.Text = passwordVisible ? "🙈" : "👁";
        }


        #region PBKDF2

        

        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string hash = HashHelper.CrearHashPBKDF2("admin123");

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Usuario SET contrasena = @c WHERE Username = 'admin'", conn);

                cmd.Parameters.AddWithValue("@c", hash);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Contraseña actualizada");
            button1.Visible = false; // 👻
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
