using Microsoft.Reporting.WinForms;
using nomina.Clases.Categoria;
using nomina.Clases.ConexionManager;
using nomina.Clases.Departamento;
using nomina.Clases.Descuentos;
using nomina.Clases.Empleado;
using nomina.Clases.Labores;
using nomina.Clases.PermisosUsuario;
using nomina.Clases.Reportes;
using nomina.Clases.Seguridad;
using nomina.Clases.Usuarios;
using nomina.Forms.Categoria;
using nomina.Forms.Departamento;
using nomina.Forms.Descuento;
using nomina.Forms.Empleado;
using nomina.Forms.EmpresaPrueba;
using nomina.Forms.Empresas;
using nomina.Forms.Labores;
using nomina.Forms.Login;
using nomina.Forms.MovimientoDescuentos;
using nomina.Forms.Movimientos.Ausencia;
using nomina.Forms.Movimientos.MovimientoAumentos;
using nomina.Forms.Movimientos.MovimientoPrestamos;
using nomina.Forms.MovimientosLabores;
using nomina.Forms.Parametros;
using nomina.Forms.Planilla;
using nomina.Forms.Reportes.ReporteNomina;
using nomina.Forms.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace nomina.Forms.Main

{

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

        }

        #region eventos
        private void FrmMain_Load(object sender, EventArgs e)
        {
            mostrarLogin();


        }

        #region mostrar login
        public void mostrarLogin()
        {
            //this.cargarBaseDeDatos();
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
            if (DialogResult.OK == frm.DialogResult)
            {
                this.usuarioId = frm.usuarioId;
                this.usuarioName = frm.usuarioName;
                Session.Usuario = this.usuarioName;
                Session.superUsuario = usuarioId == -1 && string.Equals(usuarioName?.Trim(), Properties.Settings.Default.usuario?.Trim(), StringComparison.OrdinalIgnoreCase);
                mostrarEmpresas();
            }
            else
                this.Close();
        }
        #endregion

        #region mostrar empresas
        public void mostrarEmpresas()
        {
            frmEmpresas frmEmpresas = new frmEmpresas(conexion, this);
            frmEmpresas.Tag = "empresas";
            frmEmpresas.ShowDialog();
            if (DialogResult.OK == frmEmpresas.DialogResult)
            {
                this.baseDeDatos = "n" + frmEmpresas.empData.Codigo;
                this.Text = frmEmpresas.empData.Nombre + " - Usuario: " + this.usuarioName;
                conexion = new Conexion(this.baseDeDatos);

                bdDepartamento = new DepartamentoConexion(conexion);
                bdCategoria = new CategoriaConexion(conexion);
                bdLabores = new LaboresConexion(conexion);
                bdEmpleado = new EmpleadoConexion(conexion);
                bdDescuento = new DescuentoConexion(conexion);
                bdPermisos = new PermisoUsuarioConexion();

                if (!Session.superUsuario)
                {
                    VerificarPermisos();
                }

            }


        }

        #endregion
        private void TsmDepartamentos_Click(object sender, EventArgs e)
        {

            frmDepartamento frm = new frmDepartamento(this.conexion, this);
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
                this.bdCategoria.obtenerCategorias().Count > 0)
            {
                frmEmpleado frm = new frmEmpleado(conexion, this);
                frm.Tag = "empleados";
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Agregue departamentos y Categorias", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        #endregion

        private void LaboresOTrabajosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmLabores frm = new frmLabores(conexion, this);
            frm.Tag = "labores";
            frm.ShowDialog();
        }

        /*descuentos */
        private void DescuentosEspecialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDescuento frm = new frmDescuento(conexion, this);
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
                this.bdLabores.obtenerLabores().Count > 0)
            {
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

            frmPlanilla frm = new frmPlanilla(conexion, this);
            frm.Tag = "generar";
            frm.ShowDialog();


        }



        private void UsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //reporrte departamento
        private void análisisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("D", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK) {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_nomina_departamento", frmFiltro.Id, "D");
                frm.Show();
            }
            
        }

        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("C", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK)
            {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_nomina_categoria", frmFiltro.Id, "C");
                frm.Show();
            }

        }

        private void prestamosToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            

        }
        private void UsuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //1 para ver usuarios
            if (Session.superUsuario || bdPermisos.existePermiso(this.usuarioId, 2, 1))
            {
                frmUsuarios frm = new frmUsuarios(conexion, this);
                frm.Tag = "usuarios";
                frm.ShowDialog();
            }

        }

        private void AsignarPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //6 asignar permiso
            if (Session.superUsuario || bdPermisos.existePermiso(this.usuarioId, 14, 3))
            {
                frmAsignarPermisos frm = new frmAsignarPermisos(conexion, this);
                frm.ShowDialog();
            }

        }

        private void AsignarEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAsignarEmpresa frm = new frmAsignarEmpresa(conexion, this);
            frm.ShowDialog();

        }

        private void EmpresasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEmpresas frmEmpresas = new frmEmpresas(conexion, this);
            frmEmpresas.Tag = "empresas";
            frmEmpresas.ShowDialog();
            if (DialogResult.OK == frmEmpresas.DialogResult)
            {
                this.baseDeDatos = "n" + frmEmpresas.empData.Codigo;
                this.Text = frmEmpresas.empData.Nombre + " - Usuario: " + this.usuarioName;
                conexion = new Conexion(this.baseDeDatos);
            }

        }

        public void VerificarPermisos()
        {
            //1 para ver usuarios
            if (!bdPermisos.existePermiso(this.usuarioId, 2, 1))
            {
                usuariosToolStripMenuItem.Visible = false;
            }

            //8 para ver empresas
            if (!bdPermisos.existePermiso(this.usuarioId, 1, 1))
            {
                empresasToolStripMenuItem1.Visible = false;

            }

            //12 ver departamento
            if (!bdPermisos.existePermiso(this.usuarioId, 4, 1))
            {
                tsmDepartamentos.Visible = false;

            }

            //16 ver categoria
            if (!bdPermisos.existePermiso(this.usuarioId, 5, 1))
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

        private void sueldosPorDepartamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sueldosPorCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("C", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK)
            {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_nomina_categoria", frmFiltro.Id, "C");
                frm.Show();
            }

        }

        private void resumenDeLaboresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nominaOPlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

   

        private void libroDeSalariosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void primeraQuincenaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportePlanila frm = new frmReportePlanila(conexion, this, "PQUINCENA");
            frm.ShowDialog();
        }

        private void segundaQuincenaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportePlanila frm = new frmReportePlanila(conexion, this, "SQUINCENA");
            frm.ShowDialog();
        }

        private void anticipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportePlanila frm = new frmReportePlanila(conexion, this, "ANTICIPO");
            frm.ShowDialog();
        }

        private void mensualToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmReportePlanila frm = new frmReportePlanila(conexion, this, "MENSUAL");
            frm.ShowDialog();
        }

        private void listadosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void empleadiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_general_empleados", 0, "RGE");
            frm.Show();
        }

        private void departamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_departamento", 0, "D");
            frm.Show();
        }

        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_general_categorias", 0, "RGC");
            frm.Show();
        }

        private void deduccionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_general_deducciones", 0, "RGD");
            frm.Show();
        }

        private void laboresToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void descriptivoPorEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("RE", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK)
            {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_prestamos_descriptivos_empleado", frmFiltro.Id, "RDE");
                frm.Show();
            }
        }

        private void resumenPorEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("RE", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK)
            {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_resumen_prestamos_empleados", frmFiltro.Id, "RRE");
                frm.Show();
            }

        }

        private void porEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("RE", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK)
            {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_general_deducciones", frmFiltro.Id, "RE");
                frm.Show();
            }
        }

        private void resumenGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_general_deducciones", 0, "RGD");
            frm.Show();
        }

        private void porEmpleadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("L", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK)
            {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_labores", frmFiltro.Id, "L");
                frm.Show();
            }
        }

        private void resumenGeneralToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_labores", 0, "L");
            frm.Show();
        }

        private void liquidaciónToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("RE", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK)
            {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_reporte_liquidacion_detallado", frmFiltro.Id, "RL");
                frm.Show();
            }
        }

        private void fichaDeEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("RE", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK)
            {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_ficha_empleado", frmFiltro.Id, "RF");
                frm.Show();
            }
        }

        private void ultimoAccesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("BI", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK)
            {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_consultar_bitacora", "BIULTIMO",  frmFiltro.UserName, "ULTIMO_ACCESO");
                frm.Show();
            }
        }

        private void detalleDeAccesosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("BI", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK)
            {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_consultar_bitacora", "BIULTIMO", frmFiltro.UserName, "DETALLE_ACCESOS");
                frm.Show();
            }
        }

        private void últimaModificaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFiltro frmFiltro = new frmFiltro("BI", conexion, this);
            frmFiltro.ShowDialog();
            if (frmFiltro.DialogResult == DialogResult.OK)
            {
                frmReporteVarios frm = new frmReporteVarios(conexion, "sp_consultar_bitacora","BI", frmFiltro.UserName, "ULTIMA_MODIFICACION");
                frm.Show();
            }
        }
    }

}
