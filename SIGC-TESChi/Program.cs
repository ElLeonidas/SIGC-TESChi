using System;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    internal static class Program
    {
        // Cadena de conexión "portable" disponible para toda la app
        public static string ConnectionString { get; private set; }



        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
                MessageBox.Show(e.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                MessageBox.Show(ex?.Message ?? "Error desconocido", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            // ✅ Preparar BD portable (copiar MDF a AppData y construir connection string)
            try
            {
                ConnectionString = LocalDbBootstrap.GetPortableConnectionString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al preparar la base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // salir si no se pudo preparar la BD
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashScreen());
        }
    }
}
