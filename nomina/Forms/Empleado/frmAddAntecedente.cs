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
using nomina.Clases.Antecedentes;
using nomina.Clases.ConexionManager;
using nomina.Forms.Empleado;

namespace nomina.Forms.Empleado
{
    public partial class frmAddAntecedente : Form
    {
        int idEmpleado { set; get; }
        AntedecenteConexion bdAntecedente;
        int idAntecedente;
        string accion;
        string tipoAntecedente;
        public frmAddAntecedente(int idEmpleado,int idAntecedente ,Conexion conexion, string accion,string tipoAntecedente)
        {
            InitializeComponent();
         
            this.idEmpleado = idEmpleado;
            this.idAntecedente = idAntecedente;
            bdAntecedente = new AntedecenteConexion(conexion);
            Utilidad.configuarForm(this, "Antecedentes");
            
            this.accion = accion;
            this.tipoAntecedente = tipoAntecedente;
            if (accion.Equals("modificar"))
                cargarDatos();
        }

        private void cargarDatos() {
            AntecedenteData a = new AntecedenteData();
            a = bdAntecedente.obtenerAntecedente(idAntecedente,idEmpleado,tipoAntecedente);
            txtLugarOrigen.Text = a.LugarOrigen;
            nudNumeroAntecedente.Value = a.NumeroAntecedente;
            dtpFechaEmision.Value = a.FechaEmision;
            dtpFechaVencimiento.Value = a.FechaVencimiento;
            dtpVigencia.Value = a.Vigencia;

        }

        private void nudNumeroAntecedente_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpFechaEmision_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpVigencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpFechaVencimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void nudNumeroAntecedente_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(nudNumeroAntecedente, "");
        }

        private void nudNumeroAntecedente_Validating(object sender, CancelEventArgs e)
        {
            //if (nudNumeroAntecedente.Value)
            //{
            //    e.Cancel = true;
            //    nudSalFinal.Select(0, nudSalFinal.Value.ToString().Length);
            //    errorProvider1.SetError(nudSalFinal, "El salario final debe de ser mayor al salario inicial ");
            //}
        }

        private void dtpFechaEmision_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(nudNumeroAntecedente, "");
        }

        private void dtpFechaEmision_Validating(object sender, CancelEventArgs e)
        {
            //if (dtpFechaEmision.Value>dtpFechaVencimiento.Value)
            //{
            //    e.Cancel = true;
                
            //    errorProvider1.SetError(dtpFechaEmision, "La fecha de emisión no puede ser mayor que la fecha de vencimiento");
            //}
        }

        private void dtpVigencia_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(dtpVigencia, "");
        }

        private void dtpVigencia_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtLugarOrigen_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtLugarOrigen.Text.Trim())) {
                e.Cancel = true;

                errorProvider1.SetError(dtpFechaEmision, "Ingrese el lugar de origen");
            }
        }

        private void txtLugarOrigen_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtLugarOrigen, "");
        }

        private void dtpFechaVencimiento_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(dtpFechaVencimiento, "");
        }

        private void dtpFechaVencimiento_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaVencimiento.Value<dtpFechaEmision.Value  )
            {
                e.Cancel = true;

                errorProvider1.SetError(dtpFechaEmision, "La fecha de vencimiento no puede ser menor que la fecha de vencimiento");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool agrego=false;
            if (accion.Equals("nuevo"))
            {
              agrego =  this.bdAntecedente.accionesAntecedentes("N", 0, idEmpleado, (int)nudNumeroAntecedente.Value,
                    dtpFechaEmision.Value.Date, dtpFechaVencimiento.Value.Date,dtpVigencia.Value.Date, txtLugarOrigen.Text.Trim(),tipoAntecedente);

            }
            else
                if (accion.Equals("modificar")) {
                 agrego = this.bdAntecedente.accionesAntecedentes("M", idAntecedente, idEmpleado, (int)nudNumeroAntecedente.Value,
                    dtpFechaEmision.Value.Date, dtpFechaVencimiento.Value.Date, dtpVigencia.Value.Date, txtLugarOrigen.Text.Trim(),tipoAntecedente);
            }

            if (agrego)
            {
                MessageBox.Show("Antecedente agregado exitosamente", "Agregar Antecedente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.nudNumeroAntecedente.Value = 0;
                this.txtLugarOrigen.Text = "";
            }
            else
                MessageBox.Show("Error ya existe Antecedente con ese número", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtLugarOrigen_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //frmEmpleado frm = new frmEmpleado(conexion, frmMain);
            //frm.Tag = "buscar";
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //{
            //    this.empleado = frm.empleado;
            //    mostrarInfoEmpleado();
            //    this.lblCodigoEmpleado.Text = empleado.Codigo;
            //    this.lblNombreEmpleado.Text = empleado.Nombre;
            //    this.lblSueldo.Text = empleado.Sueldo.ToString();
            //    this.lblDepartamento.Text = empleado.objDepto.NombreDepartamento;
            //    this.lblCategoria.Text = empleado.objCategoria.NombreCategoria;
            //    this.lblSalarioInicial.Text = empleado.objCategoria.SalarioInicial.ToString();
            //    this.lblSalarioFinal.Text = empleado.objCategoria.SalarioFinal.ToString();
            //    cbTipoAumento.Select();
            //}
        }
    }
}
