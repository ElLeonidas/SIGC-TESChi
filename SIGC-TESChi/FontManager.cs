using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace SIGC_TESChi
{
    internal static class FontManager
    {
        // 🔹 Tamaños disponibles
        private const float TAMANO_PEQUENO = 8F;
        private const float TAMANO_NORMAL = 9F;
        private const float TAMANO_GRANDE = 11F;

        private static string NombreFuenteActual = "Segoe UI";
        private static float TamanoActual = TAMANO_NORMAL;

        public static Font FuenteActual = new Font(NombreFuenteActual, TamanoActual);

        // =========================
        // 🔹 FUENTES
        // =========================
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
            NombreFuenteActual = nombreFuente;
            ActualizarFuente();
        }

        // =========================
        // 🔹 MODOS DE TAMAÑO
        // =========================
        public static void CambiarModoTamano(string modo)
        {
            switch (modo)
            {
                case "Pequeño":
                    TamanoActual = TAMANO_PEQUENO;
                    break;

                case "Grande":
                    TamanoActual = TAMANO_GRANDE;
                    break;

                default: // Normal
                    TamanoActual = TAMANO_NORMAL;
                    break;
            }

            ActualizarFuente();
        }

        private static void ActualizarFuente()
        {
            FuenteActual = new Font(NombreFuenteActual, TamanoActual);
        }

        // =========================
        // 🔹 APLICAR FUENTE
        // =========================
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

        internal static void CambiarFuente(Menu menu)
        {
            throw new NotImplementedException();
        }
    }
}
