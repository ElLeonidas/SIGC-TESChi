using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    internal static class FontManager
    {
        private const float TAMANO_FIJO = 9F;

        public static Font FuenteActual = new Font("Segoe UI", TAMANO_FIJO);

      
        // 🔹 Devuelve SOLO 10 fuentes y solo si están instaladas
        public static List<string> ObtenerTop10Fuentes()
        {
            InstalledFontCollection fuentesInstaladas = new InstalledFontCollection();

            string[] fuentesPopulares =
            {
        "Segoe UI",
        "Arial",
        "Calibri",
        "Times New Roman",
        "Verdana",
        "Tahoma",
        "Microsoft Sans Serif",
        "Trebuchet MS",
        "Georgia",
        "Courier New"
    };

            return fuentesPopulares
                .Where(f => fuentesInstaladas.Families.Any(i => i.Name == f))
                .Take(10)
                .ToList();
        }


        public static void CambiarFuente(string nombreFuente)
        {
            FuenteActual = new Font(nombreFuente, TAMANO_FIJO);
        }

        public static void AplicarFuente(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is Label ||
                    c is TextBox ||
                    c is RichTextBox ||
                    c is CheckedListBox ||
                    c is DateTimePicker ||
                    c is CheckBox ||
                    c is ComboBox)
                {
                    c.Font = FuenteActual;
                }

                if (c.HasChildren)
                {
                    AplicarFuente(c);
                }
            }
        }
    }
}
