using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SIGC_TESChi
{
    internal static class Utillidades
    {
        /// <summary>
        /// Normaliza el texto para validación: minúsculas y sin acentos
        /// </summary>
        private static string NormalizarParaValidacion(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            // Pasar a minúsculas
            texto = texto.ToLowerInvariant();

            // Quitar acentos
            texto = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in texto)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Limpiar espacios extra (manteniendo acentos y mayúsculas)
        /// </summary>
        public static string LimpiarEspacios(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            return Regex.Replace(texto.Trim(), @"\s+", " ");
        }

        /// <summary>
        /// Verifica si la palabra ya existe en la lista (sin importar mayúsculas o acentos)
        /// </summary>
        public static bool EsDuplicado(List<string> lista, string palabra)
        {
            string normalizada = NormalizarParaValidacion(LimpiarEspacios(palabra));
            return lista.Any(p => NormalizarParaValidacion(LimpiarEspacios(p)) == normalizada);
        }
    }
}
