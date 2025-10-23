using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ToolTip = System.Windows.Forms.ToolTip;

namespace SIGC_TESChi
{
    public partial class Menu : Form
    {
        private ToolTip toolTip;

        public Menu()
        {
            InitializeComponent();

            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;  // visible por 5 segundos
            toolTip.InitialDelay = 200;   // aparece tras 0.2 segundos
            toolTip.ReshowDelay = 100;    // retardo entre botones
            toolTip.ShowAlways = true;    // siempre visible
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            // Carga por defecto el Lobby al abrir el menú
            LoadPanel(new Lobby());

            // ✅ Asigna los textos aquí (solo una vez al cargar el formulario)
            toolTip.SetToolTip(btnLobby, "Ir al Lobby principal");
            toolTip.SetToolTip(btnRUsuarios, "Registrar nuevos usuarios");
            toolTip.SetToolTip(btnCArchivos, "Consultar archivos del sistema");
            toolTip.SetToolTip(btnUbicaciones, "Consultar ubicaciones de los archivos");
            toolTip.SetToolTip(btnSecciones, "Consultar secciones de los archivos");
        }

        // 👉 Método para cargar cualquier UserControl en el panel Categorias
        private void LoadPanel(UserControl control)
        {
            Categorias.Controls.Clear();      // Limpia el panel
            control.Dock = DockStyle.Fill;    // Se ajusta al tamaño del panel
            Categorias.Controls.Add(control); // Agrega el nuevo control
        }

        private void btnLobby_Click(object sender, EventArgs e)
        {
            LoadPanel(new Lobby());
        }

        private void btnRUsuarios_Click(object sender, EventArgs e)
        {
            LoadPanel(new RUsuarios());
        }

        private void btnCArchivos_Click(object sender, EventArgs e)
        {
            LoadPanel(new CArchivos());
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }
    }
}
