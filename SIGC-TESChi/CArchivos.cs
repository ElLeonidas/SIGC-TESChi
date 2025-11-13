using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class CArchivos : UserControl
    {

        string connectionString = @"Server=.\SQLEXPRESS;Database=DBCONTRALORIA;Trusted_Connection=True;";


        public CArchivos()
        {
            InitializeComponent();
            Load += CArchivos_Load;
        }

        private void CArchivos_Load(object sender, EventArgs e)
        {
            CargarArchivos();
        }
        private void CargarArchivos()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Control"; // Cambia 'Usuarios' por el nombre real de tu tabla
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    TablaArchivos.DataSource = dt; // Asignamos la tabla al DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
