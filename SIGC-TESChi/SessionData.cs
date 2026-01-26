using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGC_TESChi
{
    public static class SessionData
    {
        public static int IdUsuario { get; set; }
        public static string Username { get; set; }
        public static string NombreCompleto { get; set; }
        public static int IdTipoUsuario { get; set; }
    }

    public static class Permisos
    {
        public static bool EsAdministrador =>
            SessionData.IdTipoUsuario == 1;
    }


}
