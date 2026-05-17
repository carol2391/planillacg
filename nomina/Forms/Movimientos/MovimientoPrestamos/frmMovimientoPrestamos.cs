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
using nomina.Clases.Utilidades;
using nomina.Clases.Movimientos.MovimientoPrestamo;
using nomina.Clases.Empleado;
using nomina.Forms.Main;
using nomina.Clases.PermisosUsuario;

namespace nomina.Forms.Movimientos.MovimientoPrestamos
{
    public partial class frmMovimientoPrestamos : Form
    {
        Conexion conexion;
        PrestamoData prestamoData;
        List<PrestamoData> listaPrestamos;
        PrestamoConexion bdPrestamo;
        PermisoUsuarioConexion bdPermisos;
        frmMain frmMain;
        EmpleadoData empleado;

        public frmMovimientoPrestamos(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdPrestamo= new PrestamoConexion(conexion);
            bdPermisos = new PermisoUsuarioConexion();
            this.frmMain = frmMain;
            this.dtpFechaInicial.Visible = false;
            this.dtpFechaFinal.Visible = false;
            Utilidad.configurarDataGrid(dgvDatos);
        }
        #region evento tecla enter
        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void CbFechaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void DtpFechaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void CbFechaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void DtpFechaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void BtnBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }


        #endregion

        #region menu
        private void BtnBuscar_Click(object sender, EventArgs e)
        {

            if (validar())
            {
                EmpleadoConexion bd = new EmpleadoConexion(conexion);
                empleado = bd.obtenerEmpleadoCodigo(txtCodigo.Text.Trim());
                if (empleado.Codigo == null)
                    MessageBox.Show("Error no existe el empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    buscarPrestamos();

            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 49)) {
               frmAddMovimientoPrestamos frm = new frmAddMovimientoPrestamos(conexion,frmMain);
                frm.Tag = "agregar";
                frm.ShowDialog();
            //}
            //else
            //    btnNuevo.Enabled = false;


        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 50))
            //{
                modificar();
            //}
            //else
            //    btnModificar.Enabled = false;

        }

        private void BtnQuitar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 51))
            //{
                eliminar();
            //}
            //else
            //    btnQuitar.Enabled = false;
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        #region eventos check box
        private void CbFechaInicial_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbFechaInicial.Checked)
            {
                this.dtpFechaInicial.Visible = false;
            
            }
            else
            {
                this.dtpFechaInicial.Visible = true;
                
            }
        }

        private void CbFechaFinal_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbFechaFinal.Checked)
            {
                this.dtpFechaFinal.Visible = false;
            }

            else
            {
                this.dtpFechaFinal.Visible = true;
            }
        }
        #endregion
        #region modificar
        public void modificar() {
            if (dgvDatos.RowCount > 0)
            {
                DateTime fechaActual = DateTime.Now;
                int mesActual = fechaActual.Month;
                int añoActual = fechaActual.Year;
                this.cargarDatosEditar();
                if (this.prestamoData.Fecha.Month == mesActual && this.prestamoData.Fecha.Year == añoActual)
                {

                    frmAddMovimientoPrestamos frm = new frmAddMovimientoPrestamos(conexion, prestamoData);
                    frm.Tag = "modificar";
                    DialogResult result = frm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        ///cargar el datagrid con la información
                        buscarPrestamos();
                    }

                }
                else

                    MessageBox.Show("Solo puede actualizar los prestamos de este mes y año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        #endregion 

        #region eliminar
        public void eliminar()
        {

            if (dgvDatos.RowCount > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar el Prestamo?", "Eliminar Prestamo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    cargarDatosEditar();
                    DateTime fechaActual = DateTime.Now;
                    int mesActual = fechaActual.Month;
                    int añoActual = fechaActual.Year;
                    if (this.prestamoData.Fecha.Month == mesActual && this.prestamoData.Fecha.Year == añoActual)
                    {
                        bool elimino = bdPrestamo.accionesPrestamos("E",prestamoData.Id, empleado.Id, "", dtpFechaInicial.Value.Date,
                                        "", 0, (decimal)0.00, (decimal)0.00,
                                        0," ");

                        if (elimino)
                        {
                            MessageBox.Show("Prestamo eliminado exitosamente", "Eliminar Prestamo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.buscarPrestamos();
                        }
                        else
                        {
                            MessageBox.Show("No se puede eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                        MessageBox.Show("Solo puede eliminar los prestamos de este mes y año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

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
                MessageBox.Show("Seleccione la fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cbFechaInicial.Checked && cbFechaFinal.Checked)
            {
                if (this.dtpFechaInicial.Value.Date > this.dtpFechaFinal.Value.Date)
                {
                    MessageBox.Show("La fecha Inicial no puede ser mayor que la final", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }

            return true;
        }
        #endregion

        #region buscar movimiento de prestamos segun el codigo y el rango de fecha

        public void buscarPrestamos()
        {

            if (cbFechaInicial.Checked && cbFechaFinal.Checked)
            {
                listaPrestamos = bdPrestamo.buscarPrestamos(empleado.Id,
                                              this.dtpFechaInicial.Value.Date,
                                              this.dtpFechaFinal.Value.Date);

                descripciones();
                this.dgvDatos.DataSource = listaPrestamos;

            }
            else
                 if (cbFechaInicial.Checked)
            {
                DateTime fFinal = new DateTime();
                listaPrestamos = bdPrestamo.buscarPrestamos(empleado.Id,
                                        this.dtpFechaInicial.Value.Date,
                                         fFinal);
                descripciones();
                this.dgvDatos.DataSource = listaPrestamos;
                //this.dtpFechaFinal.Visible = false;
            }

        }
        #endregion

        #region descripcion tipo descuento
        private void descripciones()
        {

            //foreach (PrestamoData prestamo in listaPrestamos)
            //{
            //    switch (prestamo.TipoPago[0])
            //    {
            //        case 'Q':
            //            prestamo.TipoPago = "Quincenal";
            //            break;

            //        case 'M':
            //            prestamo.TipoPago = "Mensual";
            //            break;
            //    }

            //    switch (prestamo.Estado[0])
            //    {
            //        case 'A':
            //            prestamo.Estado= "Activo";
            //            break;

            //        case 'I':
            //            prestamo.Estado = "Inactivo";
            //            break;
            //    }
            //}
        }
        #endregion

        #region instanciar el objeto prestamo con la fila seleccionada del datagrid
        private void cargarDatosEditar()
        {
            this.prestamoData= new PrestamoData();
            if (dgvDatos.RowCount > 0)
            {
                int nlinea = dgvDatos.CurrentCell.RowIndex;
                prestamoData.CodigoEmpleado = this.dgvDatos.Rows[nlinea].Cells["CodigoEmpleado"].Value.ToString();
                prestamoData.Id = Convert.ToInt32(this.dgvDatos.Rows[nlinea].Cells["Id"].Value.ToString());

                string sFecha = this.dgvDatos.Rows[nlinea].Cells["fecha"].Value.ToString();
                string[] fechaSplit = sFecha.Split('/');
                string año = fechaSplit[2].Substring(0, 4);
                prestamoData.Fecha = new DateTime(Convert.ToInt32(año), Convert.ToInt32(fechaSplit[1]), Convert.ToInt32(fechaSplit[0]));
  
            }

        }
        #endregion

    }
}
