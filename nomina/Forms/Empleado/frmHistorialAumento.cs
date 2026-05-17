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
using nomina.Clases.ConexionManager;
using nomina.Clases.Movimientos.HistorialAumento;
using nomina.Clases.Empleado;

namespace nomina.Forms.Empleado
{
    public partial class frmHistorialAumento : Form
    {
        Conexion conexion;
        List<HistorialData> listaHistorialAumentos;
        HistorialConexion bdHistorialAum;
        //HistorialData ausenciaData;
        public frmHistorialAumento(Conexion conexion)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Historial Aumento del Empleado");
            colorForm();
            this.conexion = conexion;
            bdHistorialAum = new HistorialConexion(conexion);
        }

        #region color
        private void colorForm()
        {
            this.panel1.BackColor = Color.SkyBlue;
            this.btnBuscar.BackColor = Color.SkyBlue;
        }
        #endregion
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

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                EmpleadoConexion bd = new EmpleadoConexion(conexion);
                EmpleadoData emp = bd.obtenerEmpleadoCodigo(txtCodigo.Text.Trim());
                if (emp.Codigo == null)
                    MessageBox.Show("Error no existe el empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    buscarHistorialAumento();

            }
        }

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


        #region buscar movimiento de descuentos segun el codigo y el rango de fecha

        public void buscarHistorialAumento()
        {

            if (cbFechaInicial.Checked && cbFechaFinal.Checked)
            {
                listaHistorialAumentos = bdHistorialAum.buscarAumentosHistorial(txtCodigo.Text,
                                              this.dtpFechaInicial.Value.Date,
                                              this.dtpFechaFinal.Value.Date);

              
                this.dgvDatos.DataSource = listaHistorialAumentos;

            }
            else
                 if (cbFechaInicial.Checked)
            {
               
                listaHistorialAumentos = bdHistorialAum.buscarAumentosHistorial(txtCodigo.Text,
                                        this.dtpFechaInicial.Value.Date,
                                         this.dtpFechaInicial.Value.Date);
                
                this.dgvDatos.DataSource = listaHistorialAumentos;
                //this.dtpFechaFinal.Visible = false;
            }
        }
        #endregion
    }
}
