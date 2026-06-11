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
using nomina.Clases.Parametro;
using nomina.Clases.Utilidades;

namespace nomina.Forms.Parametros
{
    public partial class frmAddParametro : Form
    {
        Conexion conexion;
        ParametroData parametroData;
        ParametroConexion bdParametro;
        /*para agregar un nuevo parametro*/
        public frmAddParametro(Conexion conexion)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdParametro = new ParametroConexion(conexion);
        }

        /*editar*/
        public frmAddParametro(Conexion conexion, ParametroData parametroData)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.parametroData = parametroData;
            bdParametro = new ParametroConexion(conexion);
            this.parametroData = bdParametro.obtenerParametro(parametroData.Id);
            cargarDatosEditar();
        }

        #region eventos tecla enter
        private void NudPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void NudExcento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void NudSueldoPromedio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void NudInicial10_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void NudFinal10_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void NudInicial15_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void NudFinal15_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void NudInicial20_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void NudFinal20_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void NudInicial25_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void NudFinal25_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void BtnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void BtnCancelar_KeyPress(object sender, KeyPressEventArgs e)
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
                    agregar();
                else
                      if (this.Tag == "modificar")
                {
                    DialogResult resultado = MessageBox.Show("¿Está seguro de modificar el Parametro?", "Modificar Parametro", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
                
                Dispose();

            }
        }
        #endregion

        #region validar
        private bool validar() {
            if (this.nudPeriodo.Value == 0) {

                MessageBox.Show("Ingrese el periodo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudExcento.Value == 0)
            {

                MessageBox.Show("Ingrese la cantidad del Excento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudSueldoPromedio.Value == 0)
            {

                MessageBox.Show("Ingrese el sueldo minimo promedio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (this.nudInicial15.Value == 0)
            {

                MessageBox.Show("Ingrese el rango inicial del 15%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudFinal15.Value == 0)
            {

                MessageBox.Show("Ingrese el rango final del 15%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudInicial20.Value == 0)
            {

                MessageBox.Show("Ingrese el rango inicial del 20%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudFinal20.Value == 0)
            {

                MessageBox.Show("Ingrese el rango final del 20%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.nudInicial25.Value == 0)
            {

                MessageBox.Show("Ingrese el rango inicial del 25%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudFinal25.Value == 0)
            {

                MessageBox.Show("Ingrese el rango final del 25%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.nudReservaLaboralRAP.Value == 0)
            {

                MessageBox.Show("Ingrese el valor de la Reserva Laboral del RAP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudValorPisoRAP.Value == 0)
            {

                MessageBox.Show("Ingrese el valor del Piso del RAP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.nudSalarioMinimoPRO.Value == 0)
            {

                MessageBox.Show("Ingrese el salario minimo promedio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.nudValorTechoIHSS.Value == 0)
            {

                MessageBox.Show("Ingrese el valor del Techo del IHSS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        #endregion

        #region guardar nuevo parametro
        private void agregar() {
            bool agrego = bdParametro.agregarParametro((int)nudPeriodo.Value,
                                  nudExcento.Value, this.nudInicial15.Value,
                                   nudFinal15.Value, this.nudInicial20.Value,
                                   nudFinal20.Value, this.nudInicial25.Value,
                                   nudFinal25.Value, this.nudSueldoPromedio.Value,
                                   nudReservaLaboralRAP.Value, nudValorPisoRAP.Value,
                                   nudSalarioMinimoPRO.Value,nudValorTechoIHSS.Value);
            if (agrego)
            {
                MessageBox.Show("Parametro agregado exitosamente", "Agregar Parametro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarControles();
                //this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Error ya existe un parametro en ese periodo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region modificar un parametro
        private void modificar()
        {
            bool agrego = bdParametro.modificarParametro(parametroData.Id,(int)nudPeriodo.Value,
                                  nudExcento.Value, this.nudInicial15.Value,
                                   nudFinal15.Value, this.nudInicial20.Value,
                                   nudFinal20.Value, this.nudInicial25.Value,
                                   nudFinal25.Value,this.nudSueldoPromedio.Value,
                                   nudReservaLaboralRAP.Value, nudValorPisoRAP.Value,
                                   nudSalarioMinimoPRO.Value, nudValorTechoIHSS.Value);

            if (agrego)
            {
                MessageBox.Show("Parametro actualizado exitosamente", "Modificar Parametro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarControles();
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Error ya existe un parametro en ese periodo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region cargar datos a editar
        private void cargarDatosEditar() {
            this.nudPeriodo.Value = parametroData.Periodo;
            this.nudExcento.Value = parametroData.Excento;
            this.nudSueldoPromedio.Value = parametroData.SueldoPromedio;

            this.nudInicial15.Value = parametroData.RangoInicial15;
            this.nudFinal15.Value = parametroData.RangoFinal15;


            this.nudInicial20.Value = parametroData.RangoInicial20;
            this.nudFinal20.Value = parametroData.RangoFinal20;

            this.nudInicial25.Value = parametroData.RangoInicial25;
            this.nudFinal25.Value = parametroData.RangoFinal25;

            this.nudReservaLaboralRAP.Value= parametroData.ReservaLaboralRAP;
            this.nudValorPisoRAP.Value = parametroData.ValorPisoRap;
            this.nudSalarioMinimoPRO.Value = parametroData.SalarioMinimoPromedio;
            this.nudValorTechoIHSS.Value = parametroData.ValorTechoIHSS;

        }
        #endregion
        private void limpiarControles() {
            foreach (Control c in pnLabor.Controls) {
                if (c is NumericUpDown) {
                    NumericUpDown numero = (NumericUpDown)c;
                    numero.Value = 0;
                }
            }
        }

        private void FrmAddParametro_Load(object sender, EventArgs e)
        {
            
            if (this.Tag == "agregar")
            {
                lblTitulo.Text = "Nuevo Parametro";
                Text = "Nuevo Parametro";
            }
            else
                  if (this.Tag == "modificar")
            {
                lblTitulo.Text = "Modificar Parametro";
                Text = "Modificar Parametro";
            }
        }
    }
}
