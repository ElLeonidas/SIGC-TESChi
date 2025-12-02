using System;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Lobby : UserControl
    {
        public Lobby()
        {
            InitializeComponent();
        }

        private void Lobby_Load(object sender, EventArgs e)
        {
            lblUsuarioActivo.Text = "Sesión iniciada por: " + SessionData.NombreCompleto;
        }

        private void lblUsuarioActivo_Click(object sender, EventArgs e)
        {

        }
    }
}
