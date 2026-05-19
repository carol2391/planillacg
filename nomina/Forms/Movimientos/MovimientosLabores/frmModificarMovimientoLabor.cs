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
using nomina.Clases.Empleado;
using nomina.Clases.Labores;
using nomina.Clases.Utilidades;

namespace nomina.Forms.MovimientosLabores
{
    public partial class frmModificarMovimientoLabor : Form
    {
        Conexion conexion;
        MLaboresData mlabor;
        MLaboresData mLaborModificar;
        MLaboresConexion bdMLabores;
        LaboresData labor;
        EmpleadoData empleado;
        string isr;
        public frmModificarMovimientoLabor(Conexion conexion, MLaboresData mLabor)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdMLabores = new MLaboresConexion(conexion);
            this.mlabor = mLabor;
            this.mLaborModificar = mlabor;
            cargarDatos();
            
        }

        #region evento enter
        private void nudMontoLabor_ValueChanged(object sender, EventArgs e)
        {
            calcularMontoTotal(mlabor.TipoLabor[0]);
        }

        private void nudCantidadLabor_ValueChanged(object sender, EventArgs e)
        {
            calcularMontoTotal(mlabor.TipoLabor[0]);
        }

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
          // calcularMontoTotal(mlabor.TipoLabor[0]);
        }

        private void nudCantidadLabor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
            //calcularMontoTotal(mlabor.TipoLabor[0]);
        }
        #endregion


        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (validar()) {
                DialogResult resultado = MessageBox.Show("¿Está seguro de modificar el Movimiento Labor?", "Modificar Movimiento Labor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    DialogResult result = MessageBox.Show("¿Quiere aplicar esta labor al ISR?", "Aplicar ISR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.isr = "S";
                    }
                    else
                        this.isr = "N";
                    modificar();
                }
            }
        }

        #region cargar datos del movimiento de labor
        public void cargarDatos()
        {
            EmpleadoConexion empleadoConexion = new EmpleadoConexion(conexion);
            empleado = empleadoConexion.obtenerEmpleadoID(this.mlabor.IdEmpleado);
            this.lblCodigoEmpleado.Text = empleado.Codigo;
            this.lblNombreEmpleado.Text = empleado.Nombre;
            this.lblSueldo.Text = empleado.Sueldo.ToString();
            this.lblDepartamento.Text = empleado.nombreDepto;

            LaboresConexion laborConexion = new LaboresConexion(conexion);
            labor = laborConexion.obtenerLabor(this.mlabor.IdLabor);
            //lblCodigoLabor.Text = labor.Codigo;
            //lblNombreLabor.Text = labor.Nombre;

            this.mlabor = bdMLabores.obtenerMLabor(this.mlabor.IdEmpleado, this.mlabor.IdLabor, 
                            this.mlabor.FechaLabor);
            this.txtDescripcionLabor.Text = mlabor.DescripcionLabor;
            this.dtpFecha.Value = mlabor.FechaLabor;
            this.txtCodigoCuenta.Text = mlabor.IdCuenta.ToString();
            this.lblNombreCuenta.Text = mlabor.NombreCuenta;
            this.lblCodigoLabor.Text = labor.Codigo;
            this.lblNombreLabor.Text = labor.Nombre;
            establecerMonto(labor.TipoPago.descripcion.Substring(0,1),labor);
        }

        #endregion

        #region establecer monto
        public void establecerMonto(string tipoLabor, LaboresData labor) {
            switch (tipoLabor)
            {
                case "D":
                    this.nudMontoLabor.Enabled = true;
                    this.nudCantidadLabor.Enabled = false;
                    this.nudMontoLabor.Value = (decimal)mlabor.MontoLabor;
                    this.nudCantidadLabor.Value = (decimal)0.00;
                    nudMontoLabor.Maximum = (decimal)999999999999999.00;
                    nudMontoLabor.DecimalPlaces = 2;
                    break;

                case "F":
                    this.nudMontoLabor.Enabled = false;
                    this.nudCantidadLabor.Enabled = true;
                    nudMontoLabor.DecimalPlaces = 7;
                    nudMontoLabor.Maximum = (decimal)9999999999.0000000;
                    this.nudMontoLabor.Value = (decimal)labor.Factor;
                    this.nudCantidadLabor.Value = (decimal)mlabor.CantidaLabor;
                    break;

                case "H":
                    this.nudMontoLabor.Enabled = false;
                    this.nudCantidadLabor.Enabled = true;
                    this.nudMontoLabor.Value = (decimal)0.00;
                    this.nudCantidadLabor.Value = (decimal)mlabor.CantidaLabor;
                    this.nudMontoLabor.Maximum = (decimal)999999999999999.00;
                    this.nudMontoLabor.DecimalPlaces = 2;
                    break;

                case "V":
                    this.nudMontoLabor.Enabled = false;
                    this.nudCantidadLabor.Enabled = true;
                    this.nudMontoLabor.Value = (decimal)labor.Monto;
                    this.nudCantidadLabor.Value = (decimal)mlabor.CantidaLabor;
                    nudMontoLabor.DecimalPlaces = 2;
                    nudMontoLabor.Maximum = (decimal)999999999999999.00;
                    break;
            }
        }


        #endregion

        #region
        //public void activarMontoOCantidadTipoLabor(char tipoLabor)
        //{
        //    switch (tipoLabor)
        //    {
        //        case 'D':
        //            this.nudMontoLabor.Enabled = true;
        //            this.nudCantidadLabor.Enabled = false;
        //            break;

        //        case 'F':
        //            this.nudMontoLabor.Enabled = false;
        //            this.nudCantidadLabor.Enabled = true;
        //            break;

        //        case 'H':
        //            this.nudMontoLabor.Enabled = false;
        //            this.nudCantidadLabor.Enabled = true;
        //            break;

        //        case 'V':
        //            this.nudMontoLabor.Enabled = false;
        //            this.nudCantidadLabor.Enabled = true;
        //            break;
        //    }

        //}
        #endregion

        #region calcular monto total
        public void calcularMontoTotal(char tipoLabor) {
            decimal monto;
            switch (tipoLabor)
            {
                case 'D':

                    monto = Decimal.Round(nudMontoLabor.Value, 2);
                    this.nudMontoTotal.Value = monto;
                    break;
                case 'F':
                    monto = Convert.ToDecimal(labor.Factor) * Decimal.Round(nudCantidadLabor.Value, 2);
                    this.nudMontoTotal.Value = monto;
                    break;

                case 'H':
                    monto = (decimal)(empleado.Sueldo / 30) / 8 * Decimal.Round(nudCantidadLabor.Value, 2);
                    this.nudMontoTotal.Value = monto;
                    break;


                case 'V':
                    monto = (decimal)labor.Monto * Decimal.Round(nudCantidadLabor.Value, 2);
                    this.nudMontoTotal.Value = monto;
                    break;

            }
        }
        #endregion

        #region modificar
        public void modificar() {
            DateTime fechaActual = DateTime.Now;
            int mesActual = fechaActual.Month;
            if (mesActual == dtpFecha.Value.Month)
            {
                bool modifico = this.bdMLabores.accionesLabores("M",empleado.Id, labor.Id, txtDescripcionLabor.Text,
                     labor.TipoPago.id, nudCantidadLabor.Value, dtpFecha.Value.Date, nudMontoLabor.Value, labor.idCodigoCuenta, this.isr, this.mLaborModificar.Id);
                if (modifico)
                {
                    MessageBox.Show("Movimiento de labor modificado exitosamente", "Modificar Movimiento de Labor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Ya existe un movimiento de labor con esa fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  
                }
            }
            else
            {
                MessageBox.Show("Solo puede modificar las labores de este mes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            
        }
        #endregion

        #region validar
        private bool validar()
        {
            if (String.IsNullOrWhiteSpace(txtDescripcionLabor.Text))
            {
                MessageBox.Show("Ingrese una descripción de la labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((this.nudMontoLabor.Value > (decimal)999999999999999.99))
            {
                this.nudMontoLabor.Value = (decimal)0.00;
                MessageBox.Show("El monto debe de ser menor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudCantidadLabor.Value > (decimal)99999.99)
            {
                this.nudCantidadLabor.Value = (decimal)0.00;
                MessageBox.Show("La cantidad debe de ser menor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((labor.TipoPago.descripcion[0].Equals("F") || labor.TipoPago.descripcion[0].Equals("V") 
                || labor.TipoPago.descripcion[0].Equals("H")) && (double)nudCantidadLabor.Value <= 0.00)
            {
                MessageBox.Show("Ingrese la cantidad de la labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (labor.TipoPago.descripcion[0].Equals("D") && (double)this.nudMontoLabor.Value <= 0.00)
            {

                MessageBox.Show("Ingrese el monto de la Labor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void nudMontoTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

       
    }
}
