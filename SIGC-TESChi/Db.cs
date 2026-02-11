using System.Data.SqlClient;

namespace SIGC_TESChi
{
    internal static class Db
    {
        public static SqlConnection CreateConnection()
            => new SqlConnection(Program.ConnectionString);
    }
}
