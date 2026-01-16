using System.Drawing;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    internal static class FontManager
    {
        private const float TAMANO_FIJO = 9F;

        public static Font FuenteActual = new Font("Segoe UI", TAMANO_FIJO);

        public static void CambiarFuente(string nombreFuente)
        {
            FuenteActual = new Font(nombreFuente, TAMANO_FIJO);
        }

        public static void AplicarFuente(Control control)
        {
            foreach (Control c in control.Controls)
            {
                // ✅ SOLO estos controles
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

                // 🔁 Recorrer hijos (sin tocar paneles ni botones)
                if (c.HasChildren)
                {
                    AplicarFuente(c);
                }
            }
        }
    }
}
