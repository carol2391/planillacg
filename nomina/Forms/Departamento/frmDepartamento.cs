using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Clases.ConexionManager;
using nomina.Clases.Departamento;
using nomina.Clases.Utilidades;
using nomina.Forms.Main;
using nomina.Clases.PermisosUsuario;
using nomina.Clases.UsuarioPermisos;

namespace nomina.Forms.Departamento
{
    enum Opcion
    {
        Buscar,
        Salir
    }

    public partial class frmDepartamento : Form
    {
        Conexion conexion;
        frmMain frmMain;
        public DepartamentoData depto { set; get; }
        List<DepartamentoData> departamentos = new List<DepartamentoData>();
        DepartamentoConexion bdDepto;
        PermisoUsuarioConexion bdPermisos;
        Opcion opcion;

        public frmDepartamento(Conexion conexion, frmMain frm)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.frmMain = frm;
            bdPermisos = new PermisoUsuarioConexion();
            bdDepto = new DepartamentoConexion(conexion);
            dgvDeptos.DataSource = bdDepto.obtenerDepartamentos();
            txtCodigo.Visible = false;
            txtNombre.Visible= false;
            rbCodigo.Focus();
            panel1.BackColor = Color.SkyBlue;
            btnModificar.BackColor = btnNuevo.BackColor = bntQuitar.BackColor = btnSalir.BackColor = Color.SkyBlue;
        }
        #region eventos scape, f2,f4
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool result;
            if (keyData != Keys.Escape)
            {
                result = base.ProcessCmdKey(ref msg, keyData);
            }
            else
            {

                if (keyData == Keys.Escape)
                {
                    if (Opcion.Buscar.Equals(opcion))
                    {
                        dgvDeptos.DataSource = bdDepto.obtenerDepartamentos();
                    }
                    else
                        DialogResult = DialogResult.OK;

                }//fin scape
                result = true;
            }
            return result;
        }
        #endregion
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 13)) {
                frmAddDepartamento frm = new frmAddDepartamento(frmMain,this.conexion);
                //1 para nuevo
                frm.Tag = "agregar";
                frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //{
            //    this.frmMain.cargarBaseDeDatos();
                 dgvDeptos.DataSource = bdDepto.obtenerDepartamentos();
            //}
            //}
            //else
            //    btnNuevo.Enabled = false;
            opcion = Opcion.Salir;
        }

        private void frmDepartamento_Load(object sender, EventArgs e)
        {
            //departamentoPersistence depto = new departamentoPersistence(this.conexion);
          
            Utilidad.configurarDataGrid(this.dgvDeptos);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 14)) {
                if (dgvDeptos.RowCount > 0)
                {
                    this.cargarDatosEditar();
                    frmAddDepartamento frm = new frmAddDepartamento(conexion, depto);
                    frm.Tag = "modificar";
                    DialogResult result = frm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
              
                    this.dgvDeptos.DataSource = bdDepto.obtenerDepartamentos();
                    }
                    else
                    {
                       
                    }
                }
     
            opcion = Opcion.Salir;

        }//fin modificar
        #region Actualizar objetos

        private void actualizarObjetos()
        {
            DepartamentoConexion bd = new DepartamentoConexion(conexion);
            this.departamentos = bd.obtenerDepartamentos();
        }
        #endregion

        #region 
        public void refrescarControles() {
            dgvDeptos.DataSource = this.departamentos;
        }
        #endregion

        #region instanceo el departamento con sus datos
        private void cargarDatosEditar()
        {
           depto = new DepartamentoData();
            if (dgvDeptos.RowCount > 0) {
                int nlinea = dgvDeptos.CurrentCell.RowIndex;
                depto.Id = Int32.Parse(this.dgvDeptos.Rows[nlinea].Cells["Id"].Value.ToString());
                depto.CodigoDepartamento = this.dgvDeptos.Rows[nlinea].Cells["CodigoDepartamento"].Value.ToString();
                depto.NombreDepartamento = this.dgvDeptos.Rows[nlinea].Cells["NombreDepartamento"].Value.ToString();
                depto.NombreEncargado = this.dgvDeptos.Rows[nlinea].Cells["NombreEncargado"].Value.ToString();
               // depto.CodigoCuenta = this.dgvDeptos.Rows[nlinea].Cells["CodigoCuenta"].Value.ToString();

            }
           
        }
        #endregion

        private void bntQuitar_Click(object sender, EventArgs e)
        {
           
            eliminarDepartamento();
;
            opcion = Opcion.Salir;
        }

        private void eliminarDepartamento()
        {
            if (dgvDeptos.RowCount > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar este departamento?", "Eliminar departamento", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    DepartamentoConexion depto = new DepartamentoConexion(conexion);
                    int nlinea = dgvDeptos.CurrentCell.RowIndex;
                   int id = Convert.ToInt32(this.dgvDeptos.Rows[nlinea].Cells["Id"].Value.ToString());
                    string codigo = this.dgvDeptos.Rows[nlinea].Cells["CodigoDepartamento"].Value.ToString();
                    DepartamentoData d = new DepartamentoData();
                 //if (d.empleados.Count == 0)
                 //{
                    if (depto.accionDepartamento("E", id, "" , "", 1, 1))
                    {
                       
                        this.dgvDeptos.DataSource = bdDepto.obtenerDepartamentos();
                        MessageBox.Show("Departamento eliminado exitosamente", "Eliminar departamento", MessageBoxButtons.OK);
                        }
                        else
                            MessageBox.Show("No se puede eliminar, porque tiene movimientos", "Eliminar departamento", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    //}
                }
            }    
        }

        private void RbCodigo_Click(object sender, EventArgs e)
        {
            txtCodigo.Select();
            txtCodigo.Visible = true;
            txtNombre.Visible = false;
            txtNombre.Text = txtCodigo.Text = "";

        }
        private void RbNombre_Click(object sender, EventArgs e)
        {
            txtNombre.Select();
            txtCodigo.Visible = false;
            txtNombre.Visible = true;
            txtNombre.Text = txtCodigo.Text = "";
        }
        private void TxtCodigo_TextChanged(object sender, EventArgs e)
        {
            buscarDepto(txtCodigo.Text,"COD_DEP");
           
        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {
            buscarDepto(txtNombre.Text, "NOM_DEP");
        }

        private void buscarDepto(string valor, string campo) {

            DepartamentoConexion depto = new DepartamentoConexion(conexion);

            if (String.IsNullOrWhiteSpace(campo.Trim()))
            {
                dgvDeptos.DataSource = depto.obtenerDepartamentos();
            }
            else {
                dgvDeptos.DataSource = depto.buscarDepartamento(valor, campo);
            }
        }
        private void DgvDeptos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarDatosEditar();
            frmAddDepartamento frm = new frmAddDepartamento(conexion, depto);
            frm.bloquearControles();
            frm.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea salir?", "Salir", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }

        }

        private void dgvDatos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (this.Tag.Equals("buscar") && e.KeyCode == Keys.Enter)
            {
                this.cargarDatosEditar();
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                base.DialogResult = DialogResult.No;
            
            }
        }
        private void frmDepartamento_Shown(object sender, EventArgs e)
        {
            Validator.validarPermisos (this.frmMain.usuarioId, btnNuevo, btnModificar, bntQuitar, btnSalir, this,4);
        }
    }
}
    

