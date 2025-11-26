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
    public partial class DetallesCambio : Form
    {
        // VARIABLES PARA RECIBIR DATOS
        public string Tabla { get; set; }
        public string Llave { get; set; }
        public string Accion { get; set; }
        public string Usuario { get; set; }
        public string Fecha { get; set; }
        public string DatosAnteriores { get; set; }
        public string DatosNuevos { get; set; }

        public DetallesCambio()
        {
            InitializeComponent();
        }

        // 🔹 Cuando el form se cargue, llenar los campos
        private void DetallesCambio_Load(object sender, EventArgs e)
        {
            txtTabla.Text = Tabla;
            txtLlave.Text = Llave;
            txtAccion.Text = Accion;
            txtUsuario.Text = Usuario;
            txtFecha.Text = Fecha;

            richAnterior.Text = DatosAnteriores;
            richNuevo.Text = DatosNuevos;
        }
    }
}
