using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Forms.Usuarios;
using nomina.Forms.Empresas;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Threading;
using nomina.Clases.Utilidades;
using nomina.Clases.Opciones;
using nomina.Clases.GenerarPlanilla;
using nomina.Clases.ConexionManager;

namespace nomina.BarraProgreso
{
    public enum Tipo
    {
        PermisoEmpresa,
        PermisoUsuario,
        NuevaEmpresa,
        ModificarEmpresa,
        Reportes,
        GenerarPlanilla,
        Guardar
    }

    public enum DialogoResultado
    {
        Si, No
    }

    public partial class frmBarraProgreso : Form
    {
        public frmBarraProgreso()
        {
            InitializeComponent();
        }

        #region propiedades
        private Form handlers = new Form();
        public string empresa;
        public bool guardo = false;
        private frmAsignarEmpresa frmPermisosEmpresa;
        private frmAsignarPermisos frmAsignarPermisos;
        private frmAddEmpresa frmEmpresa;
        public Tipo tipo { set; get; }
        public DialogoResultado resultado { set; get; }
        private Thread hiloSecundario, hiloPrimario;
        private string codigo;
        private DateTime  fecha;
        PlanillaConexion planillabd;
        string TipoPlanilla;
        Conexion Conexion;
        #endregion

        #region constructor carga la barra cuando se estan asignando las empresas a las que el usuario tiene acceso

        public frmBarraProgreso(frmAsignarEmpresa frmEmpresa)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Cargando...");

            this.frmPermisosEmpresa = frmEmpresa;
            this.tipo = Tipo.PermisoEmpresa;
            configurarBarra();
        }
        #endregion

        public frmBarraProgreso(string codigo, DateTime fecha,Conexion conexion, string tipoPlanilla)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Cargando...");
            this.fecha  = fecha;
            this.codigo = codigo;
            this.tipo = Tipo.GenerarPlanilla;
            this.Conexion = conexion;
            this.TipoPlanilla = tipoPlanilla;
            configurarBarra();
        }
        public frmBarraProgreso(frmAddEmpresa frmEmpresa, Tipo tipoAccion)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Cargando...");
            this.frmEmpresa = frmEmpresa;
            this.tipo = tipoAccion;
            configurarBarra();

        }

        #region constructor carga la barra de progreso cuando se estan asignando los permisos de usuario
        public frmBarraProgreso(frmAsignarPermisos frmPermiso)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Cargando...");
            this.frmAsignarPermisos = frmPermiso;
            this.tipo = Tipo.PermisoUsuario;
            configurarBarra();

        }
        #endregion

        private void configurarBarra() {
            this.pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            base.FormBorderStyle = FormBorderStyle.None;
            base.FormBorderStyle = FormBorderStyle.None;
            this.handlers.Opacity = 0.0;
            this.handlers.FormBorderStyle = FormBorderStyle.None;
            this.handlers.StartPosition = FormStartPosition.Manual;
            this.handlers.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width * -1, 0);
            this.handlers.ShowInTaskbar = false;
        }


        #region guarda en la bd las empresas a las que tiene acceso el usuario
        public bool guardarEmpresasAsignasUsuario()
        {

            try
            {
                int activo;
                int f = 0;
                //foreach (DataGridViewRow fila in this.frmPermisosEmpresa.dgvPermisos.Rows)
                //{
                //    if (f != 0)
                //    {
                //        if (Convert.ToBoolean(fila.Cells["AsignarEmpresa"].Value))
                //        {
                //            activo = 1;
                //        }
                //        else
                //        {
                //            activo = 0;
                //        }

                //        this.frmPermisosEmpresa.bdEmpresa.insertarEmpresasUSuario(this.frmPermisosEmpresa.usuario.UsuarioId, Convert.ToInt32(fila.Cells["id"].Value.ToString()),
                //           activo);
                //    }
                //    f++;
                //}
                //MessageBox.Show("Empresas agregadas a usuario exitosamente", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //this.resultado = DialogoResultado.Si;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                this.resultado = DialogoResultado.No;
                return false;
            }

            return true;
        }
        #endregion

        #region guarda los permisos en la bd que tiene el usuario  
        private void guardarPermisos()
        {
            int f = 0;
            try
            {
            //    foreach (DataGridViewRow dataGridViewRow in this.frmAsignarPermisos.dgvPermisos.Rows)
            //    {
            //        int activo;
            //        if (f != 0)
            //        {
            //            if (Convert.ToBoolean(dataGridViewRow.Cells["AsignarPermiso"].Value))
            //            {
            //                activo = 1;
            //            }
            //            else
            //            {
            //                activo = 0;
            //            }
            //            LOpciones modulo = null;
            //            if (this.InvokeRequired)
            //                this.Invoke(new MethodInvoker(delegate
            //                {
            //                    modulo = (LOpciones)this.frmAsignarPermisos.cbModulo.SelectedItem;

            //                }), null);

            //            this.frmAsignarPermisos.bdPermisos.insertarPermisos(this.frmAsignarPermisos.user.UsuarioId, Convert.ToInt32(modulo.id), Convert.ToInt32(dataGridViewRow.Cells["PermisoId"].Value.ToString()), activo);
            //        }
            //        f++;
            //    }
            //    if (frmAsignarPermisos.dgvPermisos.RowCount > 0)
            //    {
            //        MessageBox.Show("Permisos agregados exitosamente", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        this.resultado = DialogoResultado.Si;
            //    }
            //}

            }
            catch (Exception e)
            {
                this.resultado = DialogoResultado.No;
                MessageBox.Show(e.Message);

            }

        }
        #endregion

        #region guarda la informacion de la empresa en la base de datos
        private void guardarEmpresa()
        {
            if (this.frmEmpresa.bdEmpresa.agregarEmpresa(this.frmEmpresa.txtCodigo.Text, this.frmEmpresa.txtNombre.Text, this.frmEmpresa.dptFecha.Value.Date, this.frmEmpresa.txtDireccion.Text, this.frmEmpresa.txtRTN.Text,
                this.frmEmpresa.txtTelefono.Text, this.frmEmpresa.txtCorreo.Text, frmEmpresa.PathImagen, frmEmpresa.frmMain.usuarioName))
            {
                this.resultado = DialogoResultado.Si;
            }
            else
            {
                this.resultado = DialogoResultado.No;
            }
        }
        #endregion

        #region crea la base de datos empresa apartir de modificar
        private void modificarEmpresa()
        {
            if (this.frmEmpresa.bdEmpresa.modificarEmpresaNomina(this.frmEmpresa.empData.Id, this.frmEmpresa.txtCodigo.Text, this.frmEmpresa.txtNombre.Text, this.frmEmpresa.dptFecha.Value.Date,
                this.frmEmpresa.txtDireccion.Text, this.frmEmpresa.txtRTN.Text, this.frmEmpresa.txtTelefono.Text, this.frmEmpresa.txtCorreo.Text, this.frmEmpresa.codigoAntiguo, frmEmpresa.frmMain.usuarioName))
            {
                this.resultado = DialogoResultado.Si;
            }
            else
            {
                this.resultado = DialogoResultado.No;
            }
        }
        #endregion

        #region hilo que se levanta para almacenar en la bd: los permisosEmpresa,permisoUsuario,empresas
        private void RunThread()
        {
            if (this.tipo.Equals(Tipo.PermisoEmpresa))
            {
                this.guardarEmpresasAsignasUsuario();
            }
            else
               if (this.tipo.Equals(Tipo.PermisoUsuario))
            {
                this.guardarPermisos();
            }
            else
                     if (this.tipo.Equals(Tipo.NuevaEmpresa))
            {
                this.guardarEmpresa();
            }
            else
                            if (this.tipo.Equals(Tipo.ModificarEmpresa))
            {
                this.modificarEmpresa();
            }
            else
                 if (this.tipo.Equals(Tipo.GenerarPlanilla))
            {
                planillabd = new PlanillaConexion(Conexion);
                if (planillabd.generarPlanilla(this.codigo, this.fecha, TipoPlanilla))
                {
                    this.resultado = DialogoResultado.Si;
                }
                else
                {
                    this.resultado = DialogoResultado.No;
                }
            }
        }
        #endregion

        #region
        private void InitThread()
        {
            this.hiloSecundario = new Thread(new ThreadStart(this.RunThread));
            this.hiloSecundario.SetApartmentState(ApartmentState.STA);
            this.hiloSecundario.Start();
            this.hiloSecundario.Join();
            this.CloseForm(hiloSecundario.ThreadState);
        }
        #endregion

        #region evento que cierra el formulario
        private void CloseForm(Object state)
        {
            if ((ThreadState)state != ThreadState.Stopped)
            {
                return;
            }
            else
            {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
            }
        }
        #endregion



        private void frmBarraProgreso_Load(object sender, EventArgs e)
        {
            hiloPrimario = new Thread(InitThread);
            //sendprint.SetApartmentState(System.Threading.ApartmentState.STA);
            hiloPrimario.Start();
        }

        #region crear base de datos con procedimientos almacenados
        private void crearBD()
        {

        }
        #endregion

    }
}
