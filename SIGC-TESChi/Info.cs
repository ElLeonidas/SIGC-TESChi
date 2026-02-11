using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.IO;

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
                MessageBox.Show("No tienes permisos para realizar respaldos.", "Acceso denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                BackupManager.EjecutarBackupCompleto();
                var ruta = BackupManager.ObtenerRutaCarpetaBackups();

                MessageBox.Show("Respaldo realizado correctamente ✔\n\nSe guardó en:\n" + ruta,
                    "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            using (OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Respaldo SIGC (*.zip)|*.zip"
            })
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                DialogResult r = MessageBox.Show(
                    "⚠️ ESTA ACCIÓN SOBREESCRIBIRÁ TODA LA INFORMACIÓN.\n\n" +
                    "El sistema se cerrará para restaurar el respaldo y se abrirá nuevamente.\n\n" +
                    "¿Deseas continuar?",
                    "Confirmar restauración",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (r != DialogResult.Yes)
                    return;

                try
                {
                    // Nombre del proceso principal (sin .exe)
                    // Ajusta si tu exe se llama diferente
                    string procName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);

                    // Ruta del exe principal
                    string exePath = Application.ExecutablePath;

                    // RestoreTool debe estar junto al exe (en la carpeta instalada / bin)
                    string restoreToolPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SIGC.RestoreTool.exe");

                    MessageBox.Show("BaseDirectory:\n" + AppDomain.CurrentDomain.BaseDirectory +
                     "\n\nExecutablePath:\n" + Application.ExecutablePath);


                    if (!File.Exists(restoreToolPath))
                    {
                        MessageBox.Show(
                            "No se encontró SIGC.RestoreTool.exe junto a la aplicación.\n" +
                            "Asegúrate de incluirlo en el instalador/publicación.",
                            "Falta componente",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    // Lanzar restaurador
                    var psi = new ProcessStartInfo
                    {
                        FileName = restoreToolPath,
                        Arguments = $"\"{ofd.FileName}\" \"{procName}\" \"{exePath}\" 30000",
                        UseShellExecute = true
                    };

                    Process.Start(psi);

                    // Cerrar la app para liberar MDF/LDF
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al iniciar restauración");
                }
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
