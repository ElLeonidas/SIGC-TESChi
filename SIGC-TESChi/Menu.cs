using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            // Carga por defecto el Lobby al abrir el menú
            LoadPanel(new Lobby());
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
