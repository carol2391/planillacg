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
using nomina.Clases.MovimientoLabores;
using nomina.Clases.Utilidades;
using nomina.Clases.Empleado;
using nomina.Forms.Main;
using nomina.Clases.PermisosUsuario;


namespace nomina.Forms.MovimientosLabores
{
    public partial class frmMovimientoLabores : Form
    {
        Conexion conexion;
        MLaboresConexion bdMlabores;
        //List<MLaboresConexion> lMlabores = new List<MLaboresConexion>();
        MLaboresData mLabor = new MLaboresData();
        DateTime fechaLabor;
        decimal total;
        List<MLaboresData> listaMovLabores = new List<MLaboresData>();
        PermisoUsuarioConexion bdPermisos;
        frmMain frmMain;

        public frmMovimientoLabores(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Movimiento Labores");

            this.conexion = conexion;
            bdMlabores = new MLaboresConexion(conexion);
            bdPermisos = new PermisoUsuarioConexion();
            this.frmMain = frmMain;
            Utilidad.configurarDataGrid(dgvLabores);
           
            dgvLabores.AutoGenerateColumns = false;
            txtCodigo.Select();
            this.dtpFechaInicial.Enabled = false;
            listaMovLabores = bdMlabores.obtenerLabores();
            DescripcionTipoLabor();
            this.dgvLabores.DataSource = listaMovLabores;
            SumarTotal();
            this.dtpFechaFinal.Enabled = false;
            
        }

        #region menu
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 33)) {
                frmAddMovimientoLabores frm = new frmAddMovimientoLabores(conexion,frmMain);
                frm.ShowDialog();
              if (DialogResult.OK == frm.DialogResult)
                {
                    MLaboresConexion bd = new MLaboresConexion(conexion);
                    this.listaMovLabores = bd.obtenerLabores();
                    dgvLabores.DataSource = this.listaMovLabores;
                //buscarMLabores();
            }
            //}

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 34)) {
              if (dgvLabores.RowCount > 0)
                {
                    cargarDatosEditar();

                    DateTime fechaActual = DateTime.Now;
                    int mesActual = fechaActual.Month;
                    int añoActual = fechaActual.Year;
                    if (this.fechaLabor.Month == mesActual && this.fechaLabor.Year == añoActual)
                    {
                        frmModificarMovimientoLabor frm = new frmModificarMovimientoLabor(conexion, this.mLabor);
                        frm.ShowDialog();
                        if (DialogResult.OK == frm.DialogResult)
                        {
                        MLaboresConexion bd = new MLaboresConexion(conexion);
                        this.listaMovLabores = bd.obtenerLabores();
                        dgvLabores.DataSource = this.listaMovLabores;
                        //buscarMLabores();
                    }
                    }
                    else
                        MessageBox.Show("Solo puede modificar las labores de este mes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
           // }            
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            //if (bdPermisos.existePermiso(this.frmMain.usuarioId, 35))
                eliminar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //this.dtpFechaInicial.Enabled = false;
            ///this.dtpFechaFinal.Enabled = false;
            if ( validar() ) {
                EmpleadoConexion bd = new EmpleadoConexion(conexion);
                EmpleadoData emp = bd.obtenerEmpleadoCodigo(txtCodigo.Text.Trim());
                if (emp.Codigo == null)
                    MessageBox.Show("Error no existe el empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    buscarMLabores();
                
            }
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
        #region eventos de enter
        private void cbFechaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void cbFechaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpFechaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpFechaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        
        #endregion

        #region validar

        private bool validar() {

            if (String.IsNullOrWhiteSpace(txtCodigo.Text)) {
                MessageBox.Show("Ingrese el código de Empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!this.cbFechaInicial.Checked && !this.cbFechaFinal.Checked) {
                MessageBox.Show("Seleccione la fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cbFechaInicial.Checked && cbFechaFinal.Checked) {
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

        #region buscar movimiento de labores

        public void buscarMLabores()
        {
           
            if (cbFechaInicial.Checked && cbFechaFinal.Checked)
            {
                listaMovLabores = bdMlabores.buscarMLabor(txtCodigo.Text,
                                              this.dtpFechaInicial.Value.Date, 
                                              this.dtpFechaFinal.Value.Date);

                DescripcionTipoLabor();
                this.dgvLabores.DataSource = listaMovLabores;
                SumarTotal();
            }
            else
                 if (cbFechaInicial.Checked)
                {
                DateTime fFinal = new DateTime();
                listaMovLabores = bdMlabores.buscarMLabor(txtCodigo.Text,
                                        this.dtpFechaInicial.Value.Date,
                                         fFinal);
               // descripcionTipoLabor();
                this.dgvLabores.DataSource = listaMovLabores;
                SumarTotal();
            }
                else
                     if (cbFechaFinal.Checked) {
                           DateTime fInicial = new DateTime();
                            DescripcionTipoLabor();
                            listaMovLabores = bdMlabores.buscarMLabor(txtCodigo.Text,
                                                                  fInicial, this.dtpFechaFinal.Value.Date );
                                      this.dgvLabores.DataSource = listaMovLabores;
                                       SumarTotal();
                        }
        }

        #endregion

        #region  cargar datos en el formulario editar
        private void cargarDatosEditar()
        {
            if (dgvLabores.RowCount > 0)
            {
                int nlinea = dgvLabores.CurrentCell.RowIndex;
                this.mLabor.Id = Convert.ToInt32(this.dgvLabores.Rows[nlinea].Cells["Id"].Value.ToString());
                this.mLabor.IdEmpleado = Convert.ToInt32( this.dgvLabores.Rows[nlinea].Cells["idEmpleado"].Value.ToString());
                this.mLabor.IdLabor = Convert.ToInt32(this.dgvLabores.Rows[nlinea].Cells["idLabor"].Value.ToString());
                string sFecha = this.dgvLabores.Rows[nlinea].Cells["FechaLabor1"].Value.ToString();
                string[] fechaSplit = sFecha.Split('/');
                string año = fechaSplit[2].Substring(0, 4);
                this.fechaLabor = new DateTime(Convert.ToInt32(año), Convert.ToInt32(fechaSplit[1]), Convert.ToInt32(fechaSplit[0]));
                mLabor.FechaLabor = fechaLabor;
            }
        }
        #endregion

        #region calcular total cuando agrega una labor
        
        public void SumarTotal()
        {
            decimal total = 0;
            foreach (MLaboresData mLabor in listaMovLabores)
            {
                total += mLabor.MontoTotal;
            }
            this.nudTotal.Value = (decimal)total;
        }
        #endregion

        private void DescripcionTipoLabor() {
       
            foreach (MLaboresData mLabor in listaMovLabores)
            {
                switch (mLabor.TipoPago.descripcion[0])
                {
                    case 'D':
                        mLabor.TipoPagoD = "Definido por el usuario";
                        break;

                    case 'F':
                        mLabor.TipoPagoD = "Por Factor";
                        break;

                    case 'H':
                        mLabor.TipoPagoD = "Por Hora";
                        break;

                    case 'V':
                        mLabor.TipoPagoD = "Por Valor";
                        break;

                }
            }
        }

        #region eliminar
        public void eliminar()
        {

            if (dgvLabores.RowCount > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar la labor?", "Eliminar labor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    cargarDatosEditar();
                    DateTime fechaActual = DateTime.Now;
                    int mesActual = fechaActual.Month;
                    int añoActual = fechaActual.Year;
                    if (this.fechaLabor.Month == mesActual && this.fechaLabor.Year == añoActual)
                    {
                        bool elimino = this.bdMlabores.accionesLabores("E", mLabor.IdEmpleado, mLabor.IdLabor,
                                "" , 0,
                                 (decimal)0.0,mLabor.FechaLabor,(decimal)0.00,
                                 0, " ", 0);
                        if (elimino)
                        {
                            MessageBox.Show("Labor eliminada exitosamente", "Eliminar Labor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            buscarMLabores();
                        }
                        else
                        {
                            MessageBox.Show("No se puede eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        MessageBox.Show("Solo puede eliminar las labores de este mes y año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    dgvLabores.DataSource = bdMlabores.obtenerLabores();
                }
            }

        }
        #endregion
    }
}
