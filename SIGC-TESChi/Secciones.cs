using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
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

            tablaSecciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaSecciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaSecciones.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaSecciones.Dock = DockStyle.Fill;

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

                    tablaSecciones.DefaultCellStyle.ForeColor = Color.Black;
                    tablaSecciones.DefaultCellStyle.BackColor = Color.White;
                    tablaSecciones.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    tablaSecciones.EnableHeadersVisualStyles = true;

                    tablaSecciones.Columns["idSeccion"].HeaderText = "Identificador";
                    tablaSecciones.Columns["claveSeccion"].HeaderText = "Clave de la Sección";
                    tablaSecciones.Columns["dSeccion"].HeaderText = "Nombre de la Sección";

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

                // 🔴 HISTORIAL (INSERT)
                string datosNuevos = $"Clave={txtClaveSeccion.Text}, Seccion={txtSeccion.Text}";
                HistorialHelper.RegistrarCambio(
                    "Seccion",
                    txtClaveSeccion.Text,
                    "INSERT",
                    null,
                    datosNuevos
                );

                MessageBox.Show("✅ Sección agregada correctamente.");
                CargarSecciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void ModificarSeccion()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) ||
                string.IsNullOrWhiteSpace(txtClaveSeccion.Text) ||
                string.IsNullOrWhiteSpace(txtSeccion.Text))
            {
                MessageBox.Show("Selecciona una sección para modificar y completa los campos.");
                return;
            }

            try
            {
                string datosAnteriores = "";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 🔴 OBTENER DATOS ANTERIORES REALES
                    string selectQuery = "SELECT claveSeccion, dSeccion FROM Seccion WHERE idSeccion = @id";
                    SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@id", txtID.Text);

                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            datosAnteriores =
                                $"Clave={reader["claveSeccion"]}, Seccion={reader["dSeccion"]}";
                        }
                    }

                    string query = "UPDATE Seccion SET claveSeccion = @clave, dSeccion = @desc WHERE idSeccion = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@clave", txtClaveSeccion.Text);
                    cmd.Parameters.AddWithValue("@desc", txtSeccion.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (UPDATE)
                string datosNuevos = $"Clave={txtClaveSeccion.Text}, Seccion={txtSeccion.Text}";
                HistorialHelper.RegistrarCambio(
                    "Seccion",
                    txtID.Text,
                    "UPDATE",
                    datosAnteriores,
                    datosNuevos
                );

                MessageBox.Show("✅ Sección modificada correctamente.");
                CargarSecciones();
                LimpiarCampos();
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

            DialogResult confirm = MessageBox.Show(
                "¿Eliminar esta sección?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                string datosAnteriores = $"Clave={txtClaveSeccion.Text}, Seccion={txtSeccion.Text}";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Seccion WHERE idSeccion = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (DELETE)
                HistorialHelper.RegistrarCambio(
                    "Seccion",
                    txtID.Text,
                    "DELETE",
                    datosAnteriores,
                    null
                );

                MessageBox.Show("✅ Sección eliminada.");
                CargarSecciones();
                LimpiarCampos();
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

        private void txtClaveSeccion_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtClaveSeccion.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtClaveSeccion.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtClaveSeccion.Text != limpio)
            {
                txtClaveSeccion.Text = limpio;
                txtClaveSeccion.SelectionStart = Math.Min(cursor, txtClaveSeccion.Text.Length);
            }
        }

        private void txtSeccion_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtSeccion.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtSeccion.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtSeccion.Text != limpio)
            {
                txtSeccion.Text = limpio;
                txtSeccion.SelectionStart = Math.Min(cursor, txtSeccion.Text.Length);
            }
        }
    }
}
