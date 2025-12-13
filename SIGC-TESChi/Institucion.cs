using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Institucion : UserControl
    {
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";
        private ToolTip toolTip;

        public Institucion()
        {
            InitializeComponent();

            tablaInstitucion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaInstitucion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaInstitucion.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaInstitucion.Dock = DockStyle.Fill;


            Load += Institucion_Load;

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;  // Visible 5 segundos
            toolTip.InitialDelay = 200;   // Aparece tras 0.2 segundos
            toolTip.ReshowDelay = 100;    // Retardo entre botones
            toolTip.ShowAlways = true;    // Siempre visible

            // Asignar eventos de mouse para mostrar tooltips
            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nueva Institución", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Institución", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Institución", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar Institución", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);
            txtID.ReadOnly = true; // ID no editable

            // Eventos de botones
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;

            // Evento de selección en la tabla
            tablaInstitucion.CellClick += tablaInstitucion_CellClick;
        }

        private void Institucion_Load(object sender, EventArgs e)
        {
            CargarInstituciones();
        }

        // MÉTODOS PRINCIPALES
        private void CargarInstituciones()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Instituto ORDER BY idInstituto ASC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaInstitucion.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Instituciones: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtAbreviatura.Clear();
            txtNombre.Clear();
            txtAbreviatura.Focus();
            CargarInstituciones();
        }

        // EVENTOS DE BOTONES
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarInstitucion();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarInstitucion();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarInstitucion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarInstituciones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarInstitucion();
        }

        // MÉTODOS AUXILIARES
        private void AgregarInstitucion()
        {
            if (string.IsNullOrWhiteSpace(txtAbreviatura.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Verificar si la clave ya existe
                    string checkQuery = "SELECT COUNT(*) FROM Instituto WHERE claveInstituto = @clave";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@clave", txtAbreviatura.Text);
                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("Esta abreviatura ya existe.");
                        return;
                    }

                    // Insertar nuevo registro (ID autonumérico)
                    string query = "INSERT INTO Instituto (claveInstituto, dInstituto) VALUES (@clave, @nombre)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@clave", txtAbreviatura.Text);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Institución agregada correctamente.");
                CargarInstituciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar institución: " + ex.Message);
            }
        }

        private void ModificarInstitucion()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una institución para modificar.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Instituto SET claveInstituto = @clave, dInstituto = @nombre WHERE idInstituto = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@clave", txtAbreviatura.Text);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));

                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0)
                    {
                        MessageBox.Show("Institución modificada.");
                        CargarInstituciones();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar, la institución no existe.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        private void EliminarInstitucion()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una institución para eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Eliminar esta institución?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Instituto WHERE idInstituto = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));

                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0)
                    {
                        MessageBox.Show("Institución eliminada.");
                        CargarInstituciones();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No existe la institución.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void BuscarInstitucion()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Instituto WHERE 1=1";
                    SqlCommand cmd = new SqlCommand { Connection = conn };

                    if (!string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        query += " AND idInstituto = @id";
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));
                    }

                    if (!string.IsNullOrWhiteSpace(txtAbreviatura.Text))
                    {
                        query += " AND claveInstituto LIKE @clave";
                        cmd.Parameters.AddWithValue("@clave", "%" + txtAbreviatura.Text + "%");
                    }

                    if (!string.IsNullOrWhiteSpace(txtNombre.Text))
                    {
                        query += " AND dInstituto LIKE @nombre";
                        cmd.Parameters.AddWithValue("@nombre", "%" + txtNombre.Text + "%");
                    }

                    cmd.CommandText = query;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    tablaInstitucion.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }

        private void tablaInstitucion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = tablaInstitucion.Rows[e.RowIndex];
            txtID.Text = fila.Cells["idInstituto"].Value?.ToString() ?? "";
            txtAbreviatura.Text = fila.Cells["claveInstituto"].Value?.ToString() ?? "";
            txtNombre.Text = fila.Cells["dInstituto"].Value?.ToString() ?? "";
        }
    }
}
