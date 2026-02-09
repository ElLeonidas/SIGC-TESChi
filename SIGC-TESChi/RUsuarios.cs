using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace SIGC_TESChi
{
    public partial class RUsuarios : UserControl
    {
        private ToolTip toolTip;

        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        public RUsuarios()
        {
            InitializeComponent();

            txtContrasena.UseSystemPasswordChar = true;

            ConfigurarDataGridViewOscuro(TablaUsuarios);

            //TablaUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //TablaUsuarios.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //TablaUsuarios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //TablaUsuarios.Dock = DockStyle.Fill;

            // Eventos
            Load += RUsuarios_Load;
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnBuscar.Click += btnBuscar_Click;
            TablaUsuarios.CellClick += TablaUsuarios_CellClick;

            // txtID no editable
            txtIdentificador.ReadOnly = true;

            // ToolTips (IGUAL QUE UBICACIONES)
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 200;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;

            btnAgregar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nuevo Usuario", btnAgregar);
            btnAgregar.MouseLeave += (s, e) => toolTip.Hide(btnAgregar);

            btnModificar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar Usuario", btnModificar);
            btnModificar.MouseLeave += (s, e) => toolTip.Hide(btnModificar);

            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Usuario", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);

            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);

            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar Usuario", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);
        }

        private void RUsuarios_Load(object sender, EventArgs e)
        {

            if (SessionData.IdTipoUsuario != 1)
            {
                MessageBox.Show("No tienes permisos para acceder a este módulo");
                this.Enabled = false;
            }


            CargarTiposUsuario();
            CargarUsuarios();
            AplicarTemaLobby();

            TablaUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TablaUsuarios.AllowUserToResizeColumns = false;
            TablaUsuarios.AllowUserToResizeRows = false;
            TablaUsuarios.RowHeadersVisible = false;


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
                label1, label2, label3, label4,
                lblTitulo, label6, label7, label8, label9
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


            // =========================
            // 📊 DATAGRIDVIEW
            // =========================
            TablaUsuarios.BackgroundColor = colorFondo;
            TablaUsuarios.BorderStyle = BorderStyle.None;
            TablaUsuarios.EnableHeadersVisualStyles = false;
            TablaUsuarios.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            TablaUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            TablaUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            TablaUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            TablaUsuarios.RowHeadersVisible = false;

            TablaUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TablaUsuarios.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TablaUsuarios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TablaUsuarios.Dock = DockStyle.Fill;

        }

        #endregion

        //  CARGAS 
        private void CargarTiposUsuario()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string q = "SELECT idTipoUsuario, dTipoUsuario FROM TipoUsuario";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboTipoUsuario.DataSource = dt;
                comboTipoUsuario.DisplayMember = "dTipoUsuario"; // texto visible
                comboTipoUsuario.ValueMember = "idTipoUsuario";  // INT real
            }

            comboTipoUsuario.SelectedIndex = -1;
        }



        private void CargarUsuarios()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"
        SELECT 
            U.idUsuario,
            U.Username,
            U.Nombre,
            U.Apaterno,
            U.Amaterno,
            U.idTipoUsuario,               
            T.dTipoUsuario AS TipoUsuario
        FROM Usuario U
        INNER JOIN TipoUsuario T ON U.idTipoUsuario = T.idTipoUsuario";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                TablaUsuarios.DataSource = null;
                TablaUsuarios.Columns.Clear();
                TablaUsuarios.AutoGenerateColumns = true;
                TablaUsuarios.DataSource = dt;

                // 🔹 CAMBIAR TÍTULOS DE COLUMNAS
                TablaUsuarios.Columns["idUsuario"].HeaderText = "Identificador";
                TablaUsuarios.Columns["Username"].HeaderText = "Username";
                TablaUsuarios.Columns["Nombre"].HeaderText = "Nombre";
                TablaUsuarios.Columns["Amaterno"].HeaderText = "Apellido Materno";
                TablaUsuarios.Columns["Apaterno"].HeaderText = "Apellido Paterno";
                TablaUsuarios.Columns["TipoUsuario"].HeaderText = "Tipo de Usuario";

                // 🔒 Ocultar ID
                TablaUsuarios.Columns["idTipoUsuario"].Visible = false;
            }
        }




        public static string CrearHashPBKDF2(string password)
        {
            int iterations = 100000;
            byte[] salt = new byte[16];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                return iterations + ":" +
                       Convert.ToBase64String(salt) + ":" +
                       Convert.ToBase64String(hash);
            }
        }


        private void AgregarUsuario()
        {
            if (CamposVaciosAgregar())
            {
                MessageBox.Show("⚠️ Llena todos los campos.");
                return;
            }

            if (comboTipoUsuario.SelectedIndex == -1)
            {
                MessageBox.Show("⚠️ Selecciona un tipo de usuario.");
                return;
            }

            if (ExisteUsuario(txtNombreAcceso.Text.Trim()))
            {
                MessageBox.Show("⚠️ Este usuario ya está registrado.");
                return;
            }

            int idTipoUsuario = Convert.ToInt32(comboTipoUsuario.SelectedValue);

            string hashPassword = CrearHashPBKDF2(txtContrasena.Text.Trim());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string q = @"
INSERT INTO Usuario
(Username, Nombre, Apaterno, Amaterno, Contrasena, idTipoUsuario)
VALUES (@u, @n, @ap, @am, @c, @t)";

                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    cmd.Parameters.Add("@u", SqlDbType.NVarChar).Value = txtNombreAcceso.Text.Trim();
                    cmd.Parameters.Add("@n", SqlDbType.NVarChar).Value = txtUsuario.Text.Trim();
                    cmd.Parameters.Add("@ap", SqlDbType.NVarChar).Value = txtApaterno.Text.Trim();
                    cmd.Parameters.Add("@am", SqlDbType.NVarChar).Value = txtAmaterno.Text.Trim();
                    cmd.Parameters.Add("@c", SqlDbType.NVarChar).Value = hashPassword;
                    cmd.Parameters.Add("@t", SqlDbType.Int).Value = idTipoUsuario;

                    cmd.ExecuteNonQuery();
                }
            }

            string datosNuevos =
                $"Username={txtNombreAcceso.Text.Trim()}, " +
                $"Nombre={txtUsuario.Text.Trim()}, " +
                $"Apaterno={txtApaterno.Text.Trim()}, " +
                $"Amaterno={txtAmaterno.Text.Trim()}, " +
                $"TipoID={idTipoUsuario}";

            HistorialHelper.RegistrarCambio(
                "Usuario",
                txtNombreAcceso.Text.Trim(),
                "INSERT",
                null,
                datosNuevos
            );

            MessageBox.Show("✅ Usuario agregado correctamente.");

            CargarUsuarios();
            LimpiarCampos();
        }


        private void ModificarUsuario()
        {
            if (string.IsNullOrWhiteSpace(txtIdentificador.Text))
            {
                MessageBox.Show("⚠️ Selecciona un usuario.");
                return;
            }

            if (CamposVaciosModificar())
            {
                MessageBox.Show("⚠️ Llena todos los campos obligatorios.");
                return;
            }

            int tipo = Convert.ToInt32(comboTipoUsuario.SelectedValue);
            string datosAnteriores = "";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string select = @"
        SELECT Username, Nombre, Apaterno, Amaterno, idTipoUsuario
        FROM Usuario WHERE idUsuario=@id";

                SqlCommand selCmd = new SqlCommand(select, con);
                selCmd.Parameters.AddWithValue("@id", txtIdentificador.Text);

                using (SqlDataReader dr = selCmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        datosAnteriores =
                            $"Username={dr["Username"]}, Nombre={dr["Nombre"]}, " +
                            $"Apaterno={dr["Apaterno"]}, Amaterno={dr["Amaterno"]}, Tipo={dr["idTipoUsuario"]}";
                    }
                }

                string q = @"
        UPDATE Usuario SET
            Username=@u,
            Nombre=@n,
            Apaterno=@ap,
            Amaterno=@am,
            idTipoUsuario=@t";

                bool cambiarPassword =
                    !string.IsNullOrWhiteSpace(txtContrasena.Text) &&
                    txtContrasena.Text != "********";

                if (cambiarPassword)
                    q += ", Contrasena=@c";

                q += " WHERE idUsuario=@id";

                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@u", txtNombreAcceso.Text.Trim());
                cmd.Parameters.AddWithValue("@n", txtUsuario.Text.Trim());
                cmd.Parameters.AddWithValue("@ap", txtApaterno.Text.Trim());
                cmd.Parameters.AddWithValue("@am", txtAmaterno.Text.Trim());
                cmd.Parameters.AddWithValue("@t", tipo);
                cmd.Parameters.AddWithValue("@id", txtIdentificador.Text);

                if (cambiarPassword)
                {
                    string hashPassword = CrearHashPBKDF2(txtContrasena.Text.Trim());
                    cmd.Parameters.AddWithValue("@c", hashPassword);
                }

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("✅ Usuario modificado.");
            CargarUsuarios();
            LimpiarCampos();
        }




        private void EliminarUsuario()
        {
            if (string.IsNullOrWhiteSpace(txtIdentificador.Text))
            {
                MessageBox.Show("⚠️ Selecciona un usuario.");
                return;
            }

            // 🔴 Evitar que el usuario se elimine a sí mismo
            int idUsuarioSeleccionado = Convert.ToInt32(txtIdentificador.Text);
            if (idUsuarioSeleccionado == SessionData.IdUsuario)
            {
                MessageBox.Show("⚠️ No puedes eliminarte a ti mismo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Seguro de eliminar?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            string datosAnteriores =
                $"Username={txtNombreAcceso.Text}, Nombre={txtUsuario.Text}, " +
                $"Apaterno={txtApaterno.Text}, Amaterno={txtAmaterno.Text}";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Usuario WHERE idUsuario=@id", con);
                cmd.Parameters.AddWithValue("@id", idUsuarioSeleccionado);
                cmd.ExecuteNonQuery();
            }

            // 🔴 HISTORIAL (DELETE)
            HistorialHelper.RegistrarCambio(
                "Usuario",
                txtIdentificador.Text,
                "DELETE",
                datosAnteriores,
                null
            );

            MessageBox.Show("✅ Usuario eliminado.");
            CargarUsuarios();
            LimpiarCampos();
        }




        //AGREGAR 
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarUsuario();
        }       

        //MODIFICAR 
        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarUsuario();
        }
         
        // ELIMINAR 
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarUsuario();
        }      

        // BUSCAR 
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUsuarios();
        }

        private void BuscarUsuarios()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"
                SELECT 
                    U.idUsuario,
                    U.Username,
                    U.Nombre,
                    U.Apaterno,
                    U.Amaterno,
                    U.idTipoUsuario,
                    T.dTipoUsuario AS TipoUsuario
                FROM Usuario U
                INNER JOIN TipoUsuario T ON U.idTipoUsuario = T.idTipoUsuario
                WHERE 1=1";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                if (!string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    query += " AND U.Nombre LIKE @n";
                    cmd.Parameters.AddWithValue("@n", "%" + txtUsuario.Text + "%");
                }

                if (comboTipoUsuario.SelectedIndex != -1)
                {
                    string tipo = comboTipoUsuario.SelectedItem.ToString().Split(' ')[0];
                    query += " AND U.idTipoUsuario=@t";
                    cmd.Parameters.AddWithValue("@t", tipo);
                }

                cmd.CommandText = query;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                TablaUsuarios.DataSource = dt;
            }
        }

        //TABLA 
        private void TablaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = TablaUsuarios.Rows[e.RowIndex];

            txtIdentificador.Text = fila.Cells["idUsuario"].Value.ToString();
            txtNombreAcceso.Text = fila.Cells["Username"].Value.ToString();
            txtUsuario.Text = fila.Cells["Nombre"].Value.ToString();
            txtApaterno.Text = fila.Cells["Apaterno"].Value.ToString();
            txtAmaterno.Text = fila.Cells["Amaterno"].Value.ToString();

            txtContrasena.Text = "********";
            comboTipoUsuario.SelectedValue = fila.Cells["idTipoUsuario"].Value;

            // 🔹 Deshabilitar botón Eliminar si es el usuario actual
            int idSeleccionado = Convert.ToInt32(fila.Cells["idUsuario"].Value);
            btnEliminar.Enabled = idSeleccionado != SessionData.IdUsuario;
        }

        private bool ExisteUsuario(string username)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Usuario WHERE Username=@u", con);
                cmd.Parameters.AddWithValue("@u", username);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private bool CamposVaciosAgregar()
        {
            return string.IsNullOrWhiteSpace(txtNombreAcceso.Text) ||
                   string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                   string.IsNullOrWhiteSpace(txtApaterno.Text) ||
                   string.IsNullOrWhiteSpace(txtAmaterno.Text) ||
                   string.IsNullOrWhiteSpace(txtContrasena.Text) ||
                   comboTipoUsuario.SelectedIndex == -1;
        }

        private bool CamposVaciosModificar()
        {
            return string.IsNullOrWhiteSpace(txtNombreAcceso.Text) ||
                   string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                   string.IsNullOrWhiteSpace(txtApaterno.Text) ||
                   string.IsNullOrWhiteSpace(txtAmaterno.Text) ||
                   comboTipoUsuario.SelectedIndex == -1;
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarUsuarios();
        }

        private void LimpiarCampos()
        {
            txtIdentificador.Clear();
            txtNombreAcceso.Clear();
            txtUsuario.Clear();
            txtApaterno.Clear();
            txtAmaterno.Clear();
            txtContrasena.Clear();
            comboTipoUsuario.SelectedIndex = -1;
            TablaUsuarios.ClearSelection();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                btnAgregar.PerformClick(); // Ejecuta el botón Agregar
                return true; // Indica que la tecla fue manejada
            }

            if (keyData == (Keys.Control | Keys.Delete))
            {
                btnEliminar.PerformClick(); // Ejecuta el botón Eliminar
                return true;
            }

            if (keyData == (Keys.Control | Keys.F))
            {
                btnBuscar.PerformClick(); // Ejecuta el botón Buscar
                return true;
            }

            if (keyData == (Keys.Control | Keys.H))
            {
                btnModificar.PerformClick(); // Ejecuta el botón Modificar
                return true;
            }

            if (keyData == (Keys.Control | Keys.N))
            {
                btnLimpiar.PerformClick(); // Ejecuta el botón Limpiar
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtNombreAcceso_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtNombreAcceso.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtNombreAcceso.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtNombreAcceso.Text != limpio)
            {
                txtNombreAcceso.Text = limpio;
                txtNombreAcceso.SelectionStart = Math.Min(cursor, txtNombreAcceso.Text.Length);
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtUsuario.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtUsuario.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtUsuario.Text != limpio)
            {
                txtUsuario.Text = limpio;
                txtUsuario.SelectionStart = Math.Min(cursor, txtUsuario.Text.Length);
            }
        }

        private void txtApaterno_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtApaterno.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtApaterno.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtApaterno.Text != limpio)
            {
                txtApaterno.Text = limpio;
                txtApaterno.SelectionStart = Math.Min(cursor, txtApaterno.Text.Length);
            }
        }

        private void txtAmaterno_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtAmaterno.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtAmaterno.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtAmaterno.Text != limpio)
            {
                txtAmaterno.Text = limpio;
                txtAmaterno.SelectionStart = Math.Min(cursor, txtAmaterno.Text.Length);
            }
        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {

            

        }

        private void txtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Bloquear la tecla espacio
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {

        }
    }
}
