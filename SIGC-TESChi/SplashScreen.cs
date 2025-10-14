using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class SplashScreen : Form

    {
        private Timer timer;

        public SplashScreen()
        {
            InitializeComponent();

            // Configurar transparencia
            this.FormBorderStyle = FormBorderStyle.None;              // Sin bordes
            this.StartPosition = FormStartPosition.CenterScreen;      // Centrado
            this.BackColor = Color.Black;                             // Color de fondo (se puede cambiar)
            this.TransparencyKey = this.BackColor;                    // Hace invisible el color de fondo
            this.TopMost = true;                                      // Mantiene el splash al frente

            // Configurar temporizador
            timer = new Timer();
            timer.Interval = 3000; // Duración del splash en milisegundos (3 segundos)
            timer.Tick += Timer_Tick;

        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timer.Start(); // Iniciar temporizador
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop(); // Detener temporizador
            this.Hide();  // Ocultar splash

            // Abrir formulario principal (Login)
            Login mainForm = new Login();
            mainForm.Show();
        }


    }
}
