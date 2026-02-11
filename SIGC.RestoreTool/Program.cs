using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;

namespace SIGC_RestoreTool
{
    internal static class Program
    {
        // Ajusta estos nombres si cambian
        private const string AppFolderName = "SIGC-TESChi";
        private const string DbMdfName = "DBCONTRALORIA.mdf";
        private const string DbLdfStandardName = "DBCONTRALORIA_log.ldf";
        private const string FilesFolderNameInZip = "Archivos";

        static int Main(string[] args)
        {
            // args:
            // 0: rutaZip
            // 1: nombreProcesoApp (sin .exe)   ej: SIGC-TESChi
            // 2: rutaExeApp (completa)         ej: C:\Program Files\...\SIGC-TESChi.exe
            // 3 (opcional): tiempoMaxEsperaMs  ej: 30000

            if (args.Length < 3)
            {
                Console.WriteLine("Uso: RestoreTool.exe <rutaZip> <procesoApp> <rutaExeApp> [timeoutMs]");
                return 2;
            }

            string rutaZip = args[0];
            string procesoApp = args[1];
            string rutaExeApp = args[2];

            int timeoutMs = 30000;
            if (args.Length >= 4 && int.TryParse(args[3], out var parsed))
                timeoutMs = parsed;

            if (!File.Exists(rutaZip))
            {
                Console.WriteLine("No existe el ZIP: " + rutaZip);
                return 3;
            }

            try
            {
                // 1) Esperar a que la app principal se cierre
                WaitForProcessExit(procesoApp, timeoutMs);

                // 2) Restaurar
                RestoreFromZip(rutaZip);

                // 3) Relanzar app
                TryStartApp(rutaExeApp);

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al restaurar: " + ex);
                return 1;
            }
        }

        private static string RutaBase
        {
            get
            {
                // Intentar OneDrive
                string oneDrive = Environment.GetEnvironmentVariable("OneDrive");
                if (!string.IsNullOrEmpty(oneDrive) && Directory.Exists(oneDrive))
                    return Path.Combine(oneDrive, "SIGC_Backups");

                // Intentar Google Drive
                string googleDrive = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Google Drive");

                if (Directory.Exists(googleDrive))
                    return Path.Combine(googleDrive, "SIGC_Backups");

                // Fallback: Documentos
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "SIGC_Backups");
            }
        }


        private static void WaitForProcessExit(string processName, int timeoutMs)
        {
            var sw = Stopwatch.StartNew();

            while (true)
            {
                var procs = Process.GetProcessesByName(processName);
                if (procs.Length == 0)
                    return;

                if (sw.ElapsedMilliseconds > timeoutMs)
                    throw new TimeoutException("La aplicación no se cerró a tiempo.");

                Thread.Sleep(300);
            }
        }


        private static void RestoreFromZip(string rutaZip)
        {
            string temp = Path.Combine(Path.GetTempPath(), "SIGC_Restore");

            if (Directory.Exists(temp))
                Directory.Delete(temp, true);

            Directory.CreateDirectory(temp);

            ZipFile.ExtractToDirectory(rutaZip, temp);

            string mdfBak = Directory.GetFiles(temp, "*.mdf.bak", SearchOption.AllDirectories)
                                     .FirstOrDefault();

            if (mdfBak == null)
                throw new FileNotFoundException("No se encontró archivo .mdf.bak en el respaldo.");

            string appDataDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                AppFolderName);

            Directory.CreateDirectory(appDataDir);

            string destMdf = Path.Combine(appDataDir, DbMdfName);

            // 🔥 Reintentar copia varias veces por si el proceso tarda en liberar
            const int maxIntentos = 10;
            for (int i = 0; i < maxIntentos; i++)
            {
                try
                {
                    File.Copy(mdfBak, destMdf, true);
                    break;
                }
                catch (IOException)
                {
                    Thread.Sleep(500);
                }
            }

            string ldfBak = mdfBak.Replace(".mdf.bak", ".ldf.bak");
            string destLdf = Path.Combine(appDataDir, DbLdfStandardName);

            if (File.Exists(ldfBak))
            {
                for (int i = 0; i < maxIntentos; i++)
                {
                    try
                    {
                        File.Copy(ldfBak, destLdf, true);
                        break;
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(500);
                    }
                }
            }

            Directory.Delete(temp, true);
        }


        private static void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                var name = Path.GetFileName(file);
                var dest = Path.Combine(destDir, name);
                File.Copy(file, dest, true);
            }

            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                var name = Path.GetFileName(dir);
                var dest = Path.Combine(destDir, name);
                CopyDirectory(dir, dest);
            }
        }

        private static void TryStartApp(string rutaExeApp)
        {
            if (!File.Exists(rutaExeApp))
                return;

            Process.Start(new ProcessStartInfo
            {
                FileName = rutaExeApp,
                UseShellExecute = true
            });
        }
    }
}
