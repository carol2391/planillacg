using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Forms.Login;
using nomina.Forms.Departamento;
using nomina.Forms.Categoria;
using nomina.Forms.Empleado;
using nomina.Clases.ConexionManager;
using nomina.Clases.Departamento;
using nomina.Clases.Categoria;
using nomina.Clases.Empleado;
using nomina.Clases.Labores;
using nomina.Clases.Descuentos;

using nomina.Forms.Labores;
using nomina.Forms.Descuento;
using nomina.Forms.MovimientosLabores;
using nomina.Forms.MovimientoDescuentos;
using nomina.Forms.Movimientos.Ausencia;
using nomina.Forms.Movimientos.MovimientoAumentos;
using nomina.Forms.Movimientos.MovimientoPrestamos;
using nomina.Forms.Parametros;
using nomina.Forms.Planilla;
using nomina.Forms.Usuarios;
using nomina.Forms.Empresas;
using nomina.Forms.EmpresaPrueba;
using nomina.Clases.Usuarios;
using nomina.Clases.PermisosUsuario;

namespace nomina.Forms.Main

{
    public static class SuperUsuario
    {
        public static bool superUsuario { get; set; }
    }
    public partial class frmMain : Form
    {
        Conexion conexion;
      
        public int usuarioId { set; get; }
        public string usuarioName { set; get; }
        PermisoUsuarioConexion bdPermisos;
        public List<DepartamentoData> departamentos { set; get; }
        public List<CategoriaData> categorias { set; get; }
        public List<EmpleadoData> empleados = new List<EmpleadoData>();
        DepartamentoConexion bdDepartamento;
        CategoriaConexion bdCategoria;
        EmpleadoConexion bdEmpleado;
        LaboresConexion bdLabores;
        DescuentoConexion bdDescuento;
        UsuarioConexion bdUsuario;
      
        string baseDeDatos;

        public frmMain()
        {
            InitializeComponent();
            mostrarLogin();
            
            
            conexion = new Conexion(this.baseDeDatos);
            bdDepartamento = new DepartamentoConexion(conexion);
            bdCategoria = new CategoriaConexion(conexion);
            bdLabores = new LaboresConexion(conexion);
            bdEmpleado = new EmpleadoConexion(conexion);
            bdDescuento = new DescuentoConexion(conexion);
            departamentos = new List<DepartamentoData>();
            categorias = new List<CategoriaData>();
            bdPermisos = new PermisoUsuarioConexion();
            if (!SuperUsuario.superUsuario) {
                VerificarPermisos();
            }
         

        }

        #region eventos
        private void FrmMain_Load(object sender, EventArgs e)
        {
            
            //mostrarEmpresas();
        }

        #region mostrar login
        public void mostrarLogin() {
            //this.cargarBaseDeDatos();
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
            if (DialogResult.OK == frm.DialogResult)
            {
                this.usuarioId = frm.usuarioId;
                this.usuarioName = frm.usuarioName;
                SuperUsuario.superUsuario = usuarioId == -1 && string.Equals(usuarioName?.Trim(), Properties.Settings.Default.usuario?.Trim(), StringComparison.OrdinalIgnoreCase);
                mostrarEmpresas();
            }
            else
                this.Close();
        }
        #endregion

        #region mostrar empresas
        public void mostrarEmpresas() {
            frmEmpresas frmEmpresas = new frmEmpresas(conexion, this);
            frmEmpresas.Tag = "empresas";
            frmEmpresas.ShowDialog();
            if (DialogResult.OK == frmEmpresas.DialogResult) {
                this.baseDeDatos = "n" + frmEmpresas.empData.Codigo;
                this.Text = frmEmpresas.empData.Nombre + " - Usuario: " + this.usuarioName;
                conexion = new Conexion(this.baseDeDatos);
            }
                
                
        }

        #endregion
        private void TsmDepartamentos_Click(object sender, EventArgs e)
        {
            
            frmDepartamento frm = new frmDepartamento(this.conexion, this, departamentos);
            frm.ShowDialog();
        }

        private void TsmCategorias_Click(object sender, EventArgs e)
        {
            frmCategoria frm = new frmCategoria(conexion, this, categorias);
            frm.ShowDialog();
        }

        private void TsmEmpleados_Click(object sender, EventArgs e)
        {
            if (this.bdDepartamento.obtenerDepartamentos().Count > 0 &&
                this.bdCategoria.obtenerCategorias().Count > 0) {
                frmEmpleado frm = new frmEmpleado(conexion,this);
                frm.Tag = "empleados";
                frm.ShowDialog();
            }else
                {
                MessageBox.Show("Agregue departamentos y Categorias", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
           
        }
        #endregion

        private void LaboresOTrabajosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmLabores frm = new frmLabores(conexion,this);
            frm.Tag = "labores";
            frm.ShowDialog();
        }

        /*descuentos */
        private void DescuentosEspecialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDescuento frm = new frmDescuento(conexion,this);
            frm.Tag = "descuentos";
            frm.ShowDialog();
        }
        //public void cargarBaseDeDatos() {
        //    DepartamentoConexion conexionDepto = new DepartamentoConexion(conexion);
        //    this.departamentos = conexionDepto.obtenerDepartamentos();
        //    CategoriaConexion conexionCategoria = new CategoriaConexion(conexion);
        //    this.categorias = conexionCategoria.obtenerCategorias();
        //    EmpleadoConexion conexionEmpleado = new EmpleadoConexion(conexion);
        //    this.empleados = conexionEmpleado.obtenerEmpleados();
        //}

      /*MOVIMIENTO LABORES*/
        private void laboresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.bdEmpleado.obtenerEmpleados().Count > 0 &&
                this.bdLabores.obtenerLabores().Count > 0) {
                frmMovimientoLabores frm = new frmMovimientoLabores(conexion, this);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Agregue Empleados y Labores", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tablasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /*movimiento descuentos*/
        private void descuentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.bdEmpleado.obtenerEmpleados().Count > 0 &&
                this.bdDescuento.obtenerDescuentos().Count > 0)
            {

                frmMovimientoDescuento frm = new frmMovimientoDescuento(conexion, this);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Agregue Empleados y Descuentos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void AusenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.bdEmpleado.obtenerEmpleados().Count > 0)
            {
                frmAusencias frm = new frmAusencias(conexion, this);
                frm.ShowDialog();
            }
                else
               {
                  MessageBox.Show("Agregue Empleados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

               }
        }

        private void aumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.bdEmpleado.obtenerEmpleados().Count > 0)
            {
                frmMovimientoAumentos frm = new frmMovimientoAumentos(conexion, this);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Agregue Empleados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void PrestamosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.bdEmpleado.obtenerEmpleados().Count > 0)
            {
                frmMovimientoPrestamos frm = new frmMovimientoPrestamos(conexion, this);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Agregue Empleados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ParametrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmParametros frm = new frmParametros(conexion, this);
            frm.ShowDialog();
        }

        private void generarPlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.usuarioId, 57)) {
            //    frmPlanilla frm = new frmPlanilla(conexion, this);
            //    frm.Tag = "generar";
            //    frm.ShowDialog();
            //}
              
        }

        private void nominaOPlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPlanilla frm = new frmPlanilla(conexion, this);
            frm.Tag = "ver";
            frm.ShowDialog();
        }

        private void UsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void UsuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //1 para ver usuarios
            if (SuperUsuario.superUsuario || bdPermisos.existePermiso(this.usuarioId,2, 1)){
                  frmUsuarios frm = new frmUsuarios(conexion,this);
                  frm.Tag = "usuarios";
                  frm.ShowDialog();
            }
           
        }

        private void AsignarPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //6 asignar permiso
            if (SuperUsuario.superUsuario || bdPermisos.existePermiso(this.usuarioId,14, 3))
            {
                frmAsignarPermisos frm = new frmAsignarPermisos(conexion, this);
                frm.ShowDialog();
            }
              
        }

        private void AsignarEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAsignarEmpresa frm = new frmAsignarEmpresa(conexion,this);
            frm.ShowDialog();
                
        }

        private void EmpresasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEmpresas frmEmpresas = new frmEmpresas(conexion,this);
            frmEmpresas.Tag = "empresas";
            frmEmpresas.ShowDialog();
            if (DialogResult.OK == frmEmpresas.DialogResult)
            {
                this.baseDeDatos = "n"+ frmEmpresas.empData.Codigo;
                this.Text = frmEmpresas.empData.Nombre + " - Usuario: " + this.usuarioName;
                conexion = new Conexion(this.baseDeDatos);
            }
                
        }

        public void VerificarPermisos() {
            //1 para ver usuarios
            if (!bdPermisos.existePermiso(this.usuarioId, 2,1))
            {
                usuariosToolStripMenuItem.Visible = false;
            }

            //8 para ver empresas
            if (!bdPermisos.existePermiso(this.usuarioId,1,1))
             {
                empresasToolStripMenuItem1.Visible = false;
                
            }

            //12 ver departamento
            if (!bdPermisos.existePermiso(this.usuarioId, 4,1))
            {
                tsmDepartamentos.Visible = false;

            }

            //16 ver categoria
            if (!bdPermisos.existePermiso(this.usuarioId,5,1))
            {
                tsmCategorias.Visible = false;
            }

            //20 ver empleado
            if (!bdPermisos.existePermiso(this.usuarioId, 3, 1))
            {
                tsmEmpleados.Visible = false;
              
            }

            //24 ver labores
            if (!bdPermisos.existePermiso(this.usuarioId, 6, 1))
            {
                laboresOTrabajosToolStripMenuItem.Visible = false;

            }

            //28 ver descuento
            if (!bdPermisos.existePermiso(this.usuarioId, 7, 1))
            {
                descuentosEspecialesToolStripMenuItem.Visible = false;

            }
            //32  ver movimiento labores
            if (!bdPermisos.existePermiso(this.usuarioId, 8, 1))
            {
                laboresToolStripMenuItem.Visible = false;

            }

            //36  ver movimiento descuento
            if (!bdPermisos.existePermiso(this.usuarioId, 9, 1))
            {
                descuentosToolStripMenuItem.Visible = false;

            }

            //40  ver movimiento ausencia
            if (!bdPermisos.existePermiso(this.usuarioId, 10, 1))
            {
                ausenciasToolStripMenuItem.Visible = false;

            }

            //44 ver movimiento aumento
            if (!bdPermisos.existePermiso(this.usuarioId, 11, 1))
            {
                aumentosToolStripMenuItem.Visible = false;

            }


            //48 ver movimiento prestamos
            if (!bdPermisos.existePermiso(this.usuarioId, 12, 1))
            {
                prestamosToolStripMenuItem.Visible = false;

            }

            ////52 ver parametros
            //if (!bdPermisos.existePermiso(this.usuarioId, 52))
            //{
            //    parametrosToolStripMenuItem.Enabled = false;

            //}
            ////57 PLANILLA
            //if (!bdPermisos.existePermiso(this.usuarioId, 57))
            //{
            //    generarPlanillaToolStripMenuItem.Enabled = false;
            //}

            ////58 PLANILLA
            //if (!bdPermisos.existePermiso(this.usuarioId, 58))
            //{
            //    nominaOPlanillaToolStripMenuItem.Enabled = false;
            //}

        }// fin permisos

        private void consultasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pruebaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmpresaPrueba frm = new frmEmpresaPrueba();
            frm.Show();
        }
    }
      
}
