using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Clases.Utilidades;
using nomina.Clases.Movimientos.MovimientoAumento;
using nomina.Clases.ConexionManager;
using nomina.Clases.Empleado;
using nomina.Forms.Main;
using nomina.Clases.PermisosUsuario;

namespace nomina.Forms.Movimientos.MovimientoAumentos
{
    public partial class frmMovimientoAumentos : Form
    {
        Conexion conexion;
        List<AumentoData> listaAumentos;
        AumentoConexion bdAumento;
        AumentoData aumentoData;
        PermisoUsuarioConexion bdPermisos;
        frmMain frmMain;
        EmpleadoData empleado;
        public frmMovimientoAumentos(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdAumento = new AumentoConexion(conexion);
            bdPermisos = new PermisoUsuarioConexion();
            this.frmMain = frmMain;
            Utilidad.configurarDataGrid(dgvDatos);
            txtCodigo.Select();
            this.dtpFechaInicial.Visible = false;
            this.dtpFechaFinal.Visible = false;
            dgvDatos.DataSource = bdAumento.obtenerAumentos();
        }

        #region eventos tecla enter
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void cbFechaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpFechaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void cbFechaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpFechaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }
        #endregion

        #region eventos checkbox
        private void cbFechaInicial_CheckedChanged(object sender, EventArgs e)
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

        private void cbFechaFinal_CheckedChanged(object sender, EventArgs e)
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

        #region menu
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                EmpleadoConexion bd = new EmpleadoConexion(conexion);
                 empleado = bd.obtenerEmpleadoCodigo(txtCodigo.Text.Trim());
                if (empleado.Codigo == null)
                    MessageBox.Show("Error no existe el empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    buscarAumentos();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 45)) {
                frmAddAumento frm = new frmAddAumento(conexion);
                frm.Tag = "agregar";
                frm.ShowDialog();
              dgvDatos.DataSource = bdAumento.obtenerAumentos();
            //}else
            //    btnNuevo.Enabled = false;

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 46))
            //{
                if (dgvDatos.RowCount > 0)
                {
                    DateTime fechaActual = DateTime.Now;
                    int mesActual = fechaActual.Month;
                    int añoActual = fechaActual.Year;
                    this.cargarDatosEditar();
                    if (this.aumentoData.Fecha.Month == mesActual && this.aumentoData.Fecha.Year == añoActual)
                    {

                        frmAddAumento frm = new frmAddAumento(conexion, aumentoData,frmMain);
                        frm.Tag = "modificar";
                        DialogResult result = frm.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                        ///cargar el datagrid con la información
                            dgvDatos.DataSource = bdAumento.obtenerAumentos();
                         }

                    }
                    else

                        MessageBox.Show("Solo puede actualizar los aumentos de este mes y año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            //}else

            //    btnModificar.Enabled = false;


        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 47))
                eliminar();
            //else
            //    btnQuitar.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
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

        #region buscar movimiento de aumento s segun el codigo y el rango de fecha

        public void buscarAumentos()
        {

            if (cbFechaInicial.Checked && cbFechaFinal.Checked)
            {
                listaAumentos = bdAumento.buscarAumentos(empleado.Id,
                                              this.dtpFechaInicial.Value.Date,
                                              this.dtpFechaFinal.Value.Date);

                //descripcionTipoAumento();
                this.dgvDatos.DataSource = listaAumentos;

            }
            else
                 if (cbFechaInicial.Checked)
            {
                DateTime fFinal = new DateTime();
                listaAumentos = bdAumento.buscarAumentos(empleado.Id,
                                        this.dtpFechaInicial.Value.Date,
                                         fFinal);
                //descripcionTipoAumento();
                this.dgvDatos.DataSource = listaAumentos;
                //this.dtpFechaFinal.Visible = false;
            }
            
        }
        #endregion

        //#region descripcion tipo descuento
        //private void descripcionTipoAumento()
        //{

        //    foreach (AumentoData aumento in listaAumentos)
        //    {
        //        switch (aumento.TipoAumento[0])
        //        {
        //            case 'M':
        //                aumento.TipoAumento = "Monto Fijo";
        //                break;

        //            case 'P':
        //                aumento.TipoAumento = "Porcentaje";
        //                break;
        //        }
        //    }
        //}
        //#endregion

        #region instanciar el objeto aumento con la fila seleccionada del datagrid
        private void cargarDatosEditar()
        {
            if (dgvDatos.RowCount > 0)
            {
                this.aumentoData = new AumentoData();
                int nlinea = dgvDatos.CurrentCell.RowIndex;
                this.aumentoData.CodigoEmpleado = this.dgvDatos.Rows[nlinea].Cells["CodigoEmpleado"].Value.ToString();
                this.aumentoData.Id = Convert.ToInt32(this.dgvDatos.Rows[nlinea].Cells["Id"].Value.ToString());
                //this.aumentoData.CodigoCategoria = this.dgvDatos.Rows[nlinea].Cells["CodigoCategoria"].Value.ToString();

                string sFecha = this.dgvDatos.Rows[nlinea].Cells["Fecha"].Value.ToString();
                string[] fechaSplit = sFecha.Split('/');
                string año = fechaSplit[2].Substring(0, 4);
                this.aumentoData.Fecha = new DateTime(Convert.ToInt32(año), Convert.ToInt32(fechaSplit[1]), Convert.ToInt32(fechaSplit[0]));          
            }

        }
        #endregion

        #region eliminar
        public void eliminar()
        {

            if (dgvDatos.RowCount > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar la labor?", "Eliminar labor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    cargarDatosEditar();
                    DateTime fechaActual = DateTime.Now;
                    int mesActual = fechaActual.Month;
                    int añoActual = fechaActual.Year;
                    if (this.aumentoData.Fecha.Month == mesActual && this.aumentoData.Fecha.Year == añoActual)
                    {
                        bool elimino = bdAumento.accionesAumentos("E",aumentoData.Id, 0,0,
                                     dtpFechaInicial.Value.Date,
                                    (decimal)0.00, (decimal)0.00, 1,
                                     (decimal)0.00, (decimal)0.00,
                                     (decimal)0.00,
                                     " ");

                        if (elimino)
                        {
                            MessageBox.Show("Aumento eliminado exitosamente", "Eliminar Aumento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvDatos.DataSource = bdAumento.obtenerAumentos();
                        }
                        else {
                            MessageBox.Show("No se puede eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                        }

                    }
                    else
                        MessageBox.Show("Solo puede eliminar los aumentos de este mes y año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        #endregion

}
}
