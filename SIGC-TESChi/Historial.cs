using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class Historial : UserControl
    {
        // Cadena de conexión
        private static string connectionString => Program.ConnectionString;

        private ToolTip toolTip;


        public Historial()
        {
            InitializeComponent();

            HistorialHelper.HistorialActualizado += RefrescarHistorial;

            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHistorial.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHistorial.Dock = DockStyle.Fill;

            //EVENTOS
            Load += Historial_Load;
            btnBuscar.Click += btnBuscar_Click;

            // ToolTips (IGUAL QUE UBICACIONES)
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 200;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;

            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar en el Historial", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);

        }

        public void RefrescarHistorial()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(RefrescarHistorial));
                return;
            }

            CargarHistorial();
        }


        private void Historial_Load(object sender, EventArgs e)
        {

            using (var con = Db.CreateConnection())
            {
                con.Open();
                // consultas reales aquí
            }

            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.ReadOnly = true;
            dgvHistorial.AutoGenerateColumns = true;

            LlenarComboTablas();
            CargarHistorial();

            AplicarTemaLobby();

            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.AllowUserToResizeColumns = false;
            dgvHistorial.AllowUserToResizeRows = false;
            dgvHistorial.RowHeadersVisible = false;

            ConfigurarDataGridViewOscuro(dgvHistorial);


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
           
            EstiloBoton(btnBuscar, Color.FromArgb(125, 141, 127));

            // =========================
            // 📊 DATAGRIDVIEW
            // =========================
            dgvHistorial.BackgroundColor = colorFondo;
            dgvHistorial.BorderStyle = BorderStyle.None;
            dgvHistorial.EnableHeadersVisualStyles = false;
            dgvHistorial.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            dgvHistorial.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistorial.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvHistorial.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvHistorial.RowHeadersVisible = false;

            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHistorial.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHistorial.Dock = DockStyle.Fill;

        }

        #endregion

        private void LlenarComboTablas()
        {
            try
            {
                cmbTabla.Items.Clear();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT TABLE_NAME 
                                   FROM INFORMATION_SCHEMA.TABLES
                                   WHERE TABLE_TYPE = 'BASE TABLE'
                                   ORDER BY TABLE_NAME";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        cmbTabla.Items.Add("TODAS");

                        while (dr.Read())
                            cmbTabla.Items.Add(dr.GetString(0));
                    }
                }

                if (cmbTabla.Items.Count > 0)
                    cmbTabla.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tablas: " + ex.Message);
            }
        }

        private void CargarHistorial()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                SELECT 
                    h.idHistorial,
                    h.Tabla,
                    h.Llave,
                    h.TipoAccion,
                    h.UsuarioBD,
                    h.FechaAccion,
                    h.DatosAnteriores,
                    h.DatosNuevos,
                    h.idUsuarioApp,
                    u.Nombre + ' ' + u.Apaterno + ' ' + u.Amaterno AS UsuarioApp
                FROM HistorialCambios h
                LEFT JOIN Usuario u 
                    ON h.idUsuarioApp = u.idUsuario
                ORDER BY h.FechaAccion DESC;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                        dgvHistorial.DataSource = dt;

                        // 🔒 Columnas técnicas (opcional ocultarlas)
                        if (dgvHistorial.Columns["idHistorial"] != null)
                            dgvHistorial.Columns["idHistorial"].Visible = false;

                        if (dgvHistorial.Columns["idUsuarioApp"] != null)
                            dgvHistorial.Columns["idUsuarioApp"].Visible = false;

                        if (dgvHistorial.Columns["Llave"] != null)
                            dgvHistorial.Columns["Llave"].Visible = false;

                        dgvHistorial.DefaultCellStyle.ForeColor = Color.Black;
                        dgvHistorial.DefaultCellStyle.BackColor = Color.White;
                        dgvHistorial.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                        dgvHistorial.EnableHeadersVisualStyles = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el historial:\n" + ex.Message,
                    "Historial de cambios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarHistorial();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {

        }
    }
}
