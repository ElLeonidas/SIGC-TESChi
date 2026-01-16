using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Info : UserControl
    {
        // Eventos
        public event Action<bool> ModoOscuroCambiado;
        public event Action<string> FuenteCambiada; //  NUEVO

        public Info()
        {
            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {
            // Cargar fuentes
            foreach (FontFamily font in FontFamily.Families)
            {
                cmbFuentes.Items.Add(font.Name);
            }

            cmbFuentes.SelectedItem = FontManager.FuenteActual.FontFamily.Name;
        }

        private void cboxModoOscuro_CheckedChanged(object sender, EventArgs e)
        {
            ModoOscuroCambiado?.Invoke(cboxModoOscuro.Checked);
        }

        //  AQUÍ ESTABA TODO EL PROBLEMA
        private void cmbFuentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFuentes.SelectedItem == null)
                return;

            // SOLO AVISA
            FuenteCambiada?.Invoke(cmbFuentes.SelectedItem.ToString());
        }
    }
}
