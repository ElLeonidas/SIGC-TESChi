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

            // 🔐 PERMISOS
            bool esAdmin = Permisos.EsAdministrador;

            btnBackup.Enabled = esAdmin;
            btnRestaurar.Enabled = esAdmin;

            if (!esAdmin)
            {
                btnBackup.Text = "Backup (Solo admin)";
                btnRestaurar.Text = "Restaurar (Solo admin)";
            }
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

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (!Permisos.EsAdministrador)
            {
                MessageBox.Show(
                    "No tienes permisos para realizar respaldos.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop
                );
                return;
            }

            try
            {
                BackupManager.EjecutarBackupCompleto();
                MessageBox.Show("Respaldo realizado correctamente ✔", "Backup");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (!Permisos.EsAdministrador)
            {
                MessageBox.Show(
                    "No tienes permisos para restaurar respaldos.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop
                );
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Respaldo SIGC (*.zip)|*.zip"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            DialogResult r = MessageBox.Show(
                "⚠️ ESTA ACCIÓN SOBREESCRIBIRÁ TODA LA INFORMACIÓN.\n\n¿Deseas continuar?",
                "Confirmar restauración",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (r != DialogResult.Yes)
                return;

            try
            {
                BackupManager.RestaurarBackup(ofd.FileName);
                MessageBox.Show("Restauración completada correctamente ✔", "Restore");
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al restaurar");
            }
        }
    }
}
