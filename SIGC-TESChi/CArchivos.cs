using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SIGC_TESChi
{
    public partial class CArchivos : UserControl
    {

        // 🔹 Cadena de conexión (ajústala si tu instancia/localdb es diferente)
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=DBCONTRALORIA;Trusted_Connection=True;";


        public CArchivos()
        {
            InitializeComponent();


            dgvControl.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvControl.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvControl.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvControl.Dock = DockStyle.Fill;


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

    -- IDS PARA LOS COMBOS
    c.idSeccion,
    c.idSubSeccion,
    c.idUbicacion,
    c.idInstituto,
    c.idEstatus,
    c.idClasificacion,

    -- DESCRIPCIONES
    s.dSeccion AS Seccion,
    ss.dSubSeccion AS SubSeccion,
    u.dUbicacion AS Ubicacion,
    i.dInstituto AS Instituto,
    e.dEstatus AS Estatus,
    cl.dClasificacion AS Clasificacion,

    c.formClasificatoria,
    c.Observaciones
FROM Control c
LEFT JOIN Seccion s ON c.idSeccion = s.idSeccion
LEFT JOIN SubSeccion ss ON c.idSubSeccion = ss.idSubSeccion
LEFT JOIN Ubicacion u ON c.idUbicacion = u.idUbicacion
LEFT JOIN Instituto i ON c.idInstituto = i.idInstituto
LEFT JOIN Estatus e ON c.idEstatus = e.idEstatus
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
            string query = "SELECT idSeccion, dSeccion FROM Seccion ORDER BY idSeccion;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);

                    cboSeccion.DataSource = dt;
                    cboSeccion.DisplayMember = "idSeccion";  // muestra 2C, 3C, 4C
                    cboSeccion.ValueMember = "idSeccion";
                    cboSeccion.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Secciones: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void CargarSubSeccionesPorSeccion(string idSeccion)
        {
            if (string.IsNullOrWhiteSpace(idSeccion)) return;

            string query = @"
        SELECT idSubSeccion, dSubSeccion
        FROM SubSeccion
        WHERE idSubSeccion LIKE @prefijo + '%'
        ORDER BY idSubSeccion;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@prefijo", idSeccion + ".");
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);

                    cboSubSeccion.DataSource = dt;
                    cboSubSeccion.DisplayMember = "idSubSeccion";
                    cboSubSeccion.ValueMember = "idSubSeccion";
                    cboSubSeccion.SelectedIndex = -1;

                    if (dt.Rows.Count == 0)
                    {
                        // Mensaje más claro — muestra el id real (string), no el DataRowView
                        MessageBox.Show($"No se encontraron subsecciones para {idSeccion}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar SubSecciones: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            if (cboInstituto.SelectedItem == null ||
                cboSeccion.SelectedItem == null ||
                cboSubSeccion.SelectedItem == null)
                return;

            string claveInstituto = ((DataRowView)cboInstituto.SelectedItem)["ClaveInstituto"].ToString();
            string idSeccion = ((DataRowView)cboSeccion.SelectedItem)["IDSeccion"].ToString();
            string idSubSeccion = ((DataRowView)cboSubSeccion.SelectedItem)["IDSubSeccion"].ToString();

            // Incrementamos el número de expediente
            string numeroExpediente = "E." + contadorExpediente.ToString();

            txtFormulaClasificatoria.Text = $"{claveInstituto}/{idSeccion}/{idSubSeccion}/{numeroExpediente}";
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
            if (!combosListos) return;            // evita que se ejecute durante la carga
            if (cboSeccion.SelectedValue == null) return;

            // Obtén el idSeccion robustamente, aceptando DataRowView o valor directo
            object val = cboSeccion.SelectedValue;
            string idSeccion;

            if (val is DataRowView drv)
            {
                // intenta primero la columna idSeccion, si no existe toma la primera columna
                if (drv.DataView.Table.Columns.Contains("idSeccion"))
                    idSeccion = drv["idSeccion"]?.ToString();
                else
                    idSeccion = drv[0]?.ToString();
            }
            else
            {
                idSeccion = val.ToString();
            }

            if (string.IsNullOrWhiteSpace(idSeccion)) return;

            CargarSubSeccionesPorSeccion(idSeccion);
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

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO Control (
                anioControl, CodUniAdm, nomUniAdm, noExpediente, nExpediente,
                fApertura, fCierre, nForjas, nLegajos, idSeccion, idSubSeccion,
                idUbicacion, idInstituto, formClasificatoria, Observaciones,
                idEstatus, idClasificacion
            )
            VALUES (
                @anio, @codAdm, @nomAdm, @noExp, @nExp,
                @fApertura, @fCierre, @forjas, @legajos, @seccion, @subSeccion,
                @ubicacion, @instituto, @formClasif, @obs,
                @estatus, @clasif)", cn))
                {
                    cmd.Parameters.AddWithValue("@anio", int.Parse(cboAño.Text));
                    cmd.Parameters.AddWithValue("@codAdm", cboCodUnidAdmin.SelectedValue);
                    cmd.Parameters.AddWithValue("@nomAdm", cboNombUniAdmin.Text);
                    cmd.Parameters.AddWithValue("@noExp", txtnExpediente.Text);
                    cmd.Parameters.AddWithValue("@nExp", txtnExpendiente.Text);
                    cmd.Parameters.AddWithValue("@fApertura", dtpfApertura.Value);
                    cmd.Parameters.AddWithValue("@fCierre", dtpfCierre.Value);
                    cmd.Parameters.AddWithValue("@forjas", int.Parse(txtFojas.Text));
                    cmd.Parameters.AddWithValue("@legajos", txtLegajos.Text);

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

                    cmd.Parameters.AddWithValue("@formClasif", txtFormulaClasificatoria.Text);
                    cmd.Parameters.AddWithValue("@obs", txtObservaciones.Text);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Registro guardado correctamente.");
                CargarTabla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message);
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

            dtpfApertura.Value = DateTime.Now;
            dtpfCierre.Value = DateTime.Now;
        }

        private void cboInstituto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvControl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Selecciona un registro para editar.");
                return;
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
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
                    cmd.Parameters.AddWithValue("@codAdm", cboCodUnidAdmin.SelectedValue?.ToString());
                    cmd.Parameters.AddWithValue("@nomAdm", cboNombUniAdmin.Text);
                    cmd.Parameters.AddWithValue("@noExp", txtnExpediente.Text);
                    cmd.Parameters.AddWithValue("@nExp", txtnExpendiente.Text);
                    cmd.Parameters.AddWithValue("@fApertura", dtpfApertura.Value);
                    cmd.Parameters.AddWithValue("@fCierre", dtpfCierre.Value);
                    cmd.Parameters.AddWithValue("@forjas", int.Parse(txtFojas.Text));
                    cmd.Parameters.AddWithValue("@legajos", txtLegajos.Text);

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

                    cmd.Parameters.AddWithValue("@formClasif", txtFormulaClasificatoria.Text);
                    cmd.Parameters.AddWithValue("@obs", txtObservaciones.Text);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Registro actualizado correctamente.");
                CargarTabla();
                //LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar:\n" + ex.Message);
            }

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
    }

    }

