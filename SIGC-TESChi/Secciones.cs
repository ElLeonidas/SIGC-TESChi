using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        private void Secciones_Load(object sender, EventArgs e)
        {
            CargarSecciones();

            AplicarTemaLobby();

            tablaSecciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaSecciones.AllowUserToResizeColumns = false;
            tablaSecciones.AllowUserToResizeRows = false;
            tablaSecciones.RowHeadersVisible = false;

            ConfigurarDataGridViewOscuro(tablaSecciones);


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
            // ✏ INPUTS (TextBox + ComboBox)
            // =========================




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
            tablaSecciones.BackgroundColor = colorFondo;
            tablaSecciones.BorderStyle = BorderStyle.None;
            tablaSecciones.EnableHeadersVisualStyles = false;
            tablaSecciones.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            tablaSecciones.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tablaSecciones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            tablaSecciones.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tablaSecciones.RowHeadersVisible = false;

            tablaSecciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaSecciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaSecciones.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaSecciones.Dock = DockStyle.Fill;

        }

        #endregion

        private void ConfigurarTooltip(Button boton, string mensaje)
        {
            boton.MouseEnter += (s, e) => toolTip.Show(mensaje, boton);
            boton.MouseLeave += (s, e) => toolTip.Hide(boton);
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
            if (string.IsNullOrWhiteSpace(txtClaveSeccion.Text) ||
                string.IsNullOrWhiteSpace(txtSeccion.Text))
            {
                MessageBox.Show(
                    "Ingresa la clave y la descripción de la sección.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string clave = txtClaveSeccion.Text.Trim();
            string descripcion = txtSeccion.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 🔎 Verificar si la clave ya existe
                    string checkQuery =
                        "SELECT COUNT(1) FROM Seccion WHERE claveSeccion = @clave";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@clave", clave);

                        if ((int)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show(
                                "⚠️ La clave de la sección ya existe.",
                                "Duplicado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return;
                        }
                    }

                    // 💾 Insertar sección
                    string insertQuery =
                        "INSERT INTO Seccion (claveSeccion, dSeccion) VALUES (@clave, @descripcion)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@clave", clave);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.ExecuteNonQuery();
                    }
                }

                // 🧾 HISTORIAL (INSERT)
                string datosNuevos =
                    $"claveSeccion={clave}, dSeccion={descripcion}";

                HistorialHelper.RegistrarCambio(
                    "Seccion",
                    clave,              // llave lógica
                    "INSERT",
                    "",
                    datosNuevos
                );

                MessageBox.Show(
                    "✅ Sección agregada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                CargarSecciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al agregar la sección:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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

        private void btnModificar_Click_1(object sender, EventArgs e)
        {

        }
    }
}
