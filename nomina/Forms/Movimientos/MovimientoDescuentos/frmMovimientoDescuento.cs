using nomina.Clases.ConexionManager;
using nomina.Clases.Empleado;
using nomina.Clases.MovimiendoDescuentos;
using nomina.Clases.MovimientoLabores;
using nomina.Clases.PermisosUsuario;
using nomina.Clases.UsuarioPermisos;
using nomina.Clases.Utilidades;
using nomina.Forms.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace nomina.Forms.MovimientoDescuentos
{
    public partial class frmMovimientoDescuento : Form
    {
        #region propiedades
        Conexion conexion;
        MDescuentoConexion bdMDescuentos;
        //List<MLaboresConexion> lMlabores = new List<MLaboresConexion>();
        MDescuentoData mDescuento = new MDescuentoData();
        /*para sacar la fecha actual*/
        DateTime fechaDescuento;
        decimal total;
        List<MDescuentoData> listaMovDescuentos = new List<MDescuentoData>();
        PermisoUsuarioConexion bdPermisos;
        frmMain frmMain;
        #endregion

        public frmMovimientoDescuento(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdMDescuentos = new MDescuentoConexion(conexion);
            bdPermisos = new PermisoUsuarioConexion();
            this.dgvDescuentos.AutoGenerateColumns = false;
            this.frmMain = frmMain;
            Utilidad.configurarDataGrid(dgvDescuentos);
            txtCodigo.Select();
            this.dtpFechaInicial.Enabled = false;
            this.dtpFechaFinal.Enabled = false;
            listaMovDescuentos = bdMDescuentos.obtenerMDescuentos();
            DescripcionTipoDescuento();
            dgvDescuentos.DataSource = listaMovDescuentos;
        }

        #region menu
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                EmpleadoConexion bd = new EmpleadoConexion(conexion);
                EmpleadoData emp = bd.obtenerEmpleadoCodigo(txtCodigo.Text.Trim());
                if (emp.Codigo == null)
                    MessageBox.Show("Error no existe el empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    buscarMDescuentos();

            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 37)) {
               frmAddMovimientoDescuento frm = new frmAddMovimientoDescuento(conexion,frmMain);
               frm.ShowDialog();
               dgvDescuentos.DataSource = bdMDescuentos.obtenerMDescuentos();
            //}
            //else
            //    btnNuevo.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 38))
            //{
            cargarDatosEditar();
            if (dgvDescuentos.RowCount > 0)
                {

                    DateTime fechaActual = DateTime.Now;
                    int mesActual = fechaActual.Month;
                    int añoActual = fechaActual.Year;
                    if (this.fechaDescuento.Month == mesActual && this.fechaDescuento.Year == añoActual)
                    {
                        
                        frmModificarMovimientoDescuento frm = new frmModificarMovimientoDescuento(conexion, mDescuento);
                        frm.ShowDialog();
                        if (DialogResult.OK == frm.DialogResult)
                        {
                            buscarMDescuentos();
                         }
                    }
                    else
                        MessageBox.Show("Solo puede modificar los descuentos de este mes y año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            //}
            //else
            //    btnModificar.Enabled = false;

        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 39))
            //{
                eliminar();
            //}
            //else
            //    btnQuitar.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        

        private void cbFechaInicial_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbFechaInicial.Checked)
                this.dtpFechaInicial.Enabled = false;
            else
                this.dtpFechaInicial.Enabled = true;
        }

        private void cbFechaFinal_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbFechaFinal.Checked)
                this.dtpFechaFinal.Enabled = false;
            else
                this.dtpFechaFinal.Enabled = true;
        }

        #endregion


        #region validar
        private bool validar()
        {

            if (String.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Ingrese el código de Empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!this.cbFechaInicial.Checked && !this.cbFechaFinal.Checked)
            {
                MessageBox.Show("Seleccione la fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cbFechaInicial.Checked && cbFechaFinal.Checked)
            {
                if (this.dtpFechaInicial.Value.Date > this.dtpFechaFinal.Value.Date)
                {
                    MessageBox.Show("La fecha Inicial no puede ser mayor que la final", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    if (this.dtpFechaFinal.Value.Date < this.dtpFechaInicial.Value.Date)
                    {
                        MessageBox.Show("La fecha Final no puede ser menor que la Inicial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion

        #region buscar movimiento de descuentos segun el codigo y el rango de fecha

        public void buscarMDescuentos()
        {

            if (cbFechaInicial.Checked && cbFechaFinal.Checked)
            {
                listaMovDescuentos = bdMDescuentos.buscarMDescuentos(txtCodigo.Text,
                                              this.dtpFechaInicial.Value.Date,
                                              this.dtpFechaFinal.Value.Date);

                DescripcionTipoDescuento();
                this.dgvDescuentos.DataSource = listaMovDescuentos;
                sumarTotal();
            }
            else
                 if (cbFechaInicial.Checked)
            {
                DateTime fFinal = new DateTime();
                listaMovDescuentos = bdMDescuentos.buscarMDescuentos(txtCodigo.Text,
                                        this.dtpFechaInicial.Value.Date,
                                         fFinal);
                DescripcionTipoDescuento();
                this.dgvDescuentos.DataSource = listaMovDescuentos;
                sumarTotal();
            }
            else
                     if (cbFechaFinal.Checked)
            {
                DateTime fInicial = new DateTime();
                DescripcionTipoDescuento();
                listaMovDescuentos = bdMDescuentos.buscarMDescuentos(txtCodigo.Text,
                                                      fInicial, this.dtpFechaFinal.Value.Date);
                this.dgvDescuentos.DataSource = listaMovDescuentos;
                sumarTotal();
            }
        }

        #endregion

        private void DescripcionTipoDescuento()
        {

            foreach (MDescuentoData mDescuento in listaMovDescuentos)
            {
                switch (mDescuento.TipoDescuento[0])
                {
                    case 'D':
                        mDescuento.TipoPagoD = "Definido por el usuario";
                        break;

                    case 'F':
                        mDescuento.TipoPagoD = "Por Factor";
                        break;

                    case 'H':
                        mDescuento.TipoPagoD = "Por Hora";
                        break;

                    case 'V':
                        mDescuento.TipoPagoD = "Por Valor";
                        break;

                }
            }
        }
        #region descripcion tipo descuento
        private void descripcionTipoLabor()
        {

            //foreach (MDescuentoData mdescuento in listaMovDescuentos)
            //{
            //    switch (mdescuento.TipoDescuento[0])
            //    {
            //        case 'D':
            //            mdescuento.objTipoPago.descripcion = "Definido por el usuario";
            //            break;

            //        case 'F':
            //            mdescuento.TipoDescuento = "Por Factor";
            //            break;

            //        case 'H':
            //            mdescuento.TipoDescuento = "Por Hora";
            //            break;

            //        case 'V':
            //            mdescuento.TipoDescuento = "Por Valor";
            //            break;

            //    }
            //}
        }
        #endregion

        #region calcular total cuando busca descuentos

        public void sumarTotal()
        {
            decimal total = 0;
            foreach (MDescuentoData mDescuento in listaMovDescuentos)
            {
                total += mDescuento.Total;
            }
            this.nudTotal.Value = total;
        }
        #endregion

        #region  cargar datos en el formulario editar
        private void cargarDatosEditar()
        {
            if (dgvDescuentos.RowCount > 0)
            {
                mDescuento = new MDescuentoData();
                mDescuento.objEmpleado = new EmpleadoData();
                mDescuento.objDescuento = new Clases.Descuentos.DescuentoData();
                int nlinea = dgvDescuentos.CurrentCell.RowIndex;
                mDescuento.idMDescuento = Convert.ToInt32(this.dgvDescuentos.Rows[nlinea].Cells["idMDescuento"].Value.ToString());
                mDescuento.objEmpleado.Id= Convert.ToInt32(this.dgvDescuentos.Rows[nlinea].Cells["idEmpleado"].Value.ToString());
                mDescuento.objEmpleado.Codigo = this.dgvDescuentos.Rows[nlinea].Cells["CodigoEmpleado"].Value.ToString();
                mDescuento.objEmpleado.Nombre = this.dgvDescuentos.Rows[nlinea].Cells["NombreEmpleado"].Value.ToString();

                mDescuento.objDescuento.Id = Convert.ToInt32( this.dgvDescuentos.Rows[nlinea].Cells["idDescuento"].Value.ToString());
                mDescuento.idMDescuento = Convert.ToInt32(this.dgvDescuentos.Rows[nlinea].Cells["idMDescuento"].Value.ToString());
                string sFecha = this.dgvDescuentos.Rows[nlinea].Cells["FechaDescuento1"].Value.ToString();
                string[] fechaSplit = sFecha.Split('/');
                string año = fechaSplit[2].Substring(0, 4);
                this.fechaDescuento = new DateTime(Convert.ToInt32(año), Convert.ToInt32(fechaSplit[1]), Convert.ToInt32(fechaSplit[0]));
                mDescuento.TipoPagoD = this.dgvDescuentos.Rows[nlinea].Cells["TipoPagoD"].Value.ToString();
                mDescuento.FechaDescuento = fechaDescuento;
            }
        }
        #endregion

        #region
        public void eliminar() {

            if (dgvDescuentos.RowCount > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar la labor?", "Eliminar labor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    cargarDatosEditar();
                    DateTime fechaActual = DateTime.Now;
                    int mesActual = fechaActual.Month;
                    int añoActual = fechaActual.Year;
                    if (this.fechaDescuento.Month == mesActual && this.fechaDescuento.Year == añoActual)
                    {
                        bool elimino = bdMDescuentos.accionesDescuento("E",mDescuento.idMDescuento,mDescuento.objEmpleado.Id, mDescuento.objDescuento.Id, 
                            "",
                           0,
                           (decimal)0.00, mDescuento.FechaDescuento, (decimal)0.00, 0);

                        if (elimino)
                        {
                            MessageBox.Show("Descuento eliminado exitosamente", "Eliminar Desuento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvDescuentos.DataSource = bdMDescuentos.obtenerMDescuentos();
                        }
                        else
                        {
                            MessageBox.Show("No se puede eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                        MessageBox.Show("Solo puede eliminar los descuentos de este mes y año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        #endregion

        private void frmMovimientoDescuento_Shown(object sender, EventArgs e)
        {
            Validator.validarPermisos(this.frmMain.usuarioId, btnNuevo, btnModificar, btnQuitar, btnSalir, this, 9);
        }
    }
}
