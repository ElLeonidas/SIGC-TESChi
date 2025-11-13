using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class TipoUsuarios : UserControl
    {
        private ToolTip toolTip;
        string connectionString = @"Server=.\SQLEXPRESS;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public TipoUsuarios()
        {
            InitializeComponent();

            Load += TipoUsuarios_Load;
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            TablaTiposUsuarios.CellClick += TablaTiposUsuarios_CellClick;

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;  // Visible 5 segundos
            toolTip.InitialDelay = 200;   // Aparece tras 0.2 segundos
            toolTip.ReshowDelay = 100;    // Retardo entre botones
            toolTip.ShowAlways = true;    // Siempre visible

            // Asignar eventos de mouse para mostrar tooltips
            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Registros", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Registros", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Registros", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);
        }

        private void TipoUsuarios_Load(object sender, EventArgs e)
        {
            CargarTipos();
        }

        // Cargar tabla
        private void CargarTipos()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string q = "SELECT idTipoUsuario, dTipoUsuario FROM TipoUsuario ORDER BY idTipoUsuario ASC";
                    SqlDataAdapter da = new SqlDataAdapter(q, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    TablaTiposUsuarios.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de usuario: " + ex.Message);
            }
        }

        // Verificar si la descripción ya existe
        private bool ExisteDescripcion(string descripcion)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string q = "SELECT COUNT(*) FROM TipoUsuario WHERE dTipoUsuario = @d";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@d", descripcion);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // Verificar si el ID ya existe
        private bool ExisteID(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string q = "SELECT COUNT(*) FROM TipoUsuario WHERE idTipoUsuario = @id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", id);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // Agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdentificador.Text) ||
                string.IsNullOrWhiteSpace(txtTipoUsuario.Text))
            {
                MessageBox.Show("Llena todos los campos.");
                return;
            }

            int id = Convert.ToInt32(txtIdentificador.Text);
            string descripcion = txtTipoUsuario.Text.Trim();

            if (ExisteID(id))
            {
                MessageBox.Show("El ID ingresado ya existe.");
                return;
            }

            if (ExisteDescripcion(descripcion))
            {
                MessageBox.Show("Esta descripción ya está registrada.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string q = "INSERT INTO TipoUsuario (idTipoUsuario, dTipoUsuario) VALUES (@id, @desc)";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@desc", descripcion);
                cmd.ExecuteNonQuery();
            }

            CargarTipos();
            Limpiar();
        }

        // Modificar
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdentificador.Text) ||
                string.IsNullOrWhiteSpace(txtTipoUsuario.Text))
            {
                MessageBox.Show("Selecciona un registro y realiza los cambios.");
                return;
            }

            int id = Convert.ToInt32(txtIdentificador.Text);
            string descripcion = txtTipoUsuario.Text.Trim();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string q = "SELECT dTipoUsuario FROM TipoUsuario WHERE idTipoUsuario=@id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", id);

                string actual = cmd.ExecuteScalar()?.ToString();

                if (actual != descripcion && ExisteDescripcion(descripcion))
                {
                    MessageBox.Show("Ya existe otro registro con esa descripción.");
                    return;
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string q = "UPDATE TipoUsuario SET dTipoUsuario=@d WHERE idTipoUsuario=@id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@d", descripcion);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            CargarTipos();
            Limpiar();
        }

        // Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdentificador.Text))
            {
                MessageBox.Show("Selecciona un registro.");
                return;
            }

            int id = Convert.ToInt32(txtIdentificador.Text);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string q = "DELETE FROM TipoUsuario WHERE idTipoUsuario=@id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            CargarTipos();
            Limpiar();
        }

        // Limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtIdentificador.Clear();
            txtTipoUsuario.Clear();
            txtIdentificador.Focus();
        }

        // Pasar datos del DataGridView a los TextBox
        private void TablaTiposUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow fila = TablaTiposUsuarios.Rows[e.RowIndex];

                txtIdentificador.Text = fila.Cells[0].Value?.ToString();
                txtTipoUsuario.Text = fila.Cells[1].Value?.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar valores: " + ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
