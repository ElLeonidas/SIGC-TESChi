using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class RUsuarios : UserControl
    {

        string connectionString = @"Server=.\SQLEXPRESS;Database=SGCTESCHI;Trusted_Connection=True;";


        public RUsuarios()
        {
            InitializeComponent();
            Load += RUsuarios_Load;
        }

        private void RUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Usuario"; // Cambia 'Usuarios' por el nombre real de tu tabla
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    TablaUsuarios.DataSource = dt; // Asignamos la tabla al DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }
    }
}