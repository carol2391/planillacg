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
using nomina.Clases.Descuentos;
using nomina.Clases.Labores;
using nomina.Clases.Utilidades;
using nomina.Clases.MovimiendoDescuentos;
using nomina.Forms.Main;
using nomina.Clases.PermisosUsuario;
using nomina.Clases.TipoJornada;
using nomina.Clases.TipoPago;

namespace nomina.Forms.Descuento
{
    public partial class frmDescuento : Form
    {
        Conexion conexion;
        List<DescuentoData> descuentos;
        DescuentoConexion bd;
        public DescuentoData descuento;
        PermisoUsuarioConexion bdPermisos;
        frmMain frmMain;

        public frmDescuento(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Descuentos");
            this.conexion = conexion;
            bd = new DescuentoConexion(conexion);
            bdPermisos = new PermisoUsuarioConexion();
            this.frmMain = frmMain;
            // agregarColumnasData();
            this.descuentos = bd.obtenerDescuentos();
            DescripcionTipoDescuento();
            dgvDescuentos.DataSource = this.descuentos;
            txtCodigo.Visible = false;
            txtNombre.Visible = false;
            Utilidad.configurarDataGrid(this.dgvDescuentos);
            panel1.BackColor = Color.SkyBlue;
            btnModificar.BackColor = btnNuevo.BackColor = btnSalir.BackColor = btnQuitar.BackColor = Color.SkyBlue;
        }

        #region agregar las columnas al datagrid
        private void agregarColumnasData()
        {

            DataGridViewColumn Id = new DataGridViewTextBoxColumn();
            Id.DataPropertyName = "Id";
            Id.Name = "Id";
            Id.Visible = false;
            this.dgvDescuentos.Columns.Add(Id);

            DataGridViewColumn codigo = new DataGridViewTextBoxColumn();
            codigo.DataPropertyName = "Codigo";
            codigo.Name = "Código";
            codigo.Visible = true;
            this.dgvDescuentos.Columns.Add(codigo);

            DataGridViewColumn nombre = new DataGridViewTextBoxColumn();
            nombre.DataPropertyName = "Nombre";
            nombre.Name = "Nombre";
            this.dgvDescuentos.Columns.Add(nombre);

            DataGridViewColumn TipoJornada = new DataGridViewTextBoxColumn();
            TipoJornada.DataPropertyName = "TipoJornada";
            TipoJornada.Name = "Tipo de Jornada";
            this.dgvDescuentos.Columns.Add(TipoJornada);

            DataGridViewColumn monto = new DataGridViewTextBoxColumn();
            monto.DataPropertyName = "Monto";
            monto.Name = "Monto";
            monto.Visible = false;
            this.dgvDescuentos.Columns.Add(monto);

            DataGridViewColumn factor = new DataGridViewTextBoxColumn();
            factor.DataPropertyName = "Factor";
            factor.Name = "Factor";
            factor.Visible = false;
            this.dgvDescuentos.Columns.Add(factor);

            DataGridViewColumn tipofactor = new DataGridViewTextBoxColumn();
            tipofactor.DataPropertyName = "TipoFactor";
            tipofactor.Name = "Tipo de Factor";
            tipofactor.Visible = false;
            this.dgvDescuentos.Columns.Add(tipofactor);

            DataGridViewColumn codigoCuenta = new DataGridViewTextBoxColumn();
            codigoCuenta.DataPropertyName = "codigoCuenta";
            codigoCuenta.Name = "Codigo de Cuenta";
            codigoCuenta.Visible = false;
            this.dgvDescuentos.Columns.Add(codigoCuenta);

        }
        #endregion
        #region eventos
        private void frmDescuento_Load(object sender, EventArgs e)
        {
           
            Utilidad.configurarDataGrid(this.dgvDescuentos);
            if (this.Tag.Equals("Buscar"))
            {
                this.btnNuevo.Text = "&Seleccionar";
                this.btnModificar.Visible = false;
                this.btnQuitar.Visible = false;
                this.btnSalir.Location = new Point(485, 131);
                this.AutoSize = true;
            }
        }

        private void rbCodigo_Click(object sender, EventArgs e)
        {
            txtCodigo.Visible = true;
            txtNombre.Visible = false;
        
            txtNombre.Text= txtCodigo.Text = "";
        }

        private void rbNombre_Click(object sender, EventArgs e)
        {
            txtCodigo.Visible = false;
            txtNombre.Visible = true;
            txtNombre.Text = txtCodigo.Text = "";
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
           
            if (String.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                this.descuentos = bd.obtenerDescuentos();
                DescripcionTipoDescuento();
                dgvDescuentos.DataSource = descuentos;
            }
            else {
                this.descuentos = bd.buscarDescuento(txtCodigo.Text.Trim(), "COD_DEC");
                DescripcionTipoDescuento();
                dgvDescuentos.DataSource = this.descuentos;
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                this.descuentos = bd.obtenerDescuentos();
                DescripcionTipoDescuento();
                dgvDescuentos.DataSource = descuentos;
            }
            else
            {
                this.descuentos = bd.buscarDescuento(txtNombre.Text.Trim(), "NOM_DEC");
                DescripcionTipoDescuento();
                dgvDescuentos.DataSource = this.descuentos;
            }
        }

        private void dgvCategorias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarDatosEditar();
            frmAddDescuento frm = new frmAddDescuento(conexion,this.descuento);
            frm.Tag = "ver";
            frm.bloquearControles();
            frm.ShowDialog();
        }
       #endregion

        #region menu
        private void btnNuevo_Click(object sender, EventArgs e)
        {

            
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 29))
            //{
                 frmAddDescuento frm = new frmAddDescuento(conexion);
                    frm.Tag = "agregar";
                    frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //{
                 dgvDescuentos.DataSource = bd.obtenerDescuentos();

            //}
            //    }
            //else
            //    btnNuevo.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 30))
                editar();
            //else
            //    btnModificar.Enabled = false;
        }

        private void bntQuitar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 31)) {
                
                eliminar();
           // }
           //else
           //     bntQuitar.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        #region actualiza la lista de descuentos y la asigna al datagrid
        private void actualizarObjetos()
        {
            this.descuentos = bd.obtenerDescuentos();
            DescripcionTipoDescuento();
            this.dgvDescuentos.DataSource = descuentos;
        }
        #endregion

        #region editar
        private void editar()
        {
            
            if (dgvDescuentos.RowCount > 0)
            {
                this.cargarDatosEditar();
                frmAddDescuento frm = new frmAddDescuento(conexion, descuento);
                frm.Tag = "modificar";
                DialogResult result = frm.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    /*
                      le asigna a la lista de descuentos, los descuentos de la bd y al datagrid le asigna esa lista
                     */
                    dgvDescuentos.DataSource = bd.obtenerDescuentos();
                }

            }
        }
        #endregion

        #region eliminar
        private void eliminar()
        {
            MDescuentoConexion bdMLabores = new MDescuentoConexion(conexion);
            if (dgvDescuentos.RowCount > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar el descuento?", "Eliminar Descuento", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    int nlinea = dgvDescuentos.CurrentCell.RowIndex;
                    int id = Convert.ToInt32(this.dgvDescuentos.Rows[nlinea].Cells["Id"].Value.ToString());
                    

                    //if (!bdMLabores.existeDescuentoEnMDescuentos(codigo)) {
                        if (bd.accionesDescuento("E", id, "", "", (decimal)0.00,
                        0, 0,0, 0))
                        {
                          dgvDescuentos.DataSource = bd.obtenerDescuentos();
                    
                          MessageBox.Show("Descuento eliminado exitosamente", "Eliminar Descuento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    else
                    {
                        MessageBox.Show("No se puede eliminar, porque tiene movimientos", "Eliminar Descuento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //}



                    /* else
                         MessageBox.Show("Hay empleados que tienen asignada está categoria, no se puede eliminar", "Eliminar Labor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     */
                }
            }
        }
        #endregion

        #region obtener el id del descuento, con el id que seleccione el usuario en el datagrid
        private void cargarDatosEditar()
        {
            if (dgvDescuentos.RowCount > 0)
            {
                this.descuento = new DescuentoData();
                int nlinea = dgvDescuentos.CurrentCell.RowIndex;
                this.descuento.Id = Convert.ToInt32(dgvDescuentos.Rows[nlinea].Cells["Id"].Value.ToString());
            }
        }
        #endregion

 
        private void dgvDescuentos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        #region obtener descuento para editar
        public DescuentoData obtenerDescuento()
        {

            if (dgvDescuentos.RowCount > 0)
            {
                this.descuento = new DescuentoData();
                int nlinea = dgvDescuentos.CurrentCell.RowIndex;
                this.descuento.Id = Convert.ToInt32(this.dgvDescuentos.Rows[nlinea].Cells["Id"].Value.ToString());
                this.descuento = bd.obtenerDescuento(descuento.Id);
                return descuento;
            }
            return null;
        }
        #endregion

        private void dgvDescuentos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (this.Tag.Equals("buscar"))
            {
                this.descuento = this.obtenerDescuento();
                if (this.descuento != null)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                    this.DialogResult = DialogResult.No;

            }
        }

        #region descripion DEL TIPO DE PAGO DEL DESCUENTO
        private void DescripcionTipoDescuento()
        {

            foreach (DescuentoData descuento in this.descuentos)
            {
                switch (descuento.TipoPago.Codigo[0])
                {
                    case 'D':
                        descuento.TipoPago.descripcion = "Definido por el usuario";
                        break;

                    case 'F':
                        descuento.TipoPago.descripcion = "Por Factor";
                        break;

                    case 'H':
                        descuento.TipoPago.descripcion = "Por Hora";
                        break;

                    case 'V':
                        descuento.TipoPago.descripcion = "Por Valor";
                        break;
                }
            }
        }
        #endregion
    }
}
