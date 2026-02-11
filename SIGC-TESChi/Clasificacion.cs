using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Clasificacion : UserControl
    {
        // Cadena de conexión
        private static string connectionString => Program.ConnectionString;

        private ToolTip toolTip;

        public Clasificacion()
        {
            InitializeComponent();

            tablaClasificacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaClasificacion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaClasificacion.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaClasificacion.Dock = DockStyle.Fill;

            Load += Clasificacion_Load;

            tablaClasificacion.AutoGenerateColumns = true;
            tablaClasificacion.CellClick += tablaClasificacion_CellClick;

            txtID.ReadOnly = true;
            txtID.Enabled = false;
            txtID.TabStop = false;

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;  // Visible 5 segundos
            toolTip.InitialDelay = 200;   // Aparece tras 0.2 segundos
            toolTip.ReshowDelay = 100;    // Retardo entre botones
            toolTip.ShowAlways = true;    // Siempre visible

            // Asignar eventos de mouse para mostrar tooltips
            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nueva Clasificación", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Clasificación", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Clasificación", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar Clasificación", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);
        }

        private void Clasificacion_Load(object sender, EventArgs e)
        {

            using (var con = Db.CreateConnection())
            {
                con.Open();
                // consultas reales aquí
            }

            CargarClasificacion();
            AplicarTemaLobby();

            tablaClasificacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaClasificacion.AllowUserToResizeColumns = false;
            tablaClasificacion.AllowUserToResizeRows = false;
            tablaClasificacion.RowHeadersVisible = false;

            ConfigurarDataGridViewOscuro(tablaClasificacion);


        }

        #region DISEÑO

        void ConfigurarDataGridViewOscuro(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;

            // Fondo general
            dgv.BackgroundColor = Color.FromArgb(30, 30, 30);
            dgv.GridColor = Color.FromArgb(45, 45, 48);

            // Filas
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgv.DefaultCellStyle.ForeColor = Color.Gainsboro;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Filas alternas
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(36, 36, 36);

            // Encabezados
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeight = 40;

            // Filas
            dgv.RowTemplate.Height = 36;
            dgv.RowHeadersVisible = false;

            // Comportamiento
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            // Auto ajuste
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void RedondearBoton(Button btn, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(btn.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(btn.Width - radio, btn.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, btn.Height - radio, radio, radio, 90, 90);
            path.CloseAllFigures();

            btn.Region = new Region(path);
        }

        private void EstiloBoton(Button btn, Color fondo)
        {
            btn.BackColor = fondo;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.UseVisualStyleBackColor = false;

            btn.ImageAlign = ContentAlignment.MiddleCenter;
            btn.Text = "";

            btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(fondo);
            btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(fondo);

            RedondearBoton(btn, 20);
        }

        private void AplicarTemaLobby()
        {
            // =========================
            // 🎨 COLORES BASE
            // =========================
            Color colorPrimario = Color.FromArgb(30, 58, 138);
            Color colorSecundario = Color.FromArgb(59, 130, 246);
            Color colorFondo = Color.FromArgb(243, 244, 246);
            Color colorTexto = Color.FromArgb(17, 24, 39);
            Color colorGris = Color.FromArgb(107, 114, 128);

            // =========================
            // 📦 PANEL PRINCIPAL
            // =========================
            panel1.BackColor = colorFondo;

            // =========================
            // 🧾 HEADER
            // =========================
            //pnlTabla.Height = 60;
            //pnlTabla.BackColor = colorPrimario;

            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;

            // =========================
            // 🔤 LABELS
            // =========================
            Label[] labels =
            {
                lblTitulo, label2, label3,
            };

            foreach (Label lbl in labels)
            {
                lbl.ForeColor = colorTexto;
                lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }

            // =========================
            // 🖱 BOTONES
            // =========================
            EstiloBoton(btnAgregar, colorSecundario);
            EstiloBoton(btnModificar, Color.FromArgb(245, 158, 11)); // Naranja
            EstiloBoton(btnEliminar, Color.FromArgb(239, 68, 68));   // Rojo
            EstiloBoton(btnLimpiar, colorGris);
            EstiloBoton(btnBuscar, Color.FromArgb(125, 141, 127));
            //EstiloBoton(btnExportarPDF, Color.FromArgb(155, 211, 171));

            // =========================
            // 📊 DATAGRIDVIEW
            // =========================
            tablaClasificacion.BackgroundColor = colorFondo;
            tablaClasificacion.BorderStyle = BorderStyle.None;
            tablaClasificacion.EnableHeadersVisualStyles = false;
            tablaClasificacion.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            tablaClasificacion.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tablaClasificacion.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            tablaClasificacion.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tablaClasificacion.RowHeadersVisible = false;

            tablaClasificacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaClasificacion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaClasificacion.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaClasificacion.Dock = DockStyle.Fill;

        }

        #endregion

        //CARGAR
        private void CargarClasificacion()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT idClasificacion, dClasificacion FROM Clasificacion ORDER BY idClasificacion";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaClasificacion.DataSource = dt;

                    tablaClasificacion.DefaultCellStyle.ForeColor = Color.Black;
                    tablaClasificacion.DefaultCellStyle.BackColor = Color.White;
                    tablaClasificacion.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    tablaClasificacion.EnableHeadersVisualStyles = true;

                    tablaClasificacion.Columns["idClasificacion"].HeaderText = "Identificador";
                    tablaClasificacion.Columns["dClasificacion"].HeaderText = "Nombre de la Clasificación";



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clasificaciones: " + ex.Message);
            }
        }

        //LIMPIAR
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtID.Text = "";
            txtClasificacion.Text = "";
            txtClasificacion.Focus();
            CargarClasificacion();
        }

        //AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarClasificacion();
        }

        private void AgregarClasificacion()
        {
            if (string.IsNullOrWhiteSpace(txtClasificacion.Text))
            {
                MessageBox.Show("Por favor ingresa una clasificación.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string dupQuery = "SELECT COUNT(*) FROM Clasificacion WHERE dClasificacion = @d";
                    SqlCommand dupCmd = new SqlCommand(dupQuery, conn);
                    dupCmd.Parameters.AddWithValue("@d", txtClasificacion.Text);

                    if ((int)dupCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("⚠️ Esta clasificación ya existe.");
                        return;
                    }

                    string sql = "INSERT INTO Clasificacion (dClasificacion) VALUES (@d)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@d", txtClasificacion.Text);
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (INSERT)
                string datosNuevos = $"Clasificacion={txtClasificacion.Text}";
                HistorialHelper.RegistrarCambio(
                    "Clasificacion",
                    txtClasificacion.Text,
                    "INSERT",
                    null,
                    datosNuevos
                );

                MessageBox.Show("✅ Clasificación agregada.");
                CargarClasificacion();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }


        //MODIFICAR

        private void ModificarClasificacion()
        {
            if (txtID.Text == "" || txtClasificacion.Text == "")
            {
                MessageBox.Show("Selecciona una clasificación.");
                return;
            }

            try
            {
                string datosAnteriores = "";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 🔴 OBTENER DATOS ANTERIORES
                    string selectQuery = "SELECT dClasificacion FROM Clasificacion WHERE idClasificacion = @id";
                    SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@id", txtID.Text);

                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            datosAnteriores = $"Clasificacion={reader["dClasificacion"]}";
                        }
                    }

                    string sql = "UPDATE Clasificacion SET dClasificacion = @d WHERE idClasificacion = @id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@d", txtClasificacion.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (UPDATE)
                string datosNuevos = $"Clasificacion={txtClasificacion.Text}";
                HistorialHelper.RegistrarCambio(
                    "Clasificacion",
                    txtID.Text,
                    "UPDATE",
                    datosAnteriores,
                    datosNuevos
                );

                MessageBox.Show("✅ Clasificación actualizada.");
                CargarClasificacion();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }


        //ELIMINAR
        private void EliminarClasificacion()
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Selecciona una clasificación.");
                return;
            }

            if (MessageBox.Show(
                "¿Eliminar definitivamente?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                string datosAnteriores = $"Clasificacion={txtClasificacion.Text}";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM Clasificacion WHERE idClasificacion = @id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (DELETE)
                HistorialHelper.RegistrarCambio(
                    "Clasificacion",
                    txtID.Text,
                    "DELETE",
                    datosAnteriores,
                    null
                );

                MessageBox.Show("✅ Clasificación eliminada.");
                CargarClasificacion();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }


        //BUSCAR
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarClasificacion();
        }

        private void BuscarClasificacion()
        {
            if (txtClasificacion.Text == "")
            {
                CargarClasificacion();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = "SELECT * FROM Clasificacion WHERE dClasificacion LIKE @b";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    da.SelectCommand.Parameters.AddWithValue("@b", "%" + txtClasificacion.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    tablaClasificacion.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }

        //SELECCIONAR FILA
        private void tablaClasificacion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = tablaClasificacion.Rows[e.RowIndex];

            txtID.Text = fila.Cells["idClasificacion"].Value.ToString();
            txtClasificacion.Text = fila.Cells["dClasificacion"].Value.ToString();
        }

        private void txtClasificacion_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtClasificacion.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtClasificacion.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtClasificacion.Text != limpio)
            {
                txtClasificacion.Text = limpio;
                txtClasificacion.SelectionStart = Math.Min(cursor, txtClasificacion.Text.Length);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarClasificacion();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarClasificacion();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
