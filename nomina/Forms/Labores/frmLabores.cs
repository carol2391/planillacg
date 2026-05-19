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
using nomina.Forms.Main;
using nomina.Clases.Labores;
using nomina.Clases.Utilidades;
using nomina.Clases.MovimientoLabores;

using nomina.Clases.PermisosUsuario;
using nomina.Forms.Main;

namespace nomina.Forms.Labores
{
    public partial class frmLabores : Form
    {
        #region propiedades
        Conexion conexion;
        List<LaboresData> labores = new List<LaboresData>();
        LaboresConexion bdLabores;
        public LaboresData labor;
        MLaboresConexion bdMLabores;
        PermisoUsuarioConexion bdPermisos;
        frmMain frmMain;
        #endregion

        public frmLabores(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Labores");
            panel1.BackColor = Color.SkyBlue;
            btnModificar.BackColor = btnNuevo.BackColor = btnQuitar.BackColor = btnSalir.BackColor = Color.SkyBlue;
            bdLabores = new LaboresConexion(conexion);
            bdPermisos = new PermisoUsuarioConexion();
            this.frmMain = frmMain;
            this.bdMLabores = new MLaboresConexion(conexion);
            labores = bdLabores.obtenerLabores();
            descripcionTipoLabor();
            this.conexion = conexion;
            txtCodigo.Visible = false;
            txtNombre.Visible = false;
        }
        #region eventos
        private void FrmLabores_Load(object sender, EventArgs e)
        {
            dgvLabores.DataSource = this.labores;
            Utilidad.configurarDataGrid(this.dgvLabores);
            if (this.Tag.Equals("Buscar"))
            {
                this.btnNuevo.Text = "&Seleccionar";
                this.btnModificar.Visible = false;
                this.btnQuitar.Visible = false;
                this.btnSalir.Location = new Point(485, 131);
                this.AutoSize = true;
            }
        }
        private void TxtCodigo_TextChanged(object sender, EventArgs e)
        {
           // LaboresConexion bd = new LaboresConexion(conexion);
           this.labores = bdLabores.buscarLabor(txtCodigo.Text.Trim(),"COD_LAB");
            descripcionTipoLabor();
            dgvLabores.DataSource = this.labores;
            if (String.IsNullOrWhiteSpace(txtCodigo.Text))
            {   this.labores = bdLabores.obtenerLabores();
                descripcionTipoLabor();
                dgvLabores.DataSource = this.labores;
            }
        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {
            //LaboresConexion bd = new LaboresConexion(conexion);
            this.labores = bdLabores.buscarLabor(txtNombre.Text.Trim(),"NOM_LAB");
            descripcionTipoLabor();
            dgvLabores.DataSource = this.labores;
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                this.labores = bdLabores.obtenerLabores();
                descripcionTipoLabor();
                dgvLabores.DataSource = this.labores;
            }
        }

        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        #endregion

        #region menu
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            //if (this.Tag.Equals("Buscar"))
            //{
            //    this.labor = this.obtenerLabor();
            //    if (this.labor != null)
            //    {
            //        this.DialogResult = DialogResult.OK;
            //    }
            //    else
            //        this.DialogResult = DialogResult.No;
            //}
            //else
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 25))
            //{
                    frmAddLabores frm = new frmAddLabores(conexion);
                    frm.Tag = "agregar";
                    frm.ShowDialog();
                    //if (frm.DialogResult == DialogResult.OK)
                    //{
                        this.actualizarObjetos();
                        this.refrescarControles();
                    //}
            //}else
            //    btnNuevo.Enabled = false;
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 26))
                editar();
            //else
            //    btnModificar.Enabled = false;
        }

        private void BntQuitar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 26))
               eliminar();
            //else btnQuitar.Enabled = false;
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void RbCodigo_Click(object sender, EventArgs e)
        {
            txtNombre.Visible = false;
            txtCodigo.Visible = true;
            txtNombre.Text = "";
        }

        private void RbNombre_Click(object sender, EventArgs e)
        {
            txtNombre.Visible = true;
            txtCodigo.Visible = false;
            txtCodigo.Text = "";
        }
        #endregion

        #region obtener labor para editar
        public LaboresData obtenerLabor()
        {
            
            if (dgvLabores.RowCount > 0)
            {
                this.labor = new LaboresData();
                int nlinea = dgvLabores.CurrentCell.RowIndex;
                this.labor.Id = Convert.ToInt32(this.dgvLabores.Rows[nlinea].Cells["Id"].Value.ToString());
                this.labor = bdLabores.obtenerLabor(labor.Id);
                return labor;
            }
            return null;
        }
        #endregion
        #region actualiza los objetos y vuelve a cargar los objetos a los controles
        private void actualizarObjetos()
        {
            LaboresConexion bd = new LaboresConexion(conexion);
            this.labores = bd.obtenerLabores();
            descripcionTipoLabor();
            dgvLabores.DataSource = this.labores;
        }


        public void refrescarControles()
        {
            
        }

        #region ver labor

        /* cuando le da doble click sobre una celda se muestra el formulario de editar
           en el objeto labor solo va el id, en el formulario agregar labores en el constructor
           de editar se hace una consulta  a la bd para obtener la labor que se va editar o ver
             */
        private void DgvLabores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarDatosEditar();
            frmAddLabores frm = new frmAddLabores(conexion, labor);
            frm.Tag = "ver";
            frm.bloquearControles();
            frm.ShowDialog();
        }

        #endregion
        #endregion

        #region editar
        private void editar()
        {
            this.cargarDatosEditar();
            if (dgvLabores.RowCount > 0)
            {
                frmAddLabores frm = new frmAddLabores(conexion, labor);
                frm.Tag = "modificar";
                DialogResult result = frm.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    //this.frmMain.cargarBaseDeDatos();
                    //actualiza la lista de labores
                    this.actualizarObjetos();
                    //actualiza el data grid
                    this.refrescarControles();
                }

            }
        }
        #endregion 

        #region eliminar
        private void eliminar()
        {
            if (dgvLabores.RowCount > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar la labor?", "Eliminar labor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    int nlinea = dgvLabores.CurrentCell.RowIndex;
                    int id = Convert.ToInt32(this.dgvLabores.Rows[nlinea].Cells["Id"].Value.ToString());
                    string codigo =this.dgvLabores.Rows[nlinea].Cells["Codigo"].Value.ToString();

                    //if (!bdMLabores.existelaborEnMLabores(codigo))
                    //{
                        LaboresConexion bd = new LaboresConexion(conexion);
                        if (bd.accionesLabor("E",id, txtCodigo.Text, txtNombre.Text, 0,
                              0, 0, 0, ""))
                        {
                        this.dgvLabores.DataSource = bdLabores.obtenerLabores();
                            MessageBox.Show("Labor eliminada exitosamente", "Eliminar Labor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    else
                    {
                        MessageBox.Show("No se puede eliminar, porque tiene movimientos", "Eliminar Labor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //
                }
                    
            }
        }
        #endregion

        #region obtener el id de la labor con el id que seleccione el usuario en el datagrid
        private void cargarDatosEditar()
        {
            this.labor = new LaboresData();
            if (dgvLabores.RowCount > 0)
            {
                int nlinea = dgvLabores.CurrentCell.RowIndex;
                this.labor.Id = Convert.ToInt32(dgvLabores.Rows[nlinea].Cells["Id"].Value.ToString());
            }
        }

        #endregion

        private void dgvLabores_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.Tag.Equals("Buscar"))
            {
                this.labor = this.obtenerLabor();
                if (this.labor != null)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                    this.DialogResult = DialogResult.No;

            }
        }
        #region descripion de la labor
        private void descripcionTipoLabor() {
           
                foreach (LaboresData mLabor in this.labores)
                {
                    switch (mLabor.TipoPago.descripcion[0])
                    {
                        case 'D':
                            mLabor.TipoPago.descripcion = "Definido por el usuario";
                            break;

                        case 'F':
                        mLabor.TipoPago.descripcion = "Por Factor";
                            break;

                        case 'H':
                        mLabor.TipoPago.descripcion = "Por Hora";
                            break;

                        case 'V':
                        mLabor.TipoPago.descripcion = "Por Valor";
                            break;
                    }
                }
            }
            #endregion
        }
}
