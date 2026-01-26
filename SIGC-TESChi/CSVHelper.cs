using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

public static class CSVHelper
{
    private static string cs = @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

    public static void ImportarCSV(string rutaCSV)
    {
        int linea = 0;
        int insertados = 0;
        List<string> errores = new List<string>();

        using (SqlConnection conn = new SqlConnection(cs))
        {
            conn.Open();

            using (var sr = new StreamReader(rutaCSV, Encoding.Default))
            using (TextFieldParser parser = new TextFieldParser(sr))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // CSV usa coma
                parser.HasFieldsEnclosedInQuotes = true;

                while (!parser.EndOfData)
                {
                    linea++;
                    string[] d = parser.ReadFields();

                    // ✨ Corregir línea con comillas al inicio y fin
                    if (d.Length == 1 && d[0].StartsWith("\"") && d[0].EndsWith("\""))
                    {
                        string line = d[0].Substring(1, d[0].Length - 2);
                        d = line.Split(',');
                    }

                    // Validar número de columnas
                    if (d.Length < 17)
                    {
                        errores.Add($"Línea {linea}: columnas insuficientes ({d.Length} columnas encontradas)");
                        continue;
                    }

                    // Validar columnas obligatorias (0 a 15)
                    bool vacioObligatorio = false;
                    for (int i = 0; i < 16; i++)
                    {
                        if (string.IsNullOrWhiteSpace(d[i]))
                        {
                            errores.Add($"Línea {linea}: columna {i + 1} obligatoria vacía");
                            vacioObligatorio = true;
                        }
                    }
                    if (vacioObligatorio)
                        continue;

                    try
                    {
                        // Año
                        if (!int.TryParse(d[0], out int anio))
                            throw new Exception("Año inválido");

                        // IDs de catálogos
                        int idSeccion = ObtenerId(conn, "SELECT idSeccion FROM Seccion WHERE claveSeccion=@v", d[9]);
                        int idSubSeccion = ObtenerId(conn, "SELECT idSubSeccion FROM SubSeccion WHERE claveSubSeccion=@v", d[10]);
                        int idInstituto = ObtenerId(conn, "SELECT idInstituto FROM Instituto WHERE claveInstituto=@v", d[11]);
                        int idUbicacion = ObtenerId(conn, "SELECT idUbicacion FROM Ubicacion WHERE dUbicacion=@v", d[12]);
                        int idEstatus = ObtenerId(conn, "SELECT idEstatus FROM Estatus WHERE dEstatus=@v", d[13]);
                        int idClasificacion = ObtenerId(conn, "SELECT idClasificacion FROM Clasificacion WHERE dClasificacion=@v", d[14]);

                        // Fechas
                        if (!DateTime.TryParseExact(d[5], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fApertura))
                            throw new Exception("Fecha de apertura inválida");

                        DateTime? fCierre = null;
                        if (!string.IsNullOrWhiteSpace(d[6]))
                        {
                            if (!DateTime.TryParseExact(d[6], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fc))
                                throw new Exception("Fecha de cierre inválida");
                            fCierre = fc;
                        }

                        // Fojas
                        int.TryParse(d[7], out int fojas);

                        // Valores opcionales
                        object formValue = string.IsNullOrWhiteSpace(d[15]) ? DBNull.Value : (object)d[15];
                        object obsValue = string.IsNullOrWhiteSpace(d[16]) ? DBNull.Value : (object)d[16];

                        // Columna enlace (opcional)
                        object enlaceValue = DBNull.Value;
                        if (d.Length > 17 && !string.IsNullOrWhiteSpace(d[17]))
                            enlaceValue = d[17];

                        // Insertar en la base
                        string sql = @"
INSERT INTO Control
(anioControl, CodUniAdm, nomUniAdm, noExpediente, nExpediente,
 fApertura, fCierre, nForjas, nLegajos,
 idSeccion, idSubSeccion, idInstituto, idUbicacion,
 idEstatus, idClasificacion,
 formClasificatoria, Observaciones, enlace)
VALUES
(@anio,@cod,@nomUA,@noExp,@nomExp,
 @fa,@fc,@fojas,@legajos,
 @sec,@sub,@inst,@ubi,
 @est,@clas,
 @form,@obs,@enlace)";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.Add("@anio", SqlDbType.Int).Value = anio;
                            cmd.Parameters.Add("@cod", SqlDbType.NVarChar).Value = d[1];
                            cmd.Parameters.Add("@nomUA", SqlDbType.NVarChar).Value = d[2];
                            cmd.Parameters.Add("@noExp", SqlDbType.NVarChar).Value = d[3];
                            cmd.Parameters.Add("@nomExp", SqlDbType.NVarChar).Value = d[4];
                            cmd.Parameters.Add("@fa", SqlDbType.Date).Value = fApertura;
                            cmd.Parameters.Add("@fc", SqlDbType.Date).Value = (object)fCierre ?? DBNull.Value;
                            cmd.Parameters.Add("@fojas", SqlDbType.Int).Value = fojas;
                            cmd.Parameters.Add("@legajos", SqlDbType.NVarChar).Value = d[8];

                            cmd.Parameters.Add("@sec", SqlDbType.Int).Value = idSeccion;
                            cmd.Parameters.Add("@sub", SqlDbType.Int).Value = idSubSeccion;
                            cmd.Parameters.Add("@inst", SqlDbType.Int).Value = idInstituto;
                            cmd.Parameters.Add("@ubi", SqlDbType.Int).Value = idUbicacion;
                            cmd.Parameters.Add("@est", SqlDbType.Int).Value = idEstatus;
                            cmd.Parameters.Add("@clas", SqlDbType.Int).Value = idClasificacion;

                            cmd.Parameters.Add("@form", SqlDbType.NVarChar).Value = formValue;
                            cmd.Parameters.Add("@obs", SqlDbType.NVarChar).Value = obsValue;
                            cmd.Parameters.Add("@enlace", SqlDbType.NVarChar).Value = enlaceValue;

                            cmd.ExecuteNonQuery();
                            insertados++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errores.Add($"Línea {linea}: {ex.Message}");
                    }
                }
            }
        }

        // Guardar errores
        if (errores.Count > 0)
            File.WriteAllLines("ErroresImportacion.txt", errores);

        MessageBox.Show($"Importación finalizada. Insertados: {insertados}. Errores: {errores.Count}");
    }

    private static int ObtenerId(SqlConnection c, string sql, string valor)
    {
        using (SqlCommand cmd = new SqlCommand(sql, c))
        {
            cmd.Parameters.AddWithValue("@v", valor.Trim());
            object r = cmd.ExecuteScalar();
            if (r == null)
                throw new Exception($"No existe en catálogo: {valor}");
            return Convert.ToInt32(r);
        }
    }
}
