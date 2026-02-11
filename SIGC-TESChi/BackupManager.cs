using System;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;

public static class BackupManager
{
    // =========================
    // 🔹 CONFIG PORTABLE
    // =========================

    // Carpeta base portable del sistema (BD + archivos) en AppData
    private static string RutaAppDataSistema =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SIGC-TESChi"
        );

    // Rutas de BD portable (MDF/LDF) en AppData
    private static string RutaMdf => Path.Combine(RutaAppDataSistema, "DBCONTRALORIA.mdf");

    // OJO: el nombre del LDF a veces cambia; intentamos el estándar primero y si no existe buscamos cualquiera .ldf
    private static string RutaLdfPreferida => Path.Combine(RutaAppDataSistema, "DBCONTRALORIA_log.ldf");

    private static string ObtenerLdfReal()
    {
        if (File.Exists(RutaLdfPreferida))
            return RutaLdfPreferida;

        var ldf = Directory.Exists(RutaAppDataSistema)
            ? Directory.GetFiles(RutaAppDataSistema, "*.ldf", SearchOption.TopDirectoryOnly).FirstOrDefault()
            : null;

        return ldf; // puede ser null y está bien
    }

    // Archivos del sistema (si guardas PDFs, imágenes, etc.)
    // Antes: C:\SIGC_Archivos (NO portable)
    // Ahora: AppData\SIGC-TESChi\Archivos
    private static string RutaArchivosSistema =>
        Path.Combine(RutaAppDataSistema, "Archivos");

    // =========================
    // 🔹 RUTAS INTELIGENTES DE BACKUP
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
            if (oneDrive != null) return oneDrive;

            string googleDrive = ObtenerRutaGoogleDrive();
            if (googleDrive != null) return googleDrive;

            // Fallback PORTABLE: Documentos
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SIGC_Backups"
            );
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

        string mdfBak = BackupBaseDatosPorArchivos(fecha);
        string carpetaArchivos = BackupArchivos(fecha);

        ComprimirBackup(fecha, mdfBak, carpetaArchivos);
        LimpiarBackupsAntiguos(30);
    }

    private static void CrearCarpetas()
    {
        Directory.CreateDirectory(RutaBase);
        Directory.CreateDirectory(RutaBD);
        Directory.CreateDirectory(RutaArchivos);

        // También aseguramos la carpeta del sistema (AppData)
        Directory.CreateDirectory(RutaAppDataSistema);
        Directory.CreateDirectory(RutaArchivosSistema);
    }

    // =========================
    // 🔹 BACKUP BD (PORTABLE: MDF/LDF)
    // =========================

    private static string BackupBaseDatosPorArchivos(string fecha)
    {
        // Libera locks/pools por si hubo conexiones recientes
        SqlConnection.ClearAllPools();

        if (!File.Exists(RutaMdf))
            throw new Exception("No se encontró el archivo de base de datos DBCONTRALORIA.mdf en AppData.");

        string destinoMdf = Path.Combine(RutaBD, $"DB_{fecha}.mdf.bak");
        File.Copy(RutaMdf, destinoMdf, overwrite: true);

        // LDF opcional
        string ldfReal = ObtenerLdfReal();
        if (!string.IsNullOrEmpty(ldfReal) && File.Exists(ldfReal))
        {
            string destinoLdf = Path.Combine(RutaBD, $"DB_{fecha}.ldf.bak");
            File.Copy(ldfReal, destinoLdf, overwrite: true);
        }

        return destinoMdf;
    }

    // =========================
    // 🔹 BACKUP ARCHIVOS (PORTABLE)
    // =========================

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

    // =========================
    // 🔹 ZIP FINAL
    // =========================

    private static void ComprimirBackup(string fecha, string mdfBak, string carpetaArchivos)
    {
        string zipFinal = Path.Combine(RutaBase, $"SIGC_Backup_{fecha}.zip");

        if (File.Exists(zipFinal))
            File.Delete(zipFinal);

        using (ZipArchive zip = ZipFile.Open(zipFinal, ZipArchiveMode.Create))
        {
            // BD (mdf/ldf backup)
            zip.CreateEntryFromFile(mdfBak, Path.GetFileName(mdfBak));

            string ldfBak = mdfBak.Replace(".mdf.bak", ".ldf.bak");
            if (File.Exists(ldfBak))
                zip.CreateEntryFromFile(ldfBak, Path.GetFileName(ldfBak));

            // Archivos del sistema
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
                File.Delete(zip);
        }
    }

    // =========================
    // 🔹 RESTAURAR BACKUP (PORTABLE)
    // =========================

    public static void RestaurarBackup(string rutaZip)
    {
        if (!File.Exists(rutaZip))
            throw new Exception("El archivo de respaldo no existe.");

        // Recomendación: restaurar con la app sin conexiones abiertas.
        // Si lo haces desde la app, cierra conexiones y evita tener formularios que mantengan conexiones vivas.
        SqlConnection.ClearAllPools();

        string temp = Path.Combine(Path.GetTempPath(), "SIGC_Restore");

        if (Directory.Exists(temp))
            Directory.Delete(temp, true);

        Directory.CreateDirectory(temp);

        ZipFile.ExtractToDirectory(rutaZip, temp);

        // Buscar MDF backup dentro del zip extraído
        string mdfBak = Directory.GetFiles(temp, "*.mdf.bak", SearchOption.AllDirectories)
                                 .FirstOrDefault();

        if (mdfBak == null)
            throw new Exception("No se encontró archivo .mdf.bak en el respaldo.");

        RestaurarBaseDatosPorArchivos(mdfBak);

        // Restaurar archivos
        string carpetaArchivos = Path.Combine(temp, "Archivos");
        RestaurarArchivos(carpetaArchivos);

        Directory.Delete(temp, true);
    }

    private static void RestaurarBaseDatosPorArchivos(string rutaMdfBak)
    {
        SqlConnection.ClearAllPools();

        Directory.CreateDirectory(RutaAppDataSistema);

        // Si la app tiene conexiones abiertas al MDF, esto puede fallar.
        // Lo ideal: restaurar cuando el sistema esté cerrado o desde un modo "mantenimiento".
        File.Copy(rutaMdfBak, RutaMdf, overwrite: true);

        string rutaLdfBak = rutaMdfBak.Replace(".mdf.bak", ".ldf.bak");
        if (File.Exists(rutaLdfBak))
        {
            // Copiamos el LDF estándar; si el real tenía otro nombre, igual funcionará al re-attach.
            File.Copy(rutaLdfBak, RutaLdfPreferida, overwrite: true);
        }
    }

    private static void RestaurarArchivos(string origen)
    {
        if (!Directory.Exists(origen)) return;

        Directory.CreateDirectory(RutaArchivosSistema);

        foreach (string archivo in Directory.GetFiles(
                     origen, "*.*", SearchOption.AllDirectories))
        {
            string nuevo = archivo.Replace(origen, RutaArchivosSistema);
            Directory.CreateDirectory(Path.GetDirectoryName(nuevo));
            File.Copy(archivo, nuevo, true);
        }
    }
}

