using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public static class HistorialHelper
    {
        private static string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        /// <summary>
        /// Registra un cambio en la tabla HistorialCambios.
        /// </summary>
        public static void RegistrarCambio(
                string tabla,
                string llave,
                string tipoAccion,
                string datosAnteriores,
                string datosNuevos)

           


        {
            if (SessionData.IdUsuario <= 0)
            {
                MessageBox.Show(
                    "No hay sesión activa. No se puede registrar el historial.",
                    "Error de auditoría",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error 
                );
                MessageBox.Show(
    $"ID Usuario sesión: {SessionData.IdUsuario}\nUsuario: {SessionData.Username}"
);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                INSERT INTO HistorialCambios
                (Tabla, Llave, TipoAccion, UsuarioBD, FechaAccion, DatosAnteriores, DatosNuevos, idUsuarioApp)
                VALUES
                (@tabla, @llave, @tipo, @usuarioBD, @fecha, @datosAnt, @datosNuevos, @idUsuarioApp)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tabla", tabla);
                        cmd.Parameters.AddWithValue("@llave", llave);
                        cmd.Parameters.AddWithValue("@tipo", tipoAccion);
                        cmd.Parameters.AddWithValue("@usuarioBD", SessionData.Username);
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@datosAnt", datosAnteriores ?? "");
                        cmd.Parameters.AddWithValue("@datosNuevos", datosNuevos ?? "");
                        cmd.Parameters.AddWithValue("@idUsuarioApp", SessionData.IdUsuario);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar cambio: " + ex.Message);
            }
        }

    }
}
