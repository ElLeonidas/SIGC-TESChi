using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SIGC_TESChi
{
    public partial class CArchivos : UserControl
    {

        // 🔹 Cadena de conexión (ajústala si tu instancia/localdb es diferente)
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

        DataTable dtImportado;
        DataTable dtValidos;
        DataTable dtErrores;

        Dictionary<string, int> dicSeccion;
        Dictionary<string, int> dicSubSeccion;
        Dictionary<string, int> dicInstituto;
        Dictionary<string, int> dicUbicacion;
        Dictionary<string, int> dicEstatus;
        Dictionary<string, int> dicClasificacion;
        Dictionary<string, int> secciones;
        Dictionary<string, int> subSecciones;


        HashSet<string> expedientesBD;

        HashSet<string> expedientesCSV = new HashSet<string>();

        int totalRegistros = 0;
        int validos = 0;
        int errores = 0;
        int duplicadosCSV = 0;
        int duplicadosBD = 0;

        private ToolTip toolTip;

        public CArchivos()
        {
            InitializeComponent();

            // Eventos
            Load += CArchivos_Load;
            btnGuardar.Click += btnGuardar_Click;
            btnEditar.Click += btnEditar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnBuscar.Click += btnBuscar_Click;
            dgvControl.CellClick += dgvControl_CellClick;

            // ToolTips (IGUAL QUE UBICACIONES)
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 200;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;

            this.AutoScroll = true;

            cboAño.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCodUnidAdmin.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNombUniAdmin.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSeccion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSubSeccion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboInstituto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUbicacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboClasificacion.DropDownStyle = ComboBoxStyle.DropDownList;

            Load += CArchivos_Load;

            this.cboSubSeccion.SelectedIndexChanged += new System.EventHandler(this.cboSubSeccion_SelectedIndexChanged);

            btnGuardar.MouseEnter += (s, e) => toolTip.Show("Boton para Agregar Nuevo Expediente", btnGuardar);
            btnGuardar.MouseLeave += (s, e) => toolTip.Hide(btnGuardar);

            btnEditar.MouseEnter += (s, e) => toolTip.Show("Boton para Modificar el Expediente", btnEditar);
            btnEditar.MouseLeave += (s, e) => toolTip.Hide(btnEditar);

            btnLimpiar.MouseEnter += (s, e) => toolTip.Show("Boton para Limpiar Campos", btnLimpiar);
            btnLimpiar.MouseLeave += (s, e) => toolTip.Hide(btnLimpiar);

            btnEliminar.MouseEnter += (s, e) => toolTip.Show("Boton para Eliminar Expediente", btnEliminar);
            btnEliminar.MouseLeave += (s, e) => toolTip.Hide(btnEliminar);

            btnExportar.MouseEnter += (s, e) => toolTip.Show("Boton para Exportar Expedientes", btnExportar);
            btnExportar.MouseLeave += (s, e) => toolTip.Hide(btnExportar);

            btnImportar.MouseEnter += (s, e) => toolTip.Show("Boton para Importar Expedientes", btnImportar);
            btnImportar.MouseLeave += (s, e) => toolTip.Hide(btnImportar);

            btnBuscar.MouseEnter += (s, e) => toolTip.Show("Boton para Buscar Expediente", btnBuscar);
            btnBuscar.MouseLeave += (s, e) => toolTip.Hide(btnBuscar);

            dgvDocumentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDocumentos.MultiSelect = false;

        }

        private void CArchivos_Load(object sender, EventArgs e)
        {
            CargarArchivos();
            LlenarComboAÑo();
            Refrescar();

            CargarTabla();
            CargarEstatus();
            CargarSecciones();
            CargarUbicaciones();
            CargarCodUnidAdmin();
            CargarNombUnidAdmin();
            CargarInstitutos();
            CargarClasificacion();
            

            combosListos = true;

            GenerarFormulaClasificatoria();

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
                lblTitulo, label2, label3, label4, label5, label6, label7, label8, 
                 label10, label11, label12, label13, label14, label15, label16,
                label17, label18, label19, label20, label21,    
            };

            foreach (Label lbl in labels)
            {
                lbl.ForeColor = colorTexto;
                lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }

            // =========================
            // 🖱 BOTONES
            // =========================
            EstiloBoton(btnGuardar, colorSecundario);
            EstiloBoton(btnEditar, Color.FromArgb(245, 158, 11)); // Naranja
            EstiloBoton(btnEliminar, Color.FromArgb(239, 68, 68));   // Rojo
            EstiloBoton(btnLimpiar, colorGris);
            EstiloBoton(btnBuscar, Color.FromArgb(125, 141, 127));
            EstiloBoton(btnExportar, Color.FromArgb(155, 211, 171));
            EstiloBoton(btnImportar, Color.FromArgb(100, 134, 105));

            EstiloBoton(btnSubirDocumento, Color.FromArgb(209, 234, 215));
            EstiloBoton(btnDescargarDocumento, Color.FromArgb(125, 141, 127));
            EstiloBoton(btnBorrarDocumento, Color.FromArgb(119, 13, 36));

            // =========================
            // 📊 DATAGRIDVIEW
            // =========================
            dgvControl.BackgroundColor = colorFondo;
            dgvControl.BorderStyle = BorderStyle.None;
            dgvControl.EnableHeadersVisualStyles = false;
            dgvControl.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            dgvControl.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvControl.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvControl.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvControl.RowHeadersVisible = false;

            dgvControl.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvControl.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvControl.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvControl.Dock = DockStyle.Fill;

            dgvDocumentos.BackgroundColor = colorFondo;
            dgvDocumentos.BorderStyle = BorderStyle.None;
            dgvDocumentos.EnableHeadersVisualStyles = false;
            dgvDocumentos.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            dgvDocumentos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDocumentos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvDocumentos.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvDocumentos.RowHeadersVisible = false;

            dgvDocumentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDocumentos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDocumentos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDocumentos.Dock = DockStyle.Fill;
        }

        #endregion

        //EXPEDIENTES EN PDF

        public static void SubirDocumentosControl(
            int idControl,
            int anioControl,
            string noExpediente,
            string connectionString)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Todos los archivos|*.*"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            string carpeta = FileManager.ObtenerRutaControl(anioControl, noExpediente);

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                foreach (string archivo in ofd.FileNames)
                {
                    string nombre = Path.GetFileName(archivo);
                    string destino = Path.Combine(carpeta, nombre);

                    File.Copy(archivo, destino, true);

                    string sql = @"
            INSERT INTO DocumentoControl
            (idControl, NombreArchivo, RutaArchivo, Extension)
            VALUES (@id, @nombre, @ruta, @ext)";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@id", idControl);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@ruta", destino);
                        cmd.Parameters.AddWithValue("@ext", Path.GetExtension(nombre));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void PrevisualizarDocumento(string ruta)
        {
            if (!File.Exists(ruta))
            {
                MessageBox.Show("El archivo no existe.");
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = ruta,
                UseShellExecute = true
            });
        }

        public static void DescargarDocumento(string ruta)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                FileName = Path.GetFileName(ruta)
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            File.Copy(ruta, sfd.FileName, true);
        }

        private void CargarDocumentos(int idControl)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
SELECT 
    idDocumento,
    NombreArchivo,
    Extension,
    FechaSubida,
    RutaArchivo
FROM DocumentoControl
WHERE idControl = @id";

                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                da.SelectCommand.Parameters.AddWithValue("@id", idControl);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvDocumentos.DataSource = dt;   // ✅ GRID CORRECTO
                dgvDocumentos.Columns["RutaArchivo"].Visible = false;
            }
        }

        public void Refrescar()
        {
            // 🔄 Recargar tabla principal
            CargarTabla();

            // 🔄 Recargar catálogos (por si agregaron nuevos)
            CargarEstatus();
            CargarSecciones();
            CargarUbicaciones();
            CargarCodUnidAdmin();
            CargarNombUnidAdmin();
            CargarInstitutos();
            CargarClasificacion();

            combosListos = true;
        }

        private void CargarExpedientesBD()
        {
            expedientesBD = new HashSet<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT NumeroExpediente FROM Expediente", con);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    expedientesBD.Add(dr[0].ToString().Trim());
            }
        }
        private void InsertarDatos()
        {
            if (dtValidos.Rows.Count == 0)
            {
                MessageBox.Show("No hay registros válidos.");
                return;
            }

            if (MessageBox.Show(
                $"Se insertarán {dtValidos.Rows.Count} registros.\n¿Desea continuar?",
                "Confirmar",
                MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();

                try
                {
                    foreach (DataRow row in dtValidos.Rows)
                    {
                        SqlCommand cmd = new SqlCommand("SP_InsertarExpediente", con, tx);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NumeroExpediente", row[3]);
                        cmd.Parameters.AddWithValue("@idSeccion", dicSeccion[row[9].ToString().Trim()]);
                        cmd.Parameters.AddWithValue("@idSubSeccion", dicSubSeccion[row[10].ToString().Trim()]);
                        cmd.Parameters.AddWithValue("@idInstitucion", dicInstituto[row[11].ToString().Trim()]);
                        cmd.Parameters.AddWithValue("@idUbicacion", dicUbicacion[row[12].ToString().Trim()]);
                        cmd.Parameters.AddWithValue("@idEstatus", dicEstatus[row[13].ToString().Trim()]);
                        cmd.Parameters.AddWithValue("@idClasificacion", dicClasificacion[row[14].ToString().Trim()]);

                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                    MessageBox.Show("Importación completada con éxito.");
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    MessageBox.Show("Error al insertar: " + ex.Message);
                }
            }
        }


        private void ValidarDatos()
        {
            if (dtImportado == null || dtImportado.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para validar.");
                return;
            }

            if (dtImportado.Columns.Count != 17)
            {
                MessageBox.Show("El archivo no cumple con el formato esperado (17 columnas).");
                return;
            }

            PrepararTablas();
            CargarCatalogos();
            CargarExpedientesBD();

            HashSet<string> expedientesCSV = new HashSet<string>();

            foreach (DataRow row in dtImportado.Rows)
            {
                string error = "";
                string expediente = row[3].ToString().Trim();

                if (string.IsNullOrWhiteSpace(expediente))
                    error += "No. Expediente vacío. ";

                if (!expedientesCSV.Add(expediente))
                    error += "Duplicado en archivo. ";

                if (expedientesBD.Contains(expediente))
                    error += "Ya existe en BD. ";

                if (!dicSeccion.ContainsKey(row[9].ToString().Trim()))
                    error += "Sección inválida. ";

                if (!dicSubSeccion.ContainsKey(row[10].ToString().Trim()))
                    error += "Subsección inválida. ";

                if (!dicInstituto.ContainsKey(row[11].ToString().Trim()))
                    error += "Instituto inválido. ";

                if (!dicUbicacion.ContainsKey(row[12].ToString().Trim()))
                    error += "Ubicación inválida. ";

                if (!dicEstatus.ContainsKey(row[13].ToString().Trim()))
                    error += "Estatus inválido. ";

                if (!dicClasificacion.ContainsKey(row[14].ToString().Trim()))
                    error += "Clasificación inválida. ";

                if (error != "")
                    dtErrores.Rows.Add(row.ItemArray.Concat(new[] { error }).ToArray());
                else
                    dtValidos.Rows.Add(row.ItemArray);
            }

            dgvControl.DataSource = dtErrores.Rows.Count > 0 ? dtErrores : dtValidos;

            if (dtErrores.Rows.Count > 0)
                ExportarErroresCSV();

            MessageBox.Show(
                $"Validación finalizada\n" +
                $"✔ Válidos: {dtValidos.Rows.Count}\n" +
                $"❌ Errores: {dtErrores.Rows.Count}"
            );
        }

        private void MarcarErrores()
        {
            foreach (DataGridViewRow row in dgvControl.Rows)
            {
                if (row.Cells.Count > 17) // columna Error
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
        }

        private void ExportarErroresCSV()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = "Errores_Importacion.csv";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (DataRow row in dtErrores.Rows)
                    sw.WriteLine(string.Join(";", row.ItemArray));
            }
        }

        private void CargarCatalogos()
        {
            dicSeccion = ObtenerDiccionario("Seccion", "Nombre", "idSeccion");
            dicSubSeccion = ObtenerDiccionario("SubSeccion", "Nombre", "idSubSeccion");
            dicInstituto = ObtenerDiccionario("Institucion", "Nombre", "idInstitucion");
            dicUbicacion = ObtenerDiccionario("Ubicacion", "Nombre", "idUbicacion");
            dicEstatus = ObtenerDiccionario("Estatus", "Nombre", "idEstatus");
            dicClasificacion = ObtenerDiccionario("Clasificacion", "Nombre", "idClasificacion");
        }


        private Dictionary<string, int> ObtenerDiccionario(string tabla, string campoTexto, string campoID)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    $"SELECT {campoID}, {campoTexto} FROM {tabla}", con);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    dic[dr[campoTexto].ToString().Trim()] = Convert.ToInt32(dr[campoID]);
            }

            return dic;
        }


        private void PrepararTablas()
        {
            dtValidos = dtImportado.Clone();
            dtErrores = dtImportado.Clone();
            dtErrores.Columns.Add("Error", typeof(string));
        }


        private void CargarTabla()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query =
@"SELECT 
    c.idControl,
    c.anioControl,
    c.CodUniAdm,
    c.nomUniAdm,
    c.noExpediente,
    c.nExpediente,
    c.fApertura,
    c.fCierre,
    c.nForjas,
    c.nLegajos,

    -- 🔹 IDS (OCULTOS)
    c.idSeccion,
    c.idSubSeccion,
    c.idInstituto,
    c.idUbicacion,
    c.idEstatus,
    c.idClasificacion,

    -- 🔹 DESCRIPCIONES (VISIBLES)
    s.claveSeccion        AS claveSeccion,
    ss.claveSubSeccion    AS claveSubSeccion,
    i.claveInstituto      AS claveInstituto,
    u.dUbicacion          AS dUbicacion,
    e.dEstatus            AS dEstatus,
    cl.dClasificacion     AS dClasificacion,

    c.formClasificatoria,
    c.Observaciones
FROM Control c
LEFT JOIN Seccion s        ON c.idSeccion = s.idSeccion
LEFT JOIN SubSeccion ss    ON c.idSubSeccion = ss.idSubSeccion
LEFT JOIN Instituto i      ON c.idInstituto = i.idInstituto
LEFT JOIN Ubicacion u      ON c.idUbicacion = u.idUbicacion
LEFT JOIN Estatus e        ON c.idEstatus = e.idEstatus
LEFT JOIN Clasificacion cl ON c.idClasificacion = cl.idClasificacion
ORDER BY c.idControl DESC";


                    SqlDataAdapter da = new SqlDataAdapter(query, cn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvControl.DataSource = dt;

                    string[] columnasOcultas =
{
    "idSeccion",
    "idSubSeccion",
    "idInstituto",
    "idUbicacion",
    "idEstatus",
    "idClasificacion"
};

                    foreach (string col in columnasOcultas)
                    {
                        if (dgvControl.Columns.Contains(col))
                            dgvControl.Columns[col].Visible = false;
                    }


                    dgvControl.Columns["idControl"].HeaderText = "Identificador";
                    dgvControl.Columns["anioControl"].HeaderText = "Año del Expediente";
                    dgvControl.Columns["CodUniAdm"].HeaderText = "Codigo de Unidad Administrativa";
                    dgvControl.Columns["nomUniAdm"].HeaderText = "Nombre de Unidad Administrativa";
                    dgvControl.Columns["noExpediente"].HeaderText = "Numero de Expediente";
                    dgvControl.Columns["nExpediente"].HeaderText = "Nombre de Expediente";
                    dgvControl.Columns["fApertura"].HeaderText = "Fecha de Apertura";
                    dgvControl.Columns["fCierre"].HeaderText = "Fecha de Cierre";
                    dgvControl.Columns["nForjas"].HeaderText = "Numero de Fojas";
                    dgvControl.Columns["nLegajos"].HeaderText = "Numero de Legajos";
                    dgvControl.Columns["claveSeccion"].HeaderText = "Clave de Seccion";
                    dgvControl.Columns["claveSubSeccion"].HeaderText = "Clave de SubSeccion";
                    dgvControl.Columns["claveInstituto"].HeaderText = "Clave del Instituto";
                    dgvControl.Columns["dUbicacion"].HeaderText = "Ubicacion";
                    dgvControl.Columns["dEstatus"].HeaderText = "Estatus";
                    dgvControl.Columns["dClasificacion"].HeaderText = "Clasificación";
                    dgvControl.Columns["formClasificatoria"].HeaderText = "Formula Clasificactoria";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tabla:\n" + ex.Message);
            }
        }

        private void CargarArchivos()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Control"; // Cambia 'Usuarios' por el nombre real de tu tabla
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvControl.DataSource = dt; // Asignamos la tabla al DataGridView

                    dgvControl.DefaultCellStyle.ForeColor = Color.Black;
                    dgvControl.DefaultCellStyle.BackColor = Color.White;
                    dgvControl.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    dgvControl.EnableHeadersVisualStyles = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private void LlenarComboAÑo()
        {
            cboAño.Items.Clear();

            int anioActual = DateTime.Now.Year;
            int anioInicio = anioActual - 10;  // puedes ajustar este rango
            int anioFin = anioActual + 1;  // por si necesitas capturar el siguiente año

            for (int anio = anioInicio; anio <= anioFin; anio++)
            {
                cboAño.Items.Add(anio);
            }

            // Seleccionar por defecto el año actual (si está en la lista)
            if (cboAño.Items.Contains(anioActual))
            {
                cboAño.SelectedItem = anioActual;
            }
            else if (cboAño.Items.Count > 0)
            {
                cboAño.SelectedIndex = 0;
            }
        }

        private void CargarClasificacion()
        {
            string query = "SELECT idClasificacion, dClasificacion FROM Clasificacion ORDER BY idClasificacion;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);

                    cboClasificacion.DataSource = dt;
                    cboClasificacion.DisplayMember = "dClasificacion";  // lo que se verá
                    cboClasificacion.ValueMember = "idClasificacion";   // el valor real
                    cboClasificacion.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Clasificación: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarSecciones()
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(
                "SELECT idSeccion, claveSeccion FROM Seccion", cn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboSeccion.DisplayMember = "nombreSeccion";
                cboSeccion.ValueMember = "idSeccion";
                cboSeccion.DataSource = dt;
                cboSeccion.SelectedIndex = -1;
            }
        }

        private void CargarSubSeccionesPorSeccion(int idSeccion)
        {
            string query = @"
        SELECT idSubSeccion, claveSubSeccion, dSubSeccion
        FROM SubSeccion
        WHERE idSeccion = @idSeccion
        ORDER BY claveSubSeccion";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@idSeccion", idSeccion);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cboSubSeccion.DataSource = dt;
                cboSubSeccion.DisplayMember = "claveSubSeccion";
                cboSubSeccion.ValueMember = "idSubSeccion";
                cboSubSeccion.SelectedIndex = -1;
            }
        }

        private void CargarUbicaciones()
        {
            string query = "SELECT idUbicacion, dUbicacion FROM Ubicacion ORDER BY idUbicacion;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);

                    cboUbicacion.DataSource = dt;
                    cboUbicacion.DisplayMember = "dUbicacion";  // texto visible
                    cboUbicacion.ValueMember = "idUbicacion";   // valor real
                    cboUbicacion.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Ubicaciones: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void CargarCodUnidAdmin()
        {
            string query = "SELECT idUniAdmin, cUniAdmin FROM UnidadAdministrativa ORDER BY idUniAdmin;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);

                    cboCodUnidAdmin.DataSource = dt;
                    cboCodUnidAdmin.DisplayMember = "cUniAdmin";   // muestra el código 210C...
                    cboCodUnidAdmin.ValueMember = "idUniAdmin";    // valor real
                    cboCodUnidAdmin.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Códigos de Unidad Administrativa: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarNombUnidAdmin()
        {
            string query = "SELECT idUniAdmin, nUniAdmin FROM UnidadAdministrativa ORDER BY idUniAdmin;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);

                    cboNombUniAdmin.DataSource = dt;
                    cboNombUniAdmin.DisplayMember = "nUniAdmin";    // muestra el nombre del área
                    cboNombUniAdmin.ValueMember = "idUniAdmin";      // valor real
                    cboNombUniAdmin.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Nombres de Unidad Administrativa: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarInstitutos()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT idInstituto, claveInstituto FROM Instituto";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboInstituto.DataSource = dt;
                    cboInstituto.DisplayMember = "claveInstituto";   // Lo que se muestra
                    cboInstituto.ValueMember = "idInstituto";        // Lo que se obtiene como ID
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar institutos: " + ex.Message);
            }
        }


        private void CargarEstatus()
        {
            string query = "SELECT idEstatus, dEstatus FROM Estatus ORDER BY idEstatus;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);

                    cboEstatus.DataSource = dt;
                    cboEstatus.DisplayMember = "dEstatus";   // Lo que ve el usuario
                    cboEstatus.ValueMember = "idEstatus";    // Valor real
                    cboEstatus.SelectedIndex = -1;           // Nada seleccionado al iniciar
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Estatus: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private int contadorExpediente = 1;


        private void GenerarFormulaClasificatoria()
        {
            if (cboInstituto.SelectedValue == null ||
                cboSeccion.SelectedValue == null ||
                cboSubSeccion.SelectedValue == null)
                return;

            string claveInstituto = cboInstituto.Text;
            string claveSeccion = cboSeccion.Text;
            string claveSubSeccion = cboSubSeccion.Text;

            string numeroExpediente = "E." + contadorExpediente.ToString("D3");

            txtFormulaClasificatoria.Text =
                $"{claveInstituto}/{claveSeccion}/{claveSubSeccion}/{numeroExpediente}";
        }

        private void cboSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!combosListos) return;
            if (cboSeccion.SelectedValue == null) return;

            int idSeccion = Convert.ToInt32(cboSeccion.SelectedValue);
            CargarSubSeccionesPorSeccion(idSeccion);

            GenerarFormulaClasificatoria();
        }

        private void cboSubSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarFormulaClasificatoria();
        }


        bool bloqueado = false;


        private void cboCodUnidAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!combosListos) return;
            if (bloqueado) return;

            if (cboCodUnidAdmin.SelectedValue == null ||
                cboCodUnidAdmin.SelectedValue is DataRowView)
                return;

            bloqueado = true;
            cboNombUniAdmin.SelectedValue = cboCodUnidAdmin.SelectedValue;
            bloqueado = false;

            GenerarFormulaClasificatoria();

        }

        bool combosListos = false;


        private void cboNombUniAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!combosListos) return;
            if (bloqueado) return;

            if (cboNombUniAdmin.SelectedValue == null ||
                cboNombUniAdmin.SelectedValue is DataRowView)
                return;

            bloqueado = true;
            cboCodUnidAdmin.SelectedValue = cboNombUniAdmin.SelectedValue;
            bloqueado = false;

            GenerarFormulaClasificatoria();

        }

        private void txtFojas_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtFojas.SelectionStart;

            string limpio = Regex.Replace(txtFojas.Text, @"[^0-9]", "");

            if (txtFojas.Text != limpio)
            {
                txtFojas.Text = limpio;
                txtFojas.SelectionStart = Math.Min(cursor, txtFojas.Text.Length);
            }
        }

        private void txtObservaciones_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtObservaciones.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtObservaciones.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtObservaciones.Text != limpio)
            {
                txtObservaciones.Text = limpio;
                txtObservaciones.SelectionStart = Math.Min(cursor, txtObservaciones.Text.Length);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            Insertar();

            CargarTabla();


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

           ModificarControl();

           CargarTabla();


        }

        private void dgvControl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvControl.Rows[e.RowIndex];

            // 🔹 ID
            txtID.Text = row.Cells["idControl"].Value?.ToString();

            // 🔹 AÑO
            cboAño.Text = row.Cells["anioControl"].Value?.ToString();

            // 🔹 UNIDAD ADMINISTRATIVA
            cboCodUnidAdmin.Text = row.Cells["CodUniAdm"].Value?.ToString();
            cboNombUniAdmin.Text = row.Cells["nomUniAdm"].Value?.ToString();

            // 🔹 EXPEDIENTES
            txtnoExpediente.Text = row.Cells["noExpediente"].Value?.ToString();
            txtnExpendiente.Text = row.Cells["nExpediente"].Value?.ToString();

            // 🔹 FECHAS
            if (row.Cells["fApertura"].Value != DBNull.Value)
                dtpfApertura.Value = Convert.ToDateTime(row.Cells["fApertura"].Value);

            if (row.Cells["fCierre"].Value != DBNull.Value)
            {
                dtpfCierre.Value = Convert.ToDateTime(row.Cells["fCierre"].Value);
                dtpfCierre.Checked = true;
            }
            else
            {
                dtpfCierre.Checked = false;
            }

            // 🔹 FOJAS Y LEGAJOS
            txtFojas.Text = row.Cells["nForjas"].Value?.ToString();
            txtLegajos.Text = row.Cells["nLegajos"].Value?.ToString();


            // =====================
            // 🔥 SECCIÓN Y SUBSECCIÓN (CLAVE)
            // =====================

            // 🔹 SECCIÓN
            if (row.Cells["idSeccion"].Value != DBNull.Value)
            {
                int idSeccion = Convert.ToInt32(row.Cells["idSeccion"].Value);

                AsignarComboSeguro(cboSeccion, idSeccion);

                // ⚠️ IMPORTANTE: cargar SubSecciones según la Sección
                CargarSubSecciones(idSeccion);
            }

            // 🔹 SUBSECCIÓN
            if (row.Cells["idSubSeccion"].Value != DBNull.Value)
            {
                int idSubSeccion = Convert.ToInt32(row.Cells["idSubSeccion"].Value);
                AsignarComboSeguro(cboSubSeccion, idSubSeccion);
            }


            // =====================
            // 🔹 OTROS COMBOS
            // =====================

            // UBICACIÓN
            if (row.Cells["idUbicacion"].Value != DBNull.Value)
                AsignarComboSeguro(cboUbicacion, Convert.ToInt32(row.Cells["idUbicacion"].Value));

            // INSTITUTO
            if (row.Cells["idInstituto"].Value != DBNull.Value)
                AsignarComboSeguro(cboInstituto, Convert.ToInt32(row.Cells["idInstituto"].Value));

            // ESTATUS
            if (row.Cells["idEstatus"].Value != DBNull.Value)
                AsignarComboSeguro(cboEstatus, Convert.ToInt32(row.Cells["idEstatus"].Value));

            // CLASIFICACIÓN
            if (row.Cells["idClasificacion"].Value != DBNull.Value)
                AsignarComboSeguro(cboClasificacion, Convert.ToInt32(row.Cells["idClasificacion"].Value));


            // 🔹 FORMULA Y OBSERVACIONES
            txtFormulaClasificatoria.Text = row.Cells["formClasificatoria"].Value?.ToString();
            txtObservaciones.Text = row.Cells["Observaciones"].Value?.ToString();

            // 🔹 DOCUMENTOS
            if (int.TryParse(txtID.Text, out int idControl))
                CargarDocumentos(idControl);


        }

        private void AsignarComboSeguro(ComboBox combo, object valor)
        {
            if (valor == null || valor == DBNull.Value) return;

            try
            {
                combo.SelectedValue = Convert.ToInt32(valor);
            }
            catch
            {
                combo.SelectedIndex = -1; // Si no existe, deselecciona
            }
        }

        private void CargarSubSecciones(int idSeccion)
        {
            cboSubSeccion.DataSource = null;
            cboSubSeccion.Items.Clear();

            string cs = @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";

            using (SqlConnection cn = new SqlConnection(cs))
            {
                cn.Open();

                string query = @"
            SELECT 
                idSubSeccion,
                dSubSeccion
            FROM SubSeccion
            WHERE idSeccion = @idSeccion";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@idSeccion", SqlDbType.Int).Value = idSeccion;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        cboSubSeccion.DataSource = null;
                        return;
                    }

                    cboSubSeccion.DisplayMember = "nombreSubSeccion";
                    cboSubSeccion.ValueMember = "idSubSeccion";
                    cboSubSeccion.DataSource = dt;

                    cboSubSeccion.SelectedIndex = -1;
                }
            }
        }



        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void Eliminar()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona un registro para eliminar.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "¿Estás seguro de eliminar este registro?\nEsta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            string datosAntes = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();

                    // 1️⃣ Datos antes
                    using (SqlCommand cmdSelect = new SqlCommand(
                        "SELECT anioControl, CodUniAdm, noExpediente FROM Control WHERE idControl = @id", cn))
                    {
                        cmdSelect.Parameters.AddWithValue("@id", int.Parse(txtID.Text));

                        using (SqlDataReader dr = cmdSelect.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                datosAntes =
                                    $"Año={dr["anioControl"]}, Unidad={dr["CodUniAdm"]}, Expediente={dr["noExpediente"]}";
                            }
                        }
                    }

                    // 2️⃣ Delete
                    using (SqlCommand cmdDelete = new SqlCommand(
                        "DELETE FROM Control WHERE idControl = @id", cn))
                    {
                        cmdDelete.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                        cmdDelete.ExecuteNonQuery();
                    }
                }

                // 🔴 HISTORIAL (DELETE)
                HistorialHelper.RegistrarCambio(
                    "Control",
                    txtID.Text,
                    "DELETE",
                    datosAntes,
                    null
                );

                MessageBox.Show("Registro eliminado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarTabla();
                LimpiarCampos();
                txtID.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Insertar()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO Control (
                anioControl, CodUniAdm, nomUniAdm, noExpediente, nExpediente,
                fApertura, fCierre, nForjas, nLegajos,
                idSeccion, idSubSeccion, idUbicacion, idInstituto,
                formClasificatoria, Observaciones, idEstatus, idClasificacion
            )
            VALUES (
                @anio, @codAdm, @nomAdm, @noExp, @nExp,
                @fApertura, @fCierre, @forjas, @legajos,
                @seccion, @subSeccion, @ubicacion, @instituto,
                @formClasif, @obs, @estatus, @clasif
            )", cn))
                {
                    // =====================
                    // DATOS NORMALES
                    // =====================
                    cmd.Parameters.AddWithValue("@anio", int.Parse(cboAño.Text));
                    cmd.Parameters.AddWithValue("@codAdm", cboCodUnidAdmin.SelectedValue?.ToString());
                    cmd.Parameters.AddWithValue("@nomAdm", cboNombUniAdmin.Text);
                    cmd.Parameters.AddWithValue("@noExp", txtnoExpediente.Text);
                    cmd.Parameters.AddWithValue("@nExp", txtnExpendiente.Text);
                    cmd.Parameters.AddWithValue("@fApertura", dtpfApertura.Value);
                    cmd.Parameters.AddWithValue("@fCierre",dtpfCierre.Checked ? (object)dtpfCierre.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@forjas", int.Parse(txtFojas.Text));
                    cmd.Parameters.AddWithValue("@legajos", txtLegajos.Text);

                    // =====================
                    // 🔥 FOREIGN KEYS (INT)
                    // =====================
                    cmd.Parameters.AddWithValue("@seccion",
                        cboSeccion.SelectedValue ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@subSeccion",
                        cboSubSeccion.SelectedValue ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@ubicacion",
                        cboUbicacion.SelectedValue ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@instituto",
                        cboInstituto.SelectedValue ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@estatus",
                        cboEstatus.SelectedValue ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@clasif",
                        cboClasificacion.SelectedValue ?? (object)DBNull.Value);

                    // =====================
                    // EXTRA
                    // =====================
                    cmd.Parameters.AddWithValue("@formClasif", txtFormulaClasificatoria.Text);
                    cmd.Parameters.AddWithValue("@obs", txtObservaciones.Text);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                if (cboSeccion.SelectedValue == null ||
     cboSubSeccion.SelectedValue == null ||
     cboUbicacion.SelectedValue == null ||
     cboInstituto.SelectedValue == null ||
     cboEstatus.SelectedValue == null ||
     cboClasificacion.SelectedValue == null)
                {
                    MessageBox.Show("Completa todos los campos obligatorios.");
                    return;
                }



                // =====================
                // 🔴 HISTORIAL (INSERT)
                // =====================
                HistorialHelper.RegistrarCambio(
                    "Control",
                    "NUEVO",
                    "INSERT",
                    null,
                    $"Año={cboAño.Text}, Sección={cboSeccion.Text}, Expediente={txtnoExpediente.Text}"
                );

                MessageBox.Show("Registro guardado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarTabla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ModificarControl()
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

                    // 🔴 OBTENER DATOS ANTERIORES
                    string selectQuery = @"
                SELECT 
                    anioControl, CodUniAdm, nomUniAdm,
                    noExpediente, nExpediente,
                    fApertura, fCierre,
                    nForjas, nLegajos,
                    idSeccion, idSubSeccion,
                    idInstituto, idUbicacion,
                    idEstatus, idClasificacion,
                    formClasificatoria, Observaciones
                FROM Control
                WHERE idControl = @id";

                    SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@id", txtID.Text);

                    using (SqlDataReader dr = selectCmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            datosAnteriores =
                                $"Año={dr["anioControl"]} | " +
                                $"CodUA={dr["CodUniAdm"]} | " +
                                $"NomUA={dr["nomUniAdm"]} | " +
                                $"NoExp={dr["noExpediente"]} | " +
                                $"Exp={dr["nExpediente"]} | " +
                                $"Fojas={dr["nForjas"]} | " +
                                $"Legajos={dr["nLegajos"]} | " +
                                $"Seccion={dr["idSeccion"]} | " +
                                $"SubSeccion={dr["idSubSeccion"]} | " +
                                $"Instituto={dr["idInstituto"]} | " +
                                $"Ubicacion={dr["idUbicacion"]} | " +
                                $"Estatus={dr["idEstatus"]} | " +
                                $"Clasificacion={dr["idClasificacion"]}";
                        }
                    }

                    // 🟡 UPDATE
                    string updateQuery = @"
                UPDATE Control SET
                    anioControl = @anio,
                    CodUniAdm = @codUA,
                    nomUniAdm = @nomUA,
                    noExpediente = @noExp,
                    nExpediente = @nExp,
                    fApertura = @fApertura,
                    fCierre = @fCierre,
                    nForjas = @fojas,
                    nLegajos = @legajos,
                    idSeccion = @idSeccion,
                    idSubSeccion = @idSubSeccion,
                    idInstituto = @idInstituto,
                    idUbicacion = @idUbicacion,
                    idEstatus = @idEstatus,
                    idClasificacion = @idClasificacion,
                    formClasificatoria = @formula,
                    Observaciones = @obs
                WHERE idControl = @id";

                    SqlCommand cmd = new SqlCommand(updateQuery, conn);

                    cmd.Parameters.AddWithValue("@anio", cboAño.Text);
                    cmd.Parameters.AddWithValue("@codUA", cboCodUnidAdmin.Text);
                    cmd.Parameters.AddWithValue("@nomUA", cboNombUniAdmin.Text);
                    cmd.Parameters.AddWithValue("@noExp", txtnoExpediente.Text);
                    cmd.Parameters.AddWithValue("@nExp", txtnExpendiente.Text);

                    cmd.Parameters.AddWithValue("@fApertura", dtpfApertura.Value);

                    cmd.Parameters.AddWithValue("@fCierre",
                        dtpfCierre.Checked ? (object)dtpfCierre.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@fojas",
                        string.IsNullOrWhiteSpace(txtFojas.Text) ? 0 : int.Parse(txtFojas.Text));

                    cmd.Parameters.AddWithValue("@legajos", txtLegajos.Text);

                    cmd.Parameters.AddWithValue("@idSeccion", cboSeccion.SelectedValue);
                    cmd.Parameters.AddWithValue("@idSubSeccion", cboSubSeccion.SelectedValue);
                    cmd.Parameters.AddWithValue("@idInstituto", cboInstituto.SelectedValue);
                    cmd.Parameters.AddWithValue("@idUbicacion", cboUbicacion.SelectedValue);
                    cmd.Parameters.AddWithValue("@idEstatus", cboEstatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@idClasificacion", cboClasificacion.SelectedValue);

                    cmd.Parameters.AddWithValue("@formula", txtFormulaClasificatoria.Text);
                    cmd.Parameters.AddWithValue("@obs", txtObservaciones.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);

                    cmd.ExecuteNonQuery();
                }

                // 🔴 HISTORIAL (UPDATE)
                string datosNuevos = ObtenerDatosControl();

                HistorialHelper.RegistrarCambio(
                    "Control",
                    txtID.Text,
                    "UPDATE",
                    datosAnteriores,
                    datosNuevos
                );

                MessageBox.Show("✅ Registro actualizado correctamente.");
                CargarTabla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar:\n" + ex.Message);
            }
        }

        private string ObtenerDatosControl()
        {
            return
                $"Año={cboAño.Text} | " +
                $"CodUA={cboCodUnidAdmin.Text} | " +
                $"NomUA={cboNombUniAdmin.Text} | " +
                $"NoExp={txtnoExpediente.Text} | " +
                $"Exp={txtnExpendiente.Text} | " +
                $"Fojas={txtFojas.Text} | " +
                $"Legajos={txtLegajos.Text} | " +
                $"Seccion={cboSeccion.Text} | " +
                $"SubSeccion={cboSubSeccion.Text} | " +
                $"Instituto={cboInstituto.Text} | " +
                $"Ubicacion={cboUbicacion.Text} | " +
                $"Estatus={cboEstatus.Text} | " +
                $"Clasificacion={cboClasificacion.Text}";
        }



        private void LimpiarCampos()
        {
            txtnoExpediente.Clear();
            txtnExpendiente.Clear();
            txtFojas.Clear();
            txtLegajos.Clear();
            txtObservaciones.Clear();
            txtFormulaClasificatoria.Clear();

            cboAño.SelectedIndex = -1;
            cboSeccion.SelectedIndex = -1;
            cboSubSeccion.SelectedIndex = -1;
            cboUbicacion.SelectedIndex = -1;
            cboCodUnidAdmin.SelectedIndex = -1;
            cboNombUniAdmin.SelectedIndex = -1;
            cboEstatus.SelectedIndex = -1;
            cboClasificacion.SelectedIndex = -1;

            dtpfApertura.Value = DateTime.Now;
            dtpfCierre.Value = DateTime.Now;
        }

        



        private void ExportarCSV()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos CSV (*.csv)|*.csv";
            sfd.FileName = "Control.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();

                // Encabezados
                foreach (DataGridViewColumn col in dgvControl.Columns)
                    sb.Append(col.HeaderText + ";");

                sb.AppendLine();

                // Filas
                foreach (DataGridViewRow row in dgvControl.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                            sb.Append(cell.Value + ";");

                        sb.AppendLine();
                    }
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("✅ Archivo CSV generado correctamente.");
            }
        }

        private void txtnoExpediente_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtnoExpediente.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtnoExpediente.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtnoExpediente.Text != limpio)
            {
                txtnoExpediente.Text = limpio;
                txtnoExpediente.SelectionStart = Math.Min(cursor, txtnoExpediente.Text.Length);
            }
        }

        private void txtnExpendiente_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtnoExpediente.SelectionStart;

            // 1️⃣ Eliminar caracteres especiales (permitir letras, números, acentos y espacios)
            string limpio = Regex.Replace(
                txtnoExpediente.Text,
                @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]",
                ""
            );

            // 2️⃣ Reemplazar múltiples espacios por uno solo
            limpio = Regex.Replace(limpio, @"\s{2,}", " ");

            // 3️⃣ Evitar espacios al inicio
            limpio = limpio.TrimStart();

            // 4️⃣ Aplicar cambios solo si hay diferencia
            if (txtnoExpediente.Text != limpio)
            {
                txtnoExpediente.Text = limpio;
                txtnoExpediente.SelectionStart = Math.Min(cursor, txtnoExpediente.Text.Length);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarCSV();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos CSV|*.csv";
                ofd.Title = "Selecciona el archivo CSV";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    CSVHelper.ImportarCSV(ofd.FileName);
                }
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            ValidarDatos();

            MessageBox.Show(
                $"Validación terminada:\n" +
                $"✔ Válidos: {dtValidos.Rows.Count}\n" +
                $"❌ Errores: {dtErrores.Rows.Count}"
            );

            dgvControl.DataSource = dtErrores.Rows.Count > 0 ? dtErrores : dtValidos;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            InsertarDatos();
        }

        private void CArchivos_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void txtLegajos_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir teclas de control (Backspace, Delete, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Permitir SOLO números
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // bloquea todo lo que no sea número
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void SubirDocumento()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona un expediente primero.");
                return;
            }

            // 🔐 SOLO ADMIN (AJUSTA EL ID)
            if (SessionData.IdTipoUsuario != 1)
            {
                MessageBox.Show("No tienes permisos para subir documentos.");
                return;
            }

            int idControl = int.Parse(txtID.Text);
            int anio = int.Parse(cboAño.Text);
            string noExpediente = txtnoExpediente.Text;

            SubirDocumentosControl(idControl, anio, noExpediente, connectionString);

            MessageBox.Show("Documento(s) subido(s) correctamente.");
        }

        private void btnSubirDocumento_Click(object sender, EventArgs e)
        {
            SubirDocumento();
        }

        private void dgvDocumentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string ruta = dgvDocumentos.Rows[e.RowIndex]
                .Cells["RutaArchivo"].Value?.ToString();

            if (string.IsNullOrEmpty(ruta) || !File.Exists(ruta))
            {
                MessageBox.Show("El archivo no existe o fue movido.");
                return;
            }

            PrevisualizarDocumento(ruta);
        }

        private void DescargarDocumento()
        {
            // 🔐 VALIDACIÓN DE PERMISOS
            if (SessionData.IdTipoUsuario != 1) // 1 = ADMIN
            {
                MessageBox.Show(
                    "Solo los administradores pueden descargar documentos.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // ✅ VALIDACIÓN DE SELECCIÓN
            if (dgvDocumentos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un documento.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rutaOrigen = dgvDocumentos.CurrentRow
                .Cells["RutaArchivo"].Value?.ToString();

            string nombreArchivo = dgvDocumentos.CurrentRow
                .Cells["NombreArchivo"].Value?.ToString();

            if (string.IsNullOrEmpty(rutaOrigen) || !File.Exists(rutaOrigen))
            {
                MessageBox.Show("El archivo no existe en el sistema.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.FileName = nombreArchivo;
            save.Filter = "Todos los archivos (*.*)|*.*";

            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Copy(rutaOrigen, save.FileName, true);

                    MessageBox.Show("Documento descargado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al descargar:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDescargarDocumento_Click(object sender, EventArgs e)
        {

            DescargarDocumento();

        }

        private void cboSeccion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboSeccion.SelectedValue == null)
                return;

            if (cboSeccion.SelectedValue is DataRowView)
                return;

            int idSeccion = Convert.ToInt32(cboSeccion.SelectedValue);
            CargarSubSecciones(idSeccion);

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void CArchivos_Load_1(object sender, EventArgs e)
        {

        }

        private void cboAño_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void EliminarDocumentoBD(int idDocumento)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM DocumentoControl WHERE idDocumento = @idDocumento", cn))
            {
                cmd.Parameters.Add("@idDocumento", SqlDbType.Int).Value = idDocumento;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void BorrarDocumento()
        {
            // 🔐 SOLO ADMIN
            if (SessionData.IdTipoUsuario != 1)
            {
                MessageBox.Show("No tienes permisos para eliminar documentos.",
                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ VALIDAR SELECCIÓN REAL
            if (dgvDocumentos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un documento.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila = dgvDocumentos.SelectedRows[0];

            int idDocumento = Convert.ToInt32(fila.Cells["idDocumento"].Value);
            string rutaArchivo = fila.Cells["RutaArchivo"].Value?.ToString();
            string nombreArchivo = fila.Cells["NombreArchivo"].Value?.ToString();

            DialogResult r = MessageBox.Show(
                $"¿Desea eliminar el documento?\n\n{nombreArchivo}",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (r != DialogResult.Yes) return;

            try
            {
                // 🗑️ BORRAR ARCHIVO
                if (!string.IsNullOrEmpty(rutaArchivo) && File.Exists(rutaArchivo))
                {
                    File.Delete(rutaArchivo);
                }

                // 🗑️ BORRAR BD
                EliminarDocumentoBD(idDocumento);

                MessageBox.Show("Documento eliminado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔄 RECARGAR
                CargarDocumentos(int.Parse(txtID.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrarDocumento_Click(object sender, EventArgs e)
        {
            BorrarDocumento();
        }
    }
    }

    

