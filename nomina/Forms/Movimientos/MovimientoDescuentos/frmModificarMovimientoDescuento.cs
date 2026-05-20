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
using nomina.Clases.MovimiendoDescuentos;
using nomina.Clases.Utilidades;
using nomina.Clases.Empleado;
using nomina.Clases.Descuentos;
namespace nomina.Forms.MovimientoDescuentos
{
    public partial class frmModificarMovimientoDescuento : Form
    {
        #region propiedades
        MDescuentoData movDescuentoData;
        Conexion conexion;
        MDescuentoConexion bdMDescuentos;
        DescuentoData descuentoData;
        EmpleadoData empleado;
        #endregion

        public frmModificarMovimientoDescuento(Conexion conexion,MDescuentoData mdescuento)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdMDescuentos = new MDescuentoConexion(conexion);
            this.movDescuentoData = mdescuento;
            cargarDatos();
        }

        #region menu
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de modificar el Movimiento Descuento", "Modificar Movimiento Descuento", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    modificar();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }
        }
        #endregion

        #region eventos de tecla enter
        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtDescripcionLabor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void nudMontoLabor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void nudCantidadLabor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void nudMontoTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnBuscarCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        #endregion

        #region modificar
        public void modificar()
        {
            DateTime fechaActual = DateTime.Now;
            int mesActual = fechaActual.Month;
            if (mesActual == dtpFecha.Value.Month)
            {
                bool modifico = bdMDescuentos.accionesDescuento("M",movDescuentoData.idMDescuento, movDescuentoData.objEmpleado.Id, 
                             movDescuentoData.objDescuento.Id, txtDescripcionDescuento.Text,
                             movDescuentoData.objTipoPago.id,
                             nudCantidadDescuento.Value, dtpFecha.Value.Date, nudMontoDescuento.Value, 0);
                if (modifico)
                {
                    MessageBox.Show("Movimiento de descuento modificado exitosamente", "Modificar Movimiento de Labor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Ya existe un movimiento de descuento con esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Solo puede modificar los descuentos de este mes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        #endregion

        #region evento cuando cambia el valor del monto y la cantidad
        private void nudMontoLabor_ValueChanged(object sender, EventArgs e)
        {
            calcularMontoTotal(movDescuentoData.objTipoPago.descripcion.Substring(0,1));
        }

        private void nudCantidadLabor_ValueChanged(object sender, EventArgs e)
        {
            calcularMontoTotal(movDescuentoData.objTipoPago.descripcion.Substring(0,1));
        }
        #endregion

        #region cargar datos del movimiento de descuento en el formulario
        public void cargarDatos()
        {
            EmpleadoConexion empleadoConexion = new EmpleadoConexion(conexion);
            empleado = empleadoConexion.obtenerEmpleadoCodigo(this.movDescuentoData.CodigoEmpleado);
            
            DescuentoConexion bdDescuento = new DescuentoConexion(conexion);
            this.movDescuentoData = bdMDescuentos.obtenerMDescuento(this.movDescuentoData.idMDescuento);

            this.lblCodigoEmpleado.Text = movDescuentoData.objEmpleado.Codigo;
            this.lblNombreEmpleado.Text = movDescuentoData.objEmpleado.Nombre;
            this.lblSueldo.Text = movDescuentoData.objEmpleado.Sueldo.ToString();
            this.lblDepartamento.Text = movDescuentoData.objEmpleado.nombreDepto;
            this.lblSueldo.Text = movDescuentoData.objEmpleado.Sueldo.ToString();
            this.lblCodigoLabor.Text = movDescuentoData.objDescuento.Codigo;
            //descuentoData = laborConexion.obtenerDescuentoPorCodigo(this.movDescuentoData.CodigoDescuento);

            lblCodigoLabor.Text = movDescuentoData.objDescuento.Codigo;
            lblDescucento.Text = movDescuentoData.objDescuento.Nombre;

            this.txtDescripcionDescuento.Text = movDescuentoData.DescripcionDescuento;
            this.dtpFecha.Value = movDescuentoData.FechaDescuento;
            this.txtCodigoCuenta.Text = movDescuentoData.CodigoCuenta;
            this.lblNombreCuenta.Text = movDescuentoData.NombreCuenta;
            EstablecerMonto(movDescuentoData.TipoPagoD, movDescuentoData.objDescuento);
        }

        #endregion

        #region calcular monto total
        public void calcularMontoTotal(string tipoPago)
        {
            decimal monto;
            switch (tipoPago)
            {
                case "D":

                    monto = Decimal.Round(nudMontoDescuento.Value, 2);
                    this.nudMontoTotal.Value = monto;
                    break;
                case "F":
                    monto = Convert.ToDecimal(this.movDescuentoData.objDescuento.Factor) * Decimal.Round(nudCantidadDescuento.Value, 2);
                    this.nudMontoTotal.Value = monto;
                    break;

                case "H":
                    monto = (decimal)(empleado.Sueldo / 30) / 8 * Decimal.Round(nudCantidadDescuento.Value, 2);
                    this.nudMontoTotal.Value = monto;
                    break;


                case "V":
                    monto = (decimal)this.movDescuentoData.MontoDescuento* Decimal.Round(nudCantidadDescuento.Value, 2);
                    this.nudMontoTotal.Value = monto;
                    break;

            }
        }
        #endregion

        #region establecer monto y cantidad
        public void EstablecerMonto(string  tipoLabor, DescuentoData descuento)
        {
            switch (tipoLabor)
            {
                case "D":
                    this.nudMontoDescuento.Enabled = true;
                    this.nudCantidadDescuento.Enabled = false;
                    this.nudMontoDescuento.Value = (decimal)movDescuentoData.MontoDescuento;
                    this.nudCantidadDescuento.Value = (decimal)0.00;
                    nudMontoDescuento.Maximum = (decimal)999999999999999.00;
                    nudMontoDescuento.DecimalPlaces = 2;
                    break;

                case "F":
                    this.nudMontoDescuento.Enabled = false;
                    this.nudCantidadDescuento.Enabled = true;
                    this.nudMontoDescuento.Value = (decimal)descuento.Factor;
                    this.nudCantidadDescuento.Value = (decimal)movDescuentoData.CantidadDescuento;
                    nudMontoDescuento.DecimalPlaces = 7;
                    this.nudMontoDescuento.Maximum = (decimal)9999999999.00;
                    break;

                case "H":
                    this.nudMontoDescuento.Enabled = false;
                    this.nudCantidadDescuento.Enabled = true;
                    this.nudMontoDescuento.Value = (decimal)0.00;
                    this.nudCantidadDescuento.Value = (decimal)movDescuentoData.CantidadDescuento;
                    nudMontoDescuento.Maximum = (decimal)999999999999999.00;
                    nudMontoDescuento.DecimalPlaces = 2;
                    break;

                case "V":
                    this.nudMontoDescuento.Enabled = false;
                    this.nudCantidadDescuento.Enabled = true;
                    this.nudMontoDescuento.Value = (decimal)descuento.Monto;
                    this.nudCantidadDescuento.Value = (decimal)movDescuentoData.CantidadDescuento;
                    nudMontoDescuento.DecimalPlaces = 2;
                    this.nudMontoDescuento.Maximum = (decimal)999999999999999.00;
                    break;
            }
        }


        #endregion

        #region validar
        private bool validar()
        {
            if (String.IsNullOrWhiteSpace(txtDescripcionDescuento.Text))
            {
                MessageBox.Show("Ingrese una descripción del Descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((this.nudMontoDescuento.Value > (decimal)999999999999999.99))
            {
                this.nudMontoDescuento.Value = (decimal)0.00;
                MessageBox.Show("El monto debe de ser menor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudCantidadDescuento.Value > (decimal)99999.99)
            {
                this.nudCantidadDescuento.Value = (decimal)0.00;
                MessageBox.Show("La cantidad debe de ser menor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((movDescuentoData.objTipoPago.descripcion.Equals("FACTOR") || movDescuentoData.objTipoPago.descripcion.Equals("VALOR") ||
                movDescuentoData.objTipoPago.descripcion.Equals("HORA")) && (double)nudCantidadDescuento.Value <= 0.00)
            {
                MessageBox.Show("Ingrese la cantidad del Descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (movDescuentoData.objTipoPago.descripcion.Equals("DEFINIDO POR EL USUARIO") && (double)this.nudMontoDescuento.Value <= 0.00)
            {

                MessageBox.Show("Ingrese el monto del Descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtCodigoCuenta.Text))
            {
                MessageBox.Show("Seleccione una cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        #endregion

        private void btnModificar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnSalir_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }
    }
}
