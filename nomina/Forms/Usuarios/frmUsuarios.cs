using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Clases.Usuarios;
using nomina.Forms.Usuarios;
using nomina.Clases.ConexionManager;
using nomina.Clases.Utilidades;
using nomina.Clases.PermisosUsuario;
using nomina.Forms.Main;

namespace nomina.Forms.Usuarios
{
    public partial class frmUsuarios : Form
    {
        Conexion conexion;
        public UsuarioData user { set; get; }
        UsuarioConexion bd;
        PermisoUsuarioConexion bdPermisos;
        frmMain frmMain;
        bool superUsuario = false;
        public frmUsuarios(Conexion conexion, frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdPermisos = new PermisoUsuarioConexion();
            this.frmMain = frmMain;
            bd = new UsuarioConexion();
            Utilidad.configurarDataGrid(dgvDatos);
            this.dgvDatos.DataSource = this.bd.obtenerUsuarios();
            this.txtNombre.Visible =false;
            txtUsuario.Visible = false;

            superUsuario = Properties.Settings.Default.usuario.ToUpper().Trim().Equals(this.frmMain.usuarioName) && this.frmMain.usuarioId==-1;
        }

        #region menu
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            if (this.Tag.Equals("buscar"))
            {
                cargarDatosEditar();
                this.user = bd.obtenerUsuario(user.UsuarioId);
                if (this.user != null)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                    this.DialogResult = DialogResult.No;
            }
           else if (superUsuario || bdPermisos.existePermiso(this.frmMain.usuarioId,2, 1))
                    {
                        frmAddUsuario frm = new frmAddUsuario(conexion,frmMain);
                        frm.Tag = "nuevo";
                        frm.ShowDialog();
                        this.dgvDatos.DataSource = bd.obtenerUsuarios();
                    }
                    else
                        this.btnNuevo.Enabled = false;
              
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            //3 modificar
            if (this.superUsuario || bdPermisos.existePermiso(this.frmMain.usuarioId,2, 3))
            {
                if (dgvDatos.RowCount > 0)
                {
                    cargarDatosEditar();
                    frmAddUsuario frm = new frmAddUsuario(conexion, this.user,  frmMain);
                    frm.Tag = "modificar";
                    DialogResult result = frm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        this.dgvDatos.DataSource = bd.obtenerUsuarios();
                    }

                }
                else
                    this.btnModificar.Enabled = false;
            }
           
        }

        private void BtnQuitar_Click(object sender, EventArgs e)
        {
            //4 eliminar
            if (bdPermisos.existePermiso(this.frmMain.usuarioId,2, 4))
            {
                if (dgvDatos.RowCount > 0)
                {
                    cargarDatosEditar();
                    if (bd.eliminarUsuario(user.UsuarioId,frmMain.usuarioName))
                    {
                        MessageBox.Show("Usuario eliminado exitosamente", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.dgvDatos.DataSource = bd.obtenerUsuarios();
                    }
                    else
                    {
                        MessageBox.Show("No se puede eliminar el usuario", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.dgvDatos.DataSource = bd.obtenerUsuarios();
                    }

                }

            }
            else
                btnQuitar.Enabled = false;
            
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        #region eventos de busqueda
        private void RbNombre1_Click(object sender, EventArgs e)
        {
            this.txtNombre.Visible = true;
            this.txtNombre.Text = "";
            this.txtUsuario.Text = "";
           txtUsuario.Visible = false;
        }

        private void RbUsuario_Click(object sender, EventArgs e)
        {
            this.txtNombre.Visible = false;
            this.txtNombre.Text = "";
            this.txtUsuario.Text = "";
            txtUsuario.Visible = true;
        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                this.dgvDatos.DataSource = bd.obtenerUsuarios();
            }
            else
            {
                this.dgvDatos.DataSource = bd.buscarUsuarioNombre(txtNombre.Text);
            }
        }

        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.txtUsuario.Text))
            {
                this.dgvDatos.DataSource = bd.obtenerUsuarios();
            }
            else
            {
                this.dgvDatos.DataSource = bd.buscarUsuarios(this.txtUsuario.Text);
            }
        }
        #endregion

        #region cargar datos
        private void cargarDatosEditar()
        {
           
            if (dgvDatos.RowCount > 0)
            {
                this.user = new UsuarioData();
                int nlinea = dgvDatos.CurrentCell.RowIndex;
                user.UsuarioId = Convert.ToInt32(this.dgvDatos.Rows[nlinea].Cells["UsuarioId"].Value.ToString());
               
            }

        }
        #endregion

        private void frmUsuarios_Load(object sender, EventArgs e)
        {

            this.dgvDatos.DataSource = bd.obtenerUsuarios();
            Utilidad.configurarDataGrid(dgvDatos);
            if (this.Tag.Equals("buscar"))
            {
                this.btnNuevo.Text = "&Seleccionar";
                this.btnModificar.Visible = false;
                this.btnQuitar.Visible = false;
                this.btnSalir.Location = new Point(604, 132);
                this.AutoSize = true;
              
            }
        }

        private void dgvDatos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.Tag.Equals("buscar"))
            {
                cargarDatosEditar();
                this.user = bd.obtenerUsuario(user.UsuarioId);
                if (this.user != null)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                    this.DialogResult = DialogResult.No;
            }
        }
    }
}
