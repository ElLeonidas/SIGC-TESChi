using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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

            //tablaUbicaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //tablaUbicaciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //tablaUbicaciones.ColumnHeadersDefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleCenter;
            //tablaUbicaciones.Dock = DockStyle.Fill;

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

        private List<string> ListaPalabras = new List<string>();

        private void Ubicaciones_Load(object sender, EventArgs e)
        {
            CargarUbicaciones();

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
                lblTitulo, label2, label3
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
            EstiloBoton(btnExportarPDF, Color.FromArgb(155, 211, 171));

            // =========================
            // 📊 DATAGRIDVIEW
            // =========================
            tablaUbicaciones.BackgroundColor = colorFondo;
            tablaUbicaciones.BorderStyle = BorderStyle.None;
            tablaUbicaciones.EnableHeadersVisualStyles = false;
            tablaUbicaciones.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            tablaUbicaciones.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tablaUbicaciones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            tablaUbicaciones.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tablaUbicaciones.RowHeadersVisible = false;

            tablaUbicaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaUbicaciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaUbicaciones.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaUbicaciones.Dock = DockStyle.Fill;

        }

        #endregion

        #region BOTONES

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

        #endregion



        #region METODOS
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

                    tablaUbicaciones.DefaultCellStyle.ForeColor = Color.Black;
                    tablaUbicaciones.DefaultCellStyle.BackColor = Color.White;
                    tablaUbicaciones.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    tablaUbicaciones.EnableHeadersVisualStyles = true;

                    // 🔹 CAMBIAR TÍTULOS DE COLUMNAS
                    tablaUbicaciones.Columns["idUbicacion"].HeaderText = "Identificador";
                    tablaUbicaciones.Columns["dUbicacion"].HeaderText = "Nombre de Ubicacion";
                    
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
            // 0️⃣ Limpiar espacios extra del texto ingresado
            string ubicacion = Utillidades.LimpiarEspacios(txtUbicacion.Text);

            if (string.IsNullOrWhiteSpace(ubicacion))
            {
                MessageBox.Show("Por favor ingresa una ubicación.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 1️⃣ Verificar duplicados usando versión normalizada
                    string checkQuery = "SELECT dUbicacion FROM Ubicacion";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        List<string> listaUbicaciones = new List<string>();
                        using (SqlDataReader reader = checkCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Limpiar espacios al traer de la BD también
                                listaUbicaciones.Add(Utillidades.LimpiarEspacios(reader.GetString(0)));
                            }
                        }

                        if (Utillidades.EsDuplicado(listaUbicaciones, ubicacion))
                        {
                            MessageBox.Show("⚠️ Esta ubicación ya está registrada.");
                            return;
                        }
                    }

                    // 2️⃣ INSERT + obtener ID generado
                    string query = @"
                INSERT INTO Ubicacion (dUbicacion)
                VALUES (@ubicacion);
                SELECT SCOPE_IDENTITY();";

                    int idUbicacion;
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Guardar la ubicación ya limpia
                        cmd.Parameters.AddWithValue("@ubicacion", ubicacion);
                        idUbicacion = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3️⃣ Registrar historial DESPUÉS del INSERT
                    HistorialHelper.RegistrarCambio(
                        "Ubicacion",                 // Tabla afectada
                        idUbicacion.ToString(),      // Llave primaria
                        "INSERT",                    // Tipo de acción
                        null,                        // No hay datos anteriores
                        $"Ubicación={ubicacion}"     // Datos nuevos
                    );
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
                string datosAntes = "";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 1️⃣ Obtener datos ANTERIORES
                    string selectQuery = "SELECT dUbicacion FROM Ubicacion WHERE idUbicacion = @id";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@id", txtID.Text);
                        datosAntes = selectCmd.ExecuteScalar()?.ToString();
                    }

                    // 2️⃣ UPDATE
                    string query = "UPDATE Ubicacion SET dUbicacion = @ubicacion WHERE idUbicacion = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ubicacion", txtUbicacion.Text);
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                        cmd.ExecuteNonQuery();
                    }

                    // 🔴🔴🔴 CORRECCIÓN CLAVE 🔴🔴🔴
                    // 3️⃣ Registrar historial
                    HistorialHelper.RegistrarCambio(
                        "Ubicacion",
                        txtID.Text,
                        "UPDATE",
                        $"Ubicación={datosAntes}",
                        $"Ubicación={txtUbicacion.Text}"
                    );
                    // 🔴🔴🔴 FIN DE LA CORRECCIÓN 🔴🔴🔴
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
                string datosAntes = "";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 1️⃣ Obtener datos ANTERIORES
                    string selectQuery = "SELECT dUbicacion FROM Ubicacion WHERE idUbicacion = @id";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@id", txtID.Text);
                        datosAntes = selectCmd.ExecuteScalar()?.ToString();
                    }

                    // 2️⃣ DELETE
                    string query = "DELETE FROM Ubicacion WHERE idUbicacion = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                        cmd.ExecuteNonQuery();
                    }

                    // 🔴🔴🔴 CORRECCIÓN CLAVE 🔴🔴🔴
                    // 3️⃣ Registrar historial
                    HistorialHelper.RegistrarCambio(
                        "Ubicacion",
                        txtID.Text,
                        "DELETE",
                        $"Ubicación={datosAntes}",
                        null
                    );
                    // 🔴🔴🔴 FIN DE LA CORRECCIÓN 🔴🔴🔴
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

        #endregion

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

        private void txtUbicacion_TextChanged(object sender, EventArgs e)
        {

            int cursor = txtUbicacion.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtUbicacion.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtUbicacion.Text != limpio)
            {
                txtUbicacion.Text = limpio;
                txtUbicacion.SelectionStart = Math.Min(cursor, txtUbicacion.Text.Length);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnExportarPDF_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}

