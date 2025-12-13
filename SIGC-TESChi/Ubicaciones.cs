using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SIGC_TESChi
{
    public partial class Ubicaciones : UserControl
    {
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        private ToolTip toolTip;

        public Ubicaciones()
        {
            InitializeComponent();
            Load += Ubicaciones_Load;

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 200;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;

            // ToolTips
            btnExportarPDF.MouseEnter += (s, e) => toolTip.Show("Boton para Exportar a CSV", btnExportarPDF);
            btnExportarPDF.MouseLeave += (s, e) => toolTip.Hide(btnExportarPDF);
            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nueva Ubicación", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Ubicación", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Ubicación", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar Ubicación", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);

            // Eventos
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnExportarPDF.Click += btnExportarPDF_Click;

            tablaUbicaciones.AutoGenerateColumns = true;
            tablaUbicaciones.CellClick += tablaUbicaciones_CellClick;

            // Bloqueo total del ID
            txtID.ReadOnly = true;
            txtID.Enabled = false;
            txtID.TabStop = false;
        }

        private void Ubicaciones_Load(object sender, EventArgs e)
        {
            CargarUbicaciones();
        }

        // EVENTOS

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarUbicacion();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarUbicacion();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarUbicacion();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUbicacion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarUbicaciones();
        }

        //MÉTODOS 

        private void CargarUbicaciones()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT idUbicacion, dUbicacion FROM Ubicacion ORDER BY idUbicacion";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaUbicaciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ubicaciones: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtUbicacion.Clear();
            txtUbicacion.Focus();
        }

        private void AgregarUbicacion()
        {
            if (string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                MessageBox.Show("Por favor ingresa una ubicación.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Ubicacion WHERE dUbicacion = @ubicacion";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@ubicacion", txtUbicacion.Text);

                    int existe = (int)checkCmd.ExecuteScalar();
                    if (existe > 0)
                    {
                        MessageBox.Show("⚠️ Esta ubicación ya está registrada.");
                        return;
                    }

                    string query = "INSERT INTO Ubicacion (dUbicacion) VALUES (@ubicacion)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ubicacion", txtUbicacion.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Ubicación agregada.");
                CargarUbicaciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar ubicación:\n" + ex.Message);
            }
        }

        private void ModificarUbicacion()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                MessageBox.Show("Selecciona una ubicación y modifícala.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Ubicacion SET dUbicacion = @ubicacion WHERE idUbicacion = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ubicacion", txtUbicacion.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Ubicación actualizada.");
                CargarUbicaciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar ubicación: " + ex.Message);
            }
        }

        private void EliminarUbicacion()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una ubicación.");
                return;
            }

            if (MessageBox.Show("¿Seguro de eliminar?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "DELETE FROM Ubicacion WHERE idUbicacion = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Ubicación eliminada.");
                CargarUbicaciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar ubicación: " + ex.Message);
            }
        }

        private void BuscarUbicacion()
        {
            if (string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                CargarUbicaciones();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT idUbicacion, dUbicacion FROM Ubicacion WHERE dUbicacion LIKE @buscar";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@buscar", "%" + txtUbicacion.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaUbicaciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message);
            }
        }

        private void tablaUbicaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = tablaUbicaciones.Rows[e.RowIndex];
            txtID.Text = fila.Cells["idUbicacion"].Value.ToString();
            txtUbicacion.Text = fila.Cells["dUbicacion"].Value.ToString();
        }

        //EXPORTAR CSV

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos CSV (*.csv)|*.csv";
            sfd.FileName = "Ubicaciones.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();

                foreach (DataGridViewColumn col in tablaUbicaciones.Columns)
                    sb.Append(col.HeaderText + ";");

                sb.AppendLine();

                foreach (DataGridViewRow row in tablaUbicaciones.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                            sb.Append(cell.Value + ";");

                        sb.AppendLine();
                    }
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("✅ Archivo CSV generado.");
            }
        }

    }
}

