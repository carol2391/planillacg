using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Forms.Main;
using nomina.Clases.ConexionManager;
using nomina.Clases.Empresas;
using nomina.Clases.Utilidades;
using nomina.Clases.PermisosUsuario;

namespace nomina.Forms.Empresas
{
    public partial class frmEmpresas : Form
    {
        enum Opcion
        {
            Buscar,
            Salir
        }
        #region propiedades
        private Conexion conexion;
        public frmMain frmMain { set; get; }
        private EmpresaConexion bdEmpresa;
        private PermisoUsuarioConexion bdPermisos;
        public EmpresaData empData { set; get; }
        Opcion opcion;
        #endregion



        public frmEmpresas(Conexion conexion, frmMain frmMain)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Empresas");
            this.panel1.BackColor = Color.SkyBlue;
            btnNuevo.BackColor = btnModificar.BackColor = btnQuitar.BackColor = btnSalir.BackColor =
             btnAsignarEmpresa.BackColor = btnAsignarPermisos.BackColor = btnActualizarBD.BackColor = Color.SkyBlue;
            this.conexion = conexion;
            empData = null;
            this.bdPermisos = new PermisoUsuarioConexion();
            this.frmMain = frmMain;
            this.bdEmpresa = new EmpresaConexion();
            if (frmMain.usuarioName!=null && frmMain.usuarioName.ToUpper().Trim().Equals(Properties.Settings.Default.usuario.ToUpper().Trim()))
            {
                dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresasAdmin();
            }
            else
                this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresas(frmMain.usuarioId);
            Utilidad.configurarDataGrid(this.dgvDatos);
            this.txtNombre.Visible = txtCodigo.Visible = false;
            this.ControlBox = false;
            if (dgvDatos.Rows.Count == 0)
            {
                MessageBox.Show("Contactese con el administrador para que le asigne empresas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.No;
            }
            dgvDatos.Select();
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
                    if (dgvDatos.Rows.Count > 0)
                    {
                        if (this.empData != null)
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Seleccione una empresa", "Seleccionar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                }//fin scape
                result = true;
            }
            return result;
        }
        #endregion
        #region evento radio buton
        private void rbCodigo_Click(object sender, EventArgs e)
        {
            this.txtCodigo.Visible = true;
            this.txtCodigo.Select();
            this.txtNombre.Visible = false;
            this.txtNombre.Text = this.txtCodigo.Text = "";
        }

        private void rbNombre_Click(object sender, EventArgs e)
        {
            this.txtCodigo.Visible = false;
            this.txtNombre.Visible = true;
            this.txtNombre.Select();
            this.txtNombre.Text = this.txtCodigo.Text = "";
        }
        #endregion

        #region evento cuando textChanged
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCodigo.Text))
            {
                this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresas(this.frmMain.usuarioId);
            }
            else
            {
                this.dgvDatos.DataSource = this.bdEmpresa.buscarEmpresaCodigo(this.txtCodigo.Text);
            }
            opcion = Opcion.Buscar;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNombre.Text))
            {
                this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresas(this.frmMain.usuarioId);
            }
            else
            {
                this.dgvDatos.DataSource = this.bdEmpresa.buscarEmpresaNombre(this.txtNombre.Text);
            }
        }
        #endregion

        #region menu
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //if (this.bdPermisos.existePermiso(this.frmMain.usuarioId, 1, 1)
            //  || Properties.Settings.Default.usuario.ToUpper().Trim().Equals(frmMain.usuarioName.ToUpper().Trim()))
            //{
                new frmAddEmpresa(this.conexion, frmMain)
                {
                    Tag = "agregar"
                }.ShowDialog();
                if (Properties.Settings.Default.usuario.ToUpper().Trim().Equals(frmMain.usuarioName.ToUpper().Trim()))
                {
                    this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresasAdmin();
                }
                else
                {
                    this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresas(this.frmMain.usuarioId);

                }
            //}
            //else
            //{
            //    this.btnNuevo.Enabled = false;
            //    MessageBox.Show("No tiene permisos para crear", "Crear", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //}
            this.dgvDatos.Select();
            opcion = Opcion.Salir;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            //if (this.bdPermisos.existePermiso(this.frmMain.usuarioId, 1, 2)
            //    || Properties.Settings.Default.usuario.ToUpper().Trim().Equals(frmMain.usuarioName.ToUpper().Trim()))

            //{
                if (this.dgvDatos.RowCount > 0)
                {
                    this.cargarDatos();
                    DialogResult dialogResult = new frmAddEmpresa(this.conexion, this.frmMain, this.empData)
                    {
                        Tag = "modificar"
                    }.ShowDialog();
                    if (Properties.Settings.Default.usuario.ToUpper().Trim().Equals(frmMain.usuarioName.ToUpper().Trim()))
                    {
                        this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresasAdmin();
                    }
                    else
                    {
                        this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresas(this.frmMain.usuarioId);
                    }

                }
                //else
                //{
                //    this.btnModificar.Enabled = false;
                //    MessageBox.Show("No tiene permisos para modificar", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //}
            //}
            this.dgvDatos.Select();
            opcion = Opcion.Salir;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
           // if (this.bdPermisos.existePermiso(this.frmMain.usuarioId, 1, 3)
            //    || Properties.Settings.Default.usuario.ToUpper().Trim().Equals(frmMain.usuarioName.ToUpper().Trim()))
            //{
                DialogResult dialogResult = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes && this.dgvDatos.RowCount > 0)
                {
                    this.cargarDatos();
                    if (this.bdEmpresa.eliminarEmpresa(this.empData.Codigo, frmMain.usuarioName))
                    {
                        MessageBox.Show("Empresa eliminada exitosamente", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if (Properties.Settings.Default.usuario.ToUpper().Trim().Equals(frmMain.usuarioName.ToUpper().Trim()))
                        {
                            this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresasAdmin();
                        }
                        else
                        {
                            this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresas(this.frmMain.usuarioId);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se puede eliminar la empresa, ya tiene movimientos", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        if (Properties.Settings.Default.usuario.ToUpper().Trim().Equals(frmMain.usuarioName.ToUpper().Trim()))
                        {
                            this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresasAdmin();
                        }
                        else
                        {
                            this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresas(this.frmMain.usuarioId);
                        }
                    }
                }
            //}
            //else
            //{
            //    MessageBox.Show("No tiene permisos para eliminar", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //    this.btnQuitar.Enabled = false;
            //}
            this.dgvDatos.Select();
            opcion = Opcion.Salir;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
        #endregion

        #region carga los datos cuando se va editar o eliminar una empresa
        private void cargarDatos()
        {
            if (this.dgvDatos.RowCount > 0)
            {
                empData = new EmpresaData();
                int nlinea = dgvDatos.CurrentCell.RowIndex;
                empData.Codigo = this.dgvDatos.Rows[nlinea].Cells["Codigo"].Value.ToString();
                empData.Id = Convert.ToInt32(this.dgvDatos.Rows[nlinea].Cells["Id"].Value.ToString());
                empData.Codigo = this.dgvDatos.Rows[nlinea].Cells["Codigo"].Value.ToString();
                empData.Nombre = this.dgvDatos.Rows[nlinea].Cells["Nombre"].Value.ToString();
                empData.RTN = this.dgvDatos.Rows[nlinea].Cells["RTN"].Value.ToString();
                empData.Correo = this.dgvDatos.Rows[nlinea].Cells["Correo"].Value.ToString();
                empData.Telefono = this.dgvDatos.Rows[nlinea].Cells["Telefono"].Value.ToString();
                empData.Direccion = this.dgvDatos.Rows[nlinea].Cells["Direccion"].Value.ToString();
                empData.NominaFlag = Convert.ToInt32(this.dgvDatos.Rows[nlinea].Cells["NominaFlag"].Value.ToString());

                empData.Image = (Image)this.dgvDatos.Rows[nlinea].Cells["Imagen"].Value;

             

            }
        }
        #endregion

        #region se carga la informacion cuando el usuario selecciona una empresa en el datagrid y presiona la tecla enter
        private void dgvDatos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cargarDatos();
                if (empData.NominaFlag != 0)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Debe activar la empresa en retenciones, en la opción de modificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }


        #endregion

        private void btnAsignarEmpresa_Click(object sender, EventArgs e)
        {
            //if (frmMain.usuarioName.ToUpper().Equals(Properties.Settings.Default.usuario.ToUpper().Trim()))
            //{
            //    frmAsignarEmpresa f = new frmAsignarEmpresa(conexion, frmMain);
            //    f.ShowDialog();
            //    this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresasAdmin();
            //}
            //else
            //    this.dgvDatos.DataSource = this.bdEmpresa.obtenerEmpresas(frmMain.usuarioId);

        }

        private void btnAsignarPermisos_Click(object sender, EventArgs e)
        {
            //if (frmMain.usuarioName.ToUpper().Equals(Properties.Settings.Default.usuario.ToUpper().Trim()))
            //{
            //    frmAsignarPermisos f = new frmAsignarPermisos(conexion, frmMain);
            //    f.ShowDialog();

            //}

        }

        private void btnActualizarBD_Click(object sender, EventArgs e)
        {
            //if (frmMain.usuarioName.ToUpper().Equals(Properties.Settings.Default.usuario.ToUpper().Trim()))
            //{
            //    frmActualizarBD f = new frmActualizarBD();
            //    f.ShowDialog();
            //}
        }
    }
}
