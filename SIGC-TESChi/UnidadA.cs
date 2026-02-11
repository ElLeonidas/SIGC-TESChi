using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class UnidadA : UserControl
    {
        // Cadena de conexión
        private static string connectionString => Program.ConnectionString;

        private ToolTip toolTip;

        public UnidadA()
        {
            InitializeComponent();           

            tablaUnidadA.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaUnidadA.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaUnidadA.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaUnidadA.Dock = DockStyle.Fill;

            // EVENTO LOAD
            Load += UnidadA_Load;

            // CONFIGURACIÓN TABLA
            tablaUnidadA.AutoGenerateColumns = true;
            tablaUnidadA.CellClick += tablaUnidadA_CellClick;

            // txtID BLOQUEADO
            txtID.ReadOnly = true;
            txtID.Enabled = false;
            txtID.TabStop = false;

            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 200;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;

            // ToolTips
            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nueva Unidad Administrativa", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Unidad Administrativa", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Unidad Administrativa", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar Unidad Administrativa", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);

            // EVENTOS DE BOTONES
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
        }

        private void UnidadA_Load(object sender, EventArgs e)
        {
            CargarUnidades();
            AplicarTemaLobby();

            tablaUnidadA.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaUnidadA.AllowUserToResizeColumns = false;
            tablaUnidadA.AllowUserToResizeRows = false;
            tablaUnidadA.RowHeadersVisible = false;

            ConfigurarDataGridViewOscuro(tablaUnidadA);


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
            tablaUnidadA.BackgroundColor = colorFondo;
            tablaUnidadA.BorderStyle = BorderStyle.None;
            tablaUnidadA.EnableHeadersVisualStyles = false;
            tablaUnidadA.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            tablaUnidadA.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tablaUnidadA.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            tablaUnidadA.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tablaUnidadA.RowHeadersVisible = false;

            tablaUnidadA.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaUnidadA.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaUnidadA.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaUnidadA.Dock = DockStyle.Fill;

        }

        #endregion


        //MÉTODOS 
        private void CargarUnidades()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT idUniAdmin, cUniAdmin, nUniAdmin FROM UnidadAdministrativa ORDER BY idUniAdmin";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaUnidadA.DataSource = dt;

                    tablaUnidadA.DefaultCellStyle.ForeColor = Color.Black;
                    tablaUnidadA.DefaultCellStyle.BackColor = Color.White;
                    tablaUnidadA.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    tablaUnidadA.EnableHeadersVisualStyles = true;

                    tablaUnidadA.Columns["idUniAdmin"].HeaderText = "Identificador";
                    tablaUnidadA.Columns["cUniAdmin"].HeaderText = "Clave de la Unidad Administrativa";
                    tablaUnidadA.Columns["nUniAdmin"].HeaderText = "Nombre de la Unidad Administrativa";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar unidades: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtClaveUnidad.Clear();
            txtNombreUnidad.Clear();
            tablaUnidadA.ClearSelection();
        }

        private void AgregarUnidad()
        {
            if (string.IsNullOrWhiteSpace(txtClaveUnidad.Text) ||
                string.IsNullOrWhiteSpace(txtNombreUnidad.Text))
            {
                MessageBox.Show("Ingresa todos los datos.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string check = "SELECT COUNT(*) FROM UnidadAdministrativa WHERE cUniAdmin = @c";
                    SqlCommand chk = new SqlCommand(check, conn);
                    chk.Parameters.AddWithValue("@c", txtClaveUnidad.Text);

                    if ((int)chk.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("⚠️ Esta clave ya existe.");
                        return;
                    }

                    string query = @"INSERT INTO UnidadAdministrativa (cUniAdmin, nUniAdmin) 
                             VALUES (@c, @n)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@c", txtClaveUnidad.Text);
                    cmd.Parameters.AddWithValue("@n", txtNombreUnidad.Text);
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (INSERT)
                string datosNuevos =
                    $"Clave={txtClaveUnidad.Text}, Nombre={txtNombreUnidad.Text}";

                HistorialHelper.RegistrarCambio(
                    "UnidadAdministrativa",
                    txtClaveUnidad.Text,
                    "INSERT",
                    null,
                    datosNuevos
                );

                MessageBox.Show("✅ Unidad agregada correctamente.");
                CargarUnidades();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }


        private void ModificarUnidad()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona un registro.");
                return;
            }

            try
            {
                string datosAnteriores = "";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 🔴 DATOS ANTERIORES REALES
                    string selectQuery = @"SELECT cUniAdmin, nUniAdmin
                                   FROM UnidadAdministrativa
                                   WHERE idUniAdmin = @id";

                    SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@id", txtID.Text);

                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            datosAnteriores =
                                $"Clave={reader["cUniAdmin"]}, Nombre={reader["nUniAdmin"]}";
                        }
                    }

                    string query = @"UPDATE UnidadAdministrativa 
                             SET cUniAdmin = @c, nUniAdmin = @n 
                             WHERE idUniAdmin = @id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@c", txtClaveUnidad.Text);
                    cmd.Parameters.AddWithValue("@n", txtNombreUnidad.Text);
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (UPDATE)
                string datosNuevos =
                    $"Clave={txtClaveUnidad.Text}, Nombre={txtNombreUnidad.Text}";

                HistorialHelper.RegistrarCambio(
                    "UnidadAdministrativa",
                    txtID.Text,
                    "UPDATE",
                    datosAnteriores,
                    datosNuevos
                );

                MessageBox.Show("✅ Unidad actualizada.");
                CargarUnidades();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }


        private void EliminarUnidad()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona un registro.");
                return;
            }

            if (MessageBox.Show("¿Eliminar esta unidad?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                string datosAnteriores =
                    $"Clave={txtClaveUnidad.Text}, Nombre={txtNombreUnidad.Text}";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "DELETE FROM UnidadAdministrativa WHERE idUniAdmin = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (DELETE)
                HistorialHelper.RegistrarCambio(
                    "UnidadAdministrativa",
                    txtID.Text,
                    "DELETE",
                    datosAnteriores,
                    null
                );

                MessageBox.Show("✅ Unidad eliminada.");
                CargarUnidades();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }



        private void BuscarUnidad()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM UnidadAdministrativa WHERE 1=1";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    if (!string.IsNullOrWhiteSpace(txtClaveUnidad.Text))
                    {
                        query += " AND cUniAdmin LIKE @c";
                        cmd.Parameters.AddWithValue("@c", "%" + txtClaveUnidad.Text + "%");
                    }

                    if (!string.IsNullOrWhiteSpace(txtNombreUnidad.Text))
                    {
                        query += " AND nUniAdmin LIKE @n";
                        cmd.Parameters.AddWithValue("@n", "%" + txtNombreUnidad.Text + "%");
                    }

                    cmd.CommandText = query;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaUnidadA.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }

        //EVENTOS
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarUnidad();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarUnidad();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarUnidad();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUnidad();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarUnidades();
        }

        private void tablaUnidadA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = tablaUnidadA.Rows[e.RowIndex];
            txtID.Text = fila.Cells["idUniAdmin"].Value.ToString();
            txtClaveUnidad.Text = fila.Cells["cUniAdmin"].Value.ToString();
            txtNombreUnidad.Text = fila.Cells["nUniAdmin"].Value.ToString();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Función de exportar pendiente.");
        }

        private void txtClaveUnidad_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtClaveUnidad.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtClaveUnidad.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtClaveUnidad.Text != limpio)
            {
                txtClaveUnidad.Text = limpio;
                txtClaveUnidad.SelectionStart = Math.Min(cursor, txtClaveUnidad.Text.Length);
            }
        }
    }
}
