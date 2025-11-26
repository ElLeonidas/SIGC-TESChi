using System;
using System.Windows.Forms;
using ToolTip = System.Windows.Forms.ToolTip;

namespace SIGC_TESChi
{
    public partial class Menu : Form
    {
        private ToolTip toolTip;

        public Menu()
        {
            InitializeComponent();

            // Evitar que la ventana se agrande o cambie de tamaño
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;  // Visible 5 segundos
            toolTip.InitialDelay = 200;   // Aparece tras 0.2 segundos
            toolTip.ReshowDelay = 100;    // Retardo entre botones
            toolTip.ShowAlways = true;    // Siempre visible

            // Asignar eventos de mouse para mostrar tooltips
            btnLobby.MouseEnter += (s, e) => toolTip.Show("Ir al Lobby principal", btnLobby);
            btnLobby.MouseLeave += (s, e) => toolTip.Hide(btnLobby);

            btnTipoUsuarios.MouseEnter += (s, e) => toolTip.Show("Registrar nuevos Tipos de usuarios", btnTipoUsuarios);
            btnTipoUsuarios.MouseLeave += (s, e) => toolTip.Hide(btnTipoUsuarios);

            btnRUsuarios.MouseEnter += (s, e) => toolTip.Show("Registrar nuevos usuarios", btnRUsuarios);
            btnRUsuarios.MouseLeave += (s, e) => toolTip.Hide(btnRUsuarios);

            btnCArchivos.MouseEnter += (s, e) => toolTip.Show("Consultar archivos del sistema", btnCArchivos);
            btnCArchivos.MouseLeave += (s, e) => toolTip.Hide(btnCArchivos);

            btnUbicaciones.MouseEnter += (s, e) => toolTip.Show("Consultar ubicaciones de los archivos", btnUbicaciones);
            btnUbicaciones.MouseLeave += (s, e) => toolTip.Hide(btnUbicaciones);

            btnSecciones.MouseEnter += (s, e) => toolTip.Show("Consultar secciones de los archivos", btnSecciones);
            btnSecciones.MouseLeave += (s, e) => toolTip.Hide(btnSecciones);

            btnHistorial.MouseEnter += (s, e) => toolTip.Show("Consultar el Historial", btnHistorial);
            btnHistorial.MouseLeave += (s, e) => toolTip.Hide(btnHistorial);

            btnCerrarSesion.MouseEnter += (s, e) => toolTip.Show("Cerrar sesión", btnCerrarSesion);
            btnCerrarSesion.MouseLeave += (s, e) => toolTip.Hide(btnCerrarSesion);
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            // Carga el panel por defecto al iniciar
            LoadPanel(new Lobby());
        }

        // Método para cargar cualquier UserControl en el panel Categorias
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

        private void btnSecciones_Click(object sender, EventArgs e)
        {
            LoadPanel(new Secciones());
        }

        private void btnUbicaciones_Click(object sender, EventArgs e)
        {
            LoadPanel(new Ubicaciones());
        }

        private void btnEstadistica_Click(object sender, EventArgs e)
        {

        }
        private void btnTipoUsuarios_Click(object sender, EventArgs e)
        {
            LoadPanel(new TipoUsuarios());
        }

        private void btnSubSecciones_Click(object sender, EventArgs e)
        {
            LoadPanel(new SubSecciones());
        }

        private void btnFormato_Click(object sender, EventArgs e)
        {
            LoadPanel(new CaratulaExpediente());
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            LoadPanel(new Historial());
        }
    }
}

