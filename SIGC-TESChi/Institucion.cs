using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Institucion : UserControl
    {
        // Cadena de conexión
        private string connectionString;

        private ToolTip toolTip;

        public Institucion()
        {
            InitializeComponent();

            var cs = System.Configuration.ConfigurationManager
                .ConnectionStrings["DB"];

            if (cs == null)
            {
                MessageBox.Show("No se encontró la cadena 'DB' en app.config");
                Application.Exit();
                return;
            }

            connectionString = cs.ConnectionString;

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

            AplicarTemaLobby();

            tablaInstitucion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaInstitucion.AllowUserToResizeColumns = false;
            tablaInstitucion.AllowUserToResizeRows = false;
            tablaInstitucion.RowHeadersVisible = false;

            ConfigurarDataGridViewOscuro(tablaInstitucion);


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
                lblTitulo, label2, label3, label4
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
            tablaInstitucion.BackgroundColor = colorFondo;
            tablaInstitucion.BorderStyle = BorderStyle.None;
            tablaInstitucion.EnableHeadersVisualStyles = false;
            tablaInstitucion.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            tablaInstitucion.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tablaInstitucion.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            tablaInstitucion.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tablaInstitucion.RowHeadersVisible = false;

            tablaInstitucion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaInstitucion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaInstitucion.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaInstitucion.Dock = DockStyle.Fill;

        }


        #endregion

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

                    tablaInstitucion.DefaultCellStyle.ForeColor = Color.Black;
                    tablaInstitucion.DefaultCellStyle.BackColor = Color.White;
                    tablaInstitucion.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    tablaInstitucion.EnableHeadersVisualStyles = true;

                    tablaInstitucion.Columns["idInstituto"].HeaderText = "Identificador";
                    tablaInstitucion.Columns["claveInstituto"].HeaderText = "Clave del Instituto";
                    tablaInstitucion.Columns["dInstituto"].HeaderText = "Nombre del Instituto";

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

                    string checkQuery = "SELECT COUNT(*) FROM Instituto WHERE claveInstituto = @clave";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@clave", txtAbreviatura.Text);
                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("Esta abreviatura ya existe.");
                        return;
                    }

                    string query = "INSERT INTO Instituto (claveInstituto, dInstituto) VALUES (@clave, @nombre)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@clave", txtAbreviatura.Text);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (INSERT)
                string datosNuevos = $"Clave={txtAbreviatura.Text}, Nombre={txtNombre.Text}";
                HistorialHelper.RegistrarCambio(
                    "Instituto",
                    txtAbreviatura.Text,
                    "INSERT",
                    null,
                    datosNuevos
                );

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
                string datosAnteriores = $"Clave={txtAbreviatura.Text}, Nombre={txtNombre.Text}";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 🔴 OBTENER DATOS ANTERIORES REALES
                    string selectQuery = "SELECT claveInstituto, dInstituto FROM Instituto WHERE idInstituto = @id";
                    SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));

                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            datosAnteriores =
                                $"Clave={reader["claveInstituto"]}, Nombre={reader["dInstituto"]}";
                        }
                    }

                    string query = "UPDATE Instituto SET claveInstituto = @clave, dInstituto = @nombre WHERE idInstituto = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@clave", txtAbreviatura.Text);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (UPDATE)
                string datosNuevos = $"Clave={txtAbreviatura.Text}, Nombre={txtNombre.Text}";
                HistorialHelper.RegistrarCambio(
                    "Instituto",
                    txtID.Text,
                    "UPDATE",
                    datosAnteriores,
                    datosNuevos
                );

                MessageBox.Show("Institución modificada.");
                CargarInstituciones();
                LimpiarCampos();
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

            DialogResult confirm = MessageBox.Show(
                "¿Eliminar esta institución?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirm != DialogResult.Yes) return;

            try
            {
                string datosAnteriores = $"Clave={txtAbreviatura.Text}, Nombre={txtNombre.Text}";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Instituto WHERE idInstituto = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (DELETE)
                HistorialHelper.RegistrarCambio(
                    "Instituto",
                    txtID.Text,
                    "DELETE",
                    datosAnteriores,
                    null
                );

                MessageBox.Show("Institución eliminada.");
                CargarInstituciones();
                LimpiarCampos();
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

        private void txtAbreviatura_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtAbreviatura.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtAbreviatura.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtAbreviatura.Text != limpio)
            {
                txtAbreviatura.Text = limpio;
                txtAbreviatura.SelectionStart = Math.Min(cursor, txtAbreviatura.Text.Length);
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtNombre.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtNombre.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtNombre.Text != limpio)
            {
                txtNombre.Text = limpio;
                txtNombre.SelectionStart = Math.Min(cursor, txtNombre.Text.Length);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {

        }
    }
}
