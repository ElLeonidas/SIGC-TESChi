using System.IO;

public static class FileManager
{
    public static string RutaRaiz = @"C:\SIGC_Archivos";
    public static string RutaExpedientes =>
        Path.Combine(RutaRaiz, "Expedientes");

    public static void Inicializar()
    {
        Directory.CreateDirectory(RutaExpedientes);
    }

    // 📁 Carpeta por expediente (Control)
    public static string ObtenerRutaControl(
        int anioControl,
        string noExpediente)
    {
        string nombreCarpeta = $"{anioControl}_{noExpediente}";
        string ruta = Path.Combine(RutaExpedientes, nombreCarpeta);

        Directory.CreateDirectory(ruta);
        return ruta;
    }
}
