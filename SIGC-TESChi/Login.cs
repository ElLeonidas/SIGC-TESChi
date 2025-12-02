using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class FrmLogin : Form
    {
        // 🔹 Cadena de conexión
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public FrmLogin()
        {
            InitializeComponent();

            // === FORM ===
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.AcceptButton = btnLogin; // Enter = Iniciar sesión

            // === LABELS TRANSPARENTES ===
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;

            // === TARJETA CON IMAGEN (pnlCard) ===
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.BackgroundImageLayout = ImageLayout.Stretch;

            // === PANEL LOGIN (glass) ===
            pnlLogin.BackColor = Color.FromArgb(200, 255, 255, 255); // blanco translúcido suave

            // === BOTÓN LOGIN ===
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.BackColor = Color.FromArgb(88, 63, 149);
            btnLogin.ForeColor = Color.White;

            // === TEXTBOX PASSWORD ===
            txtPassword.UseSystemPasswordChar = true;

            // === BOTÓN OJO ===
            btnOcultar.FlatStyle = FlatStyle.Flat;
            btnOcultar.FlatAppearance.BorderSize = 0;
            btnOcultar.Text = "👁";

            // Centrar panel login al cargar
            this.Load += FrmLogin_Load;

            // Eventos
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

            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT idUsuario, Username, Nombre, Apaterno, Amaterno, idTipoUsuario
                        FROM Usuario
                        WHERE Username = @user
                          AND contrasena = @pass;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", usuario);
                        cmd.Parameters.AddWithValue("@pass", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
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
                                // ❌ Usuario/contraseña incorrectos
                                MessageBox.Show(
                                    "Usuario o contraseña incorrectos.",
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

        // ========= EFECTO BLUR (opcional, puedes comentarlo si no lo usas) =========
        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        private enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        private enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Si te da problemas visuales, comenta esto:
            // HabilitarBlur();
        }

        private void HabilitarBlur()
        {
            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            int accentStructSize = System.Runtime.InteropServices.Marshal.SizeOf(accent);

            IntPtr accentPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(accentStructSize);
            System.Runtime.InteropServices.Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(this.Handle, ref data);
            System.Runtime.InteropServices.Marshal.FreeHGlobal(accentPtr);
        }

        private void FrmLogin_Load_1(object sender, EventArgs e)
        {

        }
    }
}
