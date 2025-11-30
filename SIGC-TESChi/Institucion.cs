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
            Load += Institucion_Load;

            toolTip = new ToolTip();

            // Eventos
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;

            tablaInstitucion.CellClick += tablaInstitucion_CellClick;

            // Tooltip
            ConfigurarTooltip(btnAgregar, "Agregar Institución");
            ConfigurarTooltip(btnModificar, "Modificar Institución");
            ConfigurarTooltip(btnEliminar, "Eliminar Institución");
            ConfigurarTooltip(btnBuscar, "Buscar Institución");
            ConfigurarTooltip(btnLimpiar, "Limpiar Campos");
        }

        private void ConfigurarTooltip(Button boton, string mensaje)
        {
            boton.MouseEnter += (s, e) => toolTip.Show(mensaje, boton);
            boton.MouseLeave += (s, e) => toolTip.Hide(boton);
        }

        private void Institucion_Load(object sender, EventArgs e)
        {
            CargarInstituciones();
        }

        //             CARGAR DATOS
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
            txtID.Focus();
        }

        //                  AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) ||
                string.IsNullOrWhiteSpace(txtAbreviatura.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Instituto WHERE claveInstituto = @clave";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@clave", txtAbreviatura.Text);

                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("Esta abreviatura ya existe.");
                        return;
                    }

                    string query = "INSERT INTO Instituto (idInstituto, claveInstituto, dInstituto) " +
                                   "VALUES (@id, @clave, @nombre)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text)); // ID INT
                    cmd.Parameters.AddWithValue("@clave", txtAbreviatura.Text);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Institución agregada correctamente.");
                CargarInstituciones();
                LimpiarCampos();
            }
            catch (FormatException)
            {
                MessageBox.Show("El ID debe ser un número entero.");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                    MessageBox.Show("El ID ya existe.");
                else
                    MessageBox.Show("Error SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //                  MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
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

                    string query = "UPDATE Instituto SET claveInstituto = @clave, dInstituto = @nombre " +
                                   "WHERE idInstituto = @id";

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
                        MessageBox.Show("La institución no existe.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        //                  ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una institución para eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Eliminar esta institución?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
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
        }

        //        CARGAR DATOS AL SELECCIONAR FILA
        private void tablaInstitucion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow fila = tablaInstitucion.Rows[e.RowIndex];

                txtID.Text = fila.Cells["idInstituto"].Value?.ToString() ?? "";
                txtAbreviatura.Text = fila.Cells["claveInstituto"].Value?.ToString() ?? "";
                txtNombre.Text = fila.Cells["dInstituto"].Value?.ToString() ?? "";
            }
            catch
            {
                txtID.Text = tablaInstitucion.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                txtAbreviatura.Text = tablaInstitucion.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
                txtNombre.Text = tablaInstitucion.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";
            }
        }

        //                   LIMPIAR
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        //                    BUSCAR
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Instituto WHERE 1=1";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

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
    }
}
