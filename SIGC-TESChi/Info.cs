using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Info : UserControl
    {
        // Eventos
        public event Action<bool> ModoOscuroCambiado;
        public event Action<string> FuenteCambiada; //  NUEVO
        public event Action<string> TamanoCambiado;


        public Info()
        {
            InitializeComponent();
        }

        private bool cargando = true;

        private void Info_Load(object sender, EventArgs e)
        {
            cargando = true;

            cmbTamaño.Items.AddRange(new object[]
            {
        "Pequeño",
        "Normal",
        "Grande"
            });
            cmbTamaño.SelectedIndex = 1;

            CargarFuentes();
            cmbFuentes.SelectedItem = FontManager.FuenteActual.FontFamily.Name;

            cargando = false;

        }

        private void CargarFuentes()
        {
            cmbFuentes.DataSource = null;
            cmbFuentes.Items.Clear();

            cmbFuentes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFuentes.DataSource = FontManager.ObtenerTop10Fuentes();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbTamaño_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargando || cmbTamaño.SelectedItem == null)
                return;

            TamanoCambiado?.Invoke(cmbTamaño.SelectedItem.ToString());
        }
    }
}
