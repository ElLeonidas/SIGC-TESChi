using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class SubSecciones : UserControl
    {
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";
        private ToolTip toolTip;

        public SubSecciones()
        {
            InitializeComponent();

            tablaSubsecciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaSubsecciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaSubsecciones.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaSubsecciones.Dock = DockStyle.Fill;

            Load += SubSecciones_Load;


            // Inicializamos el ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;  // Visible 5 segundos
            toolTip.InitialDelay = 200;   // Aparece tras 0.2 segundos
            toolTip.ReshowDelay = 100;    // Retardo entre botones
            toolTip.ShowAlways = true;    // Siempre visible

            // Asignar eventos de mouse para mostrar tooltips
            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nueva Subsección", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);
            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Subsección", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);
            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Subsección", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);
            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar Subsección", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);
            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);

            // Eventos
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            tablaSubsecciones.CellClick += tablaSubsecciones_CellClick;

            // Bloqueo total del ID
            txtID.ReadOnly = true;
            txtID.Enabled = false;
            txtID.TabStop = false;
        }

        private void SubSecciones_Load(object sender, EventArgs e)
        {
            CargarSubSecciones();
            CargarSeccionesCombo();
            AplicarTemaLobby();
        }

        #region DISEÑO

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
                lblTitulo, label2, label3, label4, label5
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
            tablaSubsecciones.BackgroundColor = colorFondo;
            tablaSubsecciones.BorderStyle = BorderStyle.None;
            tablaSubsecciones.EnableHeadersVisualStyles = false;
            tablaSubsecciones.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            tablaSubsecciones.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tablaSubsecciones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            tablaSubsecciones.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tablaSubsecciones.RowHeadersVisible = false;

            tablaSubsecciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaSubsecciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaSubsecciones.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaSubsecciones.Dock = DockStyle.Fill;

        }

        #endregion

        //EVENTOS 

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarSubSeccion();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarSubSeccion();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarSubSeccion();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarSubSeccion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarSubSecciones();
        }

        // MÉTODOS 
        private void CargarSubSecciones()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT idSubSeccion, claveSubSeccion, dSubSeccion FROM SubSeccion ORDER BY idSubSeccion";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaSubsecciones.DataSource = dt;

                    tablaSubsecciones.DefaultCellStyle.ForeColor = Color.Black;
                    tablaSubsecciones.DefaultCellStyle.BackColor = Color.White;
                    tablaSubsecciones.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    tablaSubsecciones.EnableHeadersVisualStyles = true;

                    tablaSubsecciones.Columns["idSubSeccion"].HeaderText = "Identificador";
                    tablaSubsecciones.Columns["claveSubSeccion"].HeaderText = "Clave de la Subsección";
                    tablaSubsecciones.Columns["dSubSeccion"].HeaderText = "Nombre de la Subsección";



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar SubSecciones: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtClaveSubseccion.Clear();
            txtSubseccion.Clear();
            txtClaveSubseccion.Focus();
        }

        private void CargarSeccionesCombo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                SELECT idSeccion, dSeccion
                FROM Seccion
                ORDER BY dSeccion";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        cmbSeccion.DataSource = dt;
                        cmbSeccion.DisplayMember = "dSeccion";   // lo que ve el usuario
                        cmbSeccion.ValueMember = "idSeccion";    // lo que usa el sistema
                        cmbSeccion.SelectedIndex = -1;           // nada seleccionado por defecto
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las secciones:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void AgregarSubSeccion()
        {
            if (string.IsNullOrWhiteSpace(txtClaveSubseccion.Text) ||
                string.IsNullOrWhiteSpace(txtSubseccion.Text))
            {
                MessageBox.Show(
                    "Ingresa la clave y la descripción de la SubSección.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (cmbSeccion.SelectedValue == null)
            {
                MessageBox.Show(
                    "Selecciona una sección válida.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string clave = txtClaveSubseccion.Text.Trim();
            string descripcion = txtSubseccion.Text.Trim();
            int idSeccion = Convert.ToInt32(cmbSeccion.SelectedValue);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 🔎 Validar duplicado (clave + sección)
                    string checkQuery = @"
                SELECT COUNT(1)
                FROM SubSeccion
                WHERE claveSubSeccion = @clave
                  AND idSeccion = @idSeccion";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@clave", clave);
                        checkCmd.Parameters.AddWithValue("@idSeccion", idSeccion);

                        if ((int)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show(
                                "⚠️ Esta SubSección ya existe en la sección seleccionada.",
                                "Duplicado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return;
                        }
                    }

                    // 💾 INSERT correcto (con FK)
                    string insertQuery = @"
                INSERT INTO SubSeccion
                (idSeccion, claveSubSeccion, dSubSeccion)
                VALUES
                (@idSeccion, @clave, @descripcion)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@idSeccion", idSeccion);
                        cmd.Parameters.AddWithValue("@clave", clave);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.ExecuteNonQuery();
                    }
                }

                // 🧾 HISTORIAL
                string datosNuevos =
                    $"idSeccion={idSeccion}, claveSubSeccion={clave}, dSubSeccion={descripcion}";

                HistorialHelper.RegistrarCambio(
                    "SubSeccion",
                    clave,     // llave lógica
                    "INSERT",
                    "",
                    datosNuevos
                );

                MessageBox.Show(
                    "✅ SubSección agregada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                CargarSubSecciones();
                LimpiarCampos();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show(
                    "La sección seleccionada no existe.",
                    "Error de referencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al agregar la SubSección:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }



        private void ModificarSubSeccion()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una SubSección para modificar.");
                return;
            }

            try
            {
                string datosAnteriores = "";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // 🔴 OBTENER DATOS ANTERIORES REALES
                    string selectQuery = @"SELECT claveSubSeccion, dSubSeccion
                                   FROM SubSeccion
                                   WHERE idSubSeccion = @id";

                    SqlCommand selectCmd = new SqlCommand(selectQuery, con);
                    selectCmd.Parameters.AddWithValue("@id", txtID.Text);

                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            datosAnteriores =
                                $"Clave={reader["claveSubSeccion"]}, SubSeccion={reader["dSubSeccion"]}";
                        }
                    }

                    string query = @"UPDATE SubSeccion 
                             SET claveSubSeccion = @clave, dSubSeccion = @desc
                             WHERE idSubSeccion = @id";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@clave", txtClaveSubseccion.Text);
                    cmd.Parameters.AddWithValue("@desc", txtSubseccion.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);

                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (UPDATE)
                string datosNuevos = $"Clave={txtClaveSubseccion.Text}, SubSeccion={txtSubseccion.Text}";
                HistorialHelper.RegistrarCambio(
                    "SubSeccion",
                    txtID.Text,
                    "UPDATE",
                    datosAnteriores,
                    datosNuevos
                );

                MessageBox.Show("✅ SubSección modificada.");
                CargarSubSecciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }


        private void EliminarSubSeccion()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona una SubSección para eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "¿Eliminar esta SubSección?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                string datosAnteriores =
                    $"Clave={txtClaveSubseccion.Text}, SubSeccion={txtSubseccion.Text}";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "DELETE FROM SubSeccion WHERE idSubSeccion = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (DELETE)
                HistorialHelper.RegistrarCambio(
                    "SubSeccion",
                    txtID.Text,
                    "DELETE",
                    datosAnteriores,
                    null
                );

                MessageBox.Show("✅ SubSección eliminada.");
                CargarSubSecciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }


        private void BuscarSubSeccion()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM SubSeccion WHERE 1=1";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    if (!string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        query += " AND idSubSeccion = @id";
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(txtClaveSubseccion.Text))
                    {
                        query += " AND claveSubSeccion LIKE @clave";
                        cmd.Parameters.AddWithValue("@clave", "%" + txtClaveSubseccion.Text + "%");
                    }

                    if (!string.IsNullOrWhiteSpace(txtSubseccion.Text))
                    {
                        query += " AND dSubSeccion LIKE @desc";
                        cmd.Parameters.AddWithValue("@desc", "%" + txtSubseccion.Text + "%");
                    }

                    cmd.CommandText = query;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tablaSubsecciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }

        private void tablaSubsecciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = tablaSubsecciones.Rows[e.RowIndex];

            txtID.Text = fila.Cells["idSubSeccion"].Value.ToString();
            txtClaveSubseccion.Text = fila.Cells["claveSubSeccion"].Value.ToString();
            txtSubseccion.Text = fila.Cells["dSubSeccion"].Value.ToString();
        }

        private void txtClaveSubseccion_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtClaveSubseccion.SelectionStart;
            string texto = txtClaveSubseccion.Text;

            // 1️⃣ Permitir letras, números, acentos, espacios y punto
            texto = Regex.Replace(
                texto,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\.]",
                ""
            );

            // 2️⃣ Eliminar puntos al inicio
            texto = Regex.Replace(texto, @"^\.+", "");

            // 3️⃣ Evitar puntos consecutivos
            texto = Regex.Replace(texto, @"\.{2,}", ".");

            // 4️⃣ Eliminar puntos que NO tengan un número antes en el mismo bloque
            texto = Regex.Replace(texto, @"(?<!\d)\.(?=[^0-9]*\.)", "");

            // 5️⃣ Reemplazar múltiples espacios por uno
            texto = Regex.Replace(texto, @"\s{2,}", " ");

            // 6️⃣ Evitar espacios al inicio
            texto = texto.TrimStart();

            if (txtClaveSubseccion.Text != texto)
            {
                txtClaveSubseccion.Text = texto;
                txtClaveSubseccion.SelectionStart =
                    Math.Min(cursor, txtClaveSubseccion.Text.Length);
            }
        }

        private void txtSubseccion_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtSubseccion.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtSubseccion.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtSubseccion.Text != limpio)
            {
                txtSubseccion.Text = limpio;
                txtSubseccion.SelectionStart = Math.Min(cursor, txtSubseccion.Text.Length);
            }
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
