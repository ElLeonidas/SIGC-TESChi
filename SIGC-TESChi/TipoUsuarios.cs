using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class TipoUsuarios : UserControl
    {
        public TipoUsuarios()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://youtube.com/@leonardodanielgarciagarcia1933?si=E8DpHCEjurBa4nVG";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
