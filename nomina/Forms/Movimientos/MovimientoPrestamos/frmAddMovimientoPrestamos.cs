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
using nomina.Clases.Movimientos.MovimientoPrestamo;
using nomina.Clases.ConexionManager;
using nomina.Forms.Empleado;
using nomina.Clases.Empleado;
using nomina.Forms.Main;
using nomina.Clases.TipoPagoPrestamo;

namespace nomina.Forms.Movimientos.MovimientoPrestamos
{
    public partial class frmAddMovimientoPrestamos : Form
    {
        Conexion conexion;
        PrestamoData prestamoData;
        EmpleadoData empleado;
        PrestamoConexion bdPrestamo;
        DateTime fechaAntigua;
        frmMain frmMain;
        /*agregar un nuevo prestamo*/
        public frmAddMovimientoPrestamos(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdPrestamo = new PrestamoConexion(conexion);
            desactivarInfoEmpleado();
            configurarComboboxs();
            this.cbActivo.Visible = false;
            this.lblEstado.Visible = false;
           
            this.frmMain = frmMain;
            this.gpPrestamo.Visible = false;
            this.btnBuscarEmpleado.Select();
        }

        /*editar un prestamo*/
        public frmAddMovimientoPrestamos(Conexion conexion,PrestamoData prestamo)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdPrestamo = new PrestamoConexion(conexion);
            this.prestamoData = prestamo;
            this.prestamoData = bdPrestamo.obtenerPrestamo(prestamoData.Id);
            this.cbActivo.Visible = true;
            desactivarInfoEmpleado();
            configurarComboboxs();
            this.btnBuscarEmpleado.Visible = false;
            
            cargarDatosEditar();
            cbTipoPago.SelectedIndex = prestamoData.IdTipoPago;
            txtCodigo.Focus();
        }

        public  void cambiarControlEnter(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
                this.nudCuotaPagar.Value = this.nudMonto.Value / this.nudTiempo.Value;
            }
        }
        #region evento tecla enter
        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void DtpFechaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void CbTipoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void TxtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

       

        private void NudTiempo_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }

        private void NudMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);

        }
        #endregion

        #region menu
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                if (this.Tag == "agregar")
                    nuevo();
                else
                      if (this.Tag == "modificar")
                {
                    DialogResult resultado = MessageBox.Show("¿Está seguro de modificar el Prestamo?", "Modificar Prestamo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (resultado == System.Windows.Forms.DialogResult.Yes)
                    {
                          modificar();

                    }
                }
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                limpiarControles();
                Dispose();

            }
        }
        private void BtnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            buscarEmpleado();

        }
        #endregion 

        #region mostrar informacion del empleado
        public void mostrarInfoEmpleado()
        {
            this.lblCodigoEmpleado.Visible = true;
            this.lblNombreEmpleado.Visible = true;
            this.lblDepartamento.Visible = true;
            this.lblSueldo.Visible = true;
            this.lblCategoria.Visible = true;
            this.lblSalarioInicial.Visible = true;
            this.lblSalarioFinal.Visible = true;

        }
        #endregion

        #region desactivar informacion empleado
        public void desactivarInfoEmpleado()
        {
            this.lblCodigoEmpleado.Visible = false;
            this.lblNombreEmpleado.Visible = false;
            this.lblDepartamento.Visible = false;
            this.lblSueldo.Visible = false;
            this.lblCategoria.Visible = false;
            this.lblSalarioInicial.Visible = false;
            this.lblSalarioFinal.Visible = false;

            this.lblCodigoEmpleado.Text = "";
        }

        #endregion

        #region configurar combobox
        private void configurarComboboxs()
        {
             configurarPropiedadesCombo(this.cbTipoPago);
           List<TipoPagoPrestamoData> ltipoPagos = new List<TipoPagoPrestamoData>();
            TipoPagoPrestamoConexion bdTipoPago = new TipoPagoPrestamoConexion(conexion);
            ltipoPagos = bdTipoPago.obtenerTipoPagos();
            ltipoPagos.Insert(0, new TipoPagoPrestamoData(0, "Seleccione una opción..."));
            this.cbTipoPago.DataSource = ltipoPagos;
            this.cbTipoPago.SelectedIndex = 0;
        }

        private void configurarPropiedadesCombo(ComboBox cb)
        {
            cb.DisplayMember = "descripcion";
            cb.ValueMember = "id";
        }

        #endregion

        #region buscarEmpleado
        public void buscarEmpleado()
        {
            frmEmpleado frm = new frmEmpleado(conexion,frmMain);
            frm.Tag = "buscar";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.empleado = frm.empleado;
                mostrarInfoEmpleado();
                this.lblCodigoEmpleado.Text = empleado.Codigo;
                this.lblNombreEmpleado.Text = empleado.Nombre;
                this.lblSueldo.Text = empleado.Sueldo.ToString();
                this.lblDepartamento.Text = empleado.objDepto.NombreDepartamento;
                this.lblCategoria.Text = empleado.objCategoria.NombreCategoria;
                this.lblSalarioInicial.Text = empleado.objCategoria.SalarioInicial.ToString();
                this.lblSalarioFinal.Text = empleado.objCategoria.SalarioFinal.ToString();
                this.gpPrestamo.Visible = true;
                txtCodigo.Focus();
            }
        }
        #endregion

        #region limpiarControles
        public void limpiarControles()
        {
            desactivarInfoEmpleado();
            this.txtCodigo.Text = "";
            this.cbTipoPago.SelectedIndex = 0;
            this.txtDescripcion.Text = "";
            this.nudTiempo.Value = (decimal)0;
            this.nudMonto.Value = (decimal)0;
            this.nudCuotaPagar.Value = 0;
            this.cbActivo.Checked = false;
        }
        #endregion

        #region validar
        public bool validar()
        {

            if (String.IsNullOrWhiteSpace(lblCodigoEmpleado.Text))
            {
                MessageBox.Show("Seleccione un empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Escriba el Código del prestamo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((int)cbTipoPago.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un Tipo de pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Escriba la Descripción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudTiempo.Value == 0)
            {
                MessageBox.Show("Ingrese el tiempo de pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudMonto.Value == 0)
            {
                MessageBox.Show("Ingrese el monto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region guardar
        private void nuevo() {
            decimal cuotaMes = nudMonto.Value / nudTiempo.Value;
            TipoPagoPrestamoData tipoPago = (TipoPagoPrestamoData)cbTipoPago.SelectedItem;
            bool agrego = bdPrestamo.accionesPrestamos("N",0,empleado.Id, txtCodigo.Text, dtpFechaInicial.Value.Date,
                                        txtDescripcion.Text,tipoPago.id, nudMonto.Value, cuotaMes,
                                        (int)nudTiempo.Value,"A");
            if (agrego)
            {
                MessageBox.Show("Movimiento de Aumento agregado exitosamente", "Agregar Movimiento Aumento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.gpPrestamo.Visible = false;
                limpiarControles();
                //this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Error ya existe un prestamo con ese código o en esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region cargar datos a editar en el formulario
        public void cargarDatosEditar()
        {
            EmpleadoConexion bdEmpleado = new EmpleadoConexion(conexion);
            this.empleado = bdEmpleado.obtenerEmpleadoCodigo(prestamoData.CodigoEmpleado);
            this.lblCodigoEmpleado.Text = empleado.Codigo;
            this.lblNombreEmpleado.Text = empleado.Nombre;
            this.lblDepartamento.Text = empleado.nombreDepto;
            this.lblSueldo.Text = empleado.Sueldo.ToString();
            this.dtpFechaInicial.Value = prestamoData.Fecha;

            lblCategoria.Text = empleado.objCategoria.NombreCategoria;
            this.lblSalarioInicial.Text = empleado.objCategoria.SalarioInicial.ToString();
            this.lblSalarioFinal.Text = empleado.objCategoria.SalarioFinal.ToString();

            mostrarInfoEmpleado();

            this.txtDescripcion.Text = this.prestamoData.Descripcion;
            this.dtpFechaInicial.Value = this.prestamoData.Fecha;

            this.txtCodigo.Text = this.prestamoData.CodigoPrestamo;
            this.dtpFechaInicial.Value = this.prestamoData.Fecha;
            this.cbTipoPago.SelectedValue = this.prestamoData.IdTipoPago;

            this.nudTiempo.Value = this.prestamoData.Tiempo;
            this.nudMonto.Value = this.prestamoData.MontoActual;
            this.nudCuotaPagar.Value = this.prestamoData.CuotaMes;

            switch (this.prestamoData.Estado[0]) {

                case 'A':
                    this.cbActivo.Checked = true;
                    break;

                case 'I':
                    this.cbActivo.Checked = false;
                    break;
            }
        }
        #endregion

        #region modificar
        public void modificar()
        {
            string estado;

            if (cbActivo.Checked) {
                estado = "A";

            } else

                estado = "I";
            TipoPagoPrestamoData tipoPago = (TipoPagoPrestamoData)cbTipoPago.SelectedItem;
            decimal cuotaMes = nudMonto.Value/nudTiempo.Value;
            bool agrego = bdPrestamo.accionesPrestamos("M", prestamoData.Id, empleado.Id, txtCodigo.Text, dtpFechaInicial.Value.Date,
                                        txtDescripcion.Text, tipoPago.id, nudMonto.Value, cuotaMes,
                                        (int)nudTiempo.Value,estado);
            if (agrego)
            {
                MessageBox.Show("Movimiento de Prestamo agregado exitosamente", "Modificar Movimiento Prestamo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarControles();
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Error ya existe un prestamo en esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        #endregion

        private void FrmAddMovimientoPrestamos_Load(object sender, EventArgs e)
        {
            if (this.Tag == "agregar")
            {
                lblTitulo.Text = "Nuevo Prestamo";
                Text = "Nuevo Prestamo";
            }
            else
                if (this.Tag == "modificar")
            {
                lblTitulo.Text = "Modificar Prestamo";
                Text = "Modificar Prestamo";
            }
        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnCancelar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }
    }
}
