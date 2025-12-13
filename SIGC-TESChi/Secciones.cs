using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Secciones : UserControl
    {
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";
        private ToolTip toolTip;

        public Secciones()
        {
            InitializeComponent();
            Load += Secciones_Load;
            toolTip = new ToolTip();

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;  // Visible 5 segundos
            toolTip.InitialDelay = 200;   // Aparece tras 0.2 segundos
            toolTip.ReshowDelay = 100;    // Retardo entre botones
            toolTip.ShowAlways = true;    // Siempre visible

            // Asignar eventos de mouse para mostrar tooltips
            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nueva Sección", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Sección", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Sección", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar Sección", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);
            txtID.ReadOnly = true; // ID no editable

            txtID.ReadOnly = true; // ID no editable

            // Eventos
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;

            // Evento de selección en la tabla
            tablaSecciones.CellClick += tablaSecciones_CellClick;
        }

        private void ConfigurarTooltip(Button boton, string mensaje)
        {
            boton.MouseEnter += (s, e) => toolTip.Show(mensaje, boton);
            boton.MouseLeave += (s, e) => toolTip.Hide(boton);
        }

        private void Secciones_Load(object sender, EventArgs e)
        {
            CargarSecciones();
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtClaveSeccion.Clear();
            txtSeccion.Clear();
            txtClaveSeccion.Focus();
            CargarSecciones();
        }

        // MÉTODOS DE EVENTOS
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarSeccion();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarSeccion();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarSeccion();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarSeccion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarSecciones();
        }

        private void tablaSecciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = tablaSecciones.Rows[e.RowIndex];
            txtID.Text = fila.Cells["idSeccion"].Value?.ToString() ?? "";
            txtClaveSeccion.Text = fila.Cells["claveSeccion"].Value?.ToString() ?? "";
            txtSeccion.Text = fila.Cells["dSeccion"].Value?.ToString() ?? "";
        }

        // MÉTODOS PRINCIPALES
        private void CargarSecciones()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Seccion ORDER BY idSeccion ASC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaSecciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Secciones: " + ex.Message);
            }
        }

        private void AgregarSeccion()
        {
            if (string.IsNullOrWhiteSpace(txtClaveSeccion.Text) || string.IsNullOrWhiteSpace(txtSeccion.Text))
            {
                MessageBox.Show("Ingresa la clave y la descripción de la sección.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Seccion WHERE claveSeccion = @clave";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@clave", txtClaveSeccion.Text);
                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("⚠️ Esta clave ya existe.");
                        return;
                    }

                    string query = "INSERT INTO Seccion (claveSeccion, dSeccion) VALUES (@clave, @desc)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@clave", txtClaveSeccion.Text);
                    cmd.Parameters.AddWithValue("@desc", txtSeccion.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Sección agregada correctamente.");
                CargarSecciones();
                LimpiarCampos();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ModificarSeccion()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtClaveSeccion.Text) || string.IsNullOrWhiteSpace(txtSeccion.Text))
            {
                MessageBox.Show("Selecciona una sección para modificar y completa los campos.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Seccion SET claveSeccion = @clave, dSeccion = @desc WHERE idSeccion = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@clave", txtClaveSeccion.Text);
                    cmd.Parameters.AddWithValue("@desc", txtSeccion.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("✅ Sección modificada correctamente.");
                        CargarSecciones();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("⚠️ No se encontró la sección.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        private void EliminarSeccion()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una sección a eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Eliminar esta sección?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Seccion WHERE idSeccion = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("✅ Sección eliminada.");
                        CargarSecciones();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("⚠️ La sección no existe.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void BuscarSeccion()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Seccion WHERE 1=1";
                    SqlCommand cmd = new SqlCommand { Connection = con };

                    if (!string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        query += " AND idSeccion = @id";
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                    }
                    if (!string.IsNullOrWhiteSpace(txtClaveSeccion.Text))
                    {
                        query += " AND claveSeccion LIKE @clave";
                        cmd.Parameters.AddWithValue("@clave", "%" + txtClaveSeccion.Text + "%");
                    }
                    if (!string.IsNullOrWhiteSpace(txtSeccion.Text))
                    {
                        query += " AND dSeccion LIKE @desc";
                        cmd.Parameters.AddWithValue("@desc", "%" + txtSeccion.Text + "%");
                    }

                    cmd.CommandText = query;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaSecciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }
    }
}
