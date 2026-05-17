using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Clases.Categoria;
using nomina.Clases.Empleado;
using nomina.Clases.Departamento;
using nomina.Clases.ConexionManager;
using nomina.Clases.MovimientoLabores;
using nomina.Forms.Main;
using nomina.Clases.Utilidades;
using nomina.Clases.PermisosUsuario;

//revisar las categorias y los departamentos
//no se puede agregar un empleado si no hay 
namespace nomina.Forms.Empleado
{
    public partial class frmEmpleado : Form
    {
        List<DepartamentoData> departamentos = new List<DepartamentoData>();
        List<CategoriaData> categorias = new List<CategoriaData>();
        List<EmpleadoData> empleados = new List<EmpleadoData>();
        Conexion conexion;
        frmMain frmMain;
        public EmpleadoData empleado { set; get; }
        DepartamentoConexion bdDepartamento;
        CategoriaConexion bdCategoriaConexion;
        MLaboresConexion bdMlabores;
        PermisoUsuarioConexion bdPermisos;
        EmpleadoConexion bdEmpleado;
        public frmEmpleado(Conexion conexion, frmMain frmMain)
        {
            InitializeComponent();
            panel1.BackColor = Color.SkyBlue;
            Utilidad.configurarDataGrid(dgvDatos);
            Utilidad.configuarForm(this, "empleados");
            btnModificar.BackColor = btnNuevo.BackColor = btnQuitar.BackColor = btnSalir.BackColor = Color.SkyBlue;
            this.conexion = conexion;
            bdEmpleado = new EmpleadoConexion(conexion);
            bdPermisos = new PermisoUsuarioConexion();
            dgvDatos.Select();
            txtCodigo.Visible = false;
            txtNombre.Visible = false;
            this.frmMain = frmMain;
            bdDepartamento = new DepartamentoConexion(this.conexion);
            bdCategoriaConexion = new CategoriaConexion(this.conexion);
            bdMlabores = new MLaboresConexion(this.conexion);
            dgvDatos.DataSource = bdEmpleado.obtenerEmpleados();
            this.pnTipoAntecedente.Visible = this.pnTitulo.Visible = false;
            //this.departamentos = departamentos;
            //this.categorias = categorias;
            //this.frmMain.categorias.Insert(0, new CategoriaData(0, "", "Seleccione una opción...", 0, 0));
            //this.frmMain.departamentos.Insert(0, new DepartamentoData(0, "", "Seleccione una opción...", "", ""));

        }
        #region eventos

        private void frmEmpleado_Load(object sender, EventArgs e)
        {
            //Utilidad.configurarDataGrid(dgvEmpleados);
            //if (this.Tag.Equals("Buscar"))
            //{
            //    this.btnNuevo.Text = "&Seleccionar";
            //    this.btnModificar.Visible = false;
            //    this.btnQuitar.Visible = false;
            //    this.btnSalir.Location = new Point(661, 139);
            //    this.AutoSize = true;
            //    this.btnVerHistorial.Visible = false;
            //}
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            /*buscar el empleado para cargarlo
             en el formulario de movimientos labores*/
            //if (this.Tag.Equals("Buscar"))
            //{
            //    this.empleado = this.obtenerEmpleado();
            //    if (this.empleado != null)
            //    {
            //        this.DialogResult = DialogResult.OK;
            //    }
            //    else
            //        this.DialogResult = DialogResult.No;

            //}
            //else 
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 21))
            //{
            frmAddEmpleado frm = new frmAddEmpleado(conexion);
            frm.Tag = "agregar";
            frm.ShowDialog();
            dgvDatos.DataSource = bdEmpleado.obtenerEmpleados();
            //if (frm.DialogResult == DialogResult.OK)
            //{
            //this.frmMain.cargarBaseDeDatos();

            //}
            //}
            //else
            //    btnNuevo.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 22)) {
            //    
            if (dgvDatos.RowCount > 0)
            {
                this.cargarDatosEditar();
                frmAddEmpleado frm = new frmAddEmpleado(conexion, frmMain, empleado);

                frm.Tag = "modificar";
                DialogResult result = frm.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    //this.frmMain.cargarBaseDeDatos();
                    this.dgvDatos.DataSource = bdEmpleado.obtenerEmpleados();
                }
            }
            // }
            //else

            //     btnModificar.Enabled = false;



        }

        private void bntQuitar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 23))
            //{
            if (dgvDatos.RowCount > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar el empleado?", "Eliminar empleado", MessageBoxButtons.YesNoCancel);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    int nlinea = dgvDatos.CurrentCell.RowIndex;
                    string codigo = this.dgvDatos.Rows[nlinea].Cells["Codigo"].Value.ToString();
                    int id = Convert.ToInt32(this.dgvDatos.Rows[nlinea].Cells["Id"].Value.ToString());
                    //if (!this.bdMlabores.existeEmpleadoEnMlabores(codigo))
                    //{
                    EmpleadoConexion empConexion = new EmpleadoConexion(conexion);
                    if (empConexion.accionesEmpleado("E", id, "",
                       "",
                       DateTime.Now,
                       " ",
                       " ",
                       "",
                       "",
                       "",
                       "", "",
                       "",
                       DateTime.Now,
                       "", 0, 0, 0,
                       "",
                       0.00, "", "", "", "",
                        0, "", "",

                       "", "", "", DateTime.Now, " ", "", "", "", "", "", ""))
                    {
                        //this.frmMain.cargarBaseDeDatos();
                        this.dgvDatos.DataSource = bdEmpleado.obtenerEmpleados();
                        MessageBox.Show("Empleado eliminado exitosamente", "Eliminar empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se puede eliminar, porque tiene movimientos", "Eliminar empleado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //else
                    //    MessageBox.Show("No existe el código de la categoria", "Eliminar empleado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}//fin si existe
                    //else {
                    //   
                    //}

                }//fin si message boc
            }
            //}else
            //    this.btnQuitar.Enabled = false;
        }


        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                this.dgvDatos.DataSource = bdEmpleado.obtenerEmpleados();
            }
            else {
                dgvDatos.DataSource = bdEmpleado.buscarEmpleado(txtCodigo.Text, "COD_TRB");
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                this.dgvDatos.DataSource = bdEmpleado.obtenerEmpleados();
            }
            else
            {
                dgvDatos.DataSource = bdEmpleado.buscarEmpleado(txtNombre.Text, "NOM_TRB");
            }
        }

        private void rbCodigo_Click(object sender, EventArgs e)
        {
            txtNombre.Visible = false;
            txtCodigo.Visible = true;
            txtNombre.Text = txtCodigo.Text = "";
            txtCodigo.Select();
        }

        private void rbNombre_Click(object sender, EventArgs e)
        {
            txtNombre.Visible = true;
            txtCodigo.Visible = false;
            txtNombre.Text = txtCodigo.Text = "";
            txtNombre.Select();
        }

        private void dgvEmpleados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarDatosEditar();
            frmAddEmpleado frm = new frmAddEmpleado(conexion, frmMain, empleado);
            frm.Tag = "ver";
            frm.bloquearControles();
            frm.ShowDialog();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea salir?", "Salir", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }
        }
        #endregion

        #region Actualizar objetos en el formulario principal y en los controles


        private void actualizarObjetos()
        {
            //this.frmMain.categorias.Insert(0, new CategoriaData(0, "", "Seleccione una opción...", 0, 0));
            //this.frmMain.departamentos.Insert(0, new DepartamentoData(0, "", "Seleccione una opción...", "", ""));
            //this.categorias = this.frmMain.categorias;
            EmpleadoConexion bd = new EmpleadoConexion(conexion);
            this.empleados = bd.obtenerEmpleados();
            this.departamentos = bdDepartamento.obtenerDepartamentos();
        }

        public void refrescarControles()
        {
            dgvDatos.DataSource = this.empleados;
            Utilidad.configurarDataGrid(dgvDatos);
        }

        #endregion

        #region instanciar el objeto empleado con la fila seleccionada del datagrid
        private void cargarDatosEditar()
        {
            this.empleado = new EmpleadoData();
            if (dgvDatos.RowCount > 0)
            {
                int nlinea = dgvDatos.CurrentCell.RowIndex;
                empleado.Id = Convert.ToInt32(this.dgvDatos.Rows[nlinea].Cells["Id"].Value.ToString());
            }

        }
        #endregion

        #region instancear el empleado para enviarlo al formulario de movimientos labores
        public EmpleadoData obtenerEmpleado() {
            EmpleadoConexion bd = new EmpleadoConexion(conexion);
            if (dgvDatos.RowCount > 0)
            {
                int nlinea = dgvDatos.CurrentCell.RowIndex;
                EmpleadoData empleado = new EmpleadoData();
                empleado.Id = Convert.ToInt32(this.dgvDatos.Rows[nlinea].Cells["Id"].Value.ToString());
                empleado = bd.obtenerEmpleado(empleado.Id);
                return empleado;
            }
            return null;
        }
        #endregion

        
        private void BtnVerHistorial_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 56)) {
            frmHistorialAumento frm = new frmHistorialAumento(conexion);
            frm.ShowDialog();
            //}

        }

        private void btnAntecedente_Click(object sender, EventArgs e)
        {
            this.pnTipoAntecedente.Visible = this.pnTitulo.Visible = true;
        }

        private void rbPolicial_Click(object sender, EventArgs e)
        {
            if (this.dgvDatos.RowCount > 0)
            {
                int rowIndex = this.dgvDatos.CurrentCell.RowIndex;
                int idEmpleado = Convert.ToInt32(this.dgvDatos.Rows[rowIndex].Cells["Id"].Value.ToString());
                frmAntecedente frm = new frmAntecedente(idEmpleado, conexion, "PO");
                frm.ShowDialog();

            }
            this.pnTipoAntecedente.Visible = this.pnTitulo.Visible = false;
        }

        private void rbPenal_Click(object sender, EventArgs e)
        {
            if (this.dgvDatos.RowCount > 0)
            {
                int rowIndex = this.dgvDatos.CurrentCell.RowIndex;
                int idEmpleado = Convert.ToInt32(this.dgvDatos.Rows[rowIndex].Cells["Id"].Value.ToString());
                frmAntecedente frm = new frmAntecedente(idEmpleado, conexion, "PN");
                frm.ShowDialog();
            }
            this.pnTipoAntecedente.Visible = this.pnTitulo.Visible = false;
        }

        private void dgvDatos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (base.Tag.Equals("buscar") && e.KeyCode == Keys.Enter)
            {
                cargarDatos();
                if (this.empleado != null)
                {
                    empleado = bdEmpleado.obtenerEmpleadoCodigo(empleado.Codigo);
                    base.DialogResult = DialogResult.OK;
                }
                else
                {
                    base.DialogResult = DialogResult.No;
                }
            }
        }

        private void cargarDatos(){
            
            if (this.dgvDatos.RowCount > 0)
            {
                empleado = new EmpleadoData();
                int rowIndex = this.dgvDatos.CurrentCell.RowIndex;
                this.empleado.Id = Convert.ToInt32(this.dgvDatos.Rows[rowIndex].Cells["Id"].Value.ToString());
                this.empleado.Nombre = this.dgvDatos.Rows[rowIndex].Cells["Nombre"].Value.ToString();
                this.empleado.Codigo = this.dgvDatos.Rows[rowIndex].Cells["Codigo"].Value.ToString();
                this.empleado.objDepto.NombreDepartamento = this.dgvDatos.Rows[rowIndex].Cells["Departamento"].Value.ToString();
                this.empleado.Sueldo = Convert.ToDecimal(this.dgvDatos.Rows[rowIndex].Cells["Sueldo"].Value.ToString());
            }  
        }
    }
}
