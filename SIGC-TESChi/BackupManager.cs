using System;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;

public static class BackupManager
{
    private static string NombreBD = "DBCONTRALORIA";
    private static string RutaArchivosSistema = @"C:\SIGC_Archivos"; // AJUSTA SI ES NECESARIO

    // =========================
    // 🔹 RUTAS INTELIGENTES
    // =========================

    private static string ObtenerRutaOneDrive()
    {
        string ruta = Environment.GetEnvironmentVariable("OneDrive");

        if (!string.IsNullOrEmpty(ruta) && Directory.Exists(ruta))
            return Path.Combine(ruta, "SIGC_Backups");

        return null;
    }

    private static string ObtenerRutaGoogleDrive()
    {
        string ruta = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Google Drive"
        );

        if (Directory.Exists(ruta))
            return Path.Combine(ruta, "SIGC_Backups");

        return null;
    }

    private static string RutaBase
    {
        get
        {
            string oneDrive = ObtenerRutaOneDrive();
            if (oneDrive != null)
                return oneDrive;

            string googleDrive = ObtenerRutaGoogleDrive();
            if (googleDrive != null)
                return googleDrive;

            return @"C:\SIGC_Backups";
        }
    }

    private static string RutaBD => Path.Combine(RutaBase, "BaseDatos");
    private static string RutaArchivos => Path.Combine(RutaBase, "Archivos");

    // =========================
    // 🔹 BACKUP COMPLETO
    // =========================

    public static void EjecutarBackupCompleto()
    {
        CrearCarpetas();

        string fecha = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        string bakBD = BackupBaseDatos(fecha);
        string carpetaArchivos = BackupArchivos(fecha);

        ComprimirBackup(fecha, bakBD, carpetaArchivos);
        LimpiarBackupsAntiguos(30);
    }

    private static void CrearCarpetas()
    {
        Directory.CreateDirectory(RutaBase);
        Directory.CreateDirectory(RutaBD);
        Directory.CreateDirectory(RutaArchivos);
    }

    private static string BackupBaseDatos(string fecha)
    {
        string rutaBak = Path.Combine(RutaBD, $"DB_{fecha}.bak");

        string sql = $@"
BACKUP DATABASE {NombreBD}
TO DISK = '{rutaBak}'
WITH INIT, FORMAT";

        using (SqlConnection cn = new SqlConnection(
            @"Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;"))
        {
            cn.Open();
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        return rutaBak;
    }

    private static string BackupArchivos(string fecha)
    {
        string destino = Path.Combine(RutaArchivos, fecha);
        Directory.CreateDirectory(destino);

        if (!Directory.Exists(RutaArchivosSistema))
            return destino;

        foreach (string archivo in Directory.GetFiles(
                     RutaArchivosSistema, "*.*", SearchOption.AllDirectories))
        {
            string nuevo = archivo.Replace(RutaArchivosSistema, destino);
            Directory.CreateDirectory(Path.GetDirectoryName(nuevo));
            File.Copy(archivo, nuevo, true);
        }

        return destino;
    }

    private static void ComprimirBackup(string fecha, string bakBD, string carpetaArchivos)
    {
        string zipFinal = Path.Combine(RutaBase, $"SIGC_Backup_{fecha}.zip");

        using (ZipArchive zip = ZipFile.Open(zipFinal, ZipArchiveMode.Create))
        {
            zip.CreateEntryFromFile(bakBD, Path.GetFileName(bakBD));

            if (Directory.Exists(carpetaArchivos))
            {
                foreach (string archivo in Directory.GetFiles(
                             carpetaArchivos, "*.*", SearchOption.AllDirectories))
                {
                    zip.CreateEntryFromFile(
                        archivo,
                        Path.Combine(
                            "Archivos",
                            archivo.Substring(carpetaArchivos.Length + 1))
                    );
                }
            }
        }
    }

    private static void LimpiarBackupsAntiguos(int dias)
    {
        if (!Directory.Exists(RutaBase)) return;

        foreach (string zip in Directory.GetFiles(RutaBase, "*.zip"))
        {
            if (File.GetCreationTime(zip) < DateTime.Now.AddDays(-dias))
            {
                File.Delete(zip);
            }
        }
    }

    // =========================
    // 🔹 RESTAURAR BACKUP
    // =========================

    public static void RestaurarBackup(string rutaZip)
    {
        if (!File.Exists(rutaZip))
            throw new Exception("El archivo de respaldo no existe.");

        string temp = Path.Combine(Path.GetTempPath(), "SIGC_Restore");

        if (Directory.Exists(temp))
            Directory.Delete(temp, true);

        Directory.CreateDirectory(temp);

        ZipFile.ExtractToDirectory(rutaZip, temp);

        string bak = Directory.GetFiles(temp, "*.bak", SearchOption.AllDirectories)
                              .FirstOrDefault();

        if (bak == null)
            throw new Exception("No se encontró archivo .bak en el respaldo.");

        RestaurarBaseDatos(bak);

        string carpetaArchivos = Path.Combine(temp, "Archivos");
        RestaurarArchivos(carpetaArchivos);

        Directory.Delete(temp, true);
    }

    private static void RestaurarBaseDatos(string rutaBak)
    {
        string sql = $@"
USE master;
ALTER DATABASE {NombreBD} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

RESTORE DATABASE {NombreBD}
FROM DISK = '{rutaBak}'
WITH REPLACE;

ALTER DATABASE {NombreBD} SET MULTI_USER;
";

        using (SqlConnection cn = new SqlConnection(
            @"Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;"))
        {
            cn.Open();
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    private static void RestaurarArchivos(string origen)
    {
        if (!Directory.Exists(origen)) return;

        foreach (string archivo in Directory.GetFiles(
                     origen, "*.*", SearchOption.AllDirectories))
        {
            string nuevo = archivo.Replace(origen, RutaArchivosSistema);
            Directory.CreateDirectory(Path.GetDirectoryName(nuevo));
            File.Copy(archivo, nuevo, true);
        }
    }
}
