using System;
using System.IO;

namespace SIGC_TESChi
{
    internal static class LocalDbBootstrap
    {
        public static string GetDbFolder()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SIGC-TESChi");
        }

        public static string GetMdfPath()
        {
            return Path.Combine(GetDbFolder(), "DBCONTRALORIA.mdf");
        }

        public static string GetLdfPath()
        {
            return Path.Combine(GetDbFolder(), "DBCONTRALORIA_log.ldf");
        }


        public static string GetPortableConnectionString()
        {
            // Carpeta donde está el .exe instalado
            var exeDir = AppDomain.CurrentDomain.BaseDirectory;
            var sourceMdf = Path.Combine(exeDir, "DBCONTRALORIA.mdf");

            // Carpeta "portable" por usuario (sin permisos raros)
            var appDataDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SIGC-TESChi");

            Directory.CreateDirectory(appDataDir);

            var targetMdf = Path.Combine(appDataDir, "DBCONTRALORIA.mdf");

            // Copia solo la primera vez
            if (!File.Exists(targetMdf))
            {
                if (!File.Exists(sourceMdf))
                    throw new FileNotFoundException("No se encontró DBCONTRALORIA.mdf junto al ejecutable.", sourceMdf);

                File.Copy(sourceMdf, targetMdf, overwrite: false);
            }

            // LocalDB creará/gestionará el .ldf en la misma carpeta (AppData), perfecto.
            return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={targetMdf};Integrated Security=True;Connect Timeout=30;";
        }
    }
}
