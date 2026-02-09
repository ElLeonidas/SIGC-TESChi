using System.Drawing;
using System.Windows.Forms;

public static class ThemeManager
{
    private static readonly Color Fondo = Color.FromArgb(32, 32, 32);
    private static readonly Color FondoSecundario = Color.FromArgb(45, 45, 45);
    private static readonly Color Texto = Color.White;

    public static void AplicarModoOscuro(Control control)
    {
        // 🔒 BLOQUEO TOTAL
        if (control is DataGridView)
            return;

        // Formas contenedoras
        if (control is Form || control is UserControl || control is Panel)
        {
            control.BackColor = Fondo;
            control.ForeColor = Texto;
        }

        foreach (Control c in control.Controls)
        {
            // 🔥 JAMÁS TOCAR DATAGRIDVIEW NI SUS HIJOS
            if (c is DataGridView)
                continue;

            if (c is TextBox txt)
            {
                txt.BackColor = FondoSecundario;
                txt.ForeColor = Texto;
                txt.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (c is ComboBox cmb)
            {
                cmb.BackColor = FondoSecundario;
                cmb.ForeColor = Texto;
                cmb.FlatStyle = FlatStyle.Flat;
            }
            else if (c is Button btn)
            {
                btn.UseVisualStyleBackColor = true;
                btn.FlatStyle = FlatStyle.Standard;
            }
            else if (c is CheckBox chk)
            {
                chk.ForeColor = Texto;
            }
            else
            {
                // ⚠️ SOLO SI NO ES CONTENEDOR RARO
                if (!(c is Panel))
                {
                    c.BackColor = Fondo;
                    c.ForeColor = Texto;
                }
            }

            // ⛔ NO BAJAR SI ES DATAGRIDVIEW
            if (c.HasChildren && !(c is DataGridView))
                AplicarModoOscuro(c);
        }
    }

    public static void AplicarModoClaro(Control control)
    {
        if (control is DataGridView)
            return;

        control.BackColor = Color.LightGray;
        control.ForeColor = Color.Black;

        foreach (Control c in control.Controls)
        {
            if (!(c is DataGridView))
                AplicarModoClaro(c);
        }
    }
}
