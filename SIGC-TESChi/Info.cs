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
    public partial class Info : UserControl
    {

        public event Action<bool> ModoOscuroCambiado;


        public Info()
        {
            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {

        }

        private void cboxModoOscuro_CheckedChanged(object sender, EventArgs e)
        {
            ModoOscuroCambiado?.Invoke(cboxModoOscuro.Checked);
        }
    }
}
