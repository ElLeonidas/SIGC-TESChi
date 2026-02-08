using System;
using System.Windows.Forms;
using ToolTip = System.Windows.Forms.ToolTip;

namespace SIGC_TESChi
{
    public partial class Menu : Form
    {
        private ToolTip toolTip;

        // 🔹 UserControls persistentes
        private Lobby ucLobby;
        private RUsuarios ucRUsuarios;
        private CArchivos ucCArchivos;
        private Secciones ucSecciones;
        private Ubicaciones ucUbicaciones;
        private Graficas ucGraficas;
        private TipoUsuarios ucTipoUsuarios;
        private SubSecciones ucSubSecciones;
        private CaratulaExpediente ucCaratula;
        private Historial ucHistorial;
        private Institucion ucInstitucion;
        private UnidadA ucUnidadA;
        private Clasificacion ucClasificacion;
        private Info ucInfo;

        private System.Windows.Forms.Timer inactivityTimer;
        private System.Windows.Forms.Timer warningTimer;
        private int tiempoInactividad = 5 * 60 * 1000; // 5 minutos
        private int tiempoAviso = 30 * 1000; // 30 segundos de aviso
        private bool avisoMostrado = false;

        private Info info;

        public Menu()
        {
            InitializeComponent();

            InicializarTimerInactividad();

            InicializarUserControls();   // 🔴 CLAVE
            InicializarToolTips();

            FontManager.AplicarFuente(this);

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.KeyPreview = true; 


        }

        private void InicializarUserControls()
        {
            ucLobby = new Lobby();
            ucRUsuarios = new RUsuarios();
            ucCArchivos = new CArchivos();
            ucSecciones = new Secciones();
            ucUbicaciones = new Ubicaciones();
            ucGraficas = new Graficas();
            ucTipoUsuarios = new TipoUsuarios();
            ucSubSecciones = new SubSecciones();
            ucCaratula = new CaratulaExpediente();
            ucHistorial = new Historial();
            ucInstitucion = new Institucion();
            ucUnidadA = new UnidadA();
            ucClasificacion = new Clasificacion();
            ucInfo = new Info();

            // Evento modo oscuro
            ucInfo.ModoOscuroCambiado += Info_ModoOscuroCambiado;
            ucInfo.FuenteCambiada += Info_FuenteCambiada;
            ucInfo.TamanoCambiado += Info_TamanoCambiado;

            // Agregar todos al panel
            AgregarUC(ucLobby);
            AgregarUC(ucRUsuarios);
            AgregarUC(ucCArchivos);
            AgregarUC(ucSecciones);
            AgregarUC(ucUbicaciones);
            AgregarUC(ucGraficas);
            AgregarUC(ucTipoUsuarios);
            AgregarUC(ucSubSecciones);
            AgregarUC(ucCaratula);
            AgregarUC(ucHistorial);
            AgregarUC(ucInstitucion);
            AgregarUC(ucUnidadA);
            AgregarUC(ucClasificacion);
            AgregarUC(ucInfo);

            MostrarUC(ucLobby); // Vista inicial
        }

        //BACKUP

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                BackupManager.EjecutarBackupCompleto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "El sistema se cerró, pero ocurrió un problema al respaldar:\n" + ex.Message,
                    "Respaldo automático",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }


        private void InicializarTimerInactividad()
        {
            inactivityTimer = new System.Windows.Forms.Timer();
            inactivityTimer.Interval = tiempoInactividad;
            inactivityTimer.Tick += Inactividad_Tick;
            inactivityTimer.Start();

            warningTimer = new System.Windows.Forms.Timer();
            warningTimer.Interval = tiempoAviso;
            warningTimer.Tick += Warning_Tick;

            // Detectar interacción en el form principal
            this.MouseMove += ResetTimer;
            this.KeyPress += ResetTimer;
            this.MouseClick += ResetTimer;

            // Detectar interacción en todos los UserControls
            InicializarEventosUserControls(this);
        }


        private void ResetTimer(object sender, EventArgs e)
        {
            // Si se había mostrado el aviso, lo cancelamos
            if (avisoMostrado)
            {
                warningTimer.Stop();
                avisoMostrado = false;
            }

            inactivityTimer.Stop();
            inactivityTimer.Start();
        }


        private void Inactividad_Tick(object sender, EventArgs e)
        {
            inactivityTimer.Stop();
            avisoMostrado = true;
            warningTimer.Start();

            DialogResult result = MessageBox.Show(
                "No se ha detectado actividad. La sesión se cerrará en 30 segundos.\n\n¿Deseas continuar activo?",
                "Aviso de inactividad",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                ResetTimer(this, EventArgs.Empty);
            }
        }

        private void Warning_Tick(object sender, EventArgs e)
        {
            warningTimer.Stop();

            // Si el aviso ya se mostró y el usuario no interactuó, cerramos sesión
            if (avisoMostrado)
            {
                MessageBox.Show(
                    "La sesión se ha cerrado por inactividad.",
                    "Inactividad",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                FrmLogin loginForm = new FrmLogin();
                loginForm.Show();
                this.Hide();
            }
        }


        private void InicializarEventosUserControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                c.MouseMove += ResetTimer;
                c.KeyPress += ResetTimer;
                c.MouseClick += ResetTimer;

                if (c.HasChildren)
                    InicializarEventosUserControls(c); // Recursivo
            }
        }


        private bool TienePermiso(UserControl uc)
        {
            int rol = SessionData.IdTipoUsuario;

            // ADMINISTRADOR → TODO
            if (rol == 1)
                return true;

            // LICENCIADA
            if (rol == 2)
            {
                if (uc == ucRUsuarios ||
                    uc == ucTipoUsuarios)
                    return false;

                return true;
            }

            // SERVICIO
            if (rol == 3)
            {
                if (uc == ucRUsuarios ||
                    uc == ucSecciones ||
                    uc == ucUbicaciones ||
                    uc == ucSubSecciones ||
                    uc == ucHistorial ||
                    uc == ucInstitucion ||
                    uc == ucUnidadA ||
                    uc == ucClasificacion)
                    return false;

                return true;
            }

            return false;
        }


        private void Menu_Load(object sender, EventArgs e)
        {

            switch (SessionData.IdTipoUsuario)
            {
                case 1: // Administrador
                        // Acceso total
                    break;

                case 2: // Licenciada
                    btnRUsuarios.Enabled = false;
                    btnTipoUsuarios.Enabled = false;
                    break;

                case 3: // Servicio
                    btnRUsuarios.Enabled = false;
                    btnSecciones.Enabled = false;
                    btnUbicaciones.Enabled = false;
                    btnSubSecciones.Enabled = false;
                    btnHistorial.Enabled = false;
                    btnInstitucion.Enabled = false;
                    btnUnidadA.Enabled = false;
                    btnClasificacion.Enabled = false;

                    break;
            }
        }

        // =========================
        // 🔹 GESTIÓN DE USERCONTROL
        // =========================

        private void AgregarUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            uc.Visible = false;
            Categorias.Controls.Add(uc);
        }

        private void OcultarTodos()
        {
            foreach (Control c in Categorias.Controls)
                c.Visible = false;
        }

        private void MostrarUC(UserControl uc)
        {
            if (uc == null) return;

            if (!TienePermiso(uc))
            {
                MessageBox.Show(
                    "No tienes permisos para acceder a este módulo",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (uc is CArchivos ca)
                ca.Refrescar();


            OcultarTodos();
            uc.Visible = true;
        }


        // =========================
        // 🌙 MODO OSCURO
        // =========================

        private void Info_ModoOscuroCambiado(bool activar)
        {
            if (activar)
                ThemeManager.AplicarModoOscuro(this);
            else
                ThemeManager.AplicarModoClaro(this);
        }

        // =========================
        // 🔤 CAMBIO DE FUENTE
        // =========================
        private void Info_FuenteCambiada(string fuente)
        {
            FontManager.CambiarFuente(fuente);
            FontManager.AplicarFuente(this);
        }

        private void Info_TamanoCambiado(string tamano)
        {
            FontManager.CambiarModoTamano(tamano);
            FontManager.AplicarFuente(this);
        }


        // =========================
        // 🔘 BOTONES
        // =========================

        private void btnLobby_Click(object sender, EventArgs e) => MostrarUC(ucLobby);
        private void btnRUsuarios_Click(object sender, EventArgs e) => MostrarUC(ucRUsuarios);
        private void btnCArchivos_Click(object sender, EventArgs e) => MostrarUC(ucCArchivos);
        private void btnSecciones_Click(object sender, EventArgs e) => MostrarUC(ucSecciones);
        private void btnUbicaciones_Click(object sender, EventArgs e) => MostrarUC(ucUbicaciones);
        private void btnEstadistica_Click(object sender, EventArgs e) => MostrarUC(ucGraficas);
        private void btnTipoUsuarios_Click(object sender, EventArgs e) => MostrarUC(ucTipoUsuarios);
        private void btnSubSecciones_Click(object sender, EventArgs e) => MostrarUC(ucSubSecciones);
        private void btnFormato_Click(object sender, EventArgs e) => MostrarUC(ucCaratula);
        private void btnHistorial_Click(object sender, EventArgs e) => MostrarUC(ucHistorial);
        private void btnInstitucion_Click(object sender, EventArgs e) => MostrarUC(ucInstitucion);
        private void btnUnidadA_Click(object sender, EventArgs e) => MostrarUC(ucUnidadA);
        private void btnClasificacion_Click(object sender, EventArgs e) => MostrarUC(ucClasificacion);
        private void btnInfo_Click(object sender, EventArgs e) => MostrarUC(ucInfo);

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FrmLogin loginForm = new FrmLogin();
            loginForm.Show();
            this.Hide();
        }

        // =========================
        // 🛈 TOOLTIPS
        // =========================

        private void InicializarToolTips()
        {
            toolTip = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 200,
                ReshowDelay = 100,
                ShowAlways = true
            };

            toolTip.SetToolTip(btnLobby, "Área de Recepción");
            toolTip.SetToolTip(btnRUsuarios, "Registrar y Consultar Usuarios");
            toolTip.SetToolTip(btnSecciones, "Registrar y Consultar Secciones");
            toolTip.SetToolTip(btnSubSecciones, "Registrar y Consultar Subsecciones");
            toolTip.SetToolTip(btnInstitucion, "Registrar y Consultar Instituciones");
            toolTip.SetToolTip(btnUbicaciones, "Registrar y Consultar Ubicaciones");
            toolTip.SetToolTip(btnUnidadA, "Registrar y Consultar Unidad Administrativa");
            toolTip.SetToolTip(btnClasificacion, "Registrar y Consultar Clasificaciones");
            toolTip.SetToolTip(btnCArchivos, "Registrar y Consultar Archivos");
            toolTip.SetToolTip(btnHistorial, "Consultar Historial");
            toolTip.SetToolTip(btnFormato, "Captura e impresión de portadas");
            toolTip.SetToolTip(btnEstadistica, "Visualizar gráficas");
            toolTip.SetToolTip(btnInfo, "Configuracion del sistema");
            toolTip.SetToolTip(btnTipoUsuarios, "Informacion del Sistema");
            toolTip.SetToolTip(btnCerrarSesion, "Cerrar sesión");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.D3) ||
                keyData == (Keys.Control | Keys.NumPad3))
            {
                MostrarUC(ucCaratula);
                return true;
            }

            if (keyData == (Keys.Control | Keys.D2) ||
                keyData == (Keys.Control | Keys.NumPad2))
            {
                MostrarUC(ucRUsuarios); 
                return true;
            }

            if (keyData == (Keys.Control | Keys.H))
            {
                MostrarUC(ucHistorial);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
