using System;
using System.Collections.Generic;
using System.Windows.Forms;
using nomina.Clases.Ausencia;
using nomina.Clases.ConexionManager;
using nomina.Clases.Utilidades;
using nomina.Forms.Empleado;
using nomina.Clases.Empleado;
using nomina.Forms.Main;
using nomina.Clases.PermisosUsuario;
using nomina.Clases.UsuarioPermisos;

namespace nomina.Forms.Movimientos.Ausencia
{
    public partial class frmAusencias : Form
    {
        Conexion conexion;
        List<AusenciaData> listaMovAusencia;
        AusenciaConexion bdAusencia;
        AusenciaData ausenciaData;
        PermisoUsuarioConexion bdPermisos;
        frmMain frmMain;
        EmpleadoData empleado;
        public frmAusencias(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdAusencia = new AusenciaConexion(conexion);
            Utilidad.configurarDataGrid(dgvDatos);
            txtCodigo.Select();
            this.dtpFechaInicial.Visible = false;
            this.dtpFechaFinal.Visible = false;
            bdPermisos = new PermisoUsuarioConexion();
            this.frmMain = frmMain;
            dgvDatos.AutoGenerateColumns = false;
            dgvDatos.DataSource = bdAusencia.obtenerAusencias();
        }

        #region
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                EmpleadoConexion bd = new EmpleadoConexion(conexion);
                 empleado = bd.obtenerEmpleadoCodigo(txtCodigo.Text.Trim());
                if (empleado.Codigo == null)
                    MessageBox.Show("Error no existe el empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    buscarMAusencias();

            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 41))
            //{

                frmAddAusencia frm = new frmAddAusencia(conexion, frmMain);
                frm.Tag = "agregar";
                frm.ShowDialog();
                dgvDatos.DataSource = bdAusencia.obtenerAusencias();
            //}
            //else
            //    btnNuevo.Enabled = false;
                
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 42))
            {
            }
              if (dgvDatos.RowCount > 0)
            {
                DateTime fechaActual = DateTime.Now;
                int mesActual = fechaActual.Month;
                int añoActual = fechaActual.Year;
                this.cargarDatosEditar();

                if (this.ausenciaData.fechaInicio.Month == mesActual && this.ausenciaData.fechaInicio.Year == añoActual)
                {
                    frmAddAusencia frm = new frmAddAusencia(conexion, ausenciaData,frmMain);
                    frm.Tag = "modificar";
                    DialogResult result = frm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        ///cargar el datagrid con la información
                        dgvDatos.DataSource = bdAusencia.obtenerAusencias();
                    }
                }else
                    MessageBox.Show("Solo puede actualizar las ausencias de este mes y año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            //else
            //    btnModificar.Enabled = false;
        }

        private void BtnQuitar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 43))
                eliminar();
               dgvDatos.DataSource = bdAusencia.obtenerAusencias();
            //else
            //    btnQuitar.Enabled = false;
        }

        private void BtnSalir_Click(object sender, EventArgs e)
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

        #region buscar movimiento de ausencias segun el codigo y el rango de fecha

        public void buscarMAusencias()
        {

            if (cbFechaInicial.Checked && cbFechaFinal.Checked)
            {
                listaMovAusencia = bdAusencia.buscarAusencias(txtCodigo.Text,
                                              this.dtpFechaInicial.Value.Date,
                                              this.dtpFechaFinal.Value.Date);

                descripcionTipoAusencia();
                this.dgvDatos.DataSource = listaMovAusencia;
                
            }
            else
                 if (cbFechaInicial.Checked)
            {
                DateTime fFinal = new DateTime();
                listaMovAusencia = bdAusencia.buscarAusencias(txtCodigo.Text,
                                        this.dtpFechaInicial.Value.Date,
                                         fFinal);
                descripcionTipoAusencia();
                this.dgvDatos.DataSource = listaMovAusencia;
                //this.dtpFechaFinal.Visible = false;
            }
            
        }
        #endregion

        #region descripcion tipo descuento
        private void descripcionTipoAusencia()
        {

            foreach (AusenciaData mausencia in listaMovAusencia)
            {
                switch (mausencia.tipoAusencia[0])
                {
                    case 'I':
                        mausencia.tipoAusencia = "Incapacidad";
                        break;

                    case 'V':
                        mausencia.tipoAusencia = "Vacaciones";
                        break;

                    case 'N':
                        mausencia.tipoAusencia = "No se presento";
                        break;
                    case 'P':
                        mausencia.tipoAusencia = "Permisos";
                        break;

                   default:
                        mausencia.tipoAusencia = "Permisos sin goce de sueldo";
                        break;

                }
            }
        }
        #endregion

        private void CbFechaInicial_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbFechaInicial.Checked)
            {
                this.dtpFechaInicial.Visible = false;
               // this.dtpFechaInicial.Enabled = false;

            }
            else
            {
                this.dtpFechaInicial.Visible = true;
               // this.dtpFechaInicial.Enabled = true;
            } 
        }

        private void CbFechaFinal_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbFechaFinal.Checked)
            {
                // this.dtpFechaFinal.Enabled = false;
                this.dtpFechaFinal.Visible = false;
            }

            else {
                // this.dtpFechaFinal.Enabled = true;
                this.dtpFechaFinal.Visible = true;
            }
               
        }

        #region instanciar el objeto empleado con la fila seleccionada del datagrid
        private void cargarDatosEditar()
        {
            this.ausenciaData = new AusenciaData();
            if (dgvDatos.RowCount > 0)
            {
                int nlinea = dgvDatos.CurrentCell.RowIndex;
                ausenciaData.CodigoEmpleado = this.dgvDatos.Rows[nlinea].Cells["CodigoEmpleado"].Value.ToString();
                ausenciaData.Id = Convert.ToInt32(this.dgvDatos.Rows[nlinea].Cells["Id"].Value.ToString());

                string sFecha = this.dgvDatos.Rows[nlinea].Cells["fechaInicio"].Value.ToString();
                string[] fechaSplit = sFecha.Split('/');
                string año = fechaSplit[2].Substring(0, 4);
                ausenciaData.fechaInicio = new DateTime(Convert.ToInt32(año), Convert.ToInt32(fechaSplit[1]), Convert.ToInt32(fechaSplit[0]));

                //ausenciaData.codigoNomina = this.dgvDatos.Rows[nlinea].Cells["codigoNomina"].Value.ToString();
            }

        }
        #endregion

        #region eliminar
        public void eliminar()
        {

            if (dgvDatos.RowCount > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar la ausencica?", "Eliminar ausencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    cargarDatosEditar();
                    DateTime fechaActual = DateTime.Now;
                    int mesActual = fechaActual.Month;
                    int añoActual = fechaActual.Year;
                    if (this.ausenciaData.fechaInicio.Month == mesActual && this.ausenciaData.fechaInicio.Year == añoActual)
                    {
                        bool elimino = this.bdAusencia.accionesMAusencia("E", 0, ausenciaData.Id,0, DateTime.Now, DateTime.Now, 0, "",
                            (decimal)0.00);

                        if (elimino)
                        {
                            MessageBox.Show("Ausencia eliminada exitosamente", "Eliminar Ausencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.buscarMAusencias();
                        }
                        else
                        {
                            MessageBox.Show("No se puede eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                        MessageBox.Show("Solo puede eliminar las ausencias de este mes y año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        #endregion

        private void frmAusencias_Shown(object sender, EventArgs e)
        {
            Validator.validarPermisos(this.frmMain.usuarioId, btnNuevo, btnModificar, btnQuitar, btnSalir, this, 10);
        }
    }
}
