using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Login : Form
    {

        // 🔹 Cadena de conexión (ajústala si tu instancia/localdb es diferente)
        string connectionString = @"Server=.\SQLEXPRESS;Database=SGCTESCHI;Trusted_Connection=True;";

        public Login()
        {
            InitializeComponent();

            // Configurar transparencia
            this.FormBorderStyle = FormBorderStyle.None;              // Sin bordes
            this.StartPosition = FormStartPosition.CenterScreen;      // Centrado
            this.BackColor = Color.Black;                             // Color de fondo (se puede cambiar)
            this.TransparencyKey = this.BackColor;                    // Hace invisible el color de fondo
            this.TopMost = true;

            pnlLogin.BackColor = Color.Transparent;

            // Agregamos evento de pintado personalizado
            pnlLogin.Paint += PnlLogin_Paint;

        }

        private void PnlLogin_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Color de fondo con transparencia
            Color colorInicio = Color.FromArgb(150, 30, 0, 60); // RGBA
            Color colorFin = Color.FromArgb(150, 50, 0, 150);

            using (LinearGradientBrush brush = new LinearGradientBrush(panel.ClientRectangle, colorInicio, colorFin, LinearGradientMode.Vertical))
            {
                using (GraphicsPath path = GetRoundedRectanglePath(panel.ClientRectangle, 30))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        // Función para crear un rectángulo con bordes redondeados
        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            // Obtiene los valores de los TextBox (suponiendo txtUsuario y txtContrasena)
            string usuario = txtboxUsuario.Text.Trim();
            string contrasena = txtboxPassword.Text.Trim();

            if (usuario == "" || contrasena == "")
            {
                MessageBox.Show("Por favor, ingresa usuario y contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT u.idUsuario, tu.dTipoUsuario 
                                     FROM Usuario u
                                     INNER JOIN TipoUsuario tu ON u.idTipoUsuario = tu.idTipoUsuario
                                     WHERE u.idUsuario = @idUsuario AND u.contrasena = @contrasena";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@idUsuario", usuario);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string tipoUsuario = reader["dTipoUsuario"].ToString();

                        //MessageBox.Show("Bienvenido " + tipoUsuario, "Acceso permitido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Abrir formulario Menu
                        Menu menu = new Menu();
                        menu.Show();

                        this.Hide();
                    }
                    else
                    {
                        //MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtboxUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
