using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

        HashSet<string> expedientesBD;

        HashSet<string> expedientesCSV = new HashSet<string>();

        int totalRegistros = 0;
        int validos = 0;
        int errores = 0;
        int duplicadosCSV = 0;
        int duplicadosBD = 0;

        public CArchivos()
        {
                InitializeComponent();


            //dgvControl.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgvControl.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvControl.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvControl.Dock = DockStyle.Fill;


            Load += CArchivos_Load;

            this.cboSubSeccion.SelectedIndexChanged += new System.EventHandler(this.cboSubSeccion_SelectedIndexChanged);

        }

        private void CArchivos_Load(object sender, EventArgs e)
        {
            CargarArchivos();
            LlenarComboAÑo();

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

        }

        



        private void InicializarTablas()
        {
            dtValidos = new DataTable();
            dtErrores = new DataTable();

            foreach (DataGridViewColumn col in dgvControl.Columns)
            {
                dtValidos.Columns.Add(col.HeaderText);
                dtErrores.Columns.Add(col.HeaderText);
            }

            dtErrores.Columns.Add("MotivoError");
        }


        private void InicializarContadores()
        {
            totalRegistros = validos = errores = duplicadosCSV = duplicadosBD = 0;
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



        private void MostrarResumen()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("📊 RESUMEN DE IMPORTACIÓN\n");
            sb.AppendLine($"📄 Total de registros leídos: {totalRegistros}");
            sb.AppendLine($"✅ Registros válidos: {validos}");
            sb.AppendLine($"❌ Registros con error: {errores}");
            sb.AppendLine($"🚫 Duplicados en archivo: {duplicadosCSV}");
            sb.AppendLine($"🚫 Duplicados en base de datos: {duplicadosBD}");

            if (errores > 0)
                sb.AppendLine("\n📁 Se generó un archivo CSV con los errores.");

            MessageBox.Show(
                sb.ToString(),
                "Resultado de la importación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
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




        private void ImportarCSV()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos CSV (*.csv)|*.csv";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            dtImportado = new DataTable();

            for (int i = 0; i < 17; i++)
                dtImportado.Columns.Add("C" + i);



            using (StreamReader sr = new StreamReader(ofd.FileName, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string[] fila = sr.ReadLine().Split(new char[] { ',', ';', '\t' , ' ' });

                    if (fila.Length == 17)
                        dtImportado.Rows.Add(fila);
                }
            }

            MessageBox.Show("Filas cargadas: " + dtImportado.Rows.Count);

            dgvControl.DataSource = dtImportado;
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

    -- SUSTITUCIÓN DE IDS (MISMO ORDEN)
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
            int anioInicio = anioActual - 5;  // puedes ajustar este rango
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

                cboSeccion.DataSource = dt;
                cboSeccion.DisplayMember = "claveSeccion"; // lo que VE el usuario (2C)
                cboSeccion.ValueMember = "idSeccion";      // lo que VA a SQL (INT)
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






        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CArchivos_Load_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cboAño_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

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


        private void cboUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtFormulaClasificatoria_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtFojas_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtObservaciones_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            Insertar();

        }

        private void cboInstituto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvControl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

           Editar();

        }

        private void dgvControl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvControl.Rows[e.RowIndex];

            // ID
            txtID.Text = row.Cells["idControl"].Value.ToString();

            // AÑO  
            cboAño.Text = row.Cells["anioControl"].Value.ToString();

            // CODIGO Y NOMBRE UNIDAD ADMINISTRATIVA  
            cboCodUnidAdmin.SelectedValue = row.Cells["CodUniAdm"].Value;
            cboNombUniAdmin.Text = row.Cells["nomUniAdm"].Value.ToString();

            // EXPEDIENTES  
            txtnExpediente.Text = row.Cells["noExpediente"].Value.ToString();
            txtnExpendiente.Text = row.Cells["nExpediente"].Value.ToString();

            // FECHAS
            dtpfApertura.Value = Convert.ToDateTime(row.Cells["fApertura"].Value);
            dtpfCierre.Value = Convert.ToDateTime(row.Cells["fCierre"].Value);

            // FOJAS Y LEGAJOS  
            txtFojas.Text = row.Cells["nForjas"].Value.ToString();
            txtLegajos.Text = row.Cells["nLegajos"].Value.ToString();

            // SECCIÓN
            if (row.Cells["idSeccion"].Value != DBNull.Value)
                cboSeccion.SelectedValue = row.Cells["idSeccion"].Value;


            // SUBSECCIÓN
            if (row.Cells["idSubSeccion"].Value != DBNull.Value)
                cboSubSeccion.SelectedValue = row.Cells["idSubSeccion"].Value;

            // UBICACIÓN
            if (row.Cells["idUbicacion"].Value != DBNull.Value)
                cboUbicacion.SelectedValue = row.Cells["idUbicacion"].Value;

            // INSTITUTO
            if (row.Cells["idInstituto"].Value != DBNull.Value)
                cboInstituto.SelectedValue = row.Cells["idInstituto"].Value;

            // ESTATUS
            if (row.Cells["idEstatus"].Value != DBNull.Value)
                cboEstatus.SelectedValue = row.Cells["idEstatus"].Value;

            // CLASIFICACIÓN
            if (row.Cells["idClasificacion"].Value != DBNull.Value)
                cboClasificacion.SelectedValue = row.Cells["idClasificacion"].Value;

            // FORMULA Y OBSERVACIONES
            txtFormulaClasificatoria.Text = row.Cells["formClasificatoria"].Value.ToString();
            txtObservaciones.Text = row.Cells["Observaciones"].Value.ToString();

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
                    cmd.Parameters.AddWithValue("@noExp", txtnExpediente.Text);
                    cmd.Parameters.AddWithValue("@nExp", txtnExpendiente.Text);
                    cmd.Parameters.AddWithValue("@fApertura", dtpfApertura.Value);
                    cmd.Parameters.AddWithValue("@fCierre", dtpfCierre.Value);
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

                // =====================
                // 🔴 HISTORIAL (INSERT)
                // =====================
                HistorialHelper.RegistrarCambio(
                    "Control",
                    "NUEVO",
                    "INSERT",
                    null,
                    $"Año={cboAño.Text}, Sección={cboSeccion.Text}, Expediente={txtnExpediente.Text}"
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




        private void Editar()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona un registro para editar.");
                return;
            }

            string datosAntes = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();

                    // 1️⃣ Obtener datos antes
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

                    // 2️⃣ Update
                    using (SqlCommand cmd = new SqlCommand(
                        @"UPDATE Control SET
                    anioControl = @anio,
                    CodUniAdm = @codAdm,
                    nomUniAdm = @nomAdm,
                    noExpediente = @noExp,
                    nExpediente = @nExp,
                    fApertura = @fApertura,
                    fCierre = @fCierre,
                    nForjas = @forjas,
                    nLegajos = @legajos,
                    idSeccion = @seccion,
                    idSubSeccion = @subSeccion,
                    idUbicacion = @ubicacion,
                    idInstituto = @instituto,
                    formClasificatoria = @formClasif,
                    Observaciones = @obs,
                    idEstatus = @estatus,
                    idClasificacion = @clasif
                WHERE idControl = @id", cn))
                    {
                        cmd.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                        cmd.Parameters.AddWithValue("@anio", int.Parse(cboAño.Text));
                        cmd.Parameters.AddWithValue("@codAdm", cboCodUnidAdmin.SelectedValue);
                        cmd.Parameters.AddWithValue("@nomAdm", cboNombUniAdmin.Text);
                        cmd.Parameters.AddWithValue("@noExp", txtnExpediente.Text);
                        cmd.Parameters.AddWithValue("@nExp", txtnExpendiente.Text);
                        cmd.Parameters.AddWithValue("@fApertura", dtpfApertura.Value);
                        cmd.Parameters.AddWithValue("@fCierre", dtpfCierre.Value);
                        cmd.Parameters.AddWithValue("@forjas", int.Parse(txtFojas.Text));
                        cmd.Parameters.AddWithValue("@legajos", txtLegajos.Text);
                        cmd.Parameters.AddWithValue("@seccion", cboSeccion.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@subSeccion", cboSubSeccion.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ubicacion", cboUbicacion.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@instituto", cboInstituto.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@estatus", cboEstatus.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@clasif", cboClasificacion.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@formClasif", txtFormulaClasificatoria.Text);
                        cmd.Parameters.AddWithValue("@obs", txtObservaciones.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                // 🔴 HISTORIAL (UPDATE)
                HistorialHelper.RegistrarCambio(
                    "Control",
                    txtID.Text,
                    "UPDATE",
                    datosAntes,
                    $"Año={cboAño.Text}, Unidad={cboCodUnidAdmin.Text}, Expediente={txtnExpediente.Text}"
                );

                MessageBox.Show("Registro actualizado correctamente.");
                CargarTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar:\n" + ex.Message);
            }
        }


        private void LimpiarCampos()
        {
            txtnExpediente.Clear();
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


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void txtnExpediente_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtnExpendiente_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboClasificacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboAño_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtLegajos_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarCSV();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            ImportarCSV();
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
    }
    }

    

